using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEOPC_OPCIO")]
public class Opcion
{
    [Key]
    [Column("XEOPC_CODIGO")]
    [StringLength(3)]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    [Column("XESIS_CODIGO")]
    [StringLength(1)]
    public string SistemaCodigo { get; set; } = string.Empty;

    [Required]
    [Column("XEOPC_DESCRI")]
    [StringLength(100)]
    public string Descripcion { get; set; } = string.Empty;

    public Sistema? Sistema { get; set; }

    public ICollection<PerfilOpcion> PerfilesOpciones
    { get; set; } = new List<PerfilOpcion>();
}