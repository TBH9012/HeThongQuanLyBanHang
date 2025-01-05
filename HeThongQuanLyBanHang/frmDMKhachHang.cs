using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using HeThongQuanLyBanHang.Class;

namespace HeThongQuanLyBanHang
{
    public partial class frmDMKhachHang : Form
    {
        private bool isAddingNew = false; // Trạng thái đang Thêm mới
        private bool isEditing = false;   // Trạng thái đang Sửa
        private Model1 db = new Model1();

        public frmDMKhachHang()
        {
            InitializeComponent();
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            ResetValues();
            SetControlState(false);
            SetButtonState(false);
        }
        private void LoadDataGridView()
        {
            var khachHangList = db.tblKhach.Select(kh => new
            {
                kh.MaKhach,
                kh.TenKhach,
                kh.DiaChi,
                kh.DienThoai
            }).ToList();

            dgvKhachHang.DataSource = khachHangList;
            dgvKhachHang.Columns["MaKhach"].HeaderText = "Mã Khách";
            dgvKhachHang.Columns["TenKhach"].HeaderText = "Tên Khách";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvKhachHang.Columns["DienThoai"].HeaderText = "Điện Thoại";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ResetValues();
            SetControlState(true);
            txtMaKhach.Focus();
            SetButtonState(false);
            btnBoQua.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhach.Text))
            {
                MessageBox.Show("Bạn phải chọn một khách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMaKhach.Text, out int maKhach))
            {
                MessageBox.Show("Mã khách phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa khách này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                var khach = db.tblKhach.Find(maKhach);
                if (khach != null)
                {
                    db.tblKhach.Remove(khach);
                    db.SaveChanges();
                    MessageBox.Show("Xóa khách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetValues();
                    SetControlState(false);
                    SetButtonState(false);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhach.Text))
            {
                MessageBox.Show("Bạn phải chọn một khách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditing = true;
            SetControlState(true);
            txtMaKhach.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhach.Text) || string.IsNullOrEmpty(txtTenKhach.Text))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mã khách có hợp lệ không
            if (!int.TryParse(txtMaKhach.Text, out int maKhach))
            {
                MessageBox.Show("Mã khách phải là một số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isAddingNew)
            {
                // Kiểm tra mã khách đã tồn tại chưa
                var existingKhach = db.tblKhach.FirstOrDefault(kh => kh.MaKhach == maKhach);
                if (existingKhach != null)
                {
                    MessageBox.Show("Mã khách đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm mới khách hàng
                var newKhach = new tblKhach
                {
                    MaKhach = maKhach,
                    TenKhach = txtTenKhach.Text,
                    DiaChi = txtDiaChi.Text,
                    DienThoai = mtbDienThoai.Text
                };

                db.tblKhach.Add(newKhach);
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (isEditing)
            {
                // Sửa khách hàng
                var khach = db.tblKhach.Find(maKhach);
                if (khach != null)
                {
                    khach.TenKhach = txtTenKhach.Text;
                    khach.DiaChi = txtDiaChi.Text;
                    khach.DienThoai = mtbDienThoai.Text;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Reset trạng thái
            isAddingNew = false;
            isEditing = false;
            ResetValues();
            SetControlState(false);
            SetButtonState(false);
            LoadDataGridView();
        }
        

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            SetControlState(false);
            isAddingNew = false;
            isEditing = false;
            SetButtonState(false);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn đóng cửa sổ này không?",
                "Xác nhận đóng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaKhach.Text = dgvKhachHang.Rows[e.RowIndex].Cells["MaKhach"].Value.ToString();
                txtTenKhach.Text = dgvKhachHang.Rows[e.RowIndex].Cells["TenKhach"].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                mtbDienThoai.Text = dgvKhachHang.Rows[e.RowIndex].Cells["DienThoai"].Value.ToString();

                SetControlState(false);
                SetButtonState(true);
                btnLuu.Enabled = false;
            }
        }

        private void SetControlState(bool enable)
        {
            txtMaKhach.Enabled = enable;
            txtTenKhach.Enabled = enable;
            txtDiaChi.Enabled = enable;
            mtbDienThoai.Enabled = enable;
        }

        private void SetButtonState(bool canEdit)
        {
            btnThem.Enabled = !isAddingNew && !isEditing;
            btnSua.Enabled = canEdit && !isAddingNew && !isEditing;
            btnXoa.Enabled = canEdit && !isAddingNew && !isEditing;
            btnLuu.Enabled = isAddingNew || isEditing;
            btnBoQua.Enabled = isAddingNew || isEditing || canEdit;
        }

        private void frmDMKhachHang_FormClosing(object sender, FormClosingEventArgs e)
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

