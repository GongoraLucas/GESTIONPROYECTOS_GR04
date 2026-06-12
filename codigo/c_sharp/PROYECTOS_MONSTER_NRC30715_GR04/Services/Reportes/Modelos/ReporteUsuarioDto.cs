namespace PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes.Modelos;

public class ReporteUsuarioDto
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;

    public string Empleado { get; set; } = string.Empty;
}
