using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PEDEP_DEPAR")]
public class Departamento
{
    [Key]
    [Column("PEDEP_CODIGO")]
    [StringLength(3)]
    public string Codigo { get; set; } = string.Empty;

    [Column("PEDEP_DESCRIP")]
    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Cargo> Cargos { get; set; }
    = new List<Cargo>();
}