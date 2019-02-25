namespace AttKeeper.data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BioModel : DbContext
    {
        public BioModel()
            : base("name=BioModel")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DTR> DTRs { get; set; }
        public virtual DbSet<Empoyee> Empoyees { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Leaf> Leaves { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<MacDumpLog> MacDumpLogs { get; set; }
        public virtual DbSet<MachineInstance> MachineInstances { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Miscellaneou> Miscellaneous { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<SettingDetail> SettingDetails { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyPrincipal)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentHead)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Positions)
                .WithOptional(e => e.Department)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DTR>()
                .Property(e => e.DTRDay)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.TimeInAM)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.TimeOutAM)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.TimeInPM)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.TimeOutPM)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.TimeInOT)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.TimeOutOT)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.DTRStatus)
                .IsUnicode(false);

            modelBuilder.Entity<DTR>()
                .Property(e => e.EditedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeNo)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeIdNo)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeLastName)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeFirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeMiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .Property(e => e.EmployeeSex)
                .IsUnicode(false);

            modelBuilder.Entity<Empoyee>()
                .HasMany(e => e.DTRs)
                .WithOptional(e => e.Empoyee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Empoyee>()
                .HasMany(e => e.MacDumpLogs)
                .WithOptional(e => e.Empoyee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Empoyee>()
                .HasMany(e => e.Machines)
                .WithOptional(e => e.Employee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Holiday>()
                .Property(e => e.HolidayNote)
                .IsUnicode(false);

            modelBuilder.Entity<Holiday>()
                .Property(e => e.HolidayType)
                .IsUnicode(false);

            modelBuilder.Entity<Leaf>()
                .Property(e => e.LeaveNo)
                .IsUnicode(false);

            modelBuilder.Entity<LeaveType>()
                .Property(e => e.LeaveTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<LeaveType>()
                .HasMany(e => e.Leaves)
                .WithOptional(e => e.LeaveType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MacDumpLog>()
                .Property(e => e.MacDumpDate)
                .IsUnicode(false);

            modelBuilder.Entity<MacDumpLog>()
                .Property(e => e.MacDumpTime)
                .IsUnicode(false);

            modelBuilder.Entity<MacDumpLog>()
                .Property(e => e.EditedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MachineInstance>()
                .Property(e => e.MachineInsName)
                .IsUnicode(false);

            modelBuilder.Entity<MachineInstance>()
                .HasMany(e => e.Machines)
                .WithOptional(e => e.MachineInstance)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Miscellaneou>()
                .Property(e => e.MiscRegularLabel)
                .IsUnicode(false);

            modelBuilder.Entity<Miscellaneou>()
                .Property(e => e.MiscOverRegularLabel)
                .IsUnicode(false);

            modelBuilder.Entity<Miscellaneou>()
                .Property(e => e.MiscUnderRegularLabel)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .Property(e => e.PositionName)
                .IsUnicode(false);

            modelBuilder.Entity<SettingDetail>()
                .Property(e => e.SettingDetailDay)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .HasMany(e => e.SettingDetails)
                .WithOptional(e => e.Setting)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserFullName)
                .IsUnicode(false);
        }
    }
}
