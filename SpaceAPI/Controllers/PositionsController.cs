using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;
namespace SpaceTrack.API.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly TLECalculationService _calculationService;
        private readonly TLECalculationService2 _calculationService2;
        private readonly TLECalculationService3 _calculationService3;
        private readonly TLECalculationService4 _calculationService4;
        private readonly TLECalculationService5 _calculationService5;


        public PositionsController(TLECalculationService calculationService, TLECalculationService2 calculationService2, TLECalculationService3 calculationService3, TLECalculationService4 calculationService4 , TLECalculationService5 calculationService5)
        {
            _calculationService = calculationService;
            _calculationService2 = calculationService2;
            _calculationService3 = calculationService3;
            _calculationService4 = calculationService4;
            _calculationService5 = calculationService5;

        }



        [HttpPost("updatepostions1")]
        public IActionResult UpdatePositions()
        {
            _calculationService.CalculateAndUpdatePositionsForNextHour();
            return Ok(new { message = "Satellite positions1 updated for the next hour!" });
        }
        [HttpPost("updatepostions2")]
        public IActionResult UpdatePositions2()
        {
            _calculationService2.CalculateAndUpdatePositions2ForNextHour();
            return Ok(new { message = "Satellite positions2 updated for the next hour!" });
        }
        [HttpPost("updatepostions3")]
        public IActionResult UpdatePositions3()
        {
            _calculationService3.CalculateAndUpdatePositions3ForNextHour();
            return Ok(new { message = "Satellite positions3 updated for the next hour!" });
        }
        [HttpPost("updatepostions4")]
        public IActionResult UpdatePositions4()
        {
            _calculationService4.CalculateAndUpdatePositions4ForNextHour();
            return Ok(new { message = "Satellite positions4 updated for the next hour!" });
        }
        [HttpPost("updatepostions5")]
        public IActionResult UpdatePositions5()
        {
            _calculationService5.CalculateAndUpdatePositions5ForNextHourAsync();
            return Ok(new { message = "Satellite positions5 updated for the next hour!" });
        }
    }

}

////////////////////////////////////////////////////////////////////////////////////////work for the 100

//using Microsoft.AspNetCore.Mvc;
//using SpaceTrack.Services;

//namespace SpaceTrack.API.Controllers
//{
//    [Route("api/positions")]
//    [ApiController]
//    public class PositionsController : ControllerBase
//    {
//        private readonly TLECalculationService _calculationService;

//        public PositionsController(TLECalculationService calculationService)
//        {
//            _calculationService = calculationService;
//        }

//        [HttpPut("update")]
//        public IActionResult UpdatePositions()
//        {
//            _calculationService.CalculateAndStorePositionsForNextHour();
//            return Ok(new { message = "Satellite positions updated for the next hour!" });
//        }
//    }
//}
////////////////////////////////////////////////////////////////////////////////////////work for the 100
/////////////////////////////////////////////////////////////////to update


//////////////////////////////////////////////////////////////////////////////