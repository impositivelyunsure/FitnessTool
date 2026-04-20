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
            modelBuilder.Entity<FoodGroup>().HasKey(x => x.foodGroupID);
            modelBuilder.Entity<FoodSubgroup>().HasKey(x => x.foodSubgroupID);
            modelBuilder.Entity<FoodClassification>().HasKey(x => x.classificationID);
            modelBuilder.Entity<AfcdFoodEntryRaw>().HasKey(x => x.publicFoodKey);
            modelBuilder.Entity<AusnutFoodEntryRaw>().HasKey(x => x.surveyID);
            modelBuilder.Entity<AusnutFoodMetadataRaw>().HasKey(x => x.surveyID);

            // -----------------------------
            // Basic field configuration
            // -----------------------------
            modelBuilder.Entity<FoodGroup>()
                .Property(x => x.foodGroupID).HasMaxLength(2).IsRequired();

            modelBuilder.Entity<FoodSubgroup>()
                .Property(x => x.foodSubgroupID).HasMaxLength(3).IsRequired();

            modelBuilder.Entity<FoodClassification>()
                .Property(x => x.classificationID).HasMaxLength(5).IsRequired();

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .Property(x => x.publicFoodKey).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .Property(x => x.classificationID).HasMaxLength(5).IsRequired();

            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .Property(x => x.foodName).IsRequired();

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .Property(x => x.publicFoodKey).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<AusnutFoodEntryRaw>()
                .Property(x => x.foodName).IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.publicFoodKey).HasMaxLength(20).IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.foodName).IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.foodAndDietarySupplementClassificationCode).HasMaxLength(5).IsRequired();

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.adgClassificationCode1).HasMaxLength(10);

            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .Property(x => x.adgClassificationCode2).HasMaxLength(10);

            // -----------------------------
            // Hierarchy relationships
            // -----------------------------
            modelBuilder.Entity<FoodSubgroup>()
                .HasOne(x => x.foodGroup) // Navigation property (Object)
                .WithMany()
                .HasForeignKey(x => x.foodGroupID) // Foreign Key (String)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodClassification>()
                .HasOne(x => x.foodSubGroup) // FIXED: Point to the object, NOT the string ID
                .WithMany()
                .HasForeignKey(x => x.foodSubgroupID) // Foreign Key (String)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // AFCD -> shared food classification
            // -----------------------------
            modelBuilder.Entity<AfcdFoodEntryRaw>()
                .HasOne(x => x.foodClassification)
                .WithMany()
                .HasForeignKey(x => x.classificationID)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // AUSNUT metadata -> shared food classification
            // -----------------------------
            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasOne(x => x.foodClassification)
                .WithMany()
                .HasForeignKey(x => x.foodAndDietarySupplementClassificationCode)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // AUSNUT nutrients <-> AUSNUT metadata (1:1 by SurveyId)
            // -----------------------------
            modelBuilder.Entity<AusnutFoodMetadataRaw>()
                .HasOne<AusnutFoodEntryRaw>()
                .WithOne()
                .HasForeignKey<AusnutFoodMetadataRaw>(x => x.surveyID)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // Helpful indexes
            // -----------------------------
            modelBuilder.Entity<AfcdFoodEntryRaw>().HasIndex(x => x.classificationID);
            modelBuilder.Entity<AfcdFoodEntryRaw>().HasIndex(x => x.foodName);
            modelBuilder.Entity<AusnutFoodEntryRaw>().HasIndex(x => x.publicFoodKey);
            modelBuilder.Entity<AusnutFoodEntryRaw>().HasIndex(x => x.foodName);
            modelBuilder.Entity<AusnutFoodMetadataRaw>().HasIndex(x => x.publicFoodKey);
            modelBuilder.Entity<AusnutFoodMetadataRaw>().HasIndex(x => x.foodAndDietarySupplementClassificationCode);
        }
    }
}
