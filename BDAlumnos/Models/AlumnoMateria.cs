namespace BDAlumnos.Models
{
    public class AlumnoMateria
    {
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }
        public int MateriaId { get; set; }
        public Materias Materia { get; set; }
    }
}
