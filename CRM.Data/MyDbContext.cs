using System;
using CRM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRM.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brick> Brick { get; set; }
        public virtual DbSet<BrickLocality> BrickLocality { get; set; }
        public virtual DbSet<BuDoctor> BuDoctor { get; set; }
        public virtual DbSet<BuUser> BuUser { get; set; }
        public virtual DbSet<BusinessUnit> BusinessUnit { get; set; }
        public virtual DbSet<Cycle> Cycle { get; set; }
        public virtual DbSet<CycleBu> CycleBu { get; set; }
        public virtual DbSet<CycleSectorWeekDoctors> CycleSectorWeekDoctors { get; set; }
        public virtual DbSet<DelegateManager> DelegateManager { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Establishment> Establishment { get; set; }
        public virtual DbSet<EstablishmentDoctor> EstablishmentDoctor { get; set; }
        public virtual DbSet<EstablishmentLocality> EstablishmentLocality { get; set; }
        public virtual DbSet<EstablishmentService> EstablishmentService { get; set; }
        public virtual DbSet<EstablishmentType> EstablishmentType { get; set; }
        public virtual DbSet<EstablishmentUser> EstablishmentUser { get; set; }
        public virtual DbSet<Info> Info { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Pharmacy> Pharmacy { get; set; }
        public virtual DbSet<PharmacyLocality> PharmacyLocality { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Potentiel> Potentiel { get; set; }
        public virtual DbSet<PotentielCycle> PotentielCycle { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<SectorLocality> SectorLocality { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceDoctor> ServiceDoctor { get; set; }
        public virtual DbSet<SpecialityDoctor> SpecialityDoctor { get; set; }
        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WeekInCycle> WeekInCycle { get; set; }
        public virtual DbSet<WeekInYear> WeekInYear { get; set; }
        public virtual DbSet<WeekSectorCycle> WeekSectorCycle { get; set; }
        public virtual DbSet<WeekSectorCycleInYear> WeekSectorCycleInYear { get; set; }
        public virtual DbSet<WholeSaler> WholeSaler { get; set; }
        public virtual DbSet<WholeSalerLocality> WholeSalerLocality { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5433;User ID=postgres;Password=root;Database=test");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Brick>(entity =>
            {
                entity.HasKey(e => new { e.IdBrick,e.Status,e.Version });
                
                entity.Property(x => x.IdBrick).UseIdentityColumn();
               entity.HasIndex(e => new { e.Active, e.IdBrick, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdBrick).IsUnique(false);


                entity.Property(e => e.Description)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                entity.Property(e => e.Name)
                        .HasMaxLength(50)
                        .IsUnicode(false);
            });
            modelBuilder.Entity<BrickLocality>(entity =>
            {
                entity.HasKey(e => new { e.IdLocality, e.StatusBrick, e.VersionBrick, e.IdBrick, e.StatusLocality, e.VersionLocality });

                entity.HasOne(d => d.IdBrickNavigation)
                    .WithMany(p => p.BrickLocality)
                    .HasForeignKey(d => new { d.IdBrick, d.StatusBrick,d.VersionBrick })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BrickLocality_Brick");

                entity.HasOne(d => d.IdLocalityNavigation)
                    .WithMany(p => p.BrickLocality)
                    .HasForeignKey(d => new { d.IdLocality, d.StatusLocality, d.VersionLocality })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BrickLocality_Locality");
            });

            modelBuilder.Entity<BuDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdBu, e.StatusBu, e.VersionBu, e.IdDoctor, e.StatusDoctor, e.VersionDoctor });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdBuNavigation)
                    .WithMany(p => p.BuDoctor)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuDoctor_BusinessUnit");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.BuDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuDoctor_Doctor");
            });

            modelBuilder.Entity<BuUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.StatusUser, e.VersionUser, e.StatusBu, e.VersionBu, e.IdBu });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdBuNavigation)
                    .WithMany(p => p.BuUser)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuUser_BusinessUnit");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.BuUser)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuUser_User");
            });

            modelBuilder.Entity<BusinessUnit>(entity =>
            {
                entity.HasKey(e => new { e.IdBu, e.Status, e.Version });
                entity.Property(x => x.IdBu).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdBu, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdBu).IsUnique(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<Cycle>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.Status, e.Version });
                entity.Property(x => x.IdCycle).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdCycle, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdCycle).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<CycleBu>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.StatusCycle, e.VersionCycle, e.IdBu, e.StatusBu, e.VersionBu });

                entity.HasOne(d => d.IdBuNavigation)
                    .WithMany(p => p.CycleBu)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleBu_BusinessUnit");

                entity.HasOne(d => d.IdCycleNavigation)
                    .WithMany(p => p.CycleBu)
                    .HasForeignKey(d => new{ d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleBu_Cycle");
            });

            modelBuilder.Entity<CycleSectorWeekDoctors>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.StatusCycle, e.VersionCycle, e.IdUser, e.StatusUser, e.VersionUser, e.IdDoctor,e.StatusDoctor, 
                    e.VersionDoctor, e.IdWeekCycle, e.StatusWeekCycle, e.VersionWeekCycle });

                entity.HasOne(d => d.IdCycleNavigation)
                    .WithMany(p => p.CycleSectorWeekDoctors)
                    .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleSectorWeekDoctors_Cycle");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.CycleSectorWeekDoctors)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleSectorWeekDoctors_Doctor");

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.CycleSectorWeekDoctors)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .HasConstraintName("FK_CycleSectorWeekDoctors_Sector");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.CycleSectorWeekDoctors)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleSectorWeekDoctors_User");

                entity.HasOne(d => d.IdWeekCycleNavigation)
                    .WithMany(p => p.CycleSectorWeekDoctors)
                    .HasForeignKey(d => new { d.IdWeekCycle, d.StatusWeekCycle, d.VersionWeekCycle })
                    .HasConstraintName("FK_CycleSectorWeekDoctors_WeekInCycle");
            });

            modelBuilder.Entity<DelegateManager>(entity =>
            {
                entity.HasKey(e => new { e.IdDelegate, e.VersionDelegate, e.StatusDelegate, e.IdManager, e.VersionManager, e.StatusManager });

                entity.Property(e => e.IdDelegate).HasColumnName("Id_Delegate");

                entity.Property(e => e.IdManager).HasColumnName("Id_Manager");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.TypeRelation)
                    .HasColumnName("Type_Relation")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDelegateNavigation)
                    .WithMany(p => p.DelegateManagerIdDelegateNavigation)
                    .HasForeignKey(d => new { d.IdDelegate, d.StatusDelegate, d.VersionDelegate })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelegateManager_User");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.DelegateManagerIdManagerNavigation)
                    .HasForeignKey(d => new { d.IdManager, d.StatusManager, d.VersionManager })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelegateManager_User1");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => new { e.IdDoctor, e.Status, e.Version });
                entity.Property(x => x.IdDoctor).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdDoctor, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdDoctor).IsUnique(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NbPatientsDay).HasColumnName("Nb_Patients_Day");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey<Doctor>(d => new { d.IdDoctor, d.Status, d.Version })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Potentiel");

                entity.HasOne(d => d.Linked)
                    .WithMany(p => p.InverseLinked)
                    .HasForeignKey(d => new { d.LinkedId, d.StatusLink, d.VersionLink })
                    .HasConstraintName("FK_Doctor_Doctor");
            });

            modelBuilder.Entity<Establishment>(entity =>
            {
                entity.HasKey(e => new { e.IdEstablishment, e.Status, e.Version });
                entity.Property(x => x.IdEstablishment).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdEstablishment, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdEstablishment).IsUnique(false);
                entity.Property(e => e.Adresse)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdEstablishmentTypeNavigation)
                    .WithMany(p => p.Establishment)
                    .HasForeignKey(d => new { d.IdEstablishmentType, d.StatusEstablishmentType, d.VersionEstablishmentType })
                    .HasConstraintName("FK_Establishment_EstablishmentType");
            });

            modelBuilder.Entity<EstablishmentDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdEstablishment, e.StatusEstablishment, e.VersionEstablishment, e.IdDoctor, e.StatusDoctor, e.VersionDoctor });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.EstablishmentDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentDoctor_Doctor");

                entity.HasOne(d => d.IdEstablishmentNavigation)
                    .WithMany(p => p.EstablishmentDoctor)
                    .HasForeignKey(d => new { d.IdEstablishment, d.StatusEstablishment, d.VersionEstablishment })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentDoctor_Establishment");
            });

            modelBuilder.Entity<EstablishmentLocality>(entity =>
            {
                entity.HasKey(e => new { e.IdEstablishment, e.IdLocality });

                entity.HasOne(d => d.IdEstablishmentNavigation)
                    .WithMany(p => p.EstablishmentLocality)
                    .HasForeignKey(d => new { d.IdEstablishment, d.StatusEstablishment, d.VersionEstablishment })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentLocality_Establishment");

                entity.HasOne(d => d.IdLocalityNavigation)
                    .WithMany(p => p.EstablishmentLocality)
                    .HasForeignKey(d => new { d.IdLocality, d.StatusLocality, d.VersionLocality })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentLocality_Locality");
            });

            modelBuilder.Entity<EstablishmentService>(entity =>
            {
                entity.HasKey(e => new { e.IdEstablishment, e.IdService });

                entity.HasOne(d => d.IdEstablishmentNavigation)
                    .WithMany(p => p.EstablishmentService)
                    .HasForeignKey(d => new { d.IdEstablishment, d.StatusEstablishment, d.VersionEstablishment })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentService_Establishment");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.EstablishmentService)
                    .HasForeignKey(d => new { d.IdService, d.StatusService, d.VersionService })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentService_Service");
            });

            modelBuilder.Entity<EstablishmentType>(entity =>
            {
                entity.HasKey(e => new { e.IdEstablishmentType, e.Status, e.Version });
                entity.Property(x => x.IdEstablishmentType).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdEstablishmentType, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdEstablishmentType).IsUnique(false);
                entity.Property(e => e.IdEstablishmentType).HasColumnName("idEstablishmentType");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<EstablishmentUser>(entity =>
            {
                entity.HasKey(e => new { e.IdEstablishment, e.IdUser });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdEstablishmentNavigation)
                    .WithMany(p => p.EstablishmentUser)
                    .HasForeignKey(d => new { d.IdEstablishment, d.StatusEstablishment, d.VersionEstablishment })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentUser_Establishment");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.EstablishmentUser)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstablishmentUser_User");
            });

            modelBuilder.Entity<Info>(entity =>
            {
                entity.HasKey(e => new { e.IdInf, e.Status, e.Version });
                entity.Property(x => x.IdInf).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdInf, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdInf).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("text");

                entity.Property(e => e.Datatype)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeInfo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.Info)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .HasConstraintName("FK_Info_Doctor");
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.HasKey(e => new { e.IdLocality, e.Status, e.Version });
                entity.Property(x => x.IdLocality).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdLocality, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdLocality).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.InverseIdParentNavigation)
                    .HasForeignKey(d => new { d.IdParent, d.StatusParent, d.VersionParent })
                    .HasConstraintName("FK_Locality_Locality");
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.HasKey(e => new { e.IdPharmacy, e.Status, e.Version });
                entity.Property(x => x.IdPharmacy).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdPharmacy, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdPharmacy).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.FirstNameOwner)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastNameOwner)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Seller)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<PharmacyLocality>(entity =>
            {
                entity.HasKey(e => new { e.IdPharmacy, e.IdLocality });

                entity.HasOne(d => d.IdLocalityNavigation)
                    .WithMany(p => p.PharmacyLocality)
                    .HasForeignKey(d => new { d.IdLocality, d.StatusLocality, d.VersionLocality })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PharmacyLocality_Locality");

                entity.HasOne(d => d.IdPharmacyNavigation)
                    .WithMany(p => p.PharmacyLocality)
                    .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PharmacyLocality_Pharmacy");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => new { e.IdPhone, e.Status, e.Version });
                entity.Property(x => x.IdPhone).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdPhone, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdPhone).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.IdPharmacy).HasColumnName("idPharmacy");

                entity.Property(e => e.IdUser).HasColumnName("idUser");


                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .HasConstraintName("FK_Phone_Doctor");
                entity.HasOne(d => d.IdWholeSalerNavigation)
                .WithMany(p => p.Phone)
                .HasForeignKey(d => new { d.IdWholeSaler, d.StatusWholeSaler, d.VersionWholeSaler })
                .HasConstraintName("FK_WholeSaler_Doctor");
                entity.HasOne(d => d.IdPharmacyNavigation)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                    .HasConstraintName("FK_Phone_Pharmacy");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .HasConstraintName("FK_Phone_User");
            });

            modelBuilder.Entity<Potentiel>(entity =>
            {
                entity.HasKey(e => new { e.IdPotentiel, e.Status, e.Version });
                entity.Property(x => x.IdPotentiel).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdPotentiel, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdPotentiel).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<PotentielCycle>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.StatusCycle, e.VersionCycle ,e.IdPotentiel, e.StatusPotentiel, e.VersionPotentiel });

                entity.HasOne(d => d.IdCycleNavigation)
                    .WithMany(p => p.PotentielCycle)
                    .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PotentielCycle_Cycle");

                entity.HasOne(d => d.IdPotentielNavigation)
                    .WithMany(p => p.PotentielCycle)
                    .HasForeignKey(d => new { d.IdPotentiel, d.StatusPotentiel, d.VersionPotentiel })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PotentielCycle_Potentiel");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => new { e.IdSector, e.Status, e.Version });
                entity.Property(x => x.IdSector).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdSector, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdSector).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<SectorLocality>(entity =>
            {
                entity.HasKey(e => new { e.IdLocality,e.VersionLocality,e.StatusLocality, e.IdSector, e.VersionSector, e.StatusSector });

                entity.HasOne(d => d.IdLocalityNavigation)
                    .WithMany(p => p.SectorLocality)
                    .HasForeignKey(d => new { d.IdLocality, d.StatusLocality, d.VersionLocality })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectorLocality_Locality");

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.SectorLocality)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectorLocality_Sector");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => new { e.IdService, e.Status, e.Version });
                entity.Property(x => x.IdService).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdService, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdService).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<ServiceDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdService,e.StatusService,e.VersionService, e.IdDoctor,e.StatusDoctor,e.VersionDoctor });

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.ServiceDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceDoctor_Doctor");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.ServiceDoctor)
                    .HasForeignKey(d => new { d.IdService, d.StatusService, d.VersionService })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceDoctor_Service");
            });

            modelBuilder.Entity<SpecialityDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdDoctor, e.StatusDoctor, e.VersionDoctor, e.IdSpecialty, e.StatusSpecialty, e.VersionSpecialty });

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.SpecialityDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialityDoctor_Doctor");

                entity.HasOne(d => d.IdSpecialtyNavigation)
                    .WithMany(p => p.SpecialityDoctor)
                    .HasForeignKey(d => new { d.IdSpecialty, d.StatusSpecialty, d.VersionSpecialty })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialityDoctor_Specialty");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(e => new { e.IdSpecialty, e.Status, e.Version });
                entity.Property(x => x.IdSpecialty).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdSpecialty, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdSpecialty).IsUnique(false);
                entity.Property(e => e.Abreviation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.Status, e.Version });
                entity.Property(x => x.IdUser).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdUser, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdUser).IsUnique(false);
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WeekInCycle>(entity =>
            {
                entity.HasKey(e => new { e.IdWeekCycle, e.Status, e.Version });
                entity.Property(x => x.IdWeekCycle).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdWeekCycle, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdWeekCycle).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<WeekInYear>(entity =>
            {
                entity.HasKey(e => new { e.Year, e.Order, e.Status, e.Version })
                    .HasName("PK_WeekInYear_1")
                    .IsClustered(false);

                entity.HasIndex(e => e.Order)
                    .HasName("IX_WeekInYear")
                    .IsUnique();

                entity.HasIndex(e => e.Year)
                    .HasName("IX_WeekInYear_1")
                    .IsUnique();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<WeekSectorCycle>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.StatusCycle, e.VersionCycle, e.IdWeekCycle, e.StatusWeekCycle, e.VersionWeekCycle, e.IdSector,e.StatusSector, e.VersionSector });

                entity.HasOne(d => d.IdCycleNavigation)
                    .WithMany(p => p.WeekSectorCycle)
                    .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycle_Cycle");

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.WeekSectorCycle)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycle_Sector");

                entity.HasOne(d => d.IdWeekCycleNavigation)
                    .WithMany(p => p.WeekSectorCycle)
                    .HasForeignKey(d => new { d.IdWeekCycle, d.StatusWeekCycle, d.VersionWeekCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycle_WeekInCycle");
            });

            modelBuilder.Entity<WeekSectorCycleInYear>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle,e.StatusCycle,e.VersionCycle, e.IdWeekCycle,e.VersionSector,e.StatusSector, e.IdSector,e.StatusWeekCycle,e.VersionWeekCycle });

                entity.HasOne(d => d.IdCycleNavigation)
                    .WithMany(p => p.WeekSectorCycleInYear)
                    .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_Cycle");

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.WeekSectorCycleInYear)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_Sector");

                entity.HasOne(d => d.IdWeekCycleNavigation)
                    .WithMany(p => p.WeekSectorCycleInYear)
                    .HasForeignKey(d => new { d.IdWeekCycle, d.StatusWeekCycle, d.VersionWeekCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_WeekInCycle");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.WeekSectorCycleInYearOrderNavigation)
                    .HasPrincipalKey(p => p.Order)
                    .HasForeignKey(d => d.Order)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_WeekSectorCycleInYear");

                entity.HasOne(d => d.YearNavigation)
                    .WithMany(p => p.WeekSectorCycleInYearYearNavigation)
                    .HasPrincipalKey(p => p.Year)
                    .HasForeignKey(d => d.Year)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_WeekInYear");
            });

            modelBuilder.Entity<WholeSaler>(entity =>
            {
                entity.HasKey(e => new { e.IdPwholeSaler, e.Status, e.Version });
                entity.Property(x => x.IdPwholeSaler).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdPwholeSaler, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdPwholeSaler).IsUnique(false);
                entity.Property(e => e.IdPwholeSaler).HasColumnName("IdPWholeSaler");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<WholeSalerLocality>(entity =>
            {
                entity.HasKey(e => new { e.IdLocality,e.VersionLocality,e.StatusLocality, e.IdPwholeSaler,e.StatusPwholeSaler,e.VersionPwholeSaler });

                entity.Property(e => e.IdPwholeSaler).HasColumnName("IdPWholeSaler");

                entity.HasOne(d => d.IdLocalityNavigation)
                    .WithMany(p => p.WholeSalerLocality)
                    .HasForeignKey(d => new { d.IdLocality, d.StatusLocality, d.VersionLocality })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WholeSalerLocality_Locality");

                entity.HasOne(d => d.IdPwholeSalerNavigation)
                    .WithMany(p => p.WholeSalerLocality)
                    .HasForeignKey(d => new { d.IdPwholeSaler, d.StatusPwholeSaler, d.VersionPwholeSaler })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WholeSalerLocality_WholeSaler");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
