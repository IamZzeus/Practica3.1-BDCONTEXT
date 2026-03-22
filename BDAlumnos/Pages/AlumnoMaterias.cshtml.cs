using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BDAlumnos.Pages
{
    public class AlumnoMateriasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AlumnoMateriasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AlumnoMateria> Lista { get; set; }

        public void OnGet()
        {
            Lista = _context.AlumnoMaterias
                .Include(am => am.Alumno)
                .Include(am => am.Materia)
                .ToList();
        }
    }
}
