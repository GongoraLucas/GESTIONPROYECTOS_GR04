using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEOXP_OPCPE")]
public class PerfilOpcion
{
    [Column("XEOPC_CODIGO")]
    [StringLength(3)]
    public string OpcionCodigo { get; set; } = string.Empty;

    [Column("XEPER_CODIGO")]
    [StringLength(8)]
    public string PerfilCodigo { get; set; } = string.Empty;

    [Column("XEOXP_FECASI")]
    public DateTime FechaAsignacion { get; set; }

    [Column("XEOXP_FECRET")]
    public DateTime? FechaRetiro { get; set; }

    public Opcion? Opcion { get; set; }

    public Perfil? Perfil { get; set; }
}