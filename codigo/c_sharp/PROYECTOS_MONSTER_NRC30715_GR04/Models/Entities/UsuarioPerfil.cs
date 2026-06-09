using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEUXP_USUPE")]
public class UsuarioPerfil
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("XEUXP_ID")]
    public long Id { get; set; }

    [Required]
    [Column("XEUSU_ID")]
    public int UsuarioId { get; set; }

    [Required]
    [Column("XEPER_CODIGO")]
    [StringLength(8)]
    public string PerfilCodigo { get; set; } = string.Empty;

    [Column("XEUXP_FECASI")]
    public DateTime FechaAsignacion { get; set; }

    [Column("XEUXP_FECRET")]
    public DateTime? FechaRetiro { get; set; }

    public Usuario? Usuario { get; set; }

    public Perfil? Perfil { get; set; }
}