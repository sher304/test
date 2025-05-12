using Microsoft.Data.SqlClient;

namespace AnimalClinicAPI.Service.Doctor;

public class DoctorService : DoctorInterface
{
    private readonly IConfiguration _configuration;

    public DoctorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<bool> doctorExists(string pwz)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand("SELECT count(*) FROM doctor WHERE pwz = @pwz", connection);
        command.Connection = connection;
        connection.Open();
        command.Parameters.AddWithValue("@pwz", pwz);

        var res = await command.ExecuteScalarAsync();
        if (res.Equals(0)) return false;
        else return true;  
    }
}