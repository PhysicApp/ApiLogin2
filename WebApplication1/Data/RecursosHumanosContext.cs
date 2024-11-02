using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiLogin.Models;
using ApiLogin.Modelo;

namespace ApiLogin.Data
{
    public partial class RecursosHumanosContext : DbContext
    {
        public RecursosHumanosContext()
        {
        }

        public RecursosHumanosContext(DbContextOptions<RecursosHumanosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Puesto> Puestos { get; set; } = null!;
        public virtual DbSet<UsuarioLogin> UsuarioLogins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=recursos_humanos_db;user=root;password=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PRIMARY");

                entity.ToTable("departamento");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.Abreviacion)
                    .HasMaxLength(255)
                    .HasColumnName("abreviacion");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_departamento");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PRIMARY");

                entity.ToTable("empleado");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.Departamento)
                    .HasMaxLength(255)
                    .HasColumnName("departamento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sueldo).HasColumnName("sueldo");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasKey(e => e.IdPuesto)
                    .HasName("PRIMARY");

                entity.ToTable("puesto");

                entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");

                entity.Property(e => e.Abreviacion)
                    .HasMaxLength(255)
                    .HasColumnName("abreviacion");

                entity.Property(e => e.Activo)
                    .HasColumnType("bit(1)")
                    .HasColumnName("activo");

                entity.Property(e => e.NombrePuesto)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_puesto");
            });

            modelBuilder.Entity<UsuarioLogin>(entity =>
            {
                entity.HasKey(e => e.IdLogin)
                    .HasName("PRIMARY");

                entity.ToTable("usuario_login");

                entity.Property(e => e.IdLogin).HasColumnName("id_login");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
