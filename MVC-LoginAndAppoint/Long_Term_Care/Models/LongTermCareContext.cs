using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Long_Term_Care.Models;

public partial class LongTermCareContext : DbContext
{
    public LongTermCareContext()
    {
    }

    public LongTermCareContext(DbContextOptions<LongTermCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppointmentForm> AppointmentForms { get; set; }

    public virtual DbSet<CareRecord> CareRecords { get; set; }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<ServicesItem> ServicesItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=LongTermCare;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentForm>(entity =>
        {
            entity.HasKey(e => e.ReserveId).HasName("PK__Appointm__C8C00680A5DE08C7");

            entity.ToTable("AppointmentForm");

            entity.Property(e => e.ReserveId).HasColumnName("ReserveID");
            entity.Property(e => e.CaseAvatar).HasColumnType("image");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CaseName).HasMaxLength(20);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName).HasMaxLength(20);
            entity.Property(e => e.EndTime).HasPrecision(0);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ServiceId)
                .HasMaxLength(100)
                .HasColumnName("ServiceID");
            entity.Property(e => e.StartTime).HasPrecision(0);

            entity.HasOne(d => d.Case).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__CaseI__48CFD27E");

            entity.HasOne(d => d.Employee).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Emplo__49C3F6B7");

            entity.HasOne(d => d.Member).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Membe__46E78A0C");

            entity.HasOne(d => d.Service).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__47DBAE45");
        });

        modelBuilder.Entity<CareRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__CareReco__FBDF78C9FA14F716");

            entity.ToTable("CareRecord");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CaseName).HasMaxLength(20);
            entity.Property(e => e.Dbp).HasColumnName("DBP");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName).HasMaxLength(20);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.RecordTime).HasPrecision(0);
            entity.Property(e => e.ReserveId).HasColumnName("ReserveID");
            entity.Property(e => e.Sbp).HasColumnName("SBP");
            entity.Property(e => e.Temperature).HasColumnType("decimal(3, 1)");

            entity.HasOne(d => d.Case).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK__CareRecor__CaseI__5535A963");

            entity.HasOne(d => d.Employee).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__CareRecor__Emplo__5629CD9C");

            entity.HasOne(d => d.Member).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__CareRecor__Membe__5441852A");

            entity.HasOne(d => d.Reserve).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.ReserveId)
                .HasConstraintName("FK__CareRecor__Reser__534D60F1");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("PK__Cases__6CAE526CAFA9A7EE");

            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CaseAvatar).HasColumnType("image");
            entity.Property(e => e.CaseName).HasMaxLength(20);
            entity.Property(e => e.CasePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.FamilyName).HasMaxLength(20);
            entity.Property(e => e.FamilyPhone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(4);
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdentityType).HasMaxLength(10);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Relation).HasMaxLength(10);

            entity.HasOne(d => d.Member).WithMany(p => p.Cases)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Cases__MemberID__4AB81AF0");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF142CFD7F1");

            entity.HasIndex(e => e.UserName, "UQ__Employee__C9F28456C5E08F40").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeAvatar).HasColumnType("image");
            entity.Property(e => e.EmployeeName).HasMaxLength(20);
            entity.Property(e => e.EmploymentStatus).HasMaxLength(10);
            entity.Property(e => e.Gender).HasMaxLength(5);
            entity.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Supervisor).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(10);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B38E237C995");

            entity.HasIndex(e => e.UserName, "UQ__Members__C9F28456DB08FC92").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(4);
            entity.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MemberAvatar).HasColumnType("image");
            entity.Property(e => e.MemberName).HasMaxLength(20);
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServicesItem>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EAE6C2F881");

            entity.ToTable("ServicesItem");

            entity.Property(e => e.ServiceId)
                .HasMaxLength(100)
                .HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
