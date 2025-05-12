using System.ComponentModel.DataAnnotations;

namespace AnimalClinicAPI.Model.DTO;

public class AnimalDTO
{
    
    public int id { get; set; }
    [MaxLength(200)]
    public string name { get; set; }
    [MaxLength(200)]
    public string type { get; set; }
    public DateTime admissionDate { get; set; }
    public Owner owner { get; set; }
    public List<ProcedureDto> procedures { get; set; }
    
}