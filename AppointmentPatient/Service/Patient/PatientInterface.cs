namespace AnimalClinicAPI.Service.Patient;

public interface PatientInterface
{
    Task<bool> existsPatient(int patientId);
}