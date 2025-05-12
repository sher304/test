using System.ComponentModel.DataAnnotations;

namespace AnimalClinicAPI.Model;

public class Procedure
{
    public int id { get; set; }
    [MaxLength(100)]
    public String  name { get; set; }
    [MaxLength(100)]
    public String  description { get; set; }
}