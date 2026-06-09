using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class CargoViewModel
{
    [Required(ErrorMessage = "El departamento es requerido")]
    [Display(Name = "Departamento")]
    public string DepartamentoCodigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El código es requerido")]
    [StringLength(3)]
    [Display(Name = "Código")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(50)]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;

    // Para edición: guardar el código original (por si se cambia el departamento)
    public string CodigoOriginal { get; set; } = string.Empty;
    public string DepartamentoCodigoOriginal { get; set; } = string.Empty;

    // Descripción del departamento (para Details)
    public string? DepartamentoDescripcion { get; set; }

    // Lista de departamentos para el select
    public List<SelectListItem> Departamentos { get; set; } = new();
}
