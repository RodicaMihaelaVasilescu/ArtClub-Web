using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Art.Models
{
    public partial class ClubulPasionatilorDeArtaContext : DbContext
    {
        public ClubulPasionatilorDeArtaContext()
        {
        }

        public ClubulPasionatilorDeArtaContext(DbContextOptions<ClubulPasionatilorDeArtaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<EventUsers> EventUsers { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<PersonalAccounts> PersonalAccounts { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<ValidationCodes> ValidationCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                // See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(File.ReadAllText(@"..\..\Art\Art\ConnectionString\ConnectionString.txt"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.IdEvent);

                entity.Property(e => e.IdEvent)
                    .HasColumnName("ID_Event")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Place)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EventUsers>(entity =>
            {
                entity.HasKey(e => e.GuidEventUser);

                entity.Property(e => e.GuidEventUser)
                    .HasColumnName("GUID_EventUser")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkGuidUser).HasColumnName("fk_GUID_User");

                entity.Property(e => e.FkIdEvent).HasColumnName("fk_ID_Event");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.IdInvoice);

                entity.Property(e => e.IdInvoice)
                    .HasColumnName("ID_Invoice")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(2, 0)");

                entity.Property(e => e.FkGuidUser).HasColumnName("fk_GUID_User");

                entity.Property(e => e.FkIdEvent).HasColumnName("fk_ID_Event");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.HasOne(d => d.FkGuidUserNavigation)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.FkGuidUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Users");

                entity.HasOne(d => d.FkIdEventNavigation)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.FkIdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Events");
            });

            modelBuilder.Entity<PersonalAccounts>(entity =>
            {
                entity.HasKey(e => e.IdPersonalAccount);

                entity.Property(e => e.IdPersonalAccount)
                    .HasColumnName("ID_PersonalAccount")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkGuidUser).HasColumnName("fk_GUID_User");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkGuidUserNavigation)
                    .WithMany(p => p.PersonalAccounts)
                    .HasForeignKey(d => d.FkGuidUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalAccounts_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.GuidUser);

                entity.Property(e => e.GuidUser)
                    .HasColumnName("GUID_User")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValidationCodes>(entity =>
            {
                entity.HasKey(e => e.IdConfirmationCode);

                entity.Property(e => e.IdConfirmationCode)
                    .HasColumnName("ID_ConfirmationCode")
                    .ValueGeneratedNever();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
