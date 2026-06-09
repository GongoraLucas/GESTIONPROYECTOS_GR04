using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class EmpleadoViewModel
{
    [Required]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    public string Cedula { get; set; } = string.Empty;

    [Required]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    public string Direccion { get; set; } = string.Empty;

    [Required]
    public string Telefono { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateTime FechaNacimiento { get; set; }

    public DateTime? FechaSalida { get; set; }

    [Required]
    public decimal Salario { get; set; }

    [Required]
    public string SexoCodigo { get; set; } = string.Empty;

    public string? EstadoCivilCodigo { get; set; }

    [Required]
    public string DepartamentoCodigo { get; set; } = string.Empty;

    [Required]
    public string CargoCodigo { get; set; } = string.Empty;

    public string? JefeCodigo { get; set; }

    public string? Foto { get; set; }

    public IFormFile? ArchivoFoto { get; set; }

    public List<SelectListItem> Sexos { get; set; }
        = new();

    public List<SelectListItem> EstadosCiviles { get; set; }
        = new();

    public List<SelectListItem> Departamentos { get; set; }
        = new();

    public List<SelectListItem> Cargos { get; set; }
        = new();

    public List<SelectListItem> Jefes { get; set; }
        = new();

    public string? SexoDescripcion { get; set; }

    public string? EstadoCivilDescripcion { get; set; }

    public string? CargoDescripcion { get; set; }

    public string? JefeNombre { get; set; }
}