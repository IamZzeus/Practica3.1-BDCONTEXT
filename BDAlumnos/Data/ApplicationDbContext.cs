using Microsoft.EntityFrameworkCore;
using BDAlumnos.Models;

namespace BDAlumnos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<AlumnoMateria> AlumnoMaterias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir clave compuesta para la tabla puente
            modelBuilder.Entity<AlumnoMateria>()
                .HasKey(am => new { am.AlumnoId, am.MateriaId });

            // Configurar relación con Alumno
            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Alumno)
                .WithMany(a => a.AlumnosMaterias)
                .HasForeignKey(am => am.AlumnoId);

            // Configurar relación con Materia
            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Materia)
                .WithMany(m => m.AlumnosMaterias)
                .HasForeignKey(am => am.MateriaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
