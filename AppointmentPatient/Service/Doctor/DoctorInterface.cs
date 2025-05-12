namespace AnimalClinicAPI.Service.Doctor;

public interface DoctorInterface
{
    Task<bool> doctorExists(string pwz);
}