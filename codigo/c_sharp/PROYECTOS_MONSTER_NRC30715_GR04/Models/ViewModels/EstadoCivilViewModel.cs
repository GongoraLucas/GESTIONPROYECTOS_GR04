using System.ComponentModel.DataAnnotations;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class EstadoCivilViewModel
{
    [Required(ErrorMessage = "El código es requerido")]
    [StringLength(1)]
    [Display(Name = "Código")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(50)]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;
}
