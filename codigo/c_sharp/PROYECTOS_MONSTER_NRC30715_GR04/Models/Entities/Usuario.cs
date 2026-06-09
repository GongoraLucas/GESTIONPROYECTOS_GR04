using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("XEUSU_USUAR")]
public class Usuario
{
    [Key]
    [Column("XEUSU_ID")]
    public int Id { get; set; }

    [Required]
    [Column("XEUSU_LOGIN")]
    [StringLength(50)]
    public string Login { get; set; } = string.Empty;

    [Required]
    [Column("XEUSU_EMAIL")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column("XEUSU_PASWD")]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Column("XEEST_CODIGO")]
    [StringLength(1)]
    public string EstadoCodigo { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_CODIGO")]
    [StringLength(6)]
    public string EmpleadoCodigo { get; set; } = string.Empty;

    [Required]
    [Column("XEUSU_FECCRE")]
    public DateTime FechaCreacion { get; set; }

    [Required]
    [Column("XEUSU_FECMOD")]
    public DateTime FechaModificacion { get; set; }

    [Required]
    [Column("XEUSU_PIEFIR")]
    [StringLength(100)]
    public string PieFirma { get; set; } = string.Empty;

    [Column("XEUSU_PRIMER_INGRESO")]
    public bool PrimerIngreso { get; set; }

    // Navegación
    public Estado? Estado { get; set; }

    public Empleado? Empleado { get; set; }

    public ICollection<RecuperacionPassword>
    Recuperaciones
    { get; set; }
    = new List<RecuperacionPassword>();


    public ICollection<UsuarioPerfil> UsuariosPerfiles
    {
        get;
        set;
    } = new List<UsuarioPerfil>();

    

}