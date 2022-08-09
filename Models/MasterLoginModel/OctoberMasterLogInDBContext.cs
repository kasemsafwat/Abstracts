using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models.MasterLoginModel
{
    //public partial class ParadiseMasterLogInDBContext : DbContext
    //{
    //    public ParadiseMasterLogInDBContext()
    //    {
    //    }

    //    public ParadiseMasterLogInDBContext(DbContextOptions<ParadiseMasterLogInDBContext> options)
    //        : base(options)
    //    {
    //    }

    //    public virtual DbSet<Actions> Actions { get; set; }
    //    public virtual DbSet<Departments> Departments { get; set; }
    //    public virtual DbSet<Employees> Employees { get; set; }
    //    public virtual DbSet<RoleActions> RoleActions { get; set; }
    //    public virtual DbSet<Roles> Roles { get; set; }
    //    public virtual DbSet<Systems> Systems { get; set; }
    //    public virtual DbSet<Users> Users { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        if (!optionsBuilder.IsConfigured)
    //        {
    //            optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionStringForLogin"].ConnectionString.ToString());
    //        }
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Actions>(entity =>
    //        {
    //            entity.HasKey(e => e.ActionId);

    //            entity.Property(e => e.ActionId).HasColumnName("ActionID");

    //            entity.Property(e => e.Action).HasMaxLength(50);

    //            entity.Property(e => e.ActionDescription).HasMaxLength(250);

    //            entity.Property(e => e.Controller).HasMaxLength(50);

    //            entity.Property(e => e.ParentActionId).HasColumnName("ParentActionID");

    //            entity.Property(e => e.SystemId).HasColumnName("SystemID");

    //            entity.Property(e => e.Url)
    //                .HasColumnName("URL")
    //                .HasMaxLength(250);

    //            entity.HasOne(d => d.System)
    //                .WithMany(p => p.Actions)
    //                .HasForeignKey(d => d.SystemId)
    //                .HasConstraintName("FK_Actions_Systems");
    //        });

    //        modelBuilder.Entity<Departments>(entity =>
    //        {
    //            entity.HasKey(e => e.DepartmentId);

    //            entity.ToTable("Departments", "HR");

    //            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

    //            entity.Property(e => e.DepartmentName).HasMaxLength(250);
    //        });

    //        modelBuilder.Entity<Employees>(entity =>
    //        {
    //            entity.HasKey(e => e.EmployeeId);

    //            entity.ToTable("Employees", "HR");

    //            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

    //            entity.Property(e => e.BirthDate).HasColumnType("datetime");

    //            entity.Property(e => e.DateAdd).HasColumnType("datetime");

    //            entity.Property(e => e.DateLock).HasColumnType("datetime");

    //            entity.Property(e => e.DateUpdate).HasColumnType("datetime");

    //            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

    //            entity.Property(e => e.DisableDate).HasColumnType("datetime");

    //            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

    //            entity.Property(e => e.EmployeeAddress).HasMaxLength(250);

    //            entity.Property(e => e.EmployeeCode).HasMaxLength(50);

    //            entity.Property(e => e.EmployeeName).HasMaxLength(250);

    //            entity.Property(e => e.EmployeeStatus).HasMaxLength(50);

    //            entity.Property(e => e.EmployeeTitle).HasMaxLength(50);

    //            entity.Property(e => e.Gender).HasMaxLength(50);

    //            entity.Property(e => e.IsLocked).HasDefaultValueSql("((0))");

    //            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

    //            entity.Property(e => e.NationalIdno)
    //                .HasColumnName("NationalIDNo")
    //                .HasMaxLength(50);

    //            entity.Property(e => e.UserId).HasColumnName("UserID");

    //            entity.Property(e => e.UserIdadd).HasColumnName("UserIDAdd");

    //            entity.Property(e => e.UserIdlock).HasColumnName("UserIDLock");

    //            entity.Property(e => e.UserIdupdate).HasColumnName("UserIDUpdate");

    //            entity.HasOne(d => d.Department)
    //                .WithMany(p => p.Employees)
    //                .HasForeignKey(d => d.DepartmentId)
    //                .HasConstraintName("FK_Employees_Departments");
    //        });

    //        modelBuilder.Entity<RoleActions>(entity =>
    //        {
    //            entity.HasKey(e => e.RoleActionId);

    //            entity.Property(e => e.RoleActionId).HasColumnName("RoleActionID");

    //            entity.Property(e => e.ActionId).HasColumnName("ActionID");

    //            entity.Property(e => e.RoleId).HasColumnName("RoleID");

    //            entity.HasOne(d => d.Action)
    //                .WithMany(p => p.RoleActions)
    //                .HasForeignKey(d => d.ActionId)
    //                .HasConstraintName("FK_RoleActions_Actions");

    //            entity.HasOne(d => d.Role)
    //                .WithMany(p => p.RoleActions)
    //                .HasForeignKey(d => d.RoleId)
    //                .HasConstraintName("FK_RoleActions_Roles");
    //        });

    //        modelBuilder.Entity<Roles>(entity =>
    //        {
    //            entity.HasKey(e => e.RoleId);

    //            entity.Property(e => e.RoleId).HasColumnName("RoleID");

    //            entity.Property(e => e.RoleDescription).HasMaxLength(250);

    //            entity.Property(e => e.RoleName).HasMaxLength(50);
    //        });

    //        modelBuilder.Entity<Systems>(entity =>
    //        {
    //            entity.HasKey(e => e.SystemId);

    //            entity.Property(e => e.SystemId).HasColumnName("SystemID");

    //            entity.Property(e => e.SystemCode).HasMaxLength(50);

    //            entity.Property(e => e.SystemDescription).HasMaxLength(250);

    //            entity.Property(e => e.SystemName).HasMaxLength(50);

    //            entity.Property(e => e.SystemVersion).HasMaxLength(50);
    //        });

    //        modelBuilder.Entity<Users>(entity =>
    //        {
    //            entity.HasKey(e => e.UserId);

    //            entity.Property(e => e.UserId).HasColumnName("UserID");

    //            entity.Property(e => e.AutoNo).HasDefaultValueSql("((1))");

    //            entity.Property(e => e.DateLock).HasColumnType("datetime");

    //            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

    //            entity.Property(e => e.Email).HasMaxLength(50);

    //            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

    //            entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

    //            entity.Property(e => e.IsBackOffice).HasDefaultValueSql("((0))");

    //            entity.Property(e => e.IsExecutiveEngineer).HasDefaultValueSql("((0))");

    //            entity.Property(e => e.IsFrontOffice).HasDefaultValueSql("((0))");

    //            entity.Property(e => e.IsLocked).HasDefaultValueSql("((0))");

    //            entity.Property(e => e.RoleId).HasColumnName("RoleID");

    //            entity.Property(e => e.UserCode).HasMaxLength(50);

    //            entity.Property(e => e.UserFullName).HasMaxLength(250);

    //            entity.Property(e => e.UserIdlock).HasColumnName("UserIDLock");

    //            entity.Property(e => e.UserName).HasMaxLength(50);

    //            entity.Property(e => e.UserPass).HasMaxLength(50);

    //            entity.Property(e => e.UserStatus).HasMaxLength(50);

    //            entity.HasOne(d => d.Role)
    //                .WithMany(p => p.Users)
    //                .HasForeignKey(d => d.RoleId)
    //                .HasConstraintName("FK_Users_Roles");
    //        });

    //        OnModelCreatingPartial(modelBuilder);
    //    }

    //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    //}
}
