﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Dal.Classes
{
    public partial class SaveBillsContext : DbContext
    {
        public SaveBillsContext()
        {
        }

        public SaveBillsContext(DbContextOptions<SaveBillsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillCategory> BillCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Produts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=SaveBills;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.BillId).HasColumnName("billId");

                entity.Property(e => e.BillTxt)
                    .IsRequired()
                    .HasColumnName("billTxt");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("expiryDate");

                entity.Property(e => e.ImgBiil)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("imgBiil");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issueDate");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(20)
                    .HasColumnName("storeName");

                entity.Property(e => e.TotalSum).HasColumnName("totalSum");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<BillCategory>(entity =>
            {
                entity.ToTable("bill_categories");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BillId).HasColumnName("billId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillCategories)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bill_cate__billI__5DCAEF64");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BillCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bill_cate__categ__5CD6CB2B");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("produts");

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("barcode");

                entity.Property(e => e.BillId).HasColumnName("billId");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.Produts)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__produts__billId__5629CD9C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("password");

                entity.Property(e => e.UserFirstName)
                    .HasMaxLength(20)
                    .HasColumnName("userFirstName");

                entity.Property(e => e.UserLastName)
                    .HasMaxLength(10)
                    .HasColumnName("userLastName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
