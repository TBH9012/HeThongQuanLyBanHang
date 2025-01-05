using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HeThongQuanLyBanHang.Class
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tblChatlieu> tblChatlieu { get; set; }
        public virtual DbSet<tblChitietHDBan> tblChitietHDBan { get; set; }
        public virtual DbSet<tblHang> tblHang { get; set; }
        public virtual DbSet<tblHDBan> tblHDBan { get; set; }
        public virtual DbSet<tblKhach> tblKhach { get; set; }
        public virtual DbSet<tblNhanvien> tblNhanvien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblChatlieu>()
                .HasMany(e => e.tblHang)
                .WithRequired(e => e.tblChatlieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblHang>()
                .HasMany(e => e.tblChitietHDBan)
                .WithRequired(e => e.tblHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblHDBan>()
                .HasMany(e => e.tblChitietHDBan)
                .WithRequired(e => e.tblHDBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblKhach>()
                .HasMany(e => e.tblHDBan)
                .WithRequired(e => e.tblKhach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblNhanvien>()
                .HasMany(e => e.tblHDBan)
                .WithRequired(e => e.tblNhanvien)
                .WillCascadeOnDelete(false);
        }
    }
}
