using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEUSU_RECUPERACION")]
public class RecuperacionPassword
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Required]
    [Column("TOKEN")]
    [StringLength(200)]
    public string Token { get; set; } = string.Empty;

    [Required]
    [Column("FECHA_CREACION")]
    public DateTime FechaCreacion { get; set; }

    [Required]
    [Column("FECHA_EXPIRACION")]
    public DateTime FechaExpiracion { get; set; }

    [Required]
    [Column("UTILIZADO")]
    public bool Utilizado { get; set; }

    [Required]
    [Column("XEUSU_ID")]
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }
}