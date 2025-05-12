namespace AnimalClinicAPI.Model;

public class Appointment
{
    public int appointment_Id { get; set; }
    public int patient_Id { get; set; }
    public int doctor_Id { get; set; }
    public DateTime date { get; set; }
}