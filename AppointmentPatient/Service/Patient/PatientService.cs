using Microsoft.Data.SqlClient;

namespace AnimalClinicAPI.Service.Patient;

public class PatientService : PatientInterface
{
    
    
    private readonly IConfiguration _configuration;

    public PatientService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    
    public async Task<bool> existsPatient(int patientId)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand("SELECT count(*) FROM patient WHERE patient_id = @id", connection);
        command.Connection = connection;
        connection.Open();
        command.Parameters.AddWithValue("@id", patientId);

        var res = await command.ExecuteScalarAsync();
        if (res.Equals(0)) return false;
        else return true;    
    }
}