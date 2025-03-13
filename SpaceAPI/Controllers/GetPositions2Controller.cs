using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceTrack.Services;

namespace SpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPositions2Controller : ControllerBase
    {
        private readonly PositionsService2 _positionsService;

        public GetPositions2Controller(PositionsService2 positionsService)
        {
            _positionsService = positionsService;
        }
        [HttpGet("Satellites_SecondPackage")]
        public async Task<IActionResult> GetCompressedPositions()
        {
            var zipData = await _positionsService.GetAllPositionsCompressedAsync();
            return File(zipData, "application/zip", "Satellites_SecondPackage_Positions.zip");
        }
    }
}
