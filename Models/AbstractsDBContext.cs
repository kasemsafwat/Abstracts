using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Abstracts.Models
{
    public partial class AbstractsDBContext : DbContext
    {
        public AbstractsDBContext()
        {
        }

        public AbstractsDBContext(DbContextOptions<AbstractsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<CompanyUsers> CompanyUsers { get; set; }
        public virtual DbSet<ConsultationUser> ConsultationUser { get; set; }
        public virtual DbSet<ConsultationWorkOrder> ConsultationWorkOrder { get; set; }
        public virtual DbSet<ItemClass> ItemClass { get; set; }
        public virtual DbSet<RequestDetailClass> RequestDetailClass { get; set; }
        public virtual DbSet<RequestDetails> RequestDetails { get; set; }
        public virtual DbSet<RequestLog> RequestLog { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<UserRequests> UserRequests { get; set; }
        public virtual DbSet<VwRequests> VwRequests { get; set; }
        public virtual DbSet<VwWorkOrders> VwWorkOrders { get; set; }
        public virtual DbSet<WorkOrderDetails> WorkOrderDetails { get; set; }
        public virtual DbSet<WorkOrderNotes> WorkOrderNotes { get; set; }
        public virtual DbSet<WorkOrders> WorkOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ItemClass>(entity =>
            {
                entity.Property(e => e.ItemClassId).HasColumnName("ItemClassID");

                entity.Property(e => e.DateLock).HasColumnType("datetime");

                entity.Property(e => e.IsLocked).HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemClassName).HasMaxLength(500);

                entity.Property(e => e.UserIdlock).HasColumnName("UserIDLock");
            });

            modelBuilder.Entity<RequestDetailClass>(entity =>
            {
                entity.HasKey(e => e.RequestDetailsClassId);

                entity.Property(e => e.RequestDetailsClassId).HasColumnName("RequestDetailsClassID");

                entity.Property(e => e.ItemClassId).HasColumnName("ItemClassID");

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");
            });

            modelBuilder.Entity<RequestDetails>(entity =>
            {
                entity.HasKey(e => e.RequestDetailId);

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.AutoNo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateAdd).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.RequestDetailCode).HasMaxLength(500);

                entity.Property(e => e.RequestDetailSerial).HasMaxLength(500);

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserIdadd).HasColumnName("UserIDAdd");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_RequestDetails_Requests");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_RequestDetails_Units");
            });

            modelBuilder.Entity<RequestLog>(entity =>
            {
                entity.Property(e => e.RequestLogId).HasColumnName("RequestLogID");

                entity.Property(e => e.DateAdd)
                    .HasColumnName("Date_Add")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateUpdate)
                    .HasColumnName("Date_Update")
                    .HasColumnType("datetime");

                entity.Property(e => e.LogFileName).HasMaxLength(250);

                entity.Property(e => e.NewUnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserIdAdd).HasColumnName("UserID_Add");

                entity.Property(e => e.UserUpdate).HasColumnName("User_Update");
            });

            modelBuilder.Entity<Requests>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.AutoNo).HasDefaultValueSql("((1))");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DateAdd).HasColumnType("datetime");

                entity.Property(e => e.DateLock).HasColumnType("datetime");

                entity.Property(e => e.DatePrint).HasColumnType("datetime");

                entity.Property(e => e.IsLocked).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPrinted).HasDefaultValueSql("((0))");

                entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestCode).HasMaxLength(500);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDetailsFileName).HasMaxLength(500);

                entity.Property(e => e.RequestEndDate).HasColumnType("datetime");

                entity.Property(e => e.RequestFileName).HasMaxLength(500);

                entity.Property(e => e.RequestName).HasMaxLength(500);

                entity.Property(e => e.RequestNo).HasMaxLength(500);

                entity.Property(e => e.RequestStartDate).HasColumnType("datetime");

                entity.Property(e => e.UserIdadd).HasColumnName("UserIDAdd");

                entity.Property(e => e.UserIdlock).HasColumnName("UserIDLock");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Requests_Companies");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Engineer).HasMaxLength(100);

                entity.Property(e => e.GeneralDirector).HasMaxLength(100);

                entity.Property(e => e.GeneralManager).HasMaxLength(100);

                entity.Property(e => e.President).HasMaxLength(100);

                entity.Property(e => e.Technical).HasMaxLength(100);

                entity.Property(e => e.VicePresident).HasMaxLength(100);
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.HasIndex(e => e.UnitName)
                    .HasName("UnitName")
                    .IsUnique();

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.DateLock).HasColumnType("datetime");

                entity.Property(e => e.IsLocked).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitName).HasMaxLength(250);

                entity.Property(e => e.UserIdlock).HasColumnName("UserIDlock");
            });

            modelBuilder.Entity<UserRequests>(entity =>
            {
                entity.HasKey(e => e.UserRequestId);

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<VwRequests>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Requests");

                entity.Property(e => e.CompanyCode).HasMaxLength(500);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.InsuranceNo).HasMaxLength(500);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestCode).HasMaxLength(500);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDetailCode).HasMaxLength(500);

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.RequestDetailSerial).HasMaxLength(500);

                entity.Property(e => e.RequestEndDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.RequestName).HasMaxLength(500);

                entity.Property(e => e.RequestNo).HasMaxLength(500);

                entity.Property(e => e.RequestStartDate).HasColumnType("datetime");

                entity.Property(e => e.TaxRecordNo).HasMaxLength(500);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitName).HasMaxLength(250);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<VwWorkOrders>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_WorkOrders");

                entity.Property(e => e.CompanyCode).HasMaxLength(500);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.ConsultationName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DatePrint).HasColumnType("datetime");

                entity.Property(e => e.DateSubmit).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Engineer).HasMaxLength(100);

                entity.Property(e => e.GeneralDirector).HasMaxLength(100);

                entity.Property(e => e.GeneralManager).HasMaxLength(100);

                entity.Property(e => e.InsuranceNo).HasMaxLength(500);

                entity.Property(e => e.InventoryBookNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InventoryBookPage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewDate).HasColumnType("date");

                entity.Property(e => e.NewQuantity).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.NoteStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoteTitle)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Notes).IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.President).HasMaxLength(100);

                entity.Property(e => e.RequestAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestCode).HasMaxLength(500);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDetailCode).HasMaxLength(500);

                entity.Property(e => e.RequestDetailSerial).HasMaxLength(500);

                entity.Property(e => e.RequestEndDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.RequestName).HasMaxLength(500);

                entity.Property(e => e.RequestNo).HasMaxLength(500);

                entity.Property(e => e.RequestNote)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.RequestStartDate).HasColumnType("datetime");

                entity.Property(e => e.TaxRecordNo).HasMaxLength(500);

                entity.Property(e => e.Technical).HasMaxLength(100);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitName).HasMaxLength(250);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.VicePresident).HasMaxLength(100);

                entity.Property(e => e.WorkOrderCode).HasMaxLength(50);

                entity.Property(e => e.WorkOrderEndDate).HasColumnType("datetime");

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.WorkOrderNoteId).HasColumnName("WorkOrderNoteID");

                entity.Property(e => e.WorkOrderStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WorkOrderDetails>(entity =>
            {
                entity.HasKey(e => e.WorkOrderDetailId)
                    .HasName("PK_WorkOrderRequestDetailsID");

                entity.Property(e => e.WorkOrderDetailId).HasColumnName("WorkOrderDetailID");

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.Engineer).HasMaxLength(100);

                entity.Property(e => e.GeneralDirector).HasMaxLength(100);

                entity.Property(e => e.GeneralManager).HasMaxLength(100);

                entity.Property(e => e.InventoryBookNo).HasMaxLength(50);

                entity.Property(e => e.InventoryBookPage).HasMaxLength(50);

                entity.Property(e => e.IsApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes).HasMaxLength(250);

                entity.Property(e => e.President).HasMaxLength(100);

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.RequestNoteId).HasColumnName("RequestNoteID");

                entity.Property(e => e.Technical).HasMaxLength(100);

                entity.Property(e => e.VicePresident).HasMaxLength(100);

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.HasOne(d => d.RequestDetail)
                    .WithMany(p => p.WorkOrderDetails)
                    .HasForeignKey(d => d.RequestDetailId)
                    .HasConstraintName("FK_WorkOrderDetails_RequestDetails");

                entity.HasOne(d => d.RequestNote)
                    .WithMany(p => p.WorkOrderDetails)
                    .HasForeignKey(d => d.RequestNoteId)
                    .HasConstraintName("FK_WorkOrderDetails_WorkOrderNotes");

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.WorkOrderDetails)
                    .HasForeignKey(d => d.WorkOrderId)
                    .HasConstraintName("FK_WorkOrderDetails_WorkOrders");
            });

            modelBuilder.Entity<WorkOrderNotes>(entity =>
            {
                entity.HasKey(e => e.WorkOrderNoteId);

                entity.Property(e => e.WorkOrderNoteId).HasColumnName("WorkOrderNoteID");

                entity.Property(e => e.NoteStatus).HasMaxLength(50);

                entity.Property(e => e.NoteTitle).HasMaxLength(250);

                entity.Property(e => e.RequestId).HasColumnName("RequestID");
            });

            modelBuilder.Entity<WorkOrders>(entity =>
            {
                entity.HasKey(e => e.WorkOrderId);

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.AutoNo).HasDefaultValueSql("((1))");

                entity.Property(e => e.CompanyUserIdadd).HasColumnName("CompanyUserIDAdd");

                entity.Property(e => e.ConsultationDateSubmit).HasColumnType("datetime");

                entity.Property(e => e.ConsultationFileNameApproved).HasMaxLength(100);

                entity.Property(e => e.ConsultationUserId).HasColumnName("ConsultationUserID");

                entity.Property(e => e.DateAdd).HasColumnType("datetime");

                entity.Property(e => e.DateAddedCompanyUserId)
                    .HasColumnName("DateAddedCompanyUserID")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatePrint).HasColumnType("datetime");

                entity.Property(e => e.DateSubmit).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.FileNameApproved).HasMaxLength(100);

                entity.Property(e => e.IsPrinted).HasDefaultValueSql("((0))");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.TechnicalDateSubmit).HasColumnType("datetime");

                entity.Property(e => e.TechnicalUserId).HasColumnName("TechnicalUserID");

                entity.Property(e => e.UserIdadd).HasColumnName("UserIDAdd");

                entity.Property(e => e.UserIdsubmit).HasColumnName("UserIDSubmit");

                entity.Property(e => e.WorkOrderCode).HasMaxLength(50);

                entity.Property(e => e.WorkOrderEndDate).HasColumnType("datetime");

                entity.Property(e => e.WorkOrderStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_WorkOrders_Requests");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
