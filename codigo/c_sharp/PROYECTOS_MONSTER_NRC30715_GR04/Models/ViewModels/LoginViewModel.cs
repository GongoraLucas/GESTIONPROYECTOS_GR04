using System.ComponentModel.DataAnnotations;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Ingrese el usuario")]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese la contraseña")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}