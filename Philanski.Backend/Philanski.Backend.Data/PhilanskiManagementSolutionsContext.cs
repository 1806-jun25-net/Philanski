using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Philanski.Backend.Data
{
    public partial class PhilanskiManagementSolutionsContext : DbContext
    {
        public PhilanskiManagementSolutionsContext()
        {
        }

        public PhilanskiManagementSolutionsContext(DbContextOptions<PhilanskiManagementSolutionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<ManagerDepartment> ManagerDepartment { get; set; }
        public virtual DbSet<TimeSheet> TimeSheet { get; set; }
        public virtual DbSet<TimeSheetApproval> TimeSheetApproval { get; set; }
        public virtual DbSet<Worksite> Worksite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=platt-1806.database.windows.net,1433;Initial Catalog=Philanski Management Solutions;Persist Security Info=False;User ID=philipaplatt;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.TerminationDate).HasColumnType("datetime");

                entity.Property(e => e.WorksiteId).HasColumnName("WorksiteID");

                entity.HasOne(d => d.Worksite)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.WorksiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Worksi__787EE5A0");
            });

            modelBuilder.Entity<EmployeeDepartment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Manager)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manager__Employe__7B5B524B");
            });

            modelBuilder.Entity<ManagerDepartment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RegularHours).HasColumnType("decimal(2, 2)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeSheet)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__Emplo__7E37BEF6");
            });

            modelBuilder.Entity<TimeSheetApproval>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovingManagerId).HasColumnName("ApprovingManagerID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.TimeSubmitted).HasColumnType("datetime");

                entity.Property(e => e.WeekEnd).HasColumnType("date");

                entity.Property(e => e.WeekStart).HasColumnType("date");

                entity.Property(e => e.WeekTotalRegular).HasColumnType("decimal(3, 2)");

                entity.HasOne(d => d.ApprovingManager)
                    .WithMany(p => p.TimeSheetApproval)
                    .HasForeignKey(d => d.ApprovingManagerId)
                    .HasConstraintName("FK__TimeSheet__Appro__778AC167");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeSheetApproval)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__Emplo__7F2BE32F");
            });

            modelBuilder.Entity<Worksite>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.AddressLine2).HasMaxLength(128);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(5);
            });
        }
    }
}
