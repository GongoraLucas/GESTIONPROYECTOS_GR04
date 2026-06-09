using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class AsignarOpcionViewModel
{
    public string PerfilCodigo { get; set; }
        = string.Empty;

    public List<SelectListItem> Perfiles
    {
        get;
        set;
    } = new();

    public List<OpcionItemViewModel>
        OpcionesDisponibles
    {
        get;
        set;
    } = new();

    public List<OpcionItemViewModel>
        OpcionesAsignadas
    {
        get;
        set;
    } = new();
}