using AplicationCore.interfaces.Dto.TurnoDto;
using AplicationCore.interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnosService _turnosService;

        public TurnosController(ITurnosService turnosService)
        {
            _turnosService = turnosService;
        }

        [HttpPost]
        public async Task<IActionResult> VerTurnos([FromBody] RegistrarTurnoDto registro)
        {
            return Ok(await _turnosService.VerTurnosDisponibles(registro));
        }
    }
}
