using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using yungching_quiz.Data;

namespace yungching_quiz.Context;

public partial class PubsContext : DbContext
{
    public PubsContext()
    {
    }

    public PubsContext(DbContextOptions<PubsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    //public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=pubs;User Id=pub_ad;Password=1213;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId)
                .HasName("PK_emp_id")
                .IsClustered(false);

            entity.ToTable("employee", tb => tb.HasTrigger("employee_insupd"));

            entity.HasIndex(e => new { e.Lname, e.Fname, e.Minit }, "employee_ind").IsClustered();

            entity.Property(e => e.EmpId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("emp_id");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("hire_date");
            entity.Property(e => e.JobId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("job_id");
            entity.Property(e => e.JobLvl)
                .HasDefaultValueSql("((10))")
                .HasColumnName("job_lvl");
            entity.Property(e => e.Lname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Minit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("minit");
            entity.Property(e => e.PubId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasDefaultValueSql("('9952')")
                .IsFixedLength()
                .HasColumnName("pub_id");

            //entity.HasOne(d => d.Job).WithMany(p => p.Employees)
            //    .HasForeignKey(d => d.JobId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__employee__job_id__48CFD27E");
        });

        //modelBuilder.Entity<Job>(entity =>
        //{
        //    entity.HasKey(e => e.JobId).HasName("PK__jobs__6E32B6A53E2D004D");

        //    entity.ToTable("jobs");

        //    entity.Property(e => e.JobId).HasColumnName("job_id");
        //    entity.Property(e => e.JobDesc)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValueSql("('New Position - title not formalized yet')")
        //        .HasColumnName("job_desc");
        //    entity.Property(e => e.MaxLvl).HasColumnName("max_lvl");
        //    entity.Property(e => e.MinLvl).HasColumnName("min_lvl");
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
