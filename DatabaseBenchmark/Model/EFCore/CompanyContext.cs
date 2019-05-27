using Microsoft.EntityFrameworkCore;

namespace DatabaseBenchmark.Model.EFCore
{
    public partial class CompanyContext : DbContext
    {
        public CompanyContext()
        {
        }

        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        public virtual DbSet<DeptLocations> DeptLocations { get; set; }
        public virtual DbSet<Linq2Db.Employee> Employee { get; set; }
        public virtual DbSet<Linq2Db.Project> Project { get; set; }
        public virtual DbSet<Linq2Db.WorksOn> WorksOn { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Company;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Dnumber);

                entity.Property(e => e.Dnumber)
                    .HasColumnName("DNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dname)
                    .HasColumnName("DName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MgrSsn)
                    .HasColumnName("MgrSSN")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.MgrStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.MgrSsnNavigation)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.MgrSsn)
                    .HasConstraintName("FK_Department_Employee");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.HasKey(e => new { e.Essn, e.DependentName });

                entity.Property(e => e.Essn).HasColumnType("numeric(9, 0)");

                entity.Property(e => e.DependentName)
                    .HasColumnName("Dependent_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bdate)
                    .HasColumnName("BDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Relationship)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.HasOne(d => d.EssnNavigation)
                    .WithMany(p => p.Dependent)
                    .HasForeignKey(d => d.Essn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependent_Employee");
            });

            modelBuilder.Entity<DeptLocations>(entity =>
            {
                entity.HasKey(e => new { e.Dnumber, e.Dlocation });

                entity.ToTable("Dept_Locations");

                entity.Property(e => e.Dnumber).HasColumnName("DNUmber");

                entity.Property(e => e.Dlocation)
                    .HasColumnName("DLocation")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DnumberNavigation)
                    .WithMany(p => p.DeptLocations)
                    .HasForeignKey(d => d.Dnumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dept_Locations_Department");
            });

            modelBuilder.Entity<Linq2Db.Employee>(entity =>
            {
                entity.HasKey(e => e.Ssn);

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasColumnType("numeric(9, 0)");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bdate)
                    .HasColumnName("BDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fname)
                    .HasColumnName("FName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .HasColumnName("LName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Minit)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Salary).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SuperSsn)
                    .HasColumnName("SuperSSN")
                    .HasColumnType("numeric(9, 0)");

                entity.HasOne(d => d.DnoNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.Dno)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.SuperSsnNavigation)
                    .WithMany(p => p.InverseSuperSsnNavigation)
                    .HasForeignKey(d => d.SuperSsn)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<Linq2Db.Project>(entity =>
            {
                entity.HasKey(e => e.Pnumber);

                entity.Property(e => e.Pnumber)
                    .HasColumnName("PNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dnum).HasColumnName("DNum");

                entity.Property(e => e.Plocation)
                    .HasColumnName("PLocation")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pname)
                    .HasColumnName("PName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DnumNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.Dnum)
                    .HasConstraintName("FK_Project_Department");
            });

            modelBuilder.Entity<Linq2Db.WorksOn>(entity =>
            {
                entity.HasKey(e => new { e.Essn, e.Pno });

                entity.ToTable("Works_on");

                entity.Property(e => e.Essn).HasColumnType("numeric(9, 0)");

                entity.HasOne(d => d.EssnNavigation)
                    .WithMany(p => p.WorksOn)
                    .HasForeignKey(d => d.Essn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_on_Employee");

                entity.HasOne(d => d.PnoNavigation)
                    .WithMany(p => p.WorksOn)
                    .HasForeignKey(d => d.Pno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_on_Project");
            });
        }
    }
}
