using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kers.Models;

public partial class Gr621PoaseContext : DbContext
{
    public Gr621PoaseContext()
    {
    }

    public Gr621PoaseContext(DbContextOptions<Gr621PoaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=10.30.0.137;Port=5432;Database=gr621_poase; Username=gr621_poase;Password=artempozd23");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("route_pkey");

            entity.ToTable("route", "kurs2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status", "kurs2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trip_pkey");

            entity.ToTable("trip", "kurs2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Routeid).HasColumnName("routeid");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Timeend)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timeend");
            entity.Property(e => e.Timestart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestart");

            entity.HasOne(d => d.Route).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Routeid)
                .HasConstraintName("trip_routeid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Statusid)
                .HasConstraintName("trip_statusid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", "kurs2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
