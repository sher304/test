namespace AnimalClinicAPI.Service.ServiceD;

public interface ServiceInterface
{
    Task<bool> existsService(String name);
}