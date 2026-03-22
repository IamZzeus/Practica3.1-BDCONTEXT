using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


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

        public void OnGet(int id)
        {
            AlumnoSeleccionado = _context.Alumnos
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
