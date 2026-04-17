using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FitnessTool
{
    public class FoodDbContext : DbContext
    {
        public DbSet<AfcdFoodEntryRaw> AfcdFoods => Set<AfcdFoodEntryRaw>();
        public DbSet<AusnutFoodEntryRaw> AusnutFoods => Set<AusnutFoodEntryRaw>();
        public DbSet<AusnutFoodMetadataRaw> AusnutFoodMetadata => Set<AusnutFoodMetadataRaw>();

        public DbSet<FoodGroup> FoodGroups => Set<FoodGroup>();
        public DbSet<FoodSubgroup> FoodSubgroups => Set<FoodSubgroup>();
        public DbSet<FoodClassification> FoodClassifications => Set<FoodClassification>();

        public FoodDbContext(DbContextOptions<FoodDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // Primary keys
            // -----------------------------
            modelBuilder.Entity<FoodGroup>()
                .HasKey(x => x.Code);

            modelBuilder.Entity<FoodSubgroup>()
                .HasKey(x => x.Code);

            modelBuilder.Entity<FoodClassification>()
                .HasKey(x => x.Code);

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .HasKey(x => x.PublicFoodKey);

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .HasKey(x => x.SurveyId);

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasKey(x => x.SurveyId);

            // -----------------------------
            // Basic field configuration
            // -----------------------------
            modelBuilder.Entity<FoodGroup>()
                .Property(x => x.Code)
                .HasMaxLength(2)
                .IsRequired();

            modelBuilder.Entity<FoodGroup>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<FoodSubgroup>()
                .Property(x => x.Code)
                .HasMaxLength(3)
                .IsRequired();

            modelBuilder.Entity<FoodSubgroup>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<FoodSubgroup>()
                .Property(x => x.FoodGroupCode)
                .HasMaxLength(2)
                .IsRequired();

            modelBuilder.Entity<FoodClassification>()
                .Property(x => x.Code)
                .HasMaxLength(5)
                .IsRequired();

            modelBuilder.Entity<FoodClassification>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<FoodClassification>()
                .Property(x => x.FoodSubgroupCode)
                .HasMaxLength(3)
                .IsRequired();

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .Property(x => x.PublicFoodKey)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .Property(x => x.ClassificationCode)
                .HasMaxLength(5)
                .IsRequired();

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .Property(x => x.FoodName)
                .IsRequired();

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .Property(x => x.PublicFoodKey)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .Property(x => x.FoodName)
                .IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.PublicFoodKey)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.FoodName)
                .IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.FoodAndDietarySupplementClassificationCode)
                .HasMaxLength(5)
                .IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.AdgClassificationCode1)
                .HasMaxLength(10);

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.AdgClassificationCode2)
                .HasMaxLength(10);

            // -----------------------------
            // Hierarchy relationships
            // -----------------------------
            modelBuilder.Entity<FoodSubgroup>()
                .HasOne(x => x.FoodGroup)
                .WithMany()
                .HasForeignKey(x => x.FoodGroupCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodClassification>()
                .HasOne(x => x.FoodSubgroup)
                .WithMany()
                .HasForeignKey(x => x.FoodSubgroupCode)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // AFCD -> shared food classification
            // -----------------------------
            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .HasOne(x => x.FoodClassification)
                .WithMany()
                .HasForeignKey(x => x.ClassificationCode)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // AUSNUT metadata -> shared food classification
            // -----------------------------
            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasOne(x => x.FoodClassification)
                .WithMany()
                .HasForeignKey(x => x.FoodAndDietarySupplementClassificationCode)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // AUSNUT nutrients <-> AUSNUT metadata (1:1 by SurveyId)
            // -----------------------------
            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasOne<AusnutFoodEntryRaw>()
                .WithOne()
                .HasForeignKey<AusnutFoodMetadataRaw>(x => x.SurveyId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // Helpful indexes
            // -----------------------------
            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .HasIndex(x => x.ClassificationCode);

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .HasIndex(x => x.FoodName);

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .HasIndex(x => x.PublicFoodKey);

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .HasIndex(x => x.FoodName);

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasIndex(x => x.PublicFoodKey);

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasIndex(x => x.FoodAndDietarySupplementClassificationCode);
        }
    }
}
