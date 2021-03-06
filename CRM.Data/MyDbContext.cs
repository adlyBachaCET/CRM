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
        public virtual DbSet<Support> Support { get; set; }

        public virtual DbSet<Brick> Brick { get; set; }
        public virtual DbSet<BuDoctor> BuDoctor { get; set; }
        public virtual DbSet<BuUser> BuUser { get; set; }
        public virtual DbSet<CycleUser> CycleUser { get; set; }
        public virtual DbSet<SpecialtyDoctor> SpecialtyDoctor { get; set; }

        public virtual DbSet<BuFile> BuFile { get; set; }
        public virtual DbSet<ProductFile> ProductFile { get; set; }
        public virtual DbSet<SellingObjectives> SellingObjectives { get; set; }
        public virtual DbSet<ProductPharmacy> ProductPharmacy { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<RequestRp> RequestRp { get; set; }

        public virtual DbSet<TagsRequestRp> TagsRequestRp { get; set; }

        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<VisitUser> VisitUser { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }
        public virtual DbSet<VisitChannel> VisitChannel { get; set; }
        public virtual DbSet<VisitReport> VisitReport { get; set; }
        public virtual DbSet<ProductVisitReport> ProductVisitReport { get; set; }

        public virtual DbSet<Tracking> Tracking { get; set; }
        public virtual DbSet<VisitFileTracking> VisitFileTracking { get; set; }

        public virtual DbSet<BusinessUnit> BusinessUnit { get; set; }
        public virtual DbSet<Cycle> Cycle { get; set; }
        public virtual DbSet<CycleBu> CycleBu { get; set; }
        public virtual DbSet<Holidays> Holidays { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<ActivityUser> ActivityUser { get; set; }
        public virtual DbSet<Appointement> Appointement { get; set; }

        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<Objection> Objection { get; set; }
        public virtual DbSet<VisitRequest> VisitRequest { get; set; }

        public virtual DbSet<CommandeProduct> CommandeProduct { get; set; }

        public virtual DbSet<Target> Target { get; set; }
        public virtual DbSet<SharedFiles> SharedFiles { get; set; }
        public virtual DbSet<FavouriteFiles> FavouriteFiles { get; set; }

        public virtual DbSet<Planification> Planification { get; set; }

        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationDoctor> LocationDoctor { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Externals> Externals { get; set; }

        public virtual DbSet<LocationType> LocationType { get; set; }
        public virtual DbSet<Info> Info { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Pharmacy> Pharmacy { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Potentiel> Potentiel { get; set; }
        public virtual DbSet<PotentielCycle> PotentielCycle { get; set; }
        public virtual DbSet<PotentielSector> PotentielSector { get; set; }

        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<SectorLocality> SectorLocality { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<MessageUser> MessageUser { get; set; }

        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<TagsDoctor> TagsDoctor { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WeekInYear> WeekInYear { get; set; }
        public virtual DbSet<SectorCycle> SectorCycle { get; set; }
        public virtual DbSet<SectorInYear> SectorCycleInYear { get; set; }
 

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
            modelBuilder.Entity<Support>(entity =>
            {
                entity.HasKey(e => new { e.IdSupport, e.Status, e.Version });

                entity.Property(x => x.IdSupport).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdSupport, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdSupport).IsUnique(false);


                entity.Property(e => e.Email)
                        .HasMaxLength(50)
                        .IsUnicode(false);
                entity.Property(e => e.Password)
                       .HasMaxLength(50)
                       .IsUnicode(false);
                entity.Property(e => e.Name)
                        .HasMaxLength(50)
                        .IsUnicode(false);
            });
            modelBuilder.Entity<Tracking>(entity =>
            {
                entity.HasKey(e => new { e.IdTracking, e.Status, e.Version });

                entity.Property(x => x.IdTracking).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdTracking, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdTracking).IsUnique(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UpdatedOn).HasColumnType("timestamp");
                entity.Property(e => e.Date).HasColumnType("date");


            });
            modelBuilder.Entity<Planification>(entity =>
            {
                entity.HasKey(e => new { e.IdPlanification, e.Status, e.Version });

                entity.Property(x => x.IdPlanification).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdPlanification, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdPlanification).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UpdatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Description)
                        .HasMaxLength(50)
                        .IsUnicode(false);

        
            });
            modelBuilder.Entity<VisitRequest>(entity =>
            {
                entity.HasKey(e => new { e.IdVisitRequest, e.Status, e.Version });

                entity.Property(x => x.IdVisitRequest).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdVisitRequest, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdVisitRequest).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UpdatedOn).HasColumnType("timestamp");


     

        });
            modelBuilder.Entity<Message>(entity => 
            {
                entity.HasKey(e => new { e.IdMessage, e.Status, e.Version });

                entity.Property(x => x.IdMessage).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdMessage, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdMessage).IsUnique(false);

            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => new { e.IdActivity, e.Status, e.Version });

                entity.Property(x => x.IdActivity).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdActivity, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdActivity).IsUnique(false);

            });


            modelBuilder.Entity<BuDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdBu, e.StatusBu, e.VersionBu, e.IdDoctor, e.StatusDoctor, e.VersionDoctor });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UpdatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Bu)
                    .WithMany(p => p.BuDoctor)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu})
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuDoctor_BusinessUnit");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.BuDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuDoctor_Doctor");
   

            });
            modelBuilder.Entity<SpecialtyDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdSpecialty, e.StatusSpecialty, e.VersionSpecialty, e.IdDoctor, e.StatusDoctor, e.VersionDoctor });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UpdatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.SpecialtyDoctor)
                    .HasForeignKey(d => new { d.IdSpecialty, d.StatusSpecialty, d.VersionSpecialty })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialtyDoctor_SpecialtysinessUnit");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.SpecialtyDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialtyDoctor_Doctor");


            });
            modelBuilder.Entity<BuUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.StatusUser, e.VersionUser, e.StatusBu, e.VersionBu, e.IdBu });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Bu)
                    .WithMany(p => p.BuUser)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuUser_BusinessUnit");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BuUser)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuUser_User");
            });

            modelBuilder.Entity<CycleUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.StatusUser, e.VersionUser, e.StatusCycle, e.VersionCycle, e.IdCycle });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Cycle)
                    .WithMany(p => p.CycleUser)
                    .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleUser_CyclesinessUnit");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CycleUser)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CycleUser_User");
            });
            modelBuilder.Entity<VisitUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.StatusUser, e.VersionUser, e.StatusVisit, e.VersionVisit, e.IdVisit });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.VisitUser)
                    .HasForeignKey(d => new { d.IdVisit, d.StatusVisit, d.VersionVisit })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitUser_Visit");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisitUser)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitUser_User");
            });
            modelBuilder.Entity<ActivityUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.StatusUser, e.VersionUser, e.StatusActivity, e.VersionActivity, e.IdActivity });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityUser)
                    .HasForeignKey(d => new { d.IdActivity, d.StatusActivity, d.VersionActivity })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityUser_Activity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActivityUser)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityUser_User");
            });


            modelBuilder.Entity<BuFile>(entity =>
            {
                entity.HasKey(e => new { e.IdFile, e.StatusFile, e.VersionFile, e.StatusBu, e.VersionBu, e.IdBu });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdBuNavigation)
                    .WithMany(p => p.BuFile)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuFile_BusinessUnit");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.BuFile)
                    .HasForeignKey(d => new { d.IdFile, d.StatusFile, d.VersionFile })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BuFile_File");
            });
            modelBuilder.Entity<ProductFile>(entity =>
            {
                entity.HasKey(e => new { e.IdFile, e.StatusFile, e.VersionFile, e.StatusProduct, e.VersionProduct, e.IdProduct });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductFile)
                    .HasForeignKey(d => new { d.IdProduct, d.StatusProduct, d.VersionProduct })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductFile_Product");

                entity.HasOne(d => d.IdFileNavigation)
                    .WithMany(p => p.ProductFile)
                    .HasForeignKey(d => new { d.IdFile, d.StatusFile, d.VersionFile })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductFile_File");
            });
            modelBuilder.Entity<ProductPharmacy>(entity =>
            {
                entity.HasKey(e => new { e.IdPharmacy, e.StatusPharmacy, e.VersionPharmacy, e.StatusProduct, e.VersionProduct, e.IdProduct });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPharmacy)
                    .HasForeignKey(d => new { d.IdProduct, d.StatusProduct, d.VersionProduct })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPharmacy_Product");

                entity.HasOne(d => d.Pharmacy)
                    .WithMany(p => p.ProductPharmacy)
                    .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPharmacy_Pharmacy");
            });

            modelBuilder.Entity<MessageUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser1, e.StatusUser1, e.VersionUser1,
                    e.IdUser2, e.StatusUser2, e.VersionUser2, e.StatusMessage, e.VersionMessage, e.IdMessage });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageUser)
                    .HasForeignKey(d => new { d.IdMessage, d.StatusMessage, d.VersionMessage })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageUser_Message");

                entity.HasOne(d => d.User1)
                    .WithMany(p => p.MessageUser1)
                    .HasForeignKey(d => new { d.IdUser1, d.StatusUser1, d.VersionUser1 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageUser_User1"); 
                entity.HasOne(d => d.User2)
            .WithMany(p => p.MessageUser2)
            .HasForeignKey(d => new { d.IdUser2, d.StatusUser2, d.VersionUser2 })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_MessageUser_User2");
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
                entity.HasMany(d => d.Target)
                  .WithOne(p => p.IdCycleNavigation)
                     .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Cycle_Target");
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

            modelBuilder.Entity<CycleBu>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.NameBu, e.StatusCycle, e.VersionCycle, e.IdBu, e.StatusBu, e.VersionBu });

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
     
       
            modelBuilder.Entity<Target>(entity =>
            {
      

                entity.HasKey(d => new { d.IdTarget});
                entity.Property(x => x.IdTarget).UseIdentityColumn();

                entity.HasIndex(e => e.NumTarget).IsUnique(false);


                entity.HasOne(d => d.IdCycleNavigation)
                    .WithMany(p => p.Target)
                    .HasForeignKey(d => new { d.IdCycle, d.StatusCycle, d.VersionCycle })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Target_Cycle");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.Target)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Target_Doctor").IsRequired(false);
               
           entity.HasOne(d => d.IdPharmacyNavigation)
          .WithMany(p => p.Target)
          .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("FK_Target_Pharmacy").IsRequired(false);
                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.Target)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .HasConstraintName("FK_Target_Sector");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Target)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Target_User");


            });

           

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => new { e.IdDoctor, e.Status, e.Version });
                entity.Property(x => x.IdDoctor).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdDoctor, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdDoctor).IsUnique(false);
                entity.HasIndex(e => e.Email).IsUnique(false);
                entity.HasIndex(e => e.FirstName).IsUnique(false);
                entity.HasIndex(e => e.LastName).IsUnique(false);

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

                entity.HasMany(d => d.SpecialtyDoctor)
           .WithOne(p => p.IdDoctorNavigation)
           .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
           .HasConstraintName("FK_Doctor_Specialty").IsRequired(false);

                entity.HasOne(d => d.Potentiel)
                  .WithMany(p=>p.Doctors)
                  .HasForeignKey(d => new { d.IdPotentiel, d.StatusPotentiel, d.VersionPotentiel })
                  .HasConstraintName("FK_Doctor_Potentiel").IsRequired(false);
                entity.HasMany(d => d.Target)
             .WithOne(p => p.IdDoctorNavigation)
                .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
               .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Target_Doctor");
                entity.HasOne(d => d.Linked)
                    .WithMany(p => p.InverseLinked)
                    .HasForeignKey(d => new { d.LinkedId, d.StatusLink, d.VersionLink })
                    .HasConstraintName("FK_Doctor_Doctor").IsRequired(false);
            });
            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => new { e.IdCommande, e.Status, e.Version });

                entity.Property(x => x.IdCommande).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdCommande, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdCommande).IsUnique(false);
                entity.HasOne(d => d.Pharmacy)
                      .WithMany(p=>p.Commande)
                      .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Commande_Pharmacy").IsRequired(false);
                entity.HasOne(d => d.Doctor)
                     .WithMany(p => p.Commande)
                     .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_Commande_Doctor").IsRequired(false);

                entity.HasOne(d => d.User)
     .WithMany(p => p.Commande)
     .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
     .OnDelete(DeleteBehavior.ClientSetNull)
     .HasConstraintName("FK_Commande_User").IsRequired(false);
            });
            modelBuilder.Entity<CommandeProduct>(entity =>
            {
                entity.HasKey(e => new { e.IdCommandeProduct, e.Status, e.Version });

                entity.Property(x => x.IdCommandeProduct).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdCommandeProduct, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdCommandeProduct).IsUnique(false);
              
           entity.HasOne(d => d.Product)
            .WithMany(p=>p.CommandeProducts)
            .HasForeignKey(d => new { d.IdProduct, d.StatusProduct, d.VersionProduct })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CommandeProduct_Product").IsRequired(false);

                entity.HasOne(d => d.Commande)
                        .WithMany(p => p.CommandeProducts)
                        .HasForeignKey(d => new { d.IdCommande, d.StatusCommande, d.VersionCommande })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CommandeProduct_Commande").IsRequired(false);
            });
            modelBuilder.Entity<Appointement>(entity =>
            {
                entity.HasKey(e => new { e.IdAppointement, e.Status, e.Version });

                entity.Property(x => x.IdAppointement).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdAppointement, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdAppointement).IsUnique(false);
                entity.HasOne(d => d.Doctor).WithMany(p=>p.Appointement)
                     .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionsDoctor })
                     .HasConstraintName("FK_Doctor_Appointement").IsRequired(false);
                entity.HasOne(d => d.User).WithMany(p => p.Appointement)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionsUser })
                    .HasConstraintName("FK_User_Appointement").IsRequired(false);
                entity.HasOne(d => d.Pharmacy).WithMany(p => p.Appointement)
              .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionsPharmacy })
              .HasConstraintName("FK_Pharmacy_Appointement").IsRequired(false);
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => new { e.IdLocation, e.Status, e.Version });
                entity.Property(x => x.IdLocation).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdLocation, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdLocation).IsUnique(false);
                entity.HasIndex(e => e.Name).IsUnique(false);


                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Tel)
                  .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdLocationTypeNavigation)
                    .WithMany(p=>p.Location)
                    .HasForeignKey(d => new { d.IdLocationType, d.StatusLocationType, d.VersionLocationType })
                    .HasConstraintName("FK_Location_LocationType").IsRequired(false);


                entity.HasOne(d => d.Linked)
                    .WithMany(p => p.InverseLinked)
    .HasForeignKey(d => new { d.LinkedId, d.StatusLink, d.VersionLink })
    .HasConstraintName("FK_Location_Location").IsRequired(false);
                entity.HasOne(d => d.Locality1)
           .WithMany(p => p.Location1)
           .HasForeignKey(d => new { d.IdLocality1, d.StatusLocality1, d.VersionLocality1 })
           .HasConstraintName("FK_location_locality1").IsRequired(false);
                entity.HasOne(d => d.Locality2).WithMany(p => p.Location2)
           .HasForeignKey(d => new { d.IdLocality2, d.StatusLocality2, d.VersionLocality2 })
           .HasConstraintName("FK_location_locality2").IsRequired(false);
                entity.HasOne(d => d.Brick2)
                                  .WithMany(p => p.Location1)
                                    .HasForeignKey(d => new { d.IdBrick2, d.StatusBrick2, d.VersionBrick2 })
                                    .HasConstraintName("FK_V_Brick").IsRequired(false);
                entity.HasOne(d => d.Brick1)
             .WithMany(p => p.Location2)
               .HasForeignKey(d => new { d.IdBrick1, d.StatusBrick1, d.VersionBrick1 })
               .HasConstraintName("FK_V_Brick2").IsRequired(false);

            });

            modelBuilder.Entity<LocationDoctor>(entity =>
            {
                entity.HasKey(e => e.IdLocationDoctorService);
                entity.Property(x => x.IdLocationDoctorService).UseIdentityColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.HasOne(d => d.Doctor)
                .WithMany(p => p.LocationDoctor)
                .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationDoctor_Doctor").IsRequired(false);
                entity.HasOne(d => d.Service)
                .WithMany(p => p.LocationDoctor)
                    .HasForeignKey(d => new { d.IdService, d.StatusService, d.VersionService })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK_LocationDoctor_Service").IsRequired(false);

                entity.HasOne(d => d.Location)
              .WithMany(p => p.LocationDoctor)
                  .HasForeignKey(d => new { d.IdLocation, d.StatusLocation, d.VersionLocation })
                  .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_LocationDoctor_Location").IsRequired(false);
            });
            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(e => new {
                  
                    e.IdRequestRp,
                    e.StatusRequestRp,
                    e.VersionRequestRp,
                    e.Status,
                    e.Version,
                });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p=>p.Participant)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participant_Doctor").IsRequired(false);
                entity.HasOne(d => d.IdPharmacyNavigation)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participant_Pharmacy").IsRequired(false);

                entity.HasOne(d => d.IdRequestRpNavigation)
                        .WithMany(p => p.Participant)
                        .HasForeignKey(d => new { d.IdRequestRp, d.StatusRequestRp, d.VersionRequestRp })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Participant_RequestRp");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participant_User");
                entity.HasOne(d => d.IdVisitReportNavigation)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => new { d.IdVisitReport, d.StatusVisitReport, d.VersionVisitReport })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participant_VisitReport");

            });
            modelBuilder.Entity<Externals>(entity =>
            {
                entity.HasKey(e => new {

                    e.IdExternal,
                    e.Status,
                    e.Version,
                });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

    
                entity.HasOne(d => d.IdRequestRpNavigation)
                        .WithMany(p => p.Externals)
                        .HasForeignKey(d => new { d.IdRequestRp, d.StatusRequestRp, d.VersionRequestRp })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Externalst_RequestRp");
                entity.HasOne(d => d.IdVisitReportNavigation)
                    .WithMany(p => p.Externals)
                    .HasForeignKey(d => new { d.IdVisitReport, d.StatusVisitReport, d.VersionVisitReport })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Externals_VisitReport");

            });
            modelBuilder.Entity<VisitFileTracking>(entity =>
            {
                entity.HasKey(e => new {
                    e.IdTracking,
                    e.StatusTracking,
                    e.VersionTracking,

                    e.IdVisit,
                    e.StatusVisit,
                    e.VersionVisit,
                    e.IdFile,
                    e.StatusFile,
                    e.VersionFile
                });

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdTrackingNavigation)
                    .WithMany(p => p.VisitFileTracking)
                    .HasForeignKey(d => new { d.IdTracking, d.StatusTracking, d.VersionTracking })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrackingVisitFile_Tracking");

                entity.HasOne(d => d.IdFileNavigation)
                        .WithMany(p => p.VisitFileTracking)
                        .HasForeignKey(d => new { d.IdFile, d.StatusFile, d.VersionFile })
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TrackingVisitFile_File");

                entity.HasOne(d => d.IdVisitNavigation)
                    .WithMany(p => p.VisitFileTracking)
                    .HasForeignKey(d => new { d.IdVisit, d.StatusVisit, d.VersionVisit })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrackingVisitFile_Visit");
            });
            modelBuilder.Entity<LocationType>(entity =>
            {
                entity.HasKey(e => new { e.IdLocationType, e.Status, e.Version });
                entity.Property(x => x.IdLocationType).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdLocationType, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdLocationType).IsUnique(false);
                entity.Property(e => e.IdLocationType).HasColumnName("idLocationType");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
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

                entity.Property(e => e.Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.Info)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .HasConstraintName("FK_Info_Doctor").IsRequired(false);
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.HasKey(e => new { e.IdLocality, e.Status, e.Version });
                entity.Property(x => x.IdLocality).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdLocality, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdLocality).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.HasIndex(e => e.Name).IsUnique(false);
               
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.HasIndex(e => e.Lvl);

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.InverseIdParentNavigation)
                    .HasForeignKey(d => new { d.IdParent, d.StatusParent, d.VersionParent })
                    .HasConstraintName("FK_Locality_Locality").IsRequired(false);
            });
            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => new { e.IdFile, e.Status, e.Version });
                entity.Property(x => x.IdFile).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdFile, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdFile).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

             

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.File1)
                    .WithMany(e=>e.File1Nav)
                    .HasForeignKey(d => new { d.IdParent, d.StatusParent, d.VersionParent })
                    .HasConstraintName("FK_File_File").IsRequired(false);
            });
            modelBuilder.Entity<Holidays>(entity =>
            {
                entity.HasKey(e => new { e.IdHolidays, e.Status, e.Version });
                entity.Property(x => x.IdHolidays).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdHolidays, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdHolidays).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");



                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.IdProduct, e.Status, e.Version });
                entity.Property(x => x.IdProduct).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdProduct, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdProduct).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.HasIndex(e => e.IdBu).IsUnique(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Bu)
                    .WithMany(p=>p.Product)
                    .HasForeignKey(d => new { d.IdBu, d.StatusBu, d.VersionBu })
                    .HasConstraintName("FK_Product_Bu").IsRequired(false);
            });
   
     
            modelBuilder.Entity<WeekInYear>(entity =>
            {
                entity.HasKey(e => new { e.Order, e.Status, e.Version,e.Year });
                entity.HasIndex(e => new { e.Order,e.Active, e.Year, e.Status, e.Version }).IsUnique();
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

     

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");


            });
            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Status, e.Version });
                entity.Property(x => x.Id).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.Id, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.Id).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasIndex(e => e.Email).IsUnique(false);
                entity.HasIndex(e => e.FirstNameOwner).IsUnique(false);
                entity.HasIndex(e => e.LastNameOwner).IsUnique(false);

                entity.HasIndex(e => e.Name).IsUnique(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.Potentiel)
                .WithMany(p => p.Pharmacys)
                    .HasForeignKey(d => new { d.IdPotentiel, d.StatusPotentiel, d.VersionPotentiel })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pharmacy_Potentiel");
                entity.HasMany(d => d.Target)
             .WithOne(p => p.IdPharmacyNavigation)
             .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_Target_Pharmacy");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.HasOne(d => d.Linked)
     .WithMany(p => p.InverseLinked)
     .HasForeignKey(d => new { d.LinkedId, d.StatusLink, d.VersionLink })
     .HasConstraintName("FK_Pharmacy_Pharmacy").IsRequired(false);
                entity.HasOne(d => d.Locality1)
            .WithMany(p=>p.Pharmacy1)
                .HasForeignKey(d => new { d.IdLocality1, d.StatusLocality1, d.VersionLocality1 })
            .HasConstraintName("FK_lPharmacy_locality1").IsRequired(false);
                entity.HasOne(d => d.Locality2).WithMany(p => p.Pharmacy2)
           .HasForeignKey(d => new { d.IdLocality2, d.StatusLocality2, d.VersionLocality2 })
           .HasConstraintName("FK_lPharmacy_locality2").IsRequired(false);
                entity.HasOne(d => d.Brick2)
                                  .WithMany(p => p.Pharmacy1)
                                    .HasForeignKey(d => new { d.IdBrick2, d.StatusBrick2, d.VersionBrick2 })
                                    .HasConstraintName("FK_Pharmacy_Brick").IsRequired(false);
                entity.HasOne(d => d.Brick1)
             .WithMany(p => p.Pharmacy2)
               .HasForeignKey(d => new { d.IdBrick1, d.StatusBrick1, d.VersionBrick1 })
               .HasConstraintName("FK_Pharmacy_Brick2").IsRequired(false);
            });

    
            
       

            modelBuilder.Entity<VisitChannel>(entity =>
            {
                entity.HasKey(e => new { e.IdChannel, e.Status, e.Version });
                entity.Property(x => x.IdChannel).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdChannel, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdChannel).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
             });
            modelBuilder.Entity<Objection>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Status, e.Version });
                entity.Property(x => x.Id).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.Id, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.Id).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");




                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.HasOne(d => d.Pharmacy)
                 .WithMany(p=>p.Objection)
                 .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                 .HasConstraintName("FK_Objection_Pharmacy").IsRequired(false);
                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Objection)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .HasConstraintName("FK_Objection_Doctor").IsRequired(false);
                entity.HasOne(d => d.User)
                  .WithMany(p => p.Objection)
                  .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                  .HasConstraintName("FK_Objection_User").IsRequired(false);
                entity.HasOne(d => d.Product)
             .WithMany(p => p.Objection)
             .HasForeignKey(d => new { d.IdProduct, d.StatusProduct, d.VersionProduct })
             .HasConstraintName("FK_Objection_Product").IsRequired(false);
                entity.HasOne(d => d.VisitReport)
        .WithMany(p => p.Objection)
        .HasForeignKey(d => new { d.IdVisitReport, d.StatusVisitReport, d.VersionVisitReport })
        .HasConstraintName("FK_Objection_VisitReport").IsRequired(false);
            });
          
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => new { e.IdPhone, e.Status, e.Version });
                entity.Property(x => x.IdPhone).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdPhone, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdPhone).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.IdPharmacy).HasColumnName("idPharmacy");



                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .HasConstraintName("FK_Phone_Doctor").IsRequired(false);
                entity.HasOne(d => d.Pharmacy)
                 .WithMany(p => p.Phone)
                 .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                 .HasConstraintName("FK_Phone_Pharmacy").IsRequired(false);
   



            });

            modelBuilder.Entity<RequestRp>(entity =>
            {
                entity.HasKey(e => new { e.IdRequestRp, e.Status, e.Version });
                entity.Property(x => x.IdRequestRp).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdRequestRp, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdRequestRp).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");




                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

 

            });
            modelBuilder.Entity<SellingObjectives>(entity =>
            {
                entity.HasKey(e => new { e.IdSellingObjectives, e.Status, e.Version });
                entity.Property(x => x.IdSellingObjectives).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdSellingObjectives, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdSellingObjectives).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.IdPharmacy).HasColumnName("idPharmacy");



                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Pharmacy)
                .WithMany()
                    .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                    .HasConstraintName("FK_SellingObjectives_Pharmacy").IsRequired(false);
                entity.HasOne(d => d.User)
                .WithMany()
                 .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                 .HasConstraintName("FK_SellingObjectives_User").IsRequired(false);
                entity.HasOne(d => d.Doctor)
                .WithMany()
                .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                .HasConstraintName("FK_SellingObjectives_Doctor").IsRequired(false);
                entity.HasOne(d => d.Product)
              .WithMany()
              .HasForeignKey(d => new { d.IdProduct, d.StatusProduct, d.VersionProduct })
              .HasConstraintName("FK_SellingObjectives_Product").IsRequired(false);


            });
            modelBuilder.Entity<VisitReport>(entity =>
            {
                entity.HasKey(e => new { e.IdReport, e.Status, e.Version });
                entity.Property(x => x.IdReport).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdReport, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdReport).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");



                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.HasOne(d => d.Visit)
                .WithMany()
                    .HasForeignKey(d => new { d.IdVisit, d.StatusVisit, d.VersionVisit })
                    .HasConstraintName("FK_SellingObjectives_Pharmacy").IsRequired(false);
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
            modelBuilder.Entity<PotentielSector>(entity =>
            {
                entity.HasKey(e => new { e.IdSector, e.StatusSector, e.VersionSector, e.IdPotentiel, e.StatusPotentiel, e.VersionPotentiel });

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.PotentielSector)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PotentielSector_Sector");

                entity.HasOne(d => d.IdPotentielNavigation)
                    .WithMany(p => p.PotentielSector)
                    .HasForeignKey(d => new { d.IdPotentiel, d.StatusPotentiel, d.VersionPotentiel })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PotentielSector_Potentiel");
            });
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => new { e.IdSector, e.Status, e.Version });
                entity.Property(x => x.IdSector).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdSector, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdSector).IsUnique(false);
                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

        

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasMany(d => d.Target)
       .WithOne(p => p.IdSectorNavigation)
          .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
         .OnDelete(DeleteBehavior.ClientSetNull)
      .HasConstraintName("FK_Target_Sector");
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

                entity.Property(e => e.tel)
                  .HasMaxLength(50);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
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

                entity.HasMany(d => d.SpecialtyDoctor)
        .WithOne(p => p.Specialty)
        .HasForeignKey(d => new { d.IdSpecialty, d.StatusSpecialty, d.VersionSpecialty })
        .HasConstraintName("FK_Doctor_Potentiel").IsRequired(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });
            modelBuilder.Entity<TagsDoctor>(entity =>
            {
                entity.HasKey(e => new { e.IdDoctor, e.StatusDoctor, e.VersionDoctor, e.IdTags, e.StatusTags, e.VersionTags });

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.TagsDoctor)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagsDoctor_Doctor");

                entity.HasOne(d => d.Tags)
                    .WithMany(p => p.TagsDoctor)
                    .HasForeignKey(d => new { d.IdTags, d.StatusTags, d.VersionTags })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagsDoctor_Specialty");
            });

            modelBuilder.Entity<ProductVisitReport>(entity =>
            {
                entity.HasKey(e => new { e.IdProduct, e.StatusProduct, e.VersionProduct, e.IdReport, e.StatusReport, e.VersionReport });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVisitReport)
                    .HasForeignKey(d => new { d.IdProduct, d.StatusProduct, d.VersionProduct })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitVisitReport_Visit");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ProductSampleVisitReport)
                    .HasForeignKey(d => new { d.IdReport, d.StatusReport, d.VersionReport })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisitVisitReport_Report");
            });


 
            modelBuilder.Entity<TagsRequestRp>(entity =>
            {
                entity.HasKey(e => new { e.IdRequestRp, e.StatusRequestRp, e.VersionRequestRp, e.IdTags, e.StatusTags, e.VersionTags });

                entity.HasOne(d => d.IdRequestRpNavigation)
                    .WithMany(p => p.TagsRequestRp)
                    .HasForeignKey(d => new { d.IdRequestRp, d.StatusRequestRp, d.VersionRequestRp })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagsRequestRp_RequestRp");

                entity.HasOne(d => d.IdTagsNavigation)
                    .WithMany(p => p.TagsRequestRp)
                    .HasForeignKey(d => new { d.IdTags, d.StatusTags, d.VersionTags })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagsRequestRp_Tags");
            });
            modelBuilder.Entity<SharedFiles>(entity =>
            {
                entity.HasKey(e => new { e.IdDoctor, e.StatusDoctor, e.VersionDoctor, e.IdFile, e.StatusFile, e.VersionFile,
                    e.IdUser, e.StatusUser, e.VersionUser });

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.SharedFiles)
                    .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SharedFiles_Doctor");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SharedFiles)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SharedFiles_User");
                entity.HasOne(d => d.File)
                  .WithMany(p => p.SharedFiles)
                  .HasForeignKey(d => new { d.IdFile, d.StatusFile, d.VersionFile })
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharedFiles_File");
            });
            modelBuilder.Entity<FavouriteFiles>(entity =>
            {
                entity.HasKey(e => new {

                    e.IdFile,
                    e.StatusFile,
                    e.VersionFile,
                    e.IdUser,
                    e.StatusUser,
                    e.VersionUser
                });


                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouriteFiles)
                    .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteFiles_User");
                entity.HasOne(d => d.File)
                  .WithMany(p => p.FavouriteFiles)
                  .HasForeignKey(d => new { d.IdFile, d.StatusFile, d.VersionFile })
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_FavouriteFiles_File");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => new { e.IdTags, e.Status, e.Version });
                entity.Property(x => x.IdTags).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdTags, e.Status, e.Version }).IsUnique();
                entity.HasIndex(e => e.IdTags).IsUnique(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
            });

                      modelBuilder.Entity<Visit>(entity =>
                      {
                          entity.HasKey(e => new { e.IdVisit, e.Status, e.Version });
                          entity.Property(x => x.IdVisit).UseIdentityColumn();
                          entity.HasIndex(e => new { e.Active, e.IdVisit, e.Status, e.Version }).IsUnique();
                          entity.HasIndex(e => e.IdVisit).IsUnique(false);
                          entity.Property(e => e.Description)
                              .HasMaxLength(50)
                              .IsUnicode(false);

                          entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                          entity.Property(e => e.UpdatedOn).HasColumnType("timestamp");

                          entity.HasOne(d => d.Locality1)
                                .WithMany(p => p.Visit1)
                                    .HasForeignKey(d => new { d.IdLocality1, d.StatusLocality1, d.VersionLocality1 })
                                    .HasConstraintName("FK_V_locality1").IsRequired(false);
                          entity.HasOne(d => d.Locality2).WithMany(p=>p.Visit2)
                     .HasForeignKey(d => new { d.IdLocality2, d.StatusLocality2, d.VersionLocality2 })
                     .HasConstraintName("FK_V_locality2").IsRequired(false);
                          entity.HasOne(d => d.Brick2)
                             .WithMany(p => p.Visit2)
                               .HasForeignKey(d => new { d.IdBrick2, d.StatusBrick2, d.VersionBrick2 })
                               .HasConstraintName("FK_V_Brick").IsRequired(false);
                          entity.HasOne(d => d.Brick1)
                       .WithMany(p => p.Visit1)
                         .HasForeignKey(d => new { d.IdBrick1, d.StatusBrick1, d.VersionBrick1 })
                         .HasConstraintName("FK_V_Brick2").IsRequired(false);
                          entity.HasOne(d => d.Doctor)
                                   .WithMany(p => p.Visit)
                                   .HasForeignKey(d => new { d.IdDoctor, d.StatusDoctor, d.VersionDoctor })
                                   .HasConstraintName("FK_V_doctor").IsRequired(false);
                          entity.HasOne(d => d.Pharmacy)
                                 .WithMany(p => p.Visit)
                                 .HasForeignKey(d => new { d.IdPharmacy, d.StatusPharmacy, d.VersionPharmacy })
                                 .HasConstraintName("FK_V_Pharmacy").IsRequired(false);
         

                      });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.Status, e.Version });
                entity.Property(x => x.IdUser).UseIdentityColumn();
                entity.HasIndex(e => new { e.Active, e.IdUser, e.Status,e.Version }).IsUnique();
                entity.HasIndex(e => e.IdUser).IsUnique(false);
                entity.Property(e => e.BirthDate).HasColumnType("date");
                entity.HasIndex(e => new { e.RegistrantionNumber,e.IdUser,e.Version }).IsUnique(true);
                entity.HasIndex(e => e.Login).IsUnique(false);
                entity.HasIndex(e => e.Password).IsUnique(false);
                entity.HasIndex(e => e.GeneratedPassword).IsUnique(false);

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");
                entity.Property(e => e.GeneratedPassword)
                         .HasMaxLength(300)
                          .IsRequired(false);
                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Tel1).HasMaxLength(15);
                entity.Property(e => e.Tel2).HasMaxLength(15);

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
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp");

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasMany(d => d.Target)
             .WithOne(p => p.IdUserNavigation)
                .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
               .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Target_User");
                entity.HasMany(d => d.VisitUser)
             .WithOne(p => p.User)
                .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
               .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Target_User");
                entity.HasMany(d => d.VisitRequest)
         .WithOne(p => p.IdUserNavigation)
            .HasForeignKey(d => new { d.IdUser, d.StatusUser, d.VersionUser })
            .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_Target_User");
             entity.HasOne(d => d.DirectManager)
            .WithOne(p => p.Delegates).HasForeignKey<User>(d=> new { d.IdUserDirectManager, d.StatusDirectManager, d.VersionDirectManager })
            .HasConstraintName("FK_Delegate_Manager").IsRequired(false);
                entity.HasOne(d => d.DotlineManager1)
                  .WithOne(p => p.DelegatesDotlineManager1).HasForeignKey<User>(d => new { d.IdUserDotlineManager1, d.StatusDotlineManager1, d.VersionDotlineManager1 })
                  .HasConstraintName("FK_Delegate_DotlineManager1").IsRequired(false);
                entity.HasOne(d => d.DotlineManager2)
            .WithOne(p => p.DelegatesDotlineManager2).HasForeignKey<User>(d => new { d.IdUserDotlineManager2, d.StatusDotlineManager2, d.VersionDotlineManager2 })
            .HasConstraintName("FK_Delegate_DotlineManager2").IsRequired(false);
                entity.HasOne(d => d.Locality1)
                 .WithMany(p => p.User1).HasForeignKey(d => new { d.IdLocality1, d.StatusLocality1, d.VersionLocality1 })
                 .HasConstraintName("FK_Delegate_Locality1").IsRequired(false);
                entity.HasOne(d => d.Locality2)
                 .WithMany(p => p.User2).HasForeignKey(d => new { d.IdLocality2, d.StatusLocality2, d.VersionLocality2 })
                 .HasConstraintName("FK_Delegate_Locality2").IsRequired(false);

                
            });

            

            modelBuilder.Entity<SectorCycle>(entity =>
            {
                entity.HasKey(e => new { e.IdCycle, e.StatusCycle, e.VersionCycle,  e.IdSector,e.StatusSector, e.VersionSector });

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

    
            });

            modelBuilder.Entity<SectorInYear>(entity =>
            {
                entity.HasKey(e => new {e.VersionSector,e.StatusSector, e.IdSector
                    , e.VersionWeekInYear, e.StatusWeekInYear,e.Order,e.Year });

            

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.WeekSectorCycleInYear)
                    .HasForeignKey(d => new { d.IdSector, d.StatusSector, d.VersionSector })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_Sector");


                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.WeekSectorCycleInYearOrderNavigation)
                    .HasPrincipalKey(p => new { p.Order, p.Year,p.Version,p.Status })
                    .HasForeignKey(p=> new { p.Order, p.Year, p.Version, p.Status })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeekSectorCycleInYear_WeekSectorCycleInYear");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
