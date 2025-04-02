using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Calendar.Api.Models;

namespace Calendar.Api.DatabaseConnection
{
    public partial class CalendarContext : DbContext
    {
        public CalendarContext()
        {
        }

        public CalendarContext(DbContextOptions<CalendarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Calendar> Calendars { get; set; } = null!;
        public virtual DbSet<CalendarEvent> CalendarEvents { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SharedCalendar> SharedCalendars { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAssignment> UserAssignments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Addr=DESKTOP-HJGHC9D\\SQLEXPRESS; database=Calendar; Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Calendar>(entity =>
            {
                entity.Property(e => e.CalendarId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Calendars)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calendar_User");
            });

            modelBuilder.Entity<CalendarEvent>(entity =>
            {
                entity.Property(e => e.CalendarEventId).ValueGeneratedNever();

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalendarEvent_Calendar");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserAssignment>(entity =>
            {
                entity.Property(e => e.UserAssignmentId).ValueGeneratedNever();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserAssignments)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAssignments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
