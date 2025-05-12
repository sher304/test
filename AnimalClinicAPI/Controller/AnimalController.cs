using System.Transactions;
using AnimalClinicAPI.Model;
using AnimalClinicAPI.Model.DTO;
using AnimalClinicAPI.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AnimalClinicAPI.Controller;

[ApiController]
[Route("api/animals")]
public class AnimalController : ControllerBase
{
    private readonly DBAnimalInterface _dbAnimalService;

    public AnimalController(DBAnimalInterface dbAnimalService)
    {
        _dbAnimalService = dbAnimalService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimals()
    {
        var animals = await _dbAnimalService.getAnimals();
        return Ok(animals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getAnimal(int id)
    {
        if (await _dbAnimalService.animalExists(id) == false)
        {
            return NotFound($"No animal found with id {id}");
        } 
        
        try
        {
            var animal = await _dbAnimalService.getAnimal(id);
            return Ok(animal);
        } catch (Exception e)
        {
         return NotFound("No data with this animal request id: " + id);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAnimals([FromBody] Animal animal)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _dbAnimalService.addAnimal(animal);
        if (result.Equals(true)) return Created(Request.Path.Value ?? "api/animals", animal);
        else return BadRequest("Error while trying to create animals");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var isAnimalExists = await _dbAnimalService.animalExists(id);
        if (isAnimalExists == false)
        {
            return NotFound($"No animal found with id {id}");
        }
        
        var result = await _dbAnimalService.deleteAnimal(id);
        if (result.Equals(true)) return Ok("Animal has been deleted!");
        else return BadRequest("Error while trying to delete animal");
    }

    [HttpPost]
    [Route("animals_procedure")]
    public async Task<IActionResult> PostWithProcedure([FromBody] AnimalPostDTO animal)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest("Incorrect model!");
        }
        
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            int animalID = await _dbAnimalService.addAnimal(new Animal
            {
                AdmissionDate = animal.admissionDate,
                Name = animal.name,
                Type = animal.type,
                OwnerId = animal.ownerId
            });
            foreach (var precedure in animal.procedures)
            {
                await _dbAnimalService.addAnimalWithProcedure(animalID, precedure);
            }
            scope.Complete();
        }
        return Created(Request.Path.Value ?? "api/animals", animal);
    }
}