using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFramework.Core.Models
{
    public partial class WebContext : DbContext
    {
        public WebContext()
        {
        }

        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friend> Friend { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public static string WebContextStr { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(WebContextStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.FriendId).HasColumnName("FriendID");

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MsgBody)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveUserId).HasColumnName("ReceiveUserID");

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.SendUserId).HasColumnName("SendUserID");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Passwrod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
