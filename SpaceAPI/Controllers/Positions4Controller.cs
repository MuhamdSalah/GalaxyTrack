using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;
namespace SpaceTrack.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class Positions4Controller : ControllerBase
    {
        private readonly TLECalculationServiceD16 _calculationService16;
        private readonly TLECalculationServiceD17 _calculationService17;
        private readonly TLECalculationServiceD18 _calculationService18;
        private readonly TLECalculationServiceD19 _calculationService19;
        private readonly TLECalculationServiceD20 _calculationService20;


        public Positions4Controller(TLECalculationServiceD16 calculationService16, TLECalculationServiceD17 calculationService17, TLECalculationServiceD18 calculationService18, TLECalculationServiceD19 calculationService19, TLECalculationServiceD20 calculationService20)
        {
            _calculationService16 = calculationService16;
            _calculationService17 = calculationService17;
            _calculationService18 = calculationService18;
            _calculationService19 = calculationService19;
            _calculationService20 = calculationService20;

        }



        [HttpPost("updatepostions16")]
        public IActionResult UpdatePositions()
        {
            _calculationService16.CalculateAndUpdatePositions16ForNextHourAsync();
            return Ok(new { message = "Debris positions16 updated for the next hour!" });
        }
        [HttpPost("updatepostions17")]
        public IActionResult UpdatePositions2()
        {
            _calculationService17.CalculateAndUpdatePositions17ForNextHourAsync();
            return Ok(new { message = "Debris positions17 updated for the next hour!" });
        }
        [HttpPost("updatepostions18")]
        public IActionResult UpdatePositions3()
        {
            _calculationService18.CalculateAndUpdatePositions18ForNextHourAsync();
            return Ok(new { message = "Debris positions18 updated for the next hour!" });
        }
        [HttpPost("updatepostions19")]
        public IActionResult UpdatePositions4()
        {
            _calculationService19.CalculateAndUpdatePositions19ForNextHourAsync();
            return Ok(new { message = "Rockets positions19 updated for the next hour!" });
        }
        [HttpPost("updatepostions20")]
        public IActionResult UpdatePositions5()
        {
            _calculationService20.CalculateAndUpdatePositions20ForNextHourAsync();
            return Ok(new { message = "Unknown positions20 updated for the next hour!" });
        }
    }

}
