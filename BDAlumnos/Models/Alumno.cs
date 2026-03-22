namespace BDAlumnos.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<AlumnoMateria> AlumnosMaterias { get; set; }
    }
}
