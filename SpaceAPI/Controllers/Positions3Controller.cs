using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;
namespace SpaceTrack.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class Positions3Controller : ControllerBase
    {
        private readonly TLECalculationServiceC11 _calculationService11;
        private readonly TLECalculationServiceC12 _calculationService12;
        private readonly TLECalculationServiceC13 _calculationService13;
        private readonly TLECalculationServiceC14 _calculationService14;
        private readonly TLECalculationServiceC15 _calculationService15;


        public Positions3Controller(TLECalculationServiceC11 calculationService11, TLECalculationServiceC12 calculationService12, TLECalculationServiceC13 calculationService13, TLECalculationServiceC14 calculationService14, TLECalculationServiceC15 calculationService15)
        {
            _calculationService11 = calculationService11;
            _calculationService12 = calculationService12;
            _calculationService13 = calculationService13;
            _calculationService14 = calculationService14;
            _calculationService15 = calculationService15;

        }



        [HttpPost("updatepostions11")]
        public IActionResult UpdatePositions()
        {
                                  //CalculateAndUpdatePositions11ForNextHour
            _calculationService11.CalculateAndUpdatePositions11ForNextHourAsync();
            return Ok(new { message = "Space Debris positions11 updated for the next hour!" });
        }
        [HttpPost("updatepostions12")]
        public IActionResult UpdatePositions2()
        {
            _calculationService12.CalculateAndUpdatePositions12ForNextHourAsync();
            return Ok(new { message = "Space Debris positions12 updated for the next hour!" });
        }
        [HttpPost("updatepostions13")]
        public IActionResult UpdatePositions3()
        {
            _calculationService13.CalculateAndUpdatePositions13ForNextHourAsync();
            return Ok(new { message = "Space Debris positions13 updated for the next hour!" });
        }
        [HttpPost("updatepostions14")]
        public IActionResult UpdatePositions4()
        {
            _calculationService14.CalculateAndUpdatePositions14ForNextHourAsync();
            return Ok(new { message = "Space Debris positions14 updated for the next hour!" });
        }
        [HttpPost("updatepostions15")]
        public IActionResult UpdatePositions5()
        {
            _calculationService15.CalculateAndUpdatePositions15ForNextHourAsync();
            return Ok(new { message = "Space Debris positions15 updated for the next hour!" });
        }
    }

}
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SpaceTrack.Services;

//namespace SpaceAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Positions3Controller : ControllerBase
//    {
//    }
//}