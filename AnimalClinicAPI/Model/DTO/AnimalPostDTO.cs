namespace AnimalClinicAPI.Model.DTO;

public class AnimalPostDTO
{
    public String name {get; set;}
    public String type  {get; set;}
    public DateTime admissionDate {get; set;}
    public int ownerId {get; set;}
    public List<ProcedurePostDTO> procedures {get; set;}
}