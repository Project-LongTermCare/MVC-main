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

    public virtual DbSet<Physical> Physicals { get; set; }

    public virtual DbSet<ServicesItem> ServicesItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=LongTermCare;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentForm>(entity =>
        {
            entity.HasKey(e => e.ReserveId).HasName("PK__Appointm__C8C00680303F50A2");

            entity.ToTable("AppointmentForm");

            entity.Property(e => e.ReserveId).HasColumnName("ReserveID");
            entity.Property(e => e.CaseAvatar).HasColumnType("image");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CaseName).HasMaxLength(20);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName).HasMaxLength(20);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ServiceId)
                .HasMaxLength(100)
                .HasColumnName("ServiceID");

            entity.HasOne(d => d.Case).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__CaseI__49C3F6B7");

            entity.HasOne(d => d.Employee).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Emplo__4AB81AF0");

            entity.HasOne(d => d.Member).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Membe__47DBAE45");

            entity.HasOne(d => d.Service).WithMany(p => p.AppointmentForms)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__48CFD27E");
        });

        modelBuilder.Entity<CareRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__CareReco__FBDF78C964E8EBD9");

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
                .HasConstraintName("FK__CareRecor__CaseI__5AEE82B9");

            entity.HasOne(d => d.Employee).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__CareRecor__Emplo__5BE2A6F2");

            entity.HasOne(d => d.Member).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__CareRecor__Membe__59FA5E80");

            entity.HasOne(d => d.Reserve).WithMany(p => p.CareRecords)
                .HasForeignKey(d => d.ReserveId)
                .HasConstraintName("FK__CareRecor__Reser__59063A47");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("PK__Cases__6CAE526C71C617DB");

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
                .HasConstraintName("FK__Cases__MemberID__5441852A");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF118EAC5A7");

            entity.HasIndex(e => e.EmployeeNumber, "UQ__Employee__8D663598351517FE").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Employee__C9F2845645DD2BEC").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeAvatar).HasColumnType("image");
            entity.Property(e => e.EmployeeName).HasMaxLength(20);
            entity.Property(e => e.EmployeeNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B389028B2FA");

            entity.HasIndex(e => e.UserName, "UQ__Members__C9F2845674DC28AB").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
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

            entity.HasOne(d => d.Case).WithMany(p => p.Members)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK__Members__CaseID__5CD6CB2B");
        });

        modelBuilder.Entity<Physical>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Physical");

            entity.Property(e => e.Alt)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("ALT");
            entity.Property(e => e.Ast)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("AST");
            entity.Property(e => e.Bun)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("BUN");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.Crea)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("CREA");
            entity.Property(e => e.Dbp).HasColumnName("DBP");
            entity.Property(e => e.Fe).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.Hb)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("HB");
            entity.Property(e => e.Hct)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("HCT");
            entity.Property(e => e.Hdl)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("HDL");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Ldl)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("LDL");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PhysicalId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PhysicalID");
            entity.Property(e => e.Plt).HasColumnName("PLT");
            entity.Property(e => e.Rbc)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("RBC");
            entity.Property(e => e.Sbp).HasColumnName("SBP");
            entity.Property(e => e.Tcho)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("TCHO");
            entity.Property(e => e.Tg)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("TG");
            entity.Property(e => e.Uibc)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("UIBC");
            entity.Property(e => e.Waist).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Wbc)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("WBC");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Case).WithMany()
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK__Physical__CaseID__5535A963");

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Physical__Member__5629CD9C");
        });

        modelBuilder.Entity<ServicesItem>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EAACBB62C6");

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
