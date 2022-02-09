using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlagShipHospitalBackEnd.Models
{
    public partial class FlagSHospitalContext : DbContext
    {
        public FlagSHospitalContext()
        {
        }

        public FlagSHospitalContext(DbContextOptions<FlagSHospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dossierpatient> Dossierpatients { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=FlagSHospital;User Id=postgres;Password=SuperMotDePasseOfDoom");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dossierpatient>(entity =>
            {
                entity.ToTable("dossierpatient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateArrivee)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date_arrivee");

                entity.Property(e => e.DateDepart)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date_depart");

                entity.Property(e => e.Nom)
                    .HasColumnType("character varying")
                    .HasColumnName("nom");

                entity.Property(e => e.Note)
                    .HasColumnType("character varying")
                    .HasColumnName("note");

                entity.Property(e => e.Prenom)
                    .HasColumnType("character varying")
                    .HasColumnName("prenom");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.Motdepasse)
                    .HasColumnType("character varying")
                    .HasColumnName("motdepasse");

                entity.Property(e => e.Role)
                    .HasColumnType("character varying")
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
