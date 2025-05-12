using AnimalClinicAPI.Model.DTO;

namespace AnimalClinicAPI.Service.Appointment;

public interface AppointmentInterface
{
    
    Task<AppointmentDTO> getAppointment(int id);
    Task<bool> appointmentExists(int id);
    Task<AppointmentPostDTO> addAppointmentWithProcedure(AppointmentPostDTO appointment);
    
    
}