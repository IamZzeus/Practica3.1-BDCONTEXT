using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BDAlumnos.Pages
{
    public class ConsultaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ConsultaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Alumno> ListaAlumnos { get; set; }

        // Esta propiedad recibirá el texto del buscador
        [BindProperty(SupportsGet = true)]
        public string TerminoBusqueda { get; set; }

        // Cambiamos 'void OnGet' por 'async Task OnGetAsync'
        public async Task OnGetAsync()
        {
            // Creamos la consulta base sobre la tabla Alumnos
            var query = from a in _context.Alumnos
                        select a;

            // Si el usuario escribió algo, filtramos por Nombre o Carrera
            if (!string.IsNullOrEmpty(TerminoBusqueda))
            {
                query = query.Where(s => s.Nombre.Contains(TerminoBusqueda)
                                      || s.Carrera.Contains(TerminoBusqueda));
            }

            // Ahora sí, ejecutamos la consulta de forma asíncrona
            ListaAlumnos = await query.ToListAsync();
        }
    }
}
