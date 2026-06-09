using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEEST_ESTAD")]
public class Estado
{
    [Key]
    [Column("XEEST_CODIGO")]
    [StringLength(1)]
    public string Codigo { get; set; } = string.Empty;

    [Column("XEEST_DESCRI")]
    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;

   
}