using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Necesario para FirstOrDefaultAsync

namespace BDAlumnos.Pages
{
    public class VerInfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VerInfoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Alumno AlumnoSeleccionado { get; set; }

        // Cambiamos a Task asíncrono para mejor rendimiento
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Buscamos al alumno de forma asíncrona
            AlumnoSeleccionado = await _context.Alumnos
                .FirstOrDefaultAsync(a => a.Id == id);

            // Si el ID no existe en la BD, regresamos a la lista
            if (AlumnoSeleccionado == null)
            {
                return RedirectToPage("/Consulta");
            }

            return Page();
        }
    }
}