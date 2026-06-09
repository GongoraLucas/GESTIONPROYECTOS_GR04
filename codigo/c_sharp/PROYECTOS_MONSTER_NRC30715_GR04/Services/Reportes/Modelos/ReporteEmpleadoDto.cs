namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes.Modelos;

public class ReporteEmpleadoDto
{
    public string Codigo { get; set; } = string.Empty;

    public string Cedula { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public string Nombres { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Departamento { get; set; } = string.Empty;

    public string Cargo { get; set; } = string.Empty;

    public string Sexo { get; set; } = string.Empty;

    public string EstadoCivil { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public DateTime? FechaSalida { get; set; }

    public decimal Salario { get; set; }

    public string JefeNombre { get; set; } = string.Empty;
}
