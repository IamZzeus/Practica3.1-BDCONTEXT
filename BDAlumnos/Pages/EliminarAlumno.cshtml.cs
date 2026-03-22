using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BDAlumnos.Pages
{
    public class EliminarAlumnoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EliminarAlumnoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Alumno Alumno { get; set; }

        // Se ejecuta al cargar la página para mostrar los datos
        public void OnGet(int id)
        {
            Alumno = _context.Alumnos.FirstOrDefault(a => a.Id == id);
        }

        // Se ejecuta al dar clic en el botón "Eliminar"
        public async Task<IActionResult> OnPostAsync()
        {
            var alumnoABorrar = await _context.Alumnos.FindAsync(Alumno.Id);

            if (alumnoABorrar != null)
            {
                _context.Alumnos.Remove(alumnoABorrar);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Alumnos"); // Te regresa a la lista
        }
    }
}
