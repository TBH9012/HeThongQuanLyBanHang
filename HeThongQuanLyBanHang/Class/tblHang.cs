namespace HeThongQuanLyBanHang.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblHang")]
    public partial class tblHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblHang()
        {
            tblChitietHDBan = new HashSet<tblChitietHDBan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHang { get; set; }

        public int MaChatlieu { get; set; }

        public int? SoLuong { get; set; }

        public decimal? DonGiaNhap { get; set; }

        public decimal? DonGiaBan { get; set; }

        [StringLength(255)]
        public string Anh { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public virtual tblChatlieu tblChatlieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChitietHDBan> tblChitietHDBan { get; set; }
    }
}
