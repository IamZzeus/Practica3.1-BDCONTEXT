using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BDAlumnos.Pages
{
    public class ConsultaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Alumno> ListaAlumnos { get; set; }

        public ConsultaModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            ListaAlumnos = _context.Alumnos.ToList();
        }
    }
}
