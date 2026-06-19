using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Data
{
    public class ProyectoDbContext : DbContext
    {
        public ProyectoDbContext(DbContextOptions<ProyectoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sexo> Sexos { get; set; }

        public DbSet<EstadoCivil> EstadosCiviles { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Sistema> Sistemas { get; set; }

        public DbSet<Perfil> Perfiles { get; set; }

        public DbSet<Discapacidad> Discapacidades { get; set; }

        public DbSet<Instruccion> Instrucciones { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Familiar> Familiares { get; set; }

        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<RecuperacionPassword> Recuperaciones { get; set; }

        public DbSet<UsuarioPerfil> UsuariosPerfiles { get; set; }

        public DbSet<Opcion> Opciones { get; set; }

        public DbSet<PerfilOpcion> PerfilesOpciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cargo>()
                .HasKey(c => new
                {
                    c.DepartamentoCodigo,
                    c.Codigo
                });
            modelBuilder.Entity<Cargo>()
              .HasOne(c => c.Departamento)
              .WithMany(d => d.Cargos)
              .HasForeignKey(c => c.DepartamentoCodigo);

            modelBuilder.Entity<Empleado>()
              .HasOne(e => e.Sexo)
              .WithMany(s => s.Empleados)
              .HasForeignKey(e => e.SexoCodigo);
           
            modelBuilder.Entity<Empleado>()
              .HasOne(e => e.EstadoCivil)
              .WithMany(ec => ec.Empleados)
              .HasForeignKey(e => e.EstadoCivilCodigo);

            modelBuilder.Entity<Empleado>()
              .HasOne(e => e.Jefe)
              .WithMany(j => j.Subordinados)
              .HasForeignKey(e => e.JefeCodigo)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
              .HasOne(e => e.Cargo)
              .WithMany(c => c.Empleados)
              .HasForeignKey(e => new {e.DepartamentoCodigo, e.CargoCodigo});

            modelBuilder.Entity<Empleado>()
              .HasOne(e => e.Discapacidad)
              .WithMany(d => d.Empleados)
              .HasForeignKey(e => e.DiscapacidadCodigo);

            modelBuilder.Entity<Empleado>()
              .HasOne(e => e.Instruccion)
              .WithMany(i => i.Empleados)
              .HasForeignKey(e => e.InstruccionCodigo);

            modelBuilder.Entity<Usuario>()
              .HasOne(u => u.Estado)
              .WithMany()
              .HasForeignKey(u => u.EstadoCodigo);

            modelBuilder.Entity<Usuario>()
              .HasOne(u => u.Empleado)
              .WithMany()
              .HasForeignKey(u => u.EmpleadoCodigo);

            modelBuilder.Entity<RecuperacionPassword>()
              .HasOne(r => r.Usuario)
              .WithMany(u => u.Recuperaciones)
              .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<UsuarioPerfil>()
              .HasKey(up => up.Id);

            modelBuilder.Entity<UsuarioPerfil>()
               .HasOne(up => up.Usuario)
               .WithMany(u => u.UsuariosPerfiles)
               .HasForeignKey(up => up.UsuarioId);


            modelBuilder.Entity<UsuarioPerfil>()
               .HasOne(up => up.Perfil)
               .WithMany(p => p.UsuariosPerfiles)
               .HasForeignKey(up => up.PerfilCodigo);

            modelBuilder.Entity<PerfilOpcion>()
               .HasKey(po => new { po.PerfilCodigo, po.OpcionCodigo});

           


            modelBuilder.Entity<PerfilOpcion>()
              .HasOne(po => po.Perfil)
              .WithMany(p => p.PerfilesOpciones)
              .HasForeignKey(po => po.PerfilCodigo);

            modelBuilder.Entity<PerfilOpcion>()
              .HasOne(po => po.Opcion)
              .WithMany(o => o.PerfilesOpciones)
              .HasForeignKey(po => po.OpcionCodigo);


            modelBuilder.Entity<Opcion>()
              .HasOne(o => o.Sistema)
              .WithMany(s => s.Opciones)
              .HasForeignKey(o => o.SistemaCodigo);

            modelBuilder.Entity<Familiar>()
              .HasOne(f => f.Empleado)
              .WithMany(e => e.Familiares)
              .HasForeignKey(f => f.EmpleadoCodigo)
              .OnDelete(DeleteBehavior.Restrict);

        }

    }
}