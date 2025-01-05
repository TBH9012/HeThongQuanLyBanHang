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
    public partial class frmDMNhanvien : Form
    {
        private bool isAddingNew = false; // Trạng thái đang Thêm mới
        private bool isEditing = false;   // Trạng thái đang Sửa
        private Model1 db = new Model1();

        public frmDMNhanvien()
        {
            InitializeComponent();
        }

        private void frmDMNhanvien_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            ResetValues();
            SetControlState(false);
            SetButtonState(false);
        }
        public void LoadDataGridView()
        {
            var nhanVienList = db.tblNhanvien.Select(nv => new
            {
                nv.MaNhanvien,
                nv.TenNhanvien,
                GioiTinh = nv.GioiTinh, // Giữ nguyên giá trị trong cơ sở dữ liệu
                nv.DiaChi,
                nv.DienThoai,
                nv.NgaySinh
            }).ToList();

            dgvNhanVien.DataSource = nhanVienList;
            dgvNhanVien.Columns["MaNhanvien"].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns["TenNhanvien"].HeaderText = "Tên Nhân Viên";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaNhanVien.Text = dgvNhanVien.Rows[e.RowIndex].Cells["MaNhanvien"].Value.ToString();
                txtTenNhanVien.Text = dgvNhanVien.Rows[e.RowIndex].Cells["TenNhanvien"].Value.ToString();
                txtDiaChi.Text = dgvNhanVien.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                mtbDienThoai.Text = dgvNhanVien.Rows[e.RowIndex].Cells["DienThoai"].Value.ToString();
                dtpNgaySinh.Value = DateTime.Parse(dgvNhanVien.Rows[e.RowIndex].Cells["NgaySinh"].Value.ToString());

                string gioiTinh = dgvNhanVien.Rows[e.RowIndex].Cells["GioiTinh"].Value.ToString();
                chkGioiTinh.Checked = gioiTinh == "Nam";

                SetControlState(false);
                SetButtonState(true);
                btnLuu.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ResetValues();
            SetControlState(true);
            txtMaNhanVien.Focus();
            SetButtonState(false);
            btnBoQua.Enabled = true;
        }

        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            chkGioiTinh.Checked = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text) || string.IsNullOrEmpty(txtTenNhanVien.Text))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra Mã nhân viên có phải là số hợp lệ không
            if (!int.TryParse(txtMaNhanVien.Text, out int maNhanVien))
            {
                MessageBox.Show("Mã nhân viên phải là một số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác định giới tính từ checkbox
            string gioiTinh = chkGioiTinh.Checked ? "Nam" : "Nữ";

            if (isAddingNew)
            {
                // Kiểm tra mã nhân viên đã tồn tại chưa
                var existingNhanVien = db.tblNhanvien.FirstOrDefault(nv => nv.MaNhanvien == maNhanVien);
                if (existingNhanVien != null)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm mới nhân viên
                var newNhanVien = new tblNhanvien
                {
                    MaNhanvien = maNhanVien,
                    TenNhanvien = txtTenNhanVien.Text,
                    GioiTinh = gioiTinh, // Chuyển đổi giá trị giới tính thành chuỗi
                    DiaChi = txtDiaChi.Text,
                    DienThoai = mtbDienThoai.Text,
                    NgaySinh = dtpNgaySinh.Value
                };

                db.tblNhanvien.Add(newNhanVien);
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (isEditing)
            {
                // Sửa nhân viên
                var nhanVien = db.tblNhanvien.Find(maNhanVien);
                if (nhanVien != null)
                {
                    nhanVien.TenNhanvien = txtTenNhanVien.Text;
                    nhanVien.GioiTinh = gioiTinh; // Chuyển đổi giá trị giới tính thành chuỗi
                    nhanVien.DiaChi = txtDiaChi.Text;
                    nhanVien.DienThoai = mtbDienThoai.Text;
                    nhanVien.NgaySinh = dtpNgaySinh.Value;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Reset trạng thái sau khi lưu
            isAddingNew = false;
            isEditing = false;
            ResetValues(); // Xóa dữ liệu trên form
            SetControlState(false); // Khóa các ô nhập liệu
            SetButtonState(false);  // Khóa các nút cần thiết
            LoadDataGridView(); // Cập nhật lại DataGridView
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
           
                this.Close();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Bạn phải chọn một nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditing = true;
            SetControlState(true);
            txtMaNhanVien.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Bạn phải chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuyển đổi Mã nhân viên sang kiểu int
            if (!int.TryParse(txtMaNhanVien.Text, out int maNhanVien))
            {
                MessageBox.Show("Mã nhân viên phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                var nhanVien = db.tblNhanvien.Find(maNhanVien); // Sử dụng giá trị int đã chuyển đổi
                if (nhanVien != null)
                {
                    db.tblNhanvien.Remove(nhanVien);
                    db.SaveChanges();
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetValues();
                    SetControlState(false);
                    SetButtonState(false);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            SetControlState(false);
            isAddingNew = false;
            isEditing = false;
            SetButtonState(false);
        }

        private void SetControlState(bool enable)
        {
            txtMaNhanVien.Enabled = enable;
            txtTenNhanVien.Enabled = enable;
            txtDiaChi.Enabled = enable;
            mtbDienThoai.Enabled = enable;
            dtpNgaySinh.Enabled = enable;
            chkGioiTinh.Enabled = enable; // Thay thế RadioButton bằng CheckBox
        }

        private void SetButtonState(bool canEdit)
        {
            btnThem.Enabled = !isAddingNew && !isEditing;
            btnSua.Enabled = canEdit && !isAddingNew && !isEditing;
            btnXoa.Enabled = canEdit && !isAddingNew && !isEditing;
            btnLuu.Enabled = isAddingNew || isEditing;
            btnBoQua.Enabled = isAddingNew || isEditing || canEdit;
        }

        private void frmDMNhanvien_FormClosing(object sender, FormClosingEventArgs e)
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

