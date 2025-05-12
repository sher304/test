namespace AnimalClinicAPI.Model.DTO;

public class AppointmentDTO
{
    public DateTime date { get; set; }
    public PatientDTO patient { get; set; }
    public DoctorDTO doctor { get; set; }
    public List<AppointmentServiceDTO> appointmentServices { get; set; }
}