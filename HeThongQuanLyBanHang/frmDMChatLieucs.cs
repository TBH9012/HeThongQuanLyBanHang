using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 
using HeThongQuanLyBanHang.Class;

namespace HeThongQuanLyBanHang
{
    public partial class frmDMChatLieu : Form
    {
        private bool isAddingNew = false; // Trạng thái đang Thêm mới
        private bool isEditing = false;   // Trạng thái đang Sửa
        private Model1 db = new Model1();

        public frmDMChatLieu()
        {
            InitializeComponent();
        }


        private void frmDMChatLieu_Load(object sender, EventArgs e)
        {
            LoadDataGridView(); // Tải dữ liệu vào DataGridView
            ResetValue();       // Xóa dữ liệu trong các ô nhập liệu
            SetControlState(false); // Khóa các ô nhập liệu
            isAddingNew = false; // Không trong trạng thái Thêm
            isEditing = false;   // Không trong trạng thái Sửa
            SetButtonState(false); // Đặt trạng thái ban đầu (khóa các nút cần thiết)
        }
        private void LoadDataGridView()
        {
            var chatLieuList = db.tblChatlieu.Select(cl => new
            {
                cl.MaChatlieu,
                cl.TenChatlieu
            }).ToList();

            dgvChatLieu.DataSource = chatLieuList;
            dgvChatLieu.Columns["MaChatlieu"].HeaderText = "Mã Chất Liệu";
            dgvChatLieu.Columns["TenChatlieu"].HeaderText = "Tên Chất Liệu";
        }

        private void dgvChatLieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaChatLieu.Text = dgvChatLieu.Rows[e.RowIndex].Cells["MaChatlieu"].Value.ToString();
                txtTenChatLieu.Text = dgvChatLieu.Rows[e.RowIndex].Cells["TenChatlieu"].Value.ToString();

                SetControlState(false); // Khóa các ô nhập liệu
                SetButtonState(true);   // Bật các nút Xóa và Sửa
                btnLuu.Enabled = false; // Đảm bảo nút Lưu bị khóa
                btnBoQua.Enabled = true; // Mở khóa nút Bỏ qua
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ResetValue(); // Xóa dữ liệu
            SetControlState(true); // Mở khóa các ô nhập liệu
            txtMaChatLieu.Focus(); // Đặt focus vào ô Mã chất liệu
            SetButtonState(false); // Khóa các nút Xóa, Sửa, Lưu
            btnBoQua.Enabled = true; // Mở khóa nút Bỏ qua
        }

        private void ResetValue() // xóa hết dữ liệu trong các điều khiển trên Form.
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaChatLieu.Text) || string.IsNullOrEmpty(txtTenChatLieu.Text))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maChatLieu;
            if (!int.TryParse(txtMaChatLieu.Text, out maChatLieu))
            {
                MessageBox.Show("Mã chất liệu phải là một số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isAddingNew)
            {
                // Kiểm tra mã chất liệu có tồn tại không
                var existingChatLieu = db.tblChatlieu.FirstOrDefault(cl => cl.MaChatlieu == maChatLieu);
                if (existingChatLieu != null)
                {
                    MessageBox.Show("Mã chất liệu đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm mới chất liệu
                var newChatLieu = new tblChatlieu
                {
                    MaChatlieu = maChatLieu,
                    TenChatlieu = txtTenChatLieu.Text
                };

                db.tblChatlieu.Add(newChatLieu);
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (isEditing)
            {
                // Sửa chất liệu
                var chatLieu = db.tblChatlieu.Find(maChatLieu);
                if (chatLieu != null)
                {
                    chatLieu.TenChatlieu = txtTenChatLieu.Text;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Reset trạng thái
            isAddingNew = false;
            isEditing = false;
            ResetValue(); // Xóa dữ liệu
            SetControlState(false); // Khóa các ô nhập liệu
            SetButtonState(false);  // Khóa các nút cần thiết
            LoadDataGridView(); // Cập nhật lại DataGridView
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaChatLieu.Text))
            {
                MessageBox.Show("Bạn phải chọn một chất liệu để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditing = true; // Đặt trạng thái Sửa là true
            SetControlState(true); // Mở khóa các ô nhập liệu
            txtMaChatLieu.Enabled = false; // Không cho sửa Mã chất liệu
            btnXoa.Enabled = false; // Khóa nút Xóa
            btnBoQua.Enabled = true; // Mở khóa nút Bỏ qua
            btnLuu.Enabled = true; // Mở khóa nút Lưu
            btnSua.Enabled = false; // Khóa nút Sửa
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaChatLieu.Text))
            {
                MessageBox.Show("Bạn phải chọn một chất liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maChatLieu = int.Parse(txtMaChatLieu.Text);
            var relatedHangs = db.tblHang.Where(h => h.MaChatlieu == maChatLieu).ToList();

            if (relatedHangs.Count > 0)
            {
                MessageBox.Show("Không thể xóa chất liệu này vì có dữ liệu liên quan trong bảng Hàng. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa chất liệu này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                var chatLieu = db.tblChatlieu.Find(maChatLieu);
                if (chatLieu != null)
                {
                    db.tblChatlieu.Remove(chatLieu);
                    db.SaveChanges();
                    MessageBox.Show("Xóa chất liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetValue();
                    SetControlState(false);
                    SetButtonState(false);
                    btnBoQua.Enabled = false;
                }
            }
        }
                
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue(); // Xóa dữ liệu trong các ô nhập liệu
            SetControlState(false); // Khóa các ô nhập liệu
            isAddingNew = false; // Đặt trạng thái Thêm về false
            isEditing = false;   // Đặt trạng thái Sửa về false
            SetButtonState(false); // Khóa tất cả các nút cần thiết
        }

        private void txtMaChatLieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetControlState(bool enable)
        {
            txtMaChatLieu.Enabled = enable;
            txtTenChatLieu.Enabled = enable;
        }

        private void SetButtonState(bool canEdit)
        {
            btnThem.Enabled = !isAddingNew && !isEditing; // Nút Thêm bật nếu không Thêm hoặc Sửa
            btnSua.Enabled = canEdit && !isAddingNew && !isEditing; // Nút Sửa bật khi có thể chỉnh sửa
            btnXoa.Enabled = canEdit && !isAddingNew && !isEditing; // Nút Xóa bật khi có thể xóa
            btnLuu.Enabled = isAddingNew || isEditing; // Nút Lưu bật khi đang Thêm hoặc Sửa
            btnBoQua.Enabled = isAddingNew || isEditing; // Nút Bỏ qua bật khi đang Thêm hoặc Sửa
        }

        private void frmDMChatLieu_FormClosing(object sender, FormClosingEventArgs e)
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