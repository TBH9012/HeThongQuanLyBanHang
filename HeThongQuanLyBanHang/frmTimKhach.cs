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
    public partial class frmTimKhach : Form
    {
        private Model1 db = new Model1();
        public frmTimKhach()
        {
            InitializeComponent();
        }

        private void frmTimKhach_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu từ bảng khách vào DataGridView
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            // Lấy toàn bộ danh sách khách hàng
            var danhSachKhach = db.tblKhach.Select(kh => new
            {
                kh.MaKhach,
                kh.TenKhach,
                kh.DiaChi,
                kh.DienThoai
            }).ToList();

            // Đổ dữ liệu vào DataGridView
            dgvDanhSachKhach.DataSource = danhSachKhach;

            // Đặt tên cột
            dgvDanhSachKhach.Columns["MaKhach"].HeaderText = "Mã Khách";
            dgvDanhSachKhach.Columns["TenKhach"].HeaderText = "Tên Khách";
            dgvDanhSachKhach.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvDanhSachKhach.Columns["DienThoai"].HeaderText = "Điện Thoại";
        }

        private void btnTimKiemKhach_Click(object sender, EventArgs e)
        {
            // Lọc dữ liệu theo điều kiện
            var query = db.tblKhach.AsQueryable();

            if (!string.IsNullOrEmpty(txtTKMaKhach.Text))
            {
                int maKhach = int.Parse(txtTKMaKhach.Text);
                query = query.Where(kh => kh.MaKhach == maKhach);
            }

            if (!string.IsNullOrEmpty(txtTKTenKhach.Text))
            {
                query = query.Where(kh => kh.TenKhach.Contains(txtTKTenKhach.Text));
            }

            var ketQua = query.Select(kh => new
            {
                kh.MaKhach,
                kh.TenKhach,
                kh.DiaChi,
                kh.DienThoai
            }).ToList();

            if (ketQua.Count > 0)
            {
                dgvDanhSachKhach.DataSource = ketQua;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDanhSachKhach.DataSource = null;
            }
        }

        private void btnTimLaiKhach_Click(object sender, EventArgs e)
        {
            // Làm mới danh sách khách hàng từ cơ sở dữ liệu
            LoadDataGridView();

            // Thông báo cho người dùng
            MessageBox.Show("Dữ liệu đã được làm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            // Xóa các ô nhập
            txtTKMaKhach.Clear();
            txtTKTenKhach.Clear();

            // Tải lại toàn bộ dữ liệu vào DataGridView
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void frmTimKhach_FormClosing(object sender, FormClosingEventArgs e)
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