namespace HeThongQuanLyBanHang.Class
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblChitietHDBan")]
    public partial class tblChitietHDBan
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHDBan { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHang { get; set; }

        public int SoLuong { get; set; }

        public decimal? DonGia { get; set; }

        public decimal? GiamGia { get; set; }

        public decimal? ThanhTien { get; set; }

        public virtual tblHang tblHang { get; set; }

        public virtual tblHDBan tblHDBan { get; set; }
    }
}
