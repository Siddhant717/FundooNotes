using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.EmailId)
             .IsUnique();

            modelBuilder.Entity<Label>()
            .HasKey(p => new { p.userId, p.NoteId });

            modelBuilder.Entity<Label>()
            .HasOne(u => u.user)
            .WithMany()
            .HasForeignKey(u => u.userId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Label>()
            .HasOne(n => n.Note)
            .WithMany()
            .HasForeignKey(n => n.NoteId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }

}

