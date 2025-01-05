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
    public partial class frmTimHang : Form
    {
        private Model1 db = new Model1();

        public frmTimHang()
        {
            InitializeComponent();
        }

        private void LoadDataGridView()
        {
            // Lấy toàn bộ danh sách hàng
            var danhSachHang = db.tblHang.Select(h => new
            {
                h.MaHang,
                h.TenHang,
                h.MaChatlieu,
                ChatLieu = h.tblChatlieu.TenChatlieu,
                h.SoLuong,
                h.DonGiaNhap,
                h.DonGiaBan,
                h.Anh,
                h.GhiChu
            }).ToList();

            // Đổ dữ liệu vào DataGridView
            dgvDanhSachHang.DataSource = danhSachHang;

            // Đặt tên cột
            dgvDanhSachHang.Columns["MaHang"].HeaderText = "Mã Hàng";
            dgvDanhSachHang.Columns["TenHang"].HeaderText = "Tên Hàng";
            dgvDanhSachHang.Columns["MaChatlieu"].HeaderText = "Mã Chất Liệu";
            dgvDanhSachHang.Columns["ChatLieu"].HeaderText = "Chất Liệu";
            dgvDanhSachHang.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvDanhSachHang.Columns["DonGiaNhap"].HeaderText = "Đơn Giá Nhập";
            dgvDanhSachHang.Columns["DonGiaBan"].HeaderText = "Đơn Giá Bán";
            dgvDanhSachHang.Columns["Anh"].HeaderText = "Ảnh";
            dgvDanhSachHang.Columns["GhiChu"].HeaderText = "Ghi Chú";
        }

        private void btnTimKiemHang_Click(object sender, EventArgs e)
        {
            // Lọc dữ liệu theo điều kiện
            var query = db.tblHang.AsQueryable();

            if (!string.IsNullOrEmpty(txtTKMaHang.Text))
            {
                int maHang = int.Parse(txtTKMaHang.Text);
                query = query.Where(h => h.MaHang == maHang);
            }

            if (!string.IsNullOrEmpty(txtTKTenHang.Text))
            {
                query = query.Where(h => h.TenHang.Contains(txtTKTenHang.Text));
            }

            var ketQua = query.Select(h => new
            {
                h.MaHang,
                h.TenHang,
                h.MaChatlieu,
                ChatLieu = h.tblChatlieu.TenChatlieu,
                h.SoLuong,
                h.DonGiaNhap,
                h.DonGiaBan,
                h.Anh,
                h.GhiChu
            }).ToList();

            if (ketQua.Count > 0)
            {
                dgvDanhSachHang.DataSource = ketQua;
            }
            else
            {
                MessageBox.Show("Không tìm thấy hàng nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDanhSachHang.DataSource = null;
            }
        }

        private void btnTimLaiHang_Click(object sender, EventArgs e)
        {
            // Làm mới danh sách hàng từ cơ sở dữ liệu
            LoadDataGridView();

            // Thông báo cho người dùng
            MessageBox.Show("Dữ liệu đã được làm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtTKMaHang.Clear();
            txtTKTenHang.Clear();
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void frmTimHang_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu từ bảng hàng vào DataGridView
            LoadDataGridView();
        }

        private void frmTimHang_FormClosing(object sender, FormClosingEventArgs e)
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
