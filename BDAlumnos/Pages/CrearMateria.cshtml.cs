using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BDAlumnos.Pages
{
    public class CrearMateriaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CrearMateriaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materias Materia { get; set; } = new Materias();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Esta línea es la que elimina el error que ves en pantalla
            ModelState.Remove("Materia.AlumnosMaterias");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Materias.Add(Materia);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Materias");
        }
    }
}
