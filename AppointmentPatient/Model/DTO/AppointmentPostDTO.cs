namespace AnimalClinicAPI.Model.DTO;

public class AppointmentPostDTO
{
    public int appointmentId { get; set; }
    public int patientId { get; set; }
    public String pwz { get; set; }
    public List<AppointmentServiceDTO> services { get; set; }
}