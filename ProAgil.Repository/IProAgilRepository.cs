using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
       void Add<T>(T entity) where T: class;

       void Update<T>(T entity) where T: class;
       
       void Delete<T>(T entity) where T: class;

       Task<bool> SaveChangesAsync();

       //Eventos
       Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrante);
       Task<Evento[]> GetAllEventosAsync(bool includePalestrante);
       Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrante);

       //Palestrantes
       Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos);
       Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos);
    }
}