using Microsoft.Data.SqlClient;

namespace AnimalClinicAPI.Service.ServiceD;

public class ServiceD : ServiceInterface
{
    private readonly IConfiguration _configuration;
    
    public ServiceD(IConfiguration config)
    {
        _configuration = config;
    }
    
    
    public async Task<bool> existsService(string name)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand("SELECT count(*) FROM service WHERE name = @name", connection);
        command.Connection = connection;
        connection.Open();
        command.Parameters.AddWithValue("@name", name);

        var res = await command.ExecuteScalarAsync();
        if (res.Equals(0)) return false;
        else return true;  
    }
}