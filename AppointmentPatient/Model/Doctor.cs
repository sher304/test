using System.ComponentModel.DataAnnotations;

namespace AnimalClinicAPI.Model;

public class Doctor
{
    public int doctor_Id { get; set; }
    [MaxLength(100)]
    public string first_name { get; set; }
    [MaxLength(100)]
    public string last_name { get; set; }
    [MaxLength(100)]
    public string PWZ { get; set; }
    
}