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
    public partial class frmDMHang : Form
    {
        private bool isAddingNew = false; // Trạng thái đang Thêm mới
        private bool isEditing = false;   // Trạng thái đang Sửa
        private Model1 db = new Model1();

        public frmDMHang()
        {
            InitializeComponent();
        }

        private void frmDMHang_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            LoadChatLieuComboBox();
            ResetValues();
            SetControlState(false);
            SetButtonState(false);
        }

        private void LoadDataGridView()
        {
            var hangList = db.tblHang.Select(h => new
            {
                h.MaHang,
                h.TenHang,
                h.MaChatlieu,
                h.SoLuong,
                h.DonGiaNhap,
                h.DonGiaBan,
                h.Anh,
                h.GhiChu
            }).ToList();

            dgvHangHoa.DataSource = hangList;
            dgvHangHoa.Columns["MaHang"].HeaderText = "Mã Hàng";
            dgvHangHoa.Columns["TenHang"].HeaderText = "Tên Hàng";
            dgvHangHoa.Columns["MaChatlieu"].HeaderText = "Mã Chất Liệu";
            dgvHangHoa.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvHangHoa.Columns["DonGiaNhap"].HeaderText = "Đơn Giá Nhập";
            dgvHangHoa.Columns["DonGiaBan"].HeaderText = "Đơn Giá Bán";
            dgvHangHoa.Columns["Anh"].HeaderText = "Ảnh";
            dgvHangHoa.Columns["GhiChu"].HeaderText = "Ghi Chú";
        }

        private void LoadChatLieuComboBox()
        {
            var chatLieuList = db.tblChatlieu.Select(cl => new { cl.MaChatlieu, cl.TenChatlieu }).ToList();
            cboMaChatLieu.DataSource = chatLieuList;
            cboMaChatLieu.DisplayMember = "TenChatlieu";
            cboMaChatLieu.ValueMember = "MaChatlieu";
        }

        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboMaChatLieu.SelectedIndex = -1;
            txtSoLuong.Text = "";
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
            txtAnh.Text = "";
            txtGhiChu.Text = "";
            picAnh.Image = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            
                this.Close();
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ResetValues();
            SetControlState(true);
            txtMaHang.Focus();
            SetButtonState(false);
            btnBoQua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHang.Text))
            {
                MessageBox.Show("Bạn phải chọn một hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuyển đổi Mã hàng sang kiểu số nguyên
            if (!int.TryParse(txtMaHang.Text, out int maHang))
            {
                MessageBox.Show("Mã hàng phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa hàng này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                var hang = db.tblHang.Find(maHang); // Tìm bằng giá trị số nguyên
                if (hang != null)
                {
                    db.tblHang.Remove(hang);
                    db.SaveChanges();
                    MessageBox.Show("Xóa hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView(); // Tải lại danh sách
                    ResetValues(); // Xóa dữ liệu trong các ô nhập liệu
                    SetControlState(false); // Khóa các ô nhập liệu
                    SetButtonState(false); // Khóa các nút
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHang.Text))
            {
                MessageBox.Show("Bạn phải chọn một hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditing = true;
            SetControlState(true);
            txtMaHang.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(txtMaHang.Text) || string.IsNullOrEmpty(txtTenHang.Text))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra và chuyển đổi dữ liệu đầu vào
            if (!int.TryParse(txtMaHang.Text, out int maHang))
            {
                MessageBox.Show("Mã hàng phải là một số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                MessageBox.Show("Số lượng phải là một số nguyên hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDonGiaNhap.Text, out decimal donGiaNhap))
            {
                MessageBox.Show("Đơn giá nhập phải là một số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDonGiaBan.Text, out decimal donGiaBan))
            {
                MessageBox.Show("Đơn giá bán phải là một số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã chất liệu từ ComboBox
            if (!int.TryParse(cboMaChatLieu.SelectedValue?.ToString(), out int maChatLieu))
            {
                MessageBox.Show("Bạn phải chọn mã chất liệu hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isAddingNew)
            {
                // Kiểm tra mã hàng đã tồn tại chưa
                var existingHang = db.tblHang.FirstOrDefault(h => h.MaHang == maHang);
                if (existingHang != null)
                {
                    MessageBox.Show("Mã hàng đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm mới hàng hóa
                var newHang = new tblHang
                {
                    MaHang = maHang,
                    TenHang = txtTenHang.Text,
                    MaChatlieu = maChatLieu,
                    SoLuong = soLuong,
                    DonGiaNhap = donGiaNhap,
                    DonGiaBan = donGiaBan,
                    Anh = txtAnh.Text,
                    GhiChu = txtGhiChu.Text
                };

                db.tblHang.Add(newHang);
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (isEditing)
            {
                // Sửa hàng hóa
                var hang = db.tblHang.Find(maHang);
                if (hang != null)
                {
                    hang.TenHang = txtTenHang.Text;
                    hang.MaChatlieu = maChatLieu;
                    hang.SoLuong = soLuong;
                    hang.DonGiaNhap = donGiaNhap;
                    hang.DonGiaBan = donGiaBan;
                    hang.Anh = txtAnh.Text;
                    hang.GhiChu = txtGhiChu.Text;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hàng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // Reset trạng thái sau khi lưu
            isAddingNew = false;
            isEditing = false;
            ResetValues();
            SetControlState(false);
            SetButtonState(false);
            LoadDataGridView();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues(); // Xóa thông tin trên textbox
            SetControlState(false); // Khóa các textbox
            isAddingNew = false;
            isEditing = false;
            SetButtonState(false); // Đặt lại trạng thái nút

            // Hiển thị lại toàn bộ danh sách
            LoadDataGridView();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Bật chế độ cho phép nhập liệu để tìm kiếm
            SetControlState(true);

            // Đảm bảo các nút khác bị khóa trong chế độ tìm kiếm
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Cho phép người dùng thoát khỏi chế độ tìm kiếm
            btnBoQua.Enabled = true;

            // Kiểm tra nếu tất cả các textbox đều trống
            if (string.IsNullOrEmpty(txtTenHang.Text) && string.IsNullOrEmpty(txtMaHang.Text))
            {
                // Nếu người dùng chưa nhập gì, chỉ mở chế độ nhập liệu mà không hiển thị thông báo
                return;
            }

            // Thực hiện tìm kiếm
            var hangList = db.tblHang.AsQueryable();

            if (!string.IsNullOrEmpty(txtTenHang.Text))
            {
                hangList = hangList.Where(h => h.TenHang.Contains(txtTenHang.Text));
            }

            if (int.TryParse(txtMaHang.Text, out int maHang))
            {
                hangList = hangList.Where(h => h.MaHang == maHang);
            }

            if (hangList.Any())
            {
                dgvHangHoa.DataSource = hangList.Select(h => new
                {
                    h.MaHang,
                    h.TenHang,
                    h.MaChatlieu,
                    h.SoLuong,
                    h.DonGiaNhap,
                    h.DonGiaBan,
                    h.Anh,
                    h.GhiChu
                }).ToList();

                MessageBox.Show("Tìm thấy kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHienThiDS_Click(object sender, EventArgs e)
        {
            // Hiển thị lại toàn bộ danh sách hàng hóa
            var hangList = db.tblHang.Select(h => new
            {
                h.MaHang,
                h.TenHang,
                h.MaChatlieu,
                h.SoLuong,
                h.DonGiaNhap,
                h.DonGiaBan,
                h.Anh,
                h.GhiChu
            }).ToList();

            dgvHangHoa.DataSource = hangList; // Gán dữ liệu lại cho DataGridView

            // Đặt lại các ô tìm kiếm
            txtTenHang.Clear();
            cboMaChatLieu.SelectedIndex = -1;
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtAnh.Text = ofd.FileName;
                picAnh.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void dgvHangHoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaHang.Text = dgvHangHoa.Rows[e.RowIndex].Cells["MaHang"].Value.ToString();
                txtTenHang.Text = dgvHangHoa.Rows[e.RowIndex].Cells["TenHang"].Value.ToString();
                txtSoLuong.Text = dgvHangHoa.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
                txtDonGiaNhap.Text = dgvHangHoa.Rows[e.RowIndex].Cells["DonGiaNhap"].Value.ToString();
                txtDonGiaBan.Text = dgvHangHoa.Rows[e.RowIndex].Cells["DonGiaBan"].Value.ToString();
                txtAnh.Text = dgvHangHoa.Rows[e.RowIndex].Cells["Anh"].Value.ToString();
                txtGhiChu.Text = dgvHangHoa.Rows[e.RowIndex].Cells["GhiChu"].Value.ToString();

                // Hiển thị mã chất liệu trong ComboBox
                if (dgvHangHoa.Rows[e.RowIndex].Cells["MaChatLieu"].Value != null)
                {
                    cboMaChatLieu.SelectedValue = Convert.ToInt32(dgvHangHoa.Rows[e.RowIndex].Cells["MaChatLieu"].Value);
                }

                // Hiển thị ảnh trong PictureBox
                string imagePath = dgvHangHoa.Rows[e.RowIndex].Cells["Anh"].Value.ToString();
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    picAnh.Image = Image.FromFile(imagePath);
                }
                else
                {
                    picAnh.Image = null;
                }

                SetControlState(false); // Khóa các ô nhập liệu
                SetButtonState(true);  // Bật các nút Xóa và Sửa
                btnLuu.Enabled = false; // Đảm bảo nút Lưu bị khóa
            }
        }

        private void SetControlState(bool enable)
        {
            txtMaHang.Enabled = enable;
            txtTenHang.Enabled = enable;
            cboMaChatLieu.Enabled = enable;
            txtSoLuong.Enabled = enable;
            txtDonGiaNhap.Enabled = enable;
            txtDonGiaBan.Enabled = enable;
            txtAnh.Enabled = enable;
            txtGhiChu.Enabled = enable;
            btnMo.Enabled = enable;
        }

        private void SetButtonState(bool canEdit)
        {
            btnThem.Enabled = !isAddingNew && !isEditing;
            btnSua.Enabled = canEdit && !isAddingNew && !isEditing;
            btnXoa.Enabled = canEdit && !isAddingNew && !isEditing;
            btnLuu.Enabled = isAddingNew || isEditing;
            btnBoQua.Enabled = isAddingNew || isEditing || canEdit || true; // Bỏ qua luôn được bật
            btnTimKiem.Enabled = true; // Tìm kiếm luôn bật
        }

        private void frmDMHang_FormClosing(object sender, FormClosingEventArgs e)
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
