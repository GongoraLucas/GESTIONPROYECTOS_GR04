using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class UsuarioViewModel
{
    public int Id { get; set; }

    [Required]
    public string Login { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string EmpleadoCodigo { get; set; } = string.Empty;

    public string EstadoCodigo { get; set; } = string.Empty;

    public List<SelectListItem> Empleados { get; set; } = new();

    public List<SelectListItem> Estados { get; set; } = new();

    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string? Password { get; set; }
}

