using System;
using System.Collections.Generic;
using MemberWebAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MemberWebAPI.DbContexts
{
    public partial class EldPlatContext : DbContext
    {
        public EldPlatContext(DbContextOptions<EldPlatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:EldPlatContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserNo)
                    .HasName("PK__ACCOUNT__F3BFAEF5F59BFCAC");

                entity.ToTable("ACCOUNT");

                entity.Property(e => e.UserNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_NO");

                entity.Property(e => e.Account1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("BIRTHDAY");

                entity.Property(e => e.Credate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREDATE");

                entity.Property(e => e.Creid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CREID");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.ErrDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ERR_DATE");

                entity.Property(e => e.LoginErr).HasColumnName("LOGIN_ERR");

                entity.Property(e => e.No)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NO");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Plftyp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PLFTYP");

                entity.Property(e => e.Refreshtoken)
                    .HasMaxLength(640)
                    .IsUnicode(false)
                    .HasColumnName("REFRESHTOKEN")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Refreshtokenexpiration)
                    .HasColumnType("datetime")
                    .HasColumnName("REFRESHTOKENEXPIRATION");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEX");

                entity.Property(e => e.Tel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TEL");

                entity.Property(e => e.Upddate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDDATE");

                entity.Property(e => e.Updid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UPDID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(10)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USER_STATUS");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyNo)
                    .HasName("PK__COMPANY__73BFC83098B2831D");

                entity.ToTable("COMPANY");

                entity.Property(e => e.CompanyNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NO");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.ComEmail)
                    .HasMaxLength(20)
                    .HasColumnName("COM_EMAIL");

                entity.Property(e => e.ComName)
                    .HasMaxLength(20)
                    .HasColumnName("COM_NAME");

                entity.Property(e => e.CompanyTyp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_TYP");

                entity.Property(e => e.Credate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREDATE");

                entity.Property(e => e.Creid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CREID");

                entity.Property(e => e.InvoiceNo).HasColumnName("INVOICE_NO");

                entity.Property(e => e.No)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NO");

                entity.Property(e => e.Owner)
                    .HasMaxLength(10)
                    .HasColumnName("OWNER");

                entity.Property(e => e.PlanTyp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PLAN_TYP");

                entity.Property(e => e.PlfTyp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PLF_TYP");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.SubStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUB_STATUS");

                entity.Property(e => e.SubTime)
                    .HasColumnType("datetime")
                    .HasColumnName("SUB_TIME");

                entity.Property(e => e.Tel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TEL");

                entity.Property(e => e.Upddate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDDATE");

                entity.Property(e => e.Updid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UPDID");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("PK__GROUPS__3214D54845BD81A4");

                entity.ToTable("GROUPS");

                entity.Property(e => e.No).HasColumnName("NO");

                entity.Property(e => e.CompanyNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NO");

                entity.Property(e => e.Credate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREDATE");

                entity.Property(e => e.Creid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CREID");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(10)
                    .HasColumnName("GROUP_NAME");

                entity.Property(e => e.GroupNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("GROUP_NO");

                entity.Property(e => e.GroupStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GROUP_STATUS");

                entity.Property(e => e.Upddate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDDATE");

                entity.Property(e => e.Updid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UPDID");

                entity.Property(e => e.UserNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USER_NO");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.PlansNo)
                    .HasName("PK__PLANS__B5A0175B6B6726DE");

                entity.ToTable("PLANS");

                entity.Property(e => e.PlansNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PLANS_NO");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.CompanyNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NO");

                entity.Property(e => e.Credate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREDATE");

                entity.Property(e => e.Creid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CREID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.No)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NO");

                entity.Property(e => e.PlanTyp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PLAN_TYP");

                entity.Property(e => e.PlansName)
                    .HasMaxLength(20)
                    .HasColumnName("PLANS_NAME");

                entity.Property(e => e.StaDate)
                    .HasColumnType("date")
                    .HasColumnName("STA_DATE");

                entity.Property(e => e.Upddate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDDATE");

                entity.Property(e => e.Updid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UPDID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
