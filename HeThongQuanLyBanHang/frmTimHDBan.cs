using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeThongQuanLyBanHang.Class;

namespace HeThongQuanLyBanHang
{
    public partial class frmTimHDBan : Form
    {
        private Model1 db = new Model1();
        public frmTimHDBan()
        {
            InitializeComponent();
        }

        private void frmTimHDBan_Load(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            // Xóa trắng các TextBox
            txtMaHoaDon.Clear();
            txtMaNhanVien.Clear();
            txtMaKhachHang.Clear();
            txtThang.Clear();
            txtNam.Clear();
            txtTongTien.Clear();

            // Xóa DataGridView
            dgvDanhSachHD.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
           
                this.Close();
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Tạo query tìm kiếm hóa đơn
            var query = db.tblHDBan.AsQueryable();

            // Áp dụng các điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(txtMaHoaDon.Text))
            {
                if (int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
                    query = query.Where(hd => hd.MaHDBan == maHoaDon);
            }

            if (!string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                if (int.TryParse(txtMaNhanVien.Text, out int maNhanVien))
                    query = query.Where(hd => hd.MaNhanvien == maNhanVien);
            }

            if (!string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                if (int.TryParse(txtMaKhachHang.Text, out int maKhach))
                    query = query.Where(hd => hd.MaKhach == maKhach);
            }

            if (!string.IsNullOrEmpty(txtThang.Text))
            {
                if (int.TryParse(txtThang.Text, out int thang))
                    query = query.Where(hd => hd.NgayBan.Month == thang);
            }

            if (!string.IsNullOrEmpty(txtNam.Text))
            {
                if (int.TryParse(txtNam.Text, out int nam))
                    query = query.Where(hd => hd.NgayBan.Year == nam);
            }

            if (!string.IsNullOrEmpty(txtTongTien.Text))
            {
                if (decimal.TryParse(txtTongTien.Text, out decimal tongTien))
                    query = query.Where(hd => hd.TongTien == tongTien);
            }

            // Lấy dữ liệu và hiển thị
            var result = query.Select(hd => new
            {
                hd.MaHDBan,
                hd.NgayBan,
                hd.MaNhanvien,
                TenNhanvien = hd.tblNhanvien.TenNhanvien,
                hd.MaKhach,
                TenKhach = hd.tblKhach.TenKhach,
                hd.TongTien
            }).ToList();

            if (result.Count > 0)
            {
                dgvDanhSachHD.DataSource = result;

                // Đặt header cho DataGridView
                dgvDanhSachHD.Columns["MaHDBan"].HeaderText = "Mã Hóa Đơn";
                dgvDanhSachHD.Columns["NgayBan"].HeaderText = "Ngày Bán";
                dgvDanhSachHD.Columns["MaNhanvien"].HeaderText = "Mã Nhân Viên";
                dgvDanhSachHD.Columns["TenNhanvien"].HeaderText = "Tên Nhân Viên";
                dgvDanhSachHD.Columns["MaKhach"].HeaderText = "Mã Khách Hàng";
                dgvDanhSachHD.Columns["TenKhach"].HeaderText = "Tên Khách Hàng";
                dgvDanhSachHD.Columns["TongTien"].HeaderText = "Tổng Tiền";
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDanhSachHD.DataSource = null;
            }
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            // Gọi hàm ResetValues để đặt lại toàn bộ dữ liệu
            ResetValues();
        }

        private void dgvDanhSachHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấn vào một dòng hợp lệ hay không
            if (e.RowIndex >= 0 && dgvDanhSachHD.Rows[e.RowIndex].Cells["MaHDBan"].Value != null)
            {
                // Lấy Mã Hóa Đơn từ dòng được chọn
                int maHoaDon = Convert.ToInt32(dgvDanhSachHD.Rows[e.RowIndex].Cells["MaHDBan"].Value);

                // Truy vấn chi tiết hóa đơn từ cơ sở dữ liệu
                var chiTietHoaDon = db.tblChitietHDBan
                    .Where(ct => ct.MaHDBan == maHoaDon)
                    .Select(ct => new
                    {
                        ct.MaHDBan,
                        ct.MaHang,
                        TenHang = ct.tblHang.TenHang,
                        ct.SoLuong,
                        ct.DonGia,
                        ct.GiamGia,
                        ThanhTien = ct.SoLuong * ct.DonGia * (1 - ct.GiamGia / 100)
                    })
                    .ToList();

                // Kiểm tra nếu có chi tiết hóa đơn
                if (chiTietHoaDon.Count > 0)
                {
                    // Hiển thị chi tiết hóa đơn trong một Form mới hoặc MessageBox
                    string chiTiet = "Chi tiết hóa đơn:\n\n";
                    foreach (var item in chiTietHoaDon)
                    {
                        chiTiet += $"Mã Hàng: {item.MaHang}\n";
                        chiTiet += $"Tên Hàng: {item.TenHang}\n";
                        chiTiet += $"Số Lượng: {item.SoLuong}\n";
                        chiTiet += $"Đơn Giá: {item.DonGia:N0}\n";
                        chiTiet += $"Giảm Giá: {item.GiamGia:N0}%\n";
                        chiTiet += $"Thành Tiền: {item.ThanhTien:N0}\n";
                        chiTiet += "-------------------------\n";
                    }

                    MessageBox.Show(chiTiet, "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmTimHDBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
