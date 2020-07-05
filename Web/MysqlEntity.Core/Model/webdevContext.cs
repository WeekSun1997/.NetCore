using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MysqlEntity.Core.Model
{
    public partial class webdevContext : DbContext
    {
        public webdevContext()
        {
        }

        public webdevContext(DbContextOptions<webdevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Billleaseinfo> Billleaseinfo { get; set; }
        public virtual DbSet<Billmodular> Billmodular { get; set; }
        public virtual DbSet<Billmodularinfo> Billmodularinfo { get; set; }
        public virtual DbSet<Billsql> Billsql { get; set; }
        public virtual DbSet<Billstoreretail> Billstoreretail { get; set; }
        public virtual DbSet<Billtableremask> Billtableremask { get; set; }
        public virtual DbSet<Billvipinfo> Billvipinfo { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Employeeleave> Employeeleave { get; set; }
        public virtual DbSet<Employeepayslip> Employeepayslip { get; set; }
        public virtual DbSet<Friend> Friend { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Sysdropdown> Sysdropdown { get; set; }
        public virtual DbSet<Sysdropdwondt> Sysdropdwondt { get; set; }
        public virtual DbSet<Sysuser> Sysuser { get; set; }
        public virtual DbSet<Vipamount> Vipamount { get; set; }
        public virtual DbSet<Vippre> Vippre { get; set; }
        public virtual DbSet<Viptransaction> Viptransaction { get; set; }
        public virtual DbSet<Vipvisit> Vipvisit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=127.0.0.1;User Id=root;Password=123.com;Database=webdev ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Billleaseinfo>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("billleaseinfo");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillNo).HasColumnType("varchar(45)");

                entity.Property(e => e.CardNameA).HasColumnType("varchar(45)");

                entity.Property(e => e.CardNameB).HasColumnType("varchar(45)");

                entity.Property(e => e.CardNoA).HasColumnType("varchar(45)");

                entity.Property(e => e.CardNoB).HasColumnType("varchar(45)");

                entity.Property(e => e.CardPhoneA).HasColumnType("varchar(45)");

                entity.Property(e => e.CardPhoneB).HasColumnType("varchar(45)");

                entity.Property(e => e.Deposit).HasColumnType("varchar(45)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LeaseMode).HasColumnType("int(11)");

                entity.Property(e => e.LeaseMonth).HasColumnType("int(11)");

                entity.Property(e => e.PayDate).HasColumnType("date");

                entity.Property(e => e.PayMode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Billmodular>(entity =>
            {
                entity.ToTable("billmodular");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsShow).HasColumnType("varchar(255)");

                entity.Property(e => e.Modularid).HasColumnType("varchar(255)");

                entity.Property(e => e.Modularname).HasColumnType("varchar(255)");

                entity.Property(e => e.Modularnametext).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Billmodularinfo>(entity =>
            {
                entity.ToTable("billmodularinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillTable).HasColumnType("varchar(45)");

                entity.Property(e => e.IsShow).HasColumnType("varchar(255)");

                entity.Property(e => e.Islist)
                    .HasColumnName("ISList")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModularInfoId).HasColumnType("varchar(255)");

                entity.Property(e => e.ModularInfoname).HasColumnType("varchar(255)");

                entity.Property(e => e.ModularInfoulr).HasColumnType("varchar(255)");

                entity.Property(e => e.Modulardtnametext).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Billsql>(entity =>
            {
                entity.ToTable("billsql");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillName).HasColumnType("varchar(5000)");

                entity.Property(e => e.SqlBody).HasColumnType("varchar(255)");

                entity.Property(e => e.SqlName).HasColumnType("varchar(255)");

                entity.Property(e => e.SqlTitle).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Billstoreretail>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("billstoreretail");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillNo).HasColumnType("varchar(45)");

                entity.Property(e => e.OfflineAmount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.OfflineQty).HasColumnType("int(11)");

                entity.Property(e => e.RetailDate).HasColumnType("date");

                entity.Property(e => e.WxAmount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.WxQty).HasColumnType("int(11)");

                entity.Property(e => e.ZfbAmount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.ZfbQty).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Billtableremask>(entity =>
            {
                entity.ToTable("billtableremask");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.BillTableName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.BindList).HasColumnType("varchar(45)");

                entity.Property(e => e.ColLength).HasColumnType("int(11)");

                entity.Property(e => e.Fiedx).HasColumnType("varchar(45)");

                entity.Property(e => e.FiledName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FiledType)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Remask)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TableIndex).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Billvipinfo>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("billvipinfo");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("int(11)");

                entity.Property(e => e.Channel).HasColumnType("int(11)");

                entity.Property(e => e.Phone).HasColumnType("varchar(45)");

                entity.Property(e => e.PreDate).HasColumnType("date");

                entity.Property(e => e.Project).HasColumnType("varchar(255)");

                entity.Property(e => e.Remask).HasColumnType("varchar(500)");

                entity.Property(e => e.Sex).HasColumnType("int(11)");

                entity.Property(e => e.Statc).HasColumnType("int(11)");

                entity.Property(e => e.VipName).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("employee");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Address).HasColumnType("varchar(50)");

                entity.Property(e => e.BankCard).HasColumnType("varchar(30)");

                entity.Property(e => e.BankName).HasColumnType("varchar(30)");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeType).HasColumnType("bit(1)");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnType("varchar(255)");

                entity.Property(e => e.IdentityId)
                    .HasColumnName("IdentityID")
                    .HasColumnType("varchar(18)");

                entity.Property(e => e.IsOnLine).HasColumnType("bit(1)");

                entity.Property(e => e.Phone).HasColumnType("varchar(11)");

                entity.Property(e => e.QuitDate).HasColumnType("datetime");

                entity.Property(e => e.UrgentName).HasColumnType("varchar(30)");

                entity.Property(e => e.UrgentPhone).HasColumnType("varchar(30)");

                entity.Property(e => e.UserName).HasColumnType("varchar(20)");

                entity.Property(e => e.Vxid)
                    .HasColumnName("VXID")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Zfbid)
                    .HasColumnName("ZFBID")
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<Employeeleave>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("employeeleave");

                entity.Property(e => e.BillId).HasColumnType("int(11)");

                entity.Property(e => e.Id).HasColumnType("varchar(255)");

                entity.Property(e => e.LeaveDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveReasons).HasColumnType("int(11)");

                entity.Property(e => e.LeaveType).HasColumnType("bit(1)");

                entity.Property(e => e.Overtime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(45)")
                    .HasDefaultValueSql("'Null'");
            });

            modelBuilder.Entity<Employeepayslip>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("employeepayslip");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BasePay).HasColumnType("varchar(45)");

                entity.Property(e => e.Deduction).HasColumnType("varchar(45)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Other).HasColumnType("varchar(45)");

                entity.Property(e => e.PayDate).HasColumnType("date");

                entity.Property(e => e.Remarks).HasColumnType("varchar(500)");

                entity.Property(e => e.Subsidy).HasColumnType("varchar(45)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WagesPayable).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.ToTable("friend");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FriendId).HasColumnType("int(32)");

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnType("int(32)");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MsgId)
                    .HasName("PRIMARY");

                entity.ToTable("message");

                entity.Property(e => e.MsgId).HasColumnType("int(11)");

                entity.Property(e => e.IsSuccess).HasColumnType("int(10)");

                entity.Property(e => e.MsgBody).HasColumnType("varchar(500)");

                entity.Property(e => e.ReceiveUserId)
                    .HasColumnName("ReceiveUserID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.SendUserId)
                    .HasColumnName("SendUserID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Sysdropdown>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("sysdropdown");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillName).HasColumnType("varchar(255)");

                entity.Property(e => e.Reamsk).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Sysdropdwondt>(entity =>
            {
                entity.ToTable("sysdropdwondt");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Sysuser>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("sysuser");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.PassWrod).HasColumnType("varchar(255)");

                entity.Property(e => e.UserCode).HasColumnType("varchar(255)");

                entity.Property(e => e.UserName).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Vipamount>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("vipamount");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.Channel).HasColumnType("int(11)");

                entity.Property(e => e.Project).HasColumnType("varchar(45)");

                entity.Property(e => e.Remask).HasColumnType("varchar(200)");

                entity.Property(e => e.Statc).HasColumnType("int(11)");

                entity.Property(e => e.TransactionType).HasColumnType("int(11)");

                entity.Property(e => e.VipId)
                    .HasColumnName("VipID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Vippre>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("vippre");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Channel).HasColumnType("int(11)");

                entity.Property(e => e.PreDate).HasColumnType("date");

                entity.Property(e => e.Project).HasColumnType("varchar(200)");

                entity.Property(e => e.Remask).HasColumnType("varchar(200)");

                entity.Property(e => e.Sex).HasColumnType("int(11)");

                entity.Property(e => e.VipName).HasColumnType("varchar(255)");

                entity.Property(e => e.VipPhone).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Viptransaction>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("viptransaction");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.Property(e => e.Channel).HasColumnType("int(255)");

                entity.Property(e => e.CreateTime).HasColumnType("date");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Project).HasColumnType("varchar(255)");

                entity.Property(e => e.Remask).HasColumnType("varchar(500)");

                entity.Property(e => e.Stuts).HasColumnType("int(255)");

                entity.Property(e => e.TransactionType).HasColumnType("int(255)");

                entity.Property(e => e.VipName).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Vipvisit>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PRIMARY");

                entity.ToTable("vipvisit");

                entity.Property(e => e.BillId)
                    .HasColumnName("BillID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Remask).HasColumnType("varchar(500)");

                entity.Property(e => e.VipName).HasColumnType("varchar(255)");

                entity.Property(e => e.VipPhone).HasColumnType("varchar(255)");

                entity.Property(e => e.VisitDate).HasColumnType("date");
            });
        }
    }
}
