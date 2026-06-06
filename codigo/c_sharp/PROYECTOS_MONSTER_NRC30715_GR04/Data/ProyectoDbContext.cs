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

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

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

            modelBuilder.Entity<Usuario>()
              .HasOne(u => u.Estado)
              .WithMany()
              .HasForeignKey(u => u.EstadoCodigo);

            modelBuilder.Entity<Usuario>()
              .HasOne(u => u.Empleado)
              .WithMany()
              .HasForeignKey(u => u.EmpleadoCodigo);
        }

    }
}