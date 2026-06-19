namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class UsuarioItemViewModel
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public List<string> Perfiles { get; set; } = new();
}