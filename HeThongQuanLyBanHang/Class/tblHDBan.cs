namespace HeThongQuanLyBanHang.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblHDBan")]
    public partial class tblHDBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblHDBan()
        {
            tblChitietHDBan = new HashSet<tblChitietHDBan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHDBan { get; set; }

        public int MaNhanvien { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayBan { get; set; }

        public int MaKhach { get; set; }

        public decimal? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChitietHDBan> tblChitietHDBan { get; set; }

        public virtual tblKhach tblKhach { get; set; }

        public virtual tblNhanvien tblNhanvien { get; set; }
    }
}
