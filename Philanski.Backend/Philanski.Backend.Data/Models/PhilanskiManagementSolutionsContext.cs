using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Philanski.Backend.Data.Models
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

        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<EmployeeDepartments> EmployeeDepartments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<ManagerDepartments> ManagerDepartments { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<TimeSheetApprovals> TimeSheetApprovals { get; set; }
        public virtual DbSet<TimeSheets> TimeSheets { get; set; }
        public virtual DbSet<Worksites> Worksites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<EmployeeDepartments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

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
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.WorksiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Worksi__787EE5A0");
            });

            modelBuilder.Entity<ManagerDepartments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manager__Employe__7B5B524B");
            });

            modelBuilder.Entity<TimeSheetApprovals>(entity =>
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
                    .WithMany(p => p.TimeSheetApprovals)
                    .HasForeignKey(d => d.ApprovingManagerId)
                    .HasConstraintName("FK__TimeSheet__Appro__778AC167");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeSheetApprovals)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__Emplo__7F2BE32F");
            });

            modelBuilder.Entity<TimeSheets>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RegularHours).HasColumnType("decimal(2, 2)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeSheets)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__Emplo__7E37BEF6");
            });

            modelBuilder.Entity<Worksites>(entity =>
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
