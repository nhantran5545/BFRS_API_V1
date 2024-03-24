using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class BFRS_dbContext : DbContext
    {
        public BFRS_dbContext()
        {
        }

        public BFRS_dbContext(DbContextOptions<BFRS_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<Bird> Birds { get; set; } = null!;
        public virtual DbSet<BirdMutation> BirdMutations { get; set; } = null!;
        public virtual DbSet<BirdSpecy> BirdSpecies { get; set; } = null!;
        public virtual DbSet<BirdType> BirdTypes { get; set; } = null!;
        public virtual DbSet<Breeding> Breedings { get; set; } = null!;
        public virtual DbSet<BreedingCheckList> BreedingCheckLists { get; set; } = null!;
        public virtual DbSet<BreedingCheckListDetail> BreedingCheckListDetails { get; set; } = null!;
        public virtual DbSet<BreedingNorm> BreedingNorms { get; set; } = null!;
        public virtual DbSet<BreedingReason> BreedingReasons { get; set; } = null!;
        public virtual DbSet<Cage> Cages { get; set; } = null!;
        public virtual DbSet<CheckList> CheckLists { get; set; } = null!;
        public virtual DbSet<CheckListDetail> CheckListDetails { get; set; } = null!;
        public virtual DbSet<Clutch> Clutches { get; set; } = null!;
        public virtual DbSet<ClutchReason> ClutchReasons { get; set; } = null!;
        public virtual DbSet<Egg> Eggs { get; set; } = null!;
        public virtual DbSet<EggBird> EggBirds { get; set; } = null!;
        public virtual DbSet<EggReason> EggReasons { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Issue> Issues { get; set; } = null!;
        public virtual DbSet<IssueType> IssueTypes { get; set; } = null!;
        public virtual DbSet<Mutation> Mutations { get; set; } = null!;
        public virtual DbSet<SpeciesMutation> SpeciesMutations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(255);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.AreaName).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK_FarmArea");
            });

            modelBuilder.Entity<Bird>(entity =>
            {
                entity.ToTable("Bird");

                entity.Property(e => e.AcquisitionDate).HasColumnType("date");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.HatchedDate).HasColumnType("date");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LifeStage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseFrom).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.BirdSpecies)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.BirdSpeciesId)
                    .HasConstraintName("FK_BirdSpeciesBird");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.CageId)
                    .HasConstraintName("FK_CageBird");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.FarmId)
                    .HasConstraintName("FK_FarmBird");

                entity.HasOne(d => d.FatherBird)
                    .WithMany(p => p.InverseFatherBird)
                    .HasForeignKey(d => d.FatherBirdId)
                    .HasConstraintName("FK_FatherBird");

                entity.HasOne(d => d.MotherBird)
                    .WithMany(p => p.InverseMotherBird)
                    .HasForeignKey(d => d.MotherBirdId)
                    .HasConstraintName("FK_MotherBird");
            });

            modelBuilder.Entity<BirdMutation>(entity =>
            {
                entity.HasKey(e => new { e.BirdId, e.MutationId });

                entity.ToTable("BirdMutation");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Bird)
                    .WithMany(p => p.BirdMutations)
                    .HasForeignKey(d => d.BirdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BirdBirdMutation");

                entity.HasOne(d => d.Mutation)
                    .WithMany(p => p.BirdMutations)
                    .HasForeignKey(d => d.MutationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MutationBirdMutation");
            });

            modelBuilder.Entity<BirdSpecy>(entity =>
            {
                entity.HasKey(e => e.BirdSpeciesId)
                    .HasName("PK__BirdSpec__D9DA595F439456F6");

                entity.Property(e => e.BirdSpeciesName).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.BirdType)
                    .WithMany(p => p.BirdSpecies)
                    .HasForeignKey(d => d.BirdTypeId)
                    .HasConstraintName("FK_BirdTypeBirdSpecies");
            });

            modelBuilder.Entity<BirdType>(entity =>
            {
                entity.ToTable("BirdType");

                entity.Property(e => e.BirdTypeName).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Breeding>(entity =>
            {
                entity.ToTable("Breeding");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.NextCheck).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Breedings)
                    .HasForeignKey(d => d.CageId)
                    .HasConstraintName("FK_CageBreeding");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BreedingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByBreeding");

                entity.HasOne(d => d.FatherBird)
                    .WithMany(p => p.BreedingFatherBirds)
                    .HasForeignKey(d => d.FatherBirdId)
                    .HasConstraintName("FK_FatherBirdBreeding");

                entity.HasOne(d => d.MotherBird)
                    .WithMany(p => p.BreedingMotherBirds)
                    .HasForeignKey(d => d.MotherBirdId)
                    .HasConstraintName("FK_MotherBirdBreeding");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.BreedingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UpdatedByBreeding");
            });

            modelBuilder.Entity<BreedingCheckList>(entity =>
            {
                entity.ToTable("BreedingCheckList");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Breeding)
                    .WithMany(p => p.BreedingCheckLists)
                    .HasForeignKey(d => d.BreedingId)
                    .HasConstraintName("FK_BreedingBreedingCheckList");

                entity.HasOne(d => d.CheckList)
                    .WithMany(p => p.BreedingCheckLists)
                    .HasForeignKey(d => d.CheckListId)
                    .HasConstraintName("FK_CheckListBreedingCheckList");

                entity.HasOne(d => d.Clutch)
                    .WithMany(p => p.BreedingCheckLists)
                    .HasForeignKey(d => d.ClutchId)
                    .HasConstraintName("FK_ClutchBreedingCheckList");

                entity.HasOne(d => d.Egg)
                    .WithMany(p => p.BreedingCheckLists)
                    .HasForeignKey(d => d.EggId)
                    .HasConstraintName("FK_EggBreedingCheckList");
            });

            modelBuilder.Entity<BreedingCheckListDetail>(entity =>
            {
                entity.ToTable("BreedingCheckListDetail");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.BreedingCheckList)
                    .WithMany(p => p.BreedingCheckListDetails)
                    .HasForeignKey(d => d.BreedingCheckListId)
                    .HasConstraintName("FK_BreedingCheckListBreedingCheckListDetail");

                entity.HasOne(d => d.CheckListDetail)
                    .WithMany(p => p.BreedingCheckListDetails)
                    .HasForeignKey(d => d.CheckListDetailId)
                    .HasConstraintName("FK_CheckListDetailBreedingCheckListDetail");
            });

            modelBuilder.Entity<BreedingNorm>(entity =>
            {
                entity.ToTable("BreedingNorm");

                entity.Property(e => e.BreedingEndMonth).HasColumnType("date");

                entity.Property(e => e.BreedingStartMonth).HasColumnType("date");

                entity.Property(e => e.FoodRecommendation).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.WeatherFeatures).HasMaxLength(255);

                entity.HasOne(d => d.BirdSpecies)
                    .WithMany(p => p.BreedingNorms)
                    .HasForeignKey(d => d.BirdSpeciesId)
                    .HasConstraintName("FK_BirdSpeciesBreedingNorm");
            });

            modelBuilder.Entity<BreedingReason>(entity =>
            {
                entity.ToTable("BreedingReason");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Breeding)
                    .WithMany(p => p.BreedingReasons)
                    .HasForeignKey(d => d.BreedingId)
                    .HasConstraintName("FK_BreedingBreedingReason");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BreedingReasons)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByBreedingReason");
            });

            modelBuilder.Entity<Cage>(entity =>
            {
                entity.ToTable("Cage");

                entity.Property(e => e.ManufacturedAt).HasMaxLength(255);

                entity.Property(e => e.ManufacturedDate).HasColumnType("date");

                entity.Property(e => e.PurchasedDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_AccountCage");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_AreaCage");
            });

            modelBuilder.Entity<CheckList>(entity =>
            {
                entity.ToTable("CheckList");

                entity.Property(e => e.CheckListName).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.CheckLists)
                    .HasForeignKey(d => d.SpeciesId)
                    .HasConstraintName("FK_SpeciesCheckList");
            });

            modelBuilder.Entity<CheckListDetail>(entity =>
            {
                entity.ToTable("CheckListDetail");

                entity.Property(e => e.QuestionName).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.CheckList)
                    .WithMany(p => p.CheckListDetails)
                    .HasForeignKey(d => d.CheckListId)
                    .HasConstraintName("FK_CheckListDetail");
            });

            modelBuilder.Entity<Clutch>(entity =>
            {
                entity.ToTable("Clutch");

                entity.Property(e => e.BroodEndDate).HasColumnType("date");

                entity.Property(e => e.BroodStartDate).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Breeding)
                    .WithMany(p => p.Clutches)
                    .HasForeignKey(d => d.BreedingId)
                    .HasConstraintName("FK_BreedingClutch");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Clutches)
                    .HasForeignKey(d => d.CageId)
                    .HasConstraintName("FK_CageClutch");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClutchCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByClutch");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ClutchUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UpdatedByClutch");
            });

            modelBuilder.Entity<ClutchReason>(entity =>
            {
                entity.ToTable("ClutchReason");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Clutch)
                    .WithMany(p => p.ClutchReasons)
                    .HasForeignKey(d => d.ClutchId)
                    .HasConstraintName("FK_ClutchClutchReason");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClutchReasons)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByClutchReason");
            });

            modelBuilder.Entity<Egg>(entity =>
            {
                entity.ToTable("Egg");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.HatchedDate).HasColumnType("date");

                entity.Property(e => e.LayDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Clutch)
                    .WithMany(p => p.Eggs)
                    .HasForeignKey(d => d.ClutchId)
                    .HasConstraintName("FK_ClutchEgg");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EggCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByEgg");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.EggUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UpdatedByEgg");
            });

            modelBuilder.Entity<EggBird>(entity =>
            {
                entity.HasKey(e => new { e.EggId, e.BirdId });

                entity.ToTable("EggBird");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Bird)
                    .WithMany(p => p.EggBirds)
                    .HasForeignKey(d => d.BirdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BirdEggBird");

                entity.HasOne(d => d.Egg)
                    .WithMany(p => p.EggBirds)
                    .HasForeignKey(d => d.EggId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EggEggBird");
            });

            modelBuilder.Entity<EggReason>(entity =>
            {
                entity.ToTable("EggReason");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EggReasons)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByEggReason");

                entity.HasOne(d => d.Egg)
                    .WithMany(p => p.EggReasons)
                    .HasForeignKey(d => d.EggId)
                    .HasConstraintName("FK_EggEggReason");
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FarmName).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("Issue");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.IssueName).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.IssueAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("FK_AssignedToIssue");

                entity.HasOne(d => d.Breeding)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.BreedingId)
                    .HasConstraintName("FK_BreedingIssue");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.IssueCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_CreatedByIssue");

                entity.HasOne(d => d.IssueType)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IssueTypeId)
                    .HasConstraintName("FK_TypeIssue");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.IssueUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UpdatedByIssue");
            });

            modelBuilder.Entity<IssueType>(entity =>
            {
                entity.ToTable("IssueType");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.IssueName).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Mutation>(entity =>
            {
                entity.ToTable("Mutation");

                entity.Property(e => e.MutationName).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<SpeciesMutation>(entity =>
            {
                entity.HasKey(e => new { e.BirdSpeciesId, e.MutationId });

                entity.ToTable("SpeciesMutation");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.BirdSpecies)
                    .WithMany(p => p.SpeciesMutations)
                    .HasForeignKey(d => d.BirdSpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BirdSpeciesSM");

                entity.HasOne(d => d.Mutation)
                    .WithMany(p => p.SpeciesMutations)
                    .HasForeignKey(d => d.MutationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MutationSM");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
