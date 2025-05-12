using System.ComponentModel.DataAnnotations;

namespace AnimalClinicAPI.Model;

public class Patient
{
    public int parent_id { get; set; }
    [MaxLength(100)]
    public string first_name { get; set; }
    [MaxLength(100)]
    public string last_name { get; set; }
    public DateTime date_of_birth { get; set; }
}