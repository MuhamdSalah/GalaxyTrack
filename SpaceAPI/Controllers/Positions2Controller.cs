using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;
namespace SpaceTrack.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class Positions2Controller : ControllerBase
    {
        private readonly TLECalculationServiceB6 _calculationService6;
        private readonly TLECalculationServiceB7 _calculationService7;
        private readonly TLECalculationServiceB8 _calculationService8;
        private readonly TLECalculationServiceB9 _calculationService9;
        private readonly TLECalculationServiceB10 _calculationService10;


        public Positions2Controller(TLECalculationServiceB6 calculationService6, TLECalculationServiceB7 calculationService7, TLECalculationServiceB8 calculationService8, TLECalculationServiceB9 calculationService9, TLECalculationServiceB10 calculationService10)
        {
            _calculationService6 = calculationService6;
            _calculationService7 = calculationService7;
            _calculationService8 = calculationService8;
            _calculationService9 = calculationService9;
            _calculationService10 = calculationService10;

        }



        [HttpPost("updatepostions6")]
        public IActionResult UpdatePositions()
        {
            _calculationService6.CalculateAndUpdatePositions6ForNextHour();
            return Ok(new { message = "Satellite positions6 updated for the next hour!" });
        }
        [HttpPost("updatepostions7")]
        public IActionResult UpdatePositions2()
        {
            _calculationService7.CalculateAndUpdatePositions7ForNextHour();
            return Ok(new { message = "Satellite positions7 updated for the next hour!" });
        }
        [HttpPost("updatepostions8")]
        public IActionResult UpdatePositions3()
        {
            _calculationService8.CalculateAndUpdatePositions8ForNextHour();
            return Ok(new { message = "Satellite positions8 updated for the next hour!" });
        }
        [HttpPost("updatepostions9")]
        public IActionResult UpdatePositions4()
        {
            _calculationService9.CalculateAndUpdatePositions9ForNextHour();
            return Ok(new { message = "Satellite positions9 updated for the next hour!" });
        }
        [HttpPost("updatepostions10")]
        public IActionResult UpdatePositions5()
        {
            _calculationService10.CalculateAndUpdatePositions10ForNextHour();
            return Ok(new { message = "Satellite positions10 updated for the next hour!" });
        }
    }

}