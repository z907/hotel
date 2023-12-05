using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace Model.Context;

public partial class HotelDbContext : DbContext
{
    public HotelDbContext():base()
    {
      
    }

  
    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
    {
       
    }
    

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomAttribute> RoomAttributes { get; set; }

    public virtual DbSet<RoomCapacity> RoomCapacities { get; set; }

    public virtual DbSet<RoomQuality> RoomQualities { get; set; }

    public virtual DbSet<RoomViewType> RoomViewTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel;Username=postgres;Password=zv1488");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.ToTable("Customer");

            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.LastName).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");
            entity.Property(e => e.Surname).HasColumnType("character varying");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Payment_pkey");

            entity.ToTable("Payment");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("Payment_ReservationId_fkey");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reservation_pkey");

            entity.ToTable("Reservation");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("Reservation_CustomerId_fkey");

            entity.HasOne(d => d.RoomAttributes).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomAttributesId)
                .HasConstraintName("Reservation_RoomAttributesID_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("Reservation_RoomID_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("Reservation_Status_fkey");
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReservationStatus_pkey");

            entity.ToTable("ReservationStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnType("character varying");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rooms_pkey1");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Rooms_Id_seq1\"'::regclass)");

            entity.HasOne(d => d.RoomAttributes).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomAttributesId)
                .HasConstraintName("Rooms_RoomAttributesId_fkey");
        });

        modelBuilder.Entity<RoomAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rooms_pkey");

            entity.HasIndex(e => e.QualityId, "RoomAttributes_QualityId_CapacityId_ViewId_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Rooms_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Capacity).WithMany(p => p.RoomAttributes)
                .HasForeignKey(d => d.CapacityId)
                .HasConstraintName("Rooms_CapacityId_fkey");

            entity.HasOne(d => d.Quality).WithOne(p => p.RoomAttribute)
                .HasForeignKey<RoomAttribute>(d => d.QualityId)
                .HasConstraintName("Rooms_QualityId_fkey");

            entity.HasOne(d => d.View).WithMany(p => p.RoomAttributes)
                .HasForeignKey(d => d.ViewId)
                .HasConstraintName("Rooms_ViewId_fkey");
        });

        modelBuilder.Entity<RoomCapacity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoomCapacity_pkey");

            entity.ToTable("RoomCapacity");
        });

        modelBuilder.Entity<RoomQuality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoomQuality_pkey");

            entity.ToTable("RoomQuality");

            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<RoomViewType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoomViewType_pkey");

            entity.ToTable("RoomViewType");

            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("User_Role_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey");

            entity.ToTable("UserRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
