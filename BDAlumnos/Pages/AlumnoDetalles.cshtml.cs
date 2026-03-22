using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BDAlumnos.Pages
{
    public class VerDetalleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VerDetalleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Es buena práctica inicializarlas para evitar errores de "null reference" en la vista
        public List<AlumnoMateria> Lista { get; set; } = new();
        public Alumno AlumnoSeleccionado { get; set; } = new();

        public void OnGet(int id)
        {
            // 1. Buscamos los datos básicos del alumno
            AlumnoSeleccionado = _context.Alumnos
                .FirstOrDefault(a => a.Id == id);

            // 2. Si el alumno existe, buscamos sus materias relacionadas
            if (AlumnoSeleccionado != null)
            {
                Lista = _context.AlumnoMaterias
                    .Where(am => am.AlumnoId == id)
                    .Include(am => am.Materia) // Traemos la info de la materia
                    .Include(am => am.Alumno)  // Traemos la info del alumno
                    .ToList();
            }
        }
    }
}