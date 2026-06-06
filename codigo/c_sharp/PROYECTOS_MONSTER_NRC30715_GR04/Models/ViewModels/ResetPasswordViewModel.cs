using System.ComponentModel.DataAnnotations;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.ViewModels;

public class ResetPasswordViewModel
{
    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string PasswordNueva { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(PasswordNueva))]
    [DataType(DataType.Password)]
    public string ConfirmarPassword { get; set; } = string.Empty;
}