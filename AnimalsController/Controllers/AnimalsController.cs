using AnimalsController.Models;
using AnimalsController.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnimalsController.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        private IDatabaseService _databaseService;

        public AnimalsController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult GetAnimals([FromQuery]string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "name";
            }
            if (orderBy != "name" && orderBy != "description" && orderBy != "category" && orderBy != "area")
            {
                return BadRequest("Invalid value for parameter 'orderBy'");
            }

            var resultJson = JsonConvert.SerializeObject(_databaseService.GetAnimals(orderBy), Newtonsoft.Json.Formatting.Indented);
            
            return Ok(resultJson);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal newAnimal)
        {
            _databaseService.AddAnimal(newAnimal);
            return Ok("Animal added successfully");
        }

        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal([FromRoute] int idAnimal, [FromBody] Animal updatedAnimal)
        {
            if (!_databaseService.UpdateAnimal(idAnimal, updatedAnimal))
            {
                return NotFound("Animal not found");
            }
            return Ok("Animal updated successfully");
        }

        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimals([FromRoute] int idAnimal)
        {
            if (!_databaseService.DeleteAnimal(idAnimal))
            {
                return NotFound("Animal not found");
            }
            return Ok("Animal deleted successfully");
        }


    }
}
