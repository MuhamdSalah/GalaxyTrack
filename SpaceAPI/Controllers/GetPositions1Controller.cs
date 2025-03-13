using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;
using System.Threading.Tasks;

namespace SpaceTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPositions1Controller : ControllerBase
    {
        private readonly PositionsService1 _positionsService;

        public GetPositions1Controller(PositionsService1 positionsService)
        {
            _positionsService = positionsService;
        }

        [HttpGet("Satellites_FirstPackage")]
        public async Task<IActionResult> GetCompressedPositions()
        {
            var zipData = await _positionsService.GetAllPositionsCompressedAsync();
            return File(zipData, "application/zip", "Satellites_FirstPackage_Positions.zip");
        }
    }
}


///////////////////////////////////////////////////////////////  
//using Microsoft.AspNetCore.Mvc;
//using SpaceTrack.Services;
//using System.IO;
//using System.Threading.Tasks;

//namespace SpaceTrack.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GetPositions1Controller : ControllerBase
//    {
//        private readonly PositionsService1 _positionsService;

//        public GetPositions1Controller(PositionsService1 positionsService)
//        {
//            _positionsService = positionsService;
//        }

//        [HttpGet("SatellitesPositions1")]
//        public async Task<IActionResult> GetCompressedPositions()
//        {
//            var zipData = await _positionsService.GetAllPositionsCompressedAsync();
//            return File(zipData, "application/zip", "Positions.zip");
//        }

//    }
//}
////////////////////////////////////////////////////////////////////
//using Microsoft.AspNetCore.Mvc;
//using SpaceTrack.Services;
//using SpaceTrack.DAL.Model;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SpaceTrack.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GetPositions1Controller : ControllerBase
//    {
//        private readonly PositionsService1 _positionsService;



//        public GetPositions1Controller(PositionsService1 positionsService)
//        {
//            _positionsService = positionsService;
//        }

//        [HttpGet("SatellitesPostions1")]
//        public async Task<ActionResult<List<SpaceObjectPosition>>> GetAllPositions()
//        {
//            var positions = await _positionsService.GetAllPositionsAsync();
//            return Ok(positions);
//        }
//        [HttpGet("SatellitesPostions2")]
//        public async Task<ActionResult<List<SpaceObjectPosition2>>> GetAllPositions2()
//        {
//            var positions = await _positionsService.GetAllPositions2Async();
//            return Ok(positions);
//        }
//        [HttpGet("SatellitesPostions3")]
//        public async Task<ActionResult<List<SpaceObjectPosition3>>> GetAllPositions3()
//        {
//            var positions = await _positionsService.GetAllPositions3Async();
//            return Ok(positions);
//        }
//        [HttpGet("SatellitesPostions4")]
//        public async Task<ActionResult<List<SpaceObjectPosition4>>> GetAllPositions4()
//        {
//            var positions = await _positionsService.GetAllPositions4Async();
//            return Ok(positions);
//        }
//        [HttpGet("SatellitesPostions5")]
//        public async Task<ActionResult<List<SpaceObjectPosition5>>> GetAllPositions5()
//        {
//            var positions = await _positionsService.GetAllPositions5Async();
//            return Ok(positions);
//        }


//    }
//}
