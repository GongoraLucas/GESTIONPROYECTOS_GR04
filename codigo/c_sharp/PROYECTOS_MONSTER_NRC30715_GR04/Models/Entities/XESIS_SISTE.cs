using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XESIS_SISTE")]
public class Sistema
{
    [Key]
    [Column("XESIS_CODIGO")]
    [StringLength(1)]
    public string Codigo { get; set; } = string.Empty;

    [Column("XESIS_DESCRI")]
    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;
}