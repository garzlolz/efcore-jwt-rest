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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Pubs");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId)
                .HasName("PK_emp_id")
                .IsClustered(false);

            entity.ToTable("employee");

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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
