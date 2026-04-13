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

        public List<AlumnoMateria> Lista { get; set; } = new();
        public Alumno AlumnoSeleccionado { get; set; } = new();

        // Esta lista llenará el dropdown de tu vista
        public List<Materias> TodasLasMaterias { get; set; } = new();

        // Propiedad para recibir el ID de la materia desde el formulario
        [BindProperty]
        public int MateriaIdSeleccionada { get; set; }

        public async Task OnGetAsync(int id)
        {
            await CargarDatos(id);
        }

        // Método auxiliar para no repetir código de carga
        private async Task CargarDatos(int id)
        {
            AlumnoSeleccionado = await _context.Alumnos
                .FirstOrDefaultAsync(a => a.Id == id);

            if (AlumnoSeleccionado != null)
            {
                // 1. Traemos las materias que ya tiene el alumno
                Lista = await _context.AlumnoMaterias
                    .Where(am => am.AlumnoId == id)
                    .Include(am => am.Materia)
                    .ToListAsync();

                // 2. Traemos las materias que el alumno NO tiene inscritas aún
                // Esto evita que el dropdown se llene de materias duplicadas
                var idsMateriasInscritas = Lista.Select(am => am.MateriaId).ToList();

                TodasLasMaterias = await _context.Materias
                    .Where(m => !idsMateriasInscritas.Contains(m.Id))
                    .ToListAsync();
            }
        }

        // Handler para el botón de "Asignar"
        public async Task<IActionResult> OnPostAsignarAsync(int id)
        {
            if (MateriaIdSeleccionada > 0)
            {
                // Verificamos si ya existe la relación para no duplicar
                var existe = await _context.AlumnoMaterias
                    .AnyAsync(am => am.AlumnoId == id && am.MateriaId == MateriaIdSeleccionada);

                if (!existe)
                {
                    var nuevaRelacion = new AlumnoMateria
                    {
                        AlumnoId = id,
                        MateriaId = MateriaIdSeleccionada
                    };

                    _context.AlumnoMaterias.Add(nuevaRelacion);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage(new { id = id });
        }

        // Handler para el botón de "Quitar"
        public async Task<IActionResult> OnPostQuitarAsync(int id, int materiaId)
        {
            var relacion = await _context.AlumnoMaterias
                .FirstOrDefaultAsync(am => am.AlumnoId == id && am.MateriaId == materiaId);

            if (relacion != null)
            {
                _context.AlumnoMaterias.Remove(relacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = id });
        }


    }
}   