using BDAlumnos.Data;
using BDAlumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BDAlumnos.Pages
{
    public class EditarAlumnoModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment; // Para acceder a wwwroot

        public EditarAlumnoModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Alumno Alumno { get; set; }

        [BindProperty]
        public IFormFile? FotoSubida { get; set; } // Propiedad para recibir el archivo

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Id == id);

            if (Alumno == null) return RedirectToPage("Consulta");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Intentaremos removerlo de estas tres formas para estar 100% seguros
            ModelState.Remove("Alumno.AlumnosMaterias");
            ModelState.Remove("Alumno.AlumnoMaterias"); // A veces el plural/singular varía
            ModelState.Remove("FotoSubida");

            if (!ModelState.IsValid)
            {
                // Si sigue saliendo el error, esta línea nos dirá en la consola de Visual Studio 
                // exactamente cómo se llama el campo que está fallando:
                var errores = ModelState.Values.SelectMany(v => v.Errors);
                return Page();
            }

            // Lógica para procesar la foto
            if (FotoSubida != null)
            {
                // 1. Definir la carpeta de destino
                string carpetaFotos = Path.Combine(_environment.WebRootPath, "images", "alumnos");

                // Crear la carpeta si no existe
                if (!Directory.Exists(carpetaFotos)) Directory.CreateDirectory(carpetaFotos);

                // 2. Generar un nombre único para el archivo (GUID + nombre original)
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(FotoSubida.FileName);
                string rutaCompleta = Path.Combine(carpetaFotos, nombreArchivo);

                // 3. Guardar el archivo físicamente
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await FotoSubida.CopyToAsync(stream);
                }

                // 4. Borrar la foto anterior si existe (opcional pero recomendado)
                if (!string.IsNullOrEmpty(Alumno.FotoPath))
                {
                    string rutaVieja = Path.Combine(_environment.WebRootPath, Alumno.FotoPath.TrimStart('/'));
                    if (System.IO.File.Exists(rutaVieja)) System.IO.File.Delete(rutaVieja);
                }

                // 5. Guardar la nueva ruta en el modelo
                Alumno.FotoPath = "/images/alumnos/" + nombreArchivo;
            }

            _context.Attach(Alumno).State = EntityState.Modified;

            // Evitamos que EF intente resetear la FotoPath si no se subió una nueva
            if (FotoSubida == null)
            {
                _context.Entry(Alumno).Property(x => x.FotoPath).IsModified = false;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alumnos.Any(e => e.Id == Alumno.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("Consulta");
        }
    }
}