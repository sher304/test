using System.Data.Common;
using AnimalClinicAPI.Model.DTO;
using Microsoft.Data.SqlClient;

namespace AnimalClinicAPI.Service.Appointment;

public class AppointmentService : AppointmentInterface
{
    private readonly IConfiguration _configuration;

    public AppointmentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<AppointmentDTO> getAppointment(int id)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand("select ap.date, pa.first_name, pa.last_name, pa.date_of_birth, do.doctor_id, do.pwz, se.name, aps.service_fee from appointment as ap\njoin patient as pa\non ap.patient_id = pa.patient_id\njoin doctor as do\non do.doctor_id = ap.doctor_id\njoin appointment_service as aps\non aps.appointment_id = ap.appointment_id\njoin service as se\non se.service_id = aps.service_id\nwhere ap.appointment_id= @id;", connection);
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@id", id);

        AppointmentDTO appointmentDto = null;
        
        
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            if (appointmentDto == null)
            {
                appointmentDto = new AppointmentDTO
                {
                    date = reader.GetDateTime(0),
                    patient = new PatientDTO
                    {
                        first_Name = reader.GetString(1),
                        last_Name = reader.GetString(2),
                        date_of_birth = reader.GetDateTime(3),
                    },
                    doctor = new DoctorDTO
                    {
                        doctorId = reader.GetInt32(4),
                        PWZ = reader.GetString(5),
                    },
                    appointmentServices = new List<AppointmentServiceDTO>()
                    {
                        new AppointmentServiceDTO
                        {
                            name = reader.GetString(6),
                            serviceFee = reader.GetDecimal(7)
                        }
                    }
                };
            }
            else
            {
                appointmentDto.appointmentServices.Add(new AppointmentServiceDTO()
                {
                    name = reader.GetString(6),
                    serviceFee = reader.GetDecimal(7)
                });
            }
        }

        if (appointmentDto is null) throw new Exception();
        return appointmentDto;
    }

    public async Task<bool> appointmentExists(int id)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand("SELECT count(*) FROM appointment WHERE appointment_id = @id", connection);
        command.Connection = connection;
        connection.Open();
        command.Parameters.AddWithValue("@id", id);

        var res = await command.ExecuteScalarAsync();
        if (res.Equals(0)) return false;
        else return true;
    }

    public async Task<AppointmentPostDTO> addAppointmentWithProcedure(AppointmentPostDTO appointment)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand("", connection);
        await  connection.OpenAsync();
        
        DbTransaction transaction = connection.BeginTransaction();
        command.Transaction = transaction as SqlTransaction;

        try
        {
    
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw e;
        }

        return new AppointmentPostDTO();
    }
} 