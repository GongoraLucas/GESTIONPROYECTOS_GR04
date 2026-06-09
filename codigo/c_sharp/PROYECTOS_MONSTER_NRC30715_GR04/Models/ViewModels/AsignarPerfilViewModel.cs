using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class AsignarPerfilViewModel
{
    public string PerfilCodigo { get; set; } = string.Empty;

    public List<SelectListItem> Perfiles { get; set; }
        = new();

    public List<UsuarioItemViewModel> UsuariosDisponibles { get; set; }
        = new();

    public List<UsuarioItemViewModel> UsuariosAsignados { get; set; }
        = new();
}