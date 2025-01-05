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
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Data.Entity;
using System.Globalization;

namespace HeThongQuanLyBanHang
{
    public partial class frmHoaDonBan : Form
    {
        private Model1 db = new Model1();

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadDataGridView();
            LoadMaHoaDon(); // Load danh sách mã hóa đơn vào combobox
            ResetValues();
            SetControlState(false);
            UpdateTongTien();
        }

        private void LoadComboBoxData()
        {
            // Load mã nhân viên
            cboMaNhanVien.DataSource = db.tblNhanvien.Select(nv => new { nv.MaNhanvien, nv.TenNhanvien }).ToList();
            cboMaNhanVien.DisplayMember = "MaNhanvien";
            cboMaNhanVien.ValueMember = "MaNhanvien";
            cboMaNhanVien.SelectedIndex = -1;

            // Load mã khách hàng
            cboMaKhach.DataSource = db.tblKhach.Select(kh => new { kh.MaKhach, kh.TenKhach }).ToList();
            cboMaKhach.DisplayMember = "MaKhach";
            cboMaKhach.ValueMember = "MaKhach";
            cboMaKhach.SelectedIndex = -1;

            // Load mã hàng
            cboMaHang.DataSource = db.tblHang.Select(h => new { h.MaHang, h.TenHang }).ToList();
            cboMaHang.DisplayMember = "MaHang";
            cboMaHang.ValueMember = "MaHang";
            cboMaHang.SelectedIndex = -1;
        }

        private void LoadDataGridView()
        {
            dgvHDBanHang.Rows.Clear();
            dgvHDBanHang.ColumnCount = 6;
            dgvHDBanHang.Columns[0].Name = "MaHang";
            dgvHDBanHang.Columns[1].Name = "TenHang";  // Tên hàng
            dgvHDBanHang.Columns[2].Name = "SoLuong"; // Số lượng
            dgvHDBanHang.Columns[3].Name = "DonGia";  // Đơn giá
            dgvHDBanHang.Columns[4].Name = "GiamGia"; // Giảm giá
            dgvHDBanHang.Columns[5].Name = "ThanhTien"; // Thành tiền
        }


        private void ResetValues()
        {
            txtMaHoaDon.Clear();
            txtTenNhanVien.Clear();
            txtTenKhach.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtTenHang.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            txtThanhTien.Clear();
            txtTongTien.Clear();
            lblBangChu.Text = "Bằng chữ: ";
            cboMaNhanVien.SelectedIndex = -1;
            cboMaKhach.SelectedIndex = -1;
            cboMaHang.SelectedIndex = -1;
        }

        private string GenerateMaHoaDon()
        {
            int? maxMaHoaDon = db.tblHDBan.Max(hd => (int?)hd.MaHDBan);
            int newMa = (maxMaHoaDon ?? 0) + 1;
            return newMa.ToString();
        }

        private void btnDongg_Click(object sender, EventArgs e)
        {
           
                this.Close();
            
        }

        private void frmHoaDonBan_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Reset các giá trị của form
            ResetValues();
            txtMaHoaDon.Text = GenerateMaHoaDon(); // Tự động tạo mã hóa đơn
            dtpNgayBan.Value = DateTime.Now; // Gán ngày hiện tại
            SetControlState(true); // Mở khóa các điều khiển nhập liệu
            cboMaNhanVien.Focus(); // Đặt focus vào ComboBox mã nhân viên
            dgvHDBanHang.Rows.Clear(); // Xóa dữ liệu cũ trong DataGridView
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (cboMaHang.SelectedValue == null || string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn mã hàng và nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm sản phẩm vào DataGridView
            dgvHDBanHang.Rows.Add(
                cboMaHang.SelectedValue,        // Mã hàng
                txtTenHang.Text,                // Tên hàng
                txtSoLuong.Text,                // Số lượng
                txtDonGia.Text,                 // Đơn giá
                txtGiamGia.Text,                // Giảm giá
                txtThanhTien.Text               // Thành tiền
            );

            // Cập nhật tổng tiền
            UpdateTongTien();

            // Reset các trường dữ liệu mặt hàng
            ResetValuesHang();
        }

        private void ResetValuesHang()
        {
            cboMaHang.SelectedIndex = -1;
            txtTenHang.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            txtThanhTien.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetValues();
                dgvHDBanHang.Rows.Clear();
                UpdateTongTien();
            }
        }

        private void cboMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHang.SelectedValue != null && int.TryParse(cboMaHang.SelectedValue.ToString(), out int maHang))
            {
                var hang = db.tblHang.FirstOrDefault(h => h.MaHang == maHang);
                txtTenHang.Text = hang?.TenHang ?? string.Empty;
                txtDonGia.Text = hang?.DonGiaBan.HasValue == true ? hang.DonGiaBan.Value.ToString("N0") : "0";
            }
        }

        private void cboMaKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKhach.SelectedValue != null && int.TryParse(cboMaKhach.SelectedValue.ToString(), out int maKhach))
            {
                var khach = db.tblKhach.FirstOrDefault(kh => kh.MaKhach == maKhach);
                txtTenKhach.Text = khach?.TenKhach ?? string.Empty;
                txtDiaChi.Text = khach?.DiaChi ?? string.Empty;
                txtDienThoai.Text = khach?.DienThoai ?? string.Empty;
            }
        }

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNhanVien.SelectedValue != null && int.TryParse(cboMaNhanVien.SelectedValue.ToString(), out int maNhanVien))
            {
                var nhanVien = db.tblNhanvien.FirstOrDefault(nv => nv.MaNhanvien == maNhanVien);
                txtTenNhanVien.Text = nhanVien?.TenNhanvien ?? string.Empty;
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            UpdateThanhTien();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            UpdateThanhTien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaHD.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn mã hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maHoaDon = Convert.ToInt32(cboMaHD.SelectedValue);

            // Tìm hóa đơn
            var hoaDon = db.tblHDBan.Include("tblChitietHDBan").FirstOrDefault(hd => hd.MaHDBan == maHoaDon);
            if (hoaDon != null)
            {
                // Hiển thị thông tin hóa đơn
                txtMaHoaDon.Text = hoaDon.MaHDBan.ToString();
                dtpNgayBan.Value = hoaDon.NgayBan;
                cboMaNhanVien.SelectedValue = hoaDon.MaNhanvien;
                cboMaKhach.SelectedValue = hoaDon.MaKhach;
                txtTongTien.Text = hoaDon.TongTien?.ToString("N0");
                lblBangChu.Text = "Bằng chữ: " + NumberToText((long)hoaDon.TongTien);

                // Hiển thị chi tiết hóa đơn
                dgvHDBanHang.Rows.Clear();
                foreach (var chiTiet in hoaDon.tblChitietHDBan)
                {
                    dgvHDBanHang.Rows.Add(
                        chiTiet.MaHang,
                        chiTiet.tblHang?.TenHang,
                        chiTiet.SoLuong,
                        chiTiet.DonGia,
                        chiTiet.GiamGia,
                        chiTiet.ThanhTien
                    );
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHD.SelectedValue != null && int.TryParse(cboMaHD.SelectedValue.ToString(), out int maHoaDon))
            {
                LoadHoaDon(maHoaDon); // Gọi hàm LoadHoaDon
            }
        }

        private void dgvHDBanHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblBangChu_Click(object sender, EventArgs e)
        {

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // In hóa đơn bằng Excel
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            try
            {
                // Tạo ứng dụng Excel
                COMExcel.Application excel = new COMExcel.Application();
                COMExcel.Workbook workbook = excel.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
                COMExcel.Worksheet worksheet = (COMExcel.Worksheet)workbook.Worksheets[1];

                // Thông tin chung
                worksheet.Cells[1, 1] = "Shop Bán Hàng";
                worksheet.Cells[2, 1] = "Địa chỉ: Thủ Đức - TP.HCM";
                worksheet.Cells[3, 1] = "Điện thoại: (032) 6829012";
                worksheet.Cells[5, 3] = "HÓA ĐƠN BÁN HÀNG";

                worksheet.Cells[7, 1] = "Mã hóa đơn: " + txtMaHoaDon.Text;
                worksheet.Cells[8, 1] = "Khách hàng: " + txtTenKhach.Text;
                worksheet.Cells[9, 1] = "Địa chỉ: " + txtDiaChi.Text;
                worksheet.Cells[10, 1] = "Điện thoại: " + txtDienThoai.Text;

                // Tiêu đề bảng
                worksheet.Cells[12, 1] = "STT";
                worksheet.Cells[12, 2] = "Tên hàng";
                worksheet.Cells[12, 3] = "Số lượng";
                worksheet.Cells[12, 4] = "Đơn giá";
                worksheet.Cells[12, 5] = "Giảm giá (%)";
                worksheet.Cells[12, 6] = "Thành tiền";

                // Nội dung bảng
                int row = 13;
                for (int i = 0; i < dgvHDBanHang.Rows.Count; i++)
                {
                    worksheet.Cells[row, 1] = (i + 1).ToString(); // STT
                    worksheet.Cells[row, 2] = dgvHDBanHang.Rows[i].Cells["TenHang"].Value?.ToString();
                    worksheet.Cells[row, 3] = dgvHDBanHang.Rows[i].Cells["SoLuong"].Value?.ToString();
                    worksheet.Cells[row, 4] = dgvHDBanHang.Rows[i].Cells["DonGia"].Value?.ToString();
                    worksheet.Cells[row, 5] = dgvHDBanHang.Rows[i].Cells["GiamGia"].Value?.ToString();
                    worksheet.Cells[row, 6] = dgvHDBanHang.Rows[i].Cells["ThanhTien"].Value?.ToString();
                    row++;
                }

                worksheet.Cells[row + 1, 5] = "Tổng tiền:";
                worksheet.Cells[row + 1, 6] = txtTongTien.Text;

                excel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateThanhTien()
        {
            if (decimal.TryParse(txtDonGia.Text, out decimal donGia) &&
        int.TryParse(txtSoLuong.Text, out int soLuong) &&
        decimal.TryParse(txtGiamGia.Text, out decimal giamGia))
            {
                // Thành tiền = Đơn giá * Số lượng * (1 - Giảm giá / 100)
                decimal thanhTien = donGia * soLuong * (1 - giamGia / 100);
                txtThanhTien.Text = thanhTien.ToString("N0"); // Hiển thị định dạng số
            }
            else
            {
                txtThanhTien.Text = "0";
            }
        }

        private void UpdateTongTien()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvHDBanHang.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null &&
                    decimal.TryParse(row.Cells["ThanhTien"].Value.ToString(), out decimal thanhTien))
                {
                    tongTien += thanhTien;
                }
            }

            txtTongTien.Text = tongTien.ToString("N0"); // Hiển thị định dạng số
            lblBangChu.Text = "Bằng chữ: " + NumberToText((long)tongTien);
        }

        private void SetControlState(bool enable)
        {
            // Bật/tắt trạng thái cho các ComboBox và TextBox
            cboMaNhanVien.Enabled = enable;
            cboMaKhach.Enabled = enable;
            cboMaHang.Enabled = enable;
            txtSoLuong.Enabled = enable;
            txtGiamGia.Enabled = enable;

            // Nút lưu hóa đơn, in hóa đơn, hủy hóa đơn
            btnLuu.Enabled = enable;
            btnIn.Enabled = enable;
            btnXoa.Enabled = enable;

            // Mã hóa đơn và các thông tin không cho phép chỉnh sửa
            txtMaHoaDon.ReadOnly = !enable; // Mã hóa đơn chỉ đọc
            txtTenNhanVien.ReadOnly = true; // Tên nhân viên tự động
            txtTenKhach.ReadOnly = true; // Tên khách hàng tự động
            txtDiaChi.ReadOnly = true; // Địa chỉ tự động
            txtDienThoai.ReadOnly = true; // Điện thoại tự động
            txtTenHang.ReadOnly = true; // Tên hàng tự động
            txtDonGia.ReadOnly = true; // Đơn giá tự động
            txtThanhTien.ReadOnly = true; // Thành tiền tự động
            txtTongTien.ReadOnly = true; // Tổng tiền tự động
        }

        private void dgvHDBanHang_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void dgvHDBanHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dgvHDBanHang.Rows.RemoveAt(e.RowIndex);
                    UpdateTongTien();
                }
            }
        }

        private string NumberToText(long number)
        {
            if (number == 0) return "Không đồng";

            string[] units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] scales = { "", "nghìn", "triệu", "tỷ" };

            string result = "";
            int scaleIndex = 0;

            while (number > 0)
            {
                int group = (int)(number % 1000);
                number /= 1000;

                if (group > 0)
                {
                    string groupText = "";
                    int hundreds = group / 100;
                    int tens = (group % 100) / 10;
                    int unitsPlace = group % 10;

                    if (hundreds > 0) groupText += units[hundreds] + " trăm ";
                    if (tens > 1) groupText += units[tens] + " mươi ";
                    else if (tens == 1) groupText += "mười ";

                    if (unitsPlace > 0)
                    {
                        if (tens > 1 && unitsPlace == 5) groupText += "lăm";
                        else groupText += units[unitsPlace];
                    }

                    result = groupText + " " + scales[scaleIndex] + " " + result;
                }

                scaleIndex++;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.Trim()) + " đồng";
        }

        private void LoadMaHoaDon()
        {
            var dsMaHoaDon = db.tblHDBan.Select(hd => new
            {
                MaHDBan = hd.MaHDBan
            }).ToList();

            cboMaHD.DataSource = dsMaHoaDon;
            cboMaHD.DisplayMember = "MaHDBan";
            cboMaHD.ValueMember = "MaHDBan";
            cboMaHD.SelectedIndex = -1;
        }

        public void LoadHoaDon(int maHoaDon)
        {
            // Gọi LoadDataGridView để đảm bảo DataGridView có cấu trúc cột trước khi thêm dữ liệu
            LoadDataGridView();

            // Lấy hóa đơn từ cơ sở dữ liệu
            var hoaDon = db.tblHDBan.Include("tblChitietHDBan").FirstOrDefault(hd => hd.MaHDBan == maHoaDon);

            if (hoaDon != null)
            {
                // Hiển thị thông tin hóa đơn
                txtMaHoaDon.Text = hoaDon.MaHDBan.ToString();
                dtpNgayBan.Value = hoaDon.NgayBan;
                cboMaNhanVien.SelectedValue = hoaDon.MaNhanvien;
                cboMaKhach.SelectedValue = hoaDon.MaKhach;
                txtTongTien.Text = hoaDon.TongTien?.ToString("N0");
                lblBangChu.Text = "Bằng chữ: " + NumberToText((long)hoaDon.TongTien);

                // Hiển thị chi tiết hóa đơn
                dgvHDBanHang.Rows.Clear(); // Xóa các dòng cũ (nếu có)
                foreach (var chiTiet in hoaDon.tblChitietHDBan)
                {
                    dgvHDBanHang.Rows.Add(
                        chiTiet.MaHang,
                        chiTiet.tblHang?.TenHang,
                        chiTiet.SoLuong,
                        chiTiet.DonGia,
                        chiTiet.GiamGia,
                        chiTiet.ThanhTien
                    );
                }

                // Đặt trạng thái cho các điều khiển
                SetControlState(false); // Không cho phép chỉnh sửa
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin cơ bản
            if (string.IsNullOrEmpty(txtMaHoaDon.Text) || cboMaNhanVien.SelectedValue == null || cboMaKhach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin hóa đơn trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mã hóa đơn đã tồn tại
            int maHoaDon = int.Parse(txtMaHoaDon.Text);
            if (db.tblHDBan.Any(hd => hd.MaHDBan == maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn đã tồn tại! Vui lòng thử lại với mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Tạo hóa đơn mới
                var hoaDon = new tblHDBan
                {
                    MaHDBan = maHoaDon,
                    NgayBan = dtpNgayBan.Value,
                    MaNhanvien = int.Parse(cboMaNhanVien.SelectedValue.ToString()),
                    MaKhach = int.Parse(cboMaKhach.SelectedValue.ToString()),
                    TongTien = decimal.Parse(txtTongTien.Text.Replace(",", ""))
                };

                db.tblHDBan.Add(hoaDon);

                // Lưu từng chi tiết sản phẩm vào cơ sở dữ liệu
                foreach (DataGridViewRow row in dgvHDBanHang.Rows)
                {
                    if (row.Cells["MaHang"].Value != null)
                    {
                        int maHang = int.Parse(row.Cells["MaHang"].Value.ToString());
                        if (db.tblChitietHDBan.Any(ct => ct.MaHDBan == maHoaDon && ct.MaHang == maHang))
                        {
                            MessageBox.Show($"Sản phẩm {maHang} đã tồn tại trong hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        var chiTiet = new tblChitietHDBan
                        {
                            MaHDBan = maHoaDon,
                            MaHang = maHang,
                            SoLuong = int.Parse(row.Cells["SoLuong"].Value.ToString()),
                            DonGia = decimal.Parse(row.Cells["DonGia"].Value.ToString()),
                            GiamGia = decimal.Parse(row.Cells["GiamGia"].Value.ToString()),
                            ThanhTien = decimal.Parse(row.Cells["ThanhTien"].Value.ToString())
                        };

                        db.tblChitietHDBan.Add(chiTiet);
                    }
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                MessageBox.Show("Hóa đơn đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật danh sách mã hóa đơn
                LoadMaHoaDon();

                // Reset form
                ResetValues();
                dgvHDBanHang.Rows.Clear();
                UpdateTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}