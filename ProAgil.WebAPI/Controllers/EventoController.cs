using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using AutoMapper;
using ProAgil.WebAPI.Dtos;
using System.Collections.Generic;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

         private readonly IMapper _mapper;

        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEventosAsync(true);
                var result = _mapper.Map<IEnumerable<EventoDto>>(eventos);
              
               return Ok(result); 
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }
        }
        
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var result = await _repo.GetEventoAsyncById(EventoId, true);
               return Ok(result); 
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }
        }
        
        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _repo.GetAllEventosAsyncByTema(tema, true);
               return Ok(result); 
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
               _repo.Add(model);

               if(await _repo.SaveChangesAsync())
               {
                   return Created($"/api/evento/{model.Id}", model);
               }
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento= await _repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();
                
               _repo.Update(model);
               
               if(await _repo.SaveChangesAsync())
               {
                   return Created($"/api/evento/{model.Id}", model);
               }
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento= await _repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();
                
               _repo.Delete(evento);
               
               if(await _repo.SaveChangesAsync())
               {
                   return Ok();
               }
            }
            catch (System.Exception)
            {
                return  this.StatusCode(StatusCodes.Status500InternalServerError,"db falhou");
            }

            return BadRequest();
        }
    }
}