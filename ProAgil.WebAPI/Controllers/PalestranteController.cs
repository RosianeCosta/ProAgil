using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                var result = await _repo.GetPalestranteAsync(PalestranteId, true);
               return Ok(result); 
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }
        }
        
        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var result = await _repo.GetAllPalestrantesAsyncByName(name, true);
               return Ok(result); 
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }
        }
    }
}