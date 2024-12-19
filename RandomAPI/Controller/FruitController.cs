using Microsoft.AspNetCore.Mvc;
using RandomAPI.Service;

namespace RandomAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class FruitController : ControllerBase
    {

        private readonly FruitsService _fruitsService;
        public FruitController(FruitsService fruitsService)
        {
            _fruitsService = fruitsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var fruits = _fruitsService.GetAllFruit();
            return Ok(fruits);
        }
    }
}
