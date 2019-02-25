using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krusing_down_the_aisle_backend.Models
{
   public partial class DataContext : DbContext
   {
      public virtual DbSet<Food> Food { get; set; }
      public virtual DbSet<Person> Person { get; set; }
      public virtual DbSet<PlusOne> PlusOne { get; set; }

      public DataContext(DbContextOptions<DataContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Food>(entity =>
         {
            entity.ToTable("food");

            entity.HasIndex(e => e.Id)
               .HasName("id")
               .IsUnique();

            entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int(11)")
               .IsRequired();

            entity.Property(e => e.Name)
               .HasColumnName("name")
               .HasColumnType("varchar(100)")
               .IsRequired();

            entity.Property(e => e.Desc)
               .HasColumnName("desc")
               .HasColumnType("varchar(5000)")
               .IsRequired();
         });

         modelBuilder.Entity<Person>(entity =>
         {
            entity.ToTable("person");

            entity.HasIndex(e => e.Id)
               .HasName("id")
               .IsUnique();

            entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int(11)")
               .IsRequired();

            entity.Property(e => e.FirstName)
               .HasColumnName("first_name")
               .HasColumnType("varchar(50)")
               .IsRequired();

            entity.Property(e => e.LastName)
               .HasColumnName("last_name")
               .HasColumnType("varchar(50)")
               .IsRequired();

            entity.Property(e => e.FoodId)
               .HasColumnName("food_id")
               .HasColumnType("int(11)")
               .IsRequired();

            entity.Property(e => e.PlusOneId)
               .HasColumnName("plus_one_id")
               .HasColumnType("int(11)");

            entity.Property(e => e.HasAllergy)
               .HasColumnName("has_allergy")
               .HasColumnType("tinyint(1)")
               .IsRequired();

            entity.Property(e => e.Allergy)
               .HasColumnName("allergy")
               .HasColumnType("varchar(45)");
         });

         modelBuilder.Entity<PlusOne>(entity =>
         {
            entity.ToTable("plus_one");

            entity.HasIndex(e => e.Id)
               .HasName("id")
               .IsUnique();

            entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int(11)")
               .IsRequired();

            entity.Property(e => e.FirstName)
               .HasColumnName("first_name")
               .HasColumnType("varchar(50)")
               .IsRequired();

            entity.Property(e => e.LastName)
               .HasColumnName("last_name")
               .HasColumnType("varchar(50)")
               .IsRequired();

            entity.Property(e => e.FoodId)
               .HasColumnName("food_id")
               .HasColumnType("int(11)")
               .IsRequired();
         });

      }
   }
}
