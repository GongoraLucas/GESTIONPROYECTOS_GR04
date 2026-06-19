using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class AsignarOpcionViewModel
{
    public string PerfilCodigo { get; set; } = string.Empty;

    public List<SelectListItem> Perfiles { get; set; } = new();

    public List<SistemaTreeItemViewModel> Sistemas { get; set; } = new();
}

public class SistemaTreeItemViewModel
{
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public List<GrupoTreeItemViewModel> Grupos { get; set; } = new();
}

public class GrupoTreeItemViewModel
{
    public string Nombre { get; set; } = string.Empty;
    public List<OpcionTreeItemViewModel> Opciones { get; set; } = new();
}

public class OpcionTreeItemViewModel
{
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Asignada { get; set; }
}