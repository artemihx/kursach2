using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kers.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auto> Autos { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Passporttouser> Passporttousers { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Ticketontrip> Ticketontrips { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=emka2323");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Auto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auto_pkey");

            entity.ToTable("auto", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Carnumber)
                .HasMaxLength(8)
                .HasColumnName("carnumber");
            entity.Property(e => e.Maxcountpassneger).HasColumnName("maxcountpassneger");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passport_pkey");

            entity.ToTable("passport", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetaken).HasColumnName("datetaken");
            entity.Property(e => e.Fname)
                .HasColumnType("character varying")
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasColumnType("character varying")
                .HasColumnName("lname");
            entity.Property(e => e.Mname)
                .HasColumnType("character varying")
                .HasColumnName("mname");
            entity.Property(e => e.Number)
                .HasMaxLength(6)
                .HasColumnName("number");
            entity.Property(e => e.Serail)
                .HasMaxLength(4)
                .HasColumnName("serail");
        });

        modelBuilder.Entity<Passporttouser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passporttouser_pkey");

            entity.ToTable("passporttouser", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fkpassportid).HasColumnName("fkpassportid");
            entity.Property(e => e.Fkuserid).HasColumnName("fkuserid");

            entity.HasOne(d => d.Fkpassport).WithMany(p => p.Passporttousers)
                .HasForeignKey(d => d.Fkpassportid)
                .HasConstraintName("passporttouser_fkpassportid_fkey");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.Passporttousers)
                .HasForeignKey(d => d.Fkuserid)
                .HasConstraintName("passporttouser_fkuserid_fkey");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("route_pkey");

            entity.ToTable("route", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_pkey");

            entity.ToTable("ticket", "kurs2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fkpassportid).HasColumnName("fkpassportid");
            entity.Property(e => e.Fktripid).HasColumnName("fktripid");

            entity.HasOne(d => d.Fkpassport).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Fkpassportid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_fkpassportid_fkey");

            entity.HasOne(d => d.Fktrip).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Fktripid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_fktripid_fkey");
        });

        modelBuilder.Entity<Ticketontrip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticketontrip_pkey");

            entity.ToTable("ticketontrip", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fkticketid).HasColumnName("fkticketid");
            entity.Property(e => e.Fktripid).HasColumnName("fktripid");

            entity.HasOne(d => d.Fktrip).WithMany(p => p.Ticketontrips)
                .HasForeignKey(d => d.Fktripid)
                .HasConstraintName("ticketontrip_fktripid_fkey");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trip_pkey");

            entity.ToTable("trip", "kursach2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autoid).HasColumnName("autoid");
            entity.Property(e => e.Routeid).HasColumnName("routeid");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Timeend)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timeend");
            entity.Property(e => e.Timestart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestart");

            entity.HasOne(d => d.Auto).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Autoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trip_autoid_fkey");

            entity.HasOne(d => d.Route).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Routeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trip_routeid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Trips)
                .HasForeignKey(d => d.Statusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trip_statusid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", "kursach2");

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
