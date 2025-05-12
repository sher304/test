using System.Transactions;
using AnimalClinicAPI.Model;
using AnimalClinicAPI.Model.DTO;
using AnimalClinicAPI.Service.Appointment;
using AnimalClinicAPI.Service.Doctor;
using AnimalClinicAPI.Service.Patient;
using AnimalClinicAPI.Service.ServiceD;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentPatient.Controller;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppointmentInterface _appointmentService;
    private readonly PatientInterface _patientService;
    private readonly DoctorInterface _doctorService;
    private readonly ServiceInterface _serviceD;

    public AppointmentsController(
        AppointmentInterface appointmentService,
        PatientService patientService,
        DoctorInterface doctorService,
        ServiceInterface serviceD)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _doctorService = doctorService;
        _serviceD = serviceD;
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> getAppointment(int id)
    {
        if (await _appointmentService.appointmentExists(id) is false)
        {
            return BadRequest("Appoint with this id doesn't exist");
        }
        
        var results = await _appointmentService.getAppointment(id);
        return Ok(results);
    }


    [HttpPost]
    public async Task<IActionResult> addAppointment(AppointmentPostDTO appointment)
    {
     if (await _patientService.existsPatient(appointment.patientId) is false) return BadRequest("No patient exists");
     if (await _doctorService.doctorExists(appointment.pwz) is false) return BadRequest("No doctor exists");
     foreach (var service in appointment.services)
     {
         if (await _serviceD.existsService(service.name) is false) return BadRequest("No service exists");
     }


     using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
     {
         await _appointmentService.addAppointmentWithProcedure(appointment);
         scope.Complete();
     }
     
     return Created(Request.Path.Value ?? "api/appointment", appointment);    
    }
}