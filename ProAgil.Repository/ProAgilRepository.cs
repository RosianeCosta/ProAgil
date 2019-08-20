using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
           _context = context;
           //No tracker = Não ser mudança rastreada para não travar recurso no EntityFramework 
           _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //EVENTOS
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p =>p.Palestrante);
            }

            query = query.OrderByDescending(x =>x.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrante)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p =>p.Palestrante);
            }

            query = query.OrderByDescending(x =>x.DataEvento).Where(x =>x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        
        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p =>p.Palestrante);
            }

            query = query.OrderByDescending(x =>x.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        //PALESTRANTE
        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEventos)
        {IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEventos)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p =>p.Evento);
            }

            query = query.OrderBy(x => x.Nome).Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEventos)
            {
                query = query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p =>p.Evento);
            }

            query = query.OrderBy(x => x.Nome).Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}