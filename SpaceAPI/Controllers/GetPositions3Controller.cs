using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;

namespace SpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPositions3Controller : ControllerBase
    {
        private readonly PositionsService3 _positionsService;

        public GetPositions3Controller(PositionsService3 positionsService)
        {
            _positionsService = positionsService;
        }
        [HttpGet("Debris_FirstPackage")]
        public async Task<IActionResult> GetCompressedPositions()
        {
            var zipData = await _positionsService.GetAllPositionsCompressedAsync();
            return File(zipData, "application/zip", "Debris_FirstPackage_Positions.zip");
        }
    }
}
