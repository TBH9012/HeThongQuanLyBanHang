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
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace HeThongQuanLyBanHang
{
    public partial class frmHangTon : Form
    {
        private Model1 db = new Model1();
        public frmHangTon()
        {
            InitializeComponent();
        }

        private void frmHangTon_Load(object sender, EventArgs e)
        {
            // Load mã hàng vào ComboBox
            var hangList = db.tblHang
                             .Select(h => new { h.MaHang, h.TenHang })
                             .ToList();

            cboMaHang.DataSource = hangList;
            cboMaHang.DisplayMember = "MaHang";
            cboMaHang.ValueMember = "MaHang";
            cboMaHang.SelectedIndex = -1;

            // Thêm cột vào DataGridView nếu chưa có
            if (dgvDSHangTon.Columns.Count == 0)
            {
                dgvDSHangTon.Columns.Add("MaHang", "Mã Hàng");
                dgvDSHangTon.Columns.Add("TenHang", "Tên Hàng");
                dgvDSHangTon.Columns.Add("SoLuong", "Số Lượng");
                dgvDSHangTon.Columns.Add("DonGiaNhap", "Đơn Giá Nhập");
                dgvDSHangTon.Columns.Add("DonGiaBan", "Đơn Giá Bán");
            }

            // Làm sạch TextBox và DataGridView
            txtTenHang.Clear();
            dgvDSHangTon.Rows.Clear();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy query ban đầu
                var query = db.tblHang.Select(h => new
                {
                    h.MaHang,
                    h.TenHang,
                    h.SoLuong,
                    h.DonGiaNhap,
                    h.DonGiaBan
                });

                // Nếu chọn mã hàng, lọc theo mã hàng
                if (cboMaHang.SelectedValue != null && !string.IsNullOrEmpty(cboMaHang.SelectedValue.ToString()))
                {
                    int maHang = int.Parse(cboMaHang.SelectedValue.ToString());
                    query = query.Where(h => h.MaHang == maHang);
                }

                // Lấy dữ liệu từ query
                var hangTon = query.ToList();

                if (hangTon.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Thêm dữ liệu vào DataGridView (không xóa dữ liệu cũ)
                foreach (var item in hangTon)
                {
                    bool exists = false;

                    // Kiểm tra nếu hàng đã tồn tại
                    foreach (DataGridViewRow row in dgvDSHangTon.Rows)
                    {
                        if (row.Cells["MaHang"].Value != null && row.Cells["MaHang"].Value.ToString() == item.MaHang.ToString())
                        {
                            exists = true;
                            break;
                        }
                    }

                    // Thêm hàng mới nếu chưa tồn tại
                    if (!exists)
                    {
                        dgvDSHangTon.Rows.Add(item.MaHang, item.TenHang, item.SoLuong, item.DonGiaNhap, item.DonGiaBan);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDSHangTon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Khởi tạo Excel
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Báo Cáo Hàng Tồn";

                // Tiêu đề chính
                worksheet.Cells[1, 1] = "BÁO CÁO HÀNG TỒN";

                // Header dữ liệu
                worksheet.Cells[3, 1] = "Mã Hàng";
                worksheet.Cells[3, 2] = "Tên Hàng";
                worksheet.Cells[3, 3] = "Số Lượng";
                worksheet.Cells[3, 4] = "Đơn Giá Nhập";
                worksheet.Cells[3, 5] = "Đơn Giá Bán";

                // Đổ dữ liệu từ DataGridView vào Excel
                int rowExcel = 4; // Bắt đầu từ dòng 4 sau header
                foreach (DataGridViewRow dgvRow in dgvDSHangTon.Rows)
                {
                    if (dgvRow.Cells["MaHang"].Value != null)
                    {
                        worksheet.Cells[rowExcel, 1] = dgvRow.Cells["MaHang"].Value.ToString();
                        worksheet.Cells[rowExcel, 2] = dgvRow.Cells["TenHang"].Value.ToString();
                        worksheet.Cells[rowExcel, 3] = dgvRow.Cells["SoLuong"].Value.ToString();
                        worksheet.Cells[rowExcel, 4] = dgvRow.Cells["DonGiaNhap"].Value.ToString();
                        worksheet.Cells[rowExcel, 5] = dgvRow.Cells["DonGiaBan"].Value.ToString();
                        rowExcel++;
                    }
                }

                // Định dạng Excel
                worksheet.Columns.AutoFit();
                Excel.Range header = worksheet.get_Range("A3", "E3");
                header.Font.Bold = true;
                header.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                // Hiển thị Excel
                excelApp.Visible = true;

                // Giải phóng tài nguyên
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            // Xóa lựa chọn trong ComboBox và nội dung trong TextBox
            cboMaHang.SelectedIndex = -1; // Bỏ chọn mã hàng
            txtTenHang.Clear();           // Xóa tên hàng

            // Xóa dữ liệu trên DataGridView
            dgvDSHangTon.DataSource = null; // Xóa toàn bộ dữ liệu hiện có
            dgvDSHangTon.Rows.Clear();     // Làm sạch các hàng trong DataGridView (nếu có)
        }

        private void btnDong_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void cboMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy mã hàng từ ComboBox và hiển thị tên hàng
            if (cboMaHang.SelectedValue != null)
            {
                // Kiểm tra giá trị SelectedValue
                string selectedValue = cboMaHang.SelectedValue.ToString();
                if (int.TryParse(selectedValue, out int maHang))
                {
                    var tenHang = db.tblHang
                                    .Where(h => h.MaHang == maHang)
                                    .Select(h => h.TenHang)
                                    .FirstOrDefault();

                    txtTenHang.Text = tenHang ?? string.Empty;
                }
                else
                {
                    // Nếu không chuyển đổi được, gán giá trị rỗng
                    txtTenHang.Text = string.Empty;
                    Console.WriteLine($"Giá trị không hợp lệ: {selectedValue}");
                }
            }
        }

        private void dgvDSHangTon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấn đúp vào một hàng hợp lệ không
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSHangTon.Rows.Count)
            {
                var confirmResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa hàng này khỏi danh sách không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    // Xóa hàng được nhấn đúp
                    dgvDSHangTon.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void frmHangTon_FormClosing(object sender, FormClosingEventArgs e)
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