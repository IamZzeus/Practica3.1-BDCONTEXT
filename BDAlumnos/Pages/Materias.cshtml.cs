using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BDAlumnos.Pages
{
    public class MateriasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MateriasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Materias> ListaMaterias { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Traemos todas las materias de la tabla
            ListaMaterias = await _context.Materias.ToListAsync();
        }
    }
}
