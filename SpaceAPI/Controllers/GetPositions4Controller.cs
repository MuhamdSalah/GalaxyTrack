using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;

namespace SpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPositions4Controller : ControllerBase
    {
        private readonly PositionsService4 _positionsService;

        public GetPositions4Controller(PositionsService4 positionsService)
        {
            _positionsService = positionsService;
        }
        [HttpGet("Debris_SecondPackage_Rockets_Unknown_Positions")]
        public async Task<IActionResult> GetCompressedPositions()
        {
            var zipData = await _positionsService.GetAllPositionsCompressedAsync();
            return File(zipData, "application/zip", "Debris_SecondPackage_Rockets_Unknown_Positions.zip");
        }
    }
}
