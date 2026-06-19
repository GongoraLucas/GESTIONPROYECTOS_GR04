using System;
using System.ComponentModel.DataAnnotations;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class FamiliarViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50)]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50)]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

    public int Edad { get; set; }

    [Required(ErrorMessage = "El parentesco es obligatorio")]
    [StringLength(50)]
    public string Parentesco { get; set; } = string.Empty;
}
