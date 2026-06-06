using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEPER_PERFI")]
public class Perfil
{
    [Key]
    [Column("XEPER_CODIGO")]
    [StringLength(8)]
    public string Codigo { get; set; } = string.Empty;

    [Column("XEPER_DESCRI")]
    [Required]
    [StringLength(100)]
    public string Descripcion { get; set; } = string.Empty;

    [Column("XEPER_OBSER")]
    public string? Observacion { get; set; }
}