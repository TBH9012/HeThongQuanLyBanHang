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
    public partial class frmDoanhThu : Form
    {
        private Model1 db = new Model1();
        public frmDoanhThu()
        {
            InitializeComponent();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (cboMaNhanVien.SelectedValue == null || dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên và ngày bắt đầu/ kết thúc hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maNhanVien = int.Parse(cboMaNhanVien.SelectedValue.ToString());
                DateTime ngayBatDau = dtpNgayBatDau.Value.Date;
                DateTime ngayKetThuc = dtpNgayKetThuc.Value.Date;

                // Truy vấn dữ liệu doanh thu
                var doanhThu = db.tblHDBan
                                 .Where(hd => hd.MaNhanvien == maNhanVien &&
                                              hd.NgayBan >= ngayBatDau && hd.NgayBan <= ngayKetThuc)
                                 .Select(hd => new
                                 {
                                     hd.MaHDBan,
                                     hd.NgayBan,
                                     hd.TongTien
                                 }).ToList();

                if (doanhThu.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu báo cáo trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Khởi tạo Excel
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Báo Cáo Doanh Thu";

                // Tiêu đề
                worksheet.Cells[1, 1] = "BÁO CÁO DOANH THU";
                worksheet.Cells[2, 1] = $"Nhân viên: {txtTenNhanVien.Text}";
                worksheet.Cells[3, 1] = $"Từ ngày {ngayBatDau:dd/MM/yyyy} đến ngày {ngayKetThuc:dd/MM/yyyy}";

                // Header
                worksheet.Cells[5, 1] = "STT";
                worksheet.Cells[5, 2] = "Mã Hóa Đơn";
                worksheet.Cells[5, 3] = "Ngày Bán";
                worksheet.Cells[5, 4] = "Tổng Tiền (VND)";

                // Đổ dữ liệu vào Excel
                int row = 6;
                int stt = 1;
                decimal tongDoanhThu = 0;

                foreach (var item in doanhThu)
                {
                    worksheet.Cells[row, 1] = stt++;
                    worksheet.Cells[row, 2] = item.MaHDBan;
                    worksheet.Cells[row, 3] = item.NgayBan.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 4] = item.TongTien;
                    tongDoanhThu += item.TongTien ?? 0;
                    row++;
                }

                // Tổng doanh thu
                worksheet.Cells[row, 3] = "TỔNG DOANH THU:";
                worksheet.Cells[row, 4] = tongDoanhThu;

                // Định dạng cột Tổng Tiền
                ((Excel.Range)worksheet.Columns[4]).NumberFormat = "#,##0";

                // Định dạng tiêu đề
                Excel.Range title = worksheet.get_Range("A1", "D1");
                title.Merge();
                title.Font.Size = 16;
                title.Font.Bold = true;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Định dạng header
                Excel.Range header = worksheet.get_Range("A5", "D5");
                header.Font.Bold = true;
                header.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                // Auto-fit cột
                worksheet.Columns.AutoFit();

                // Hiển thị Excel
                excelApp.Visible = true;

                // Giải phóng tài nguyên
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            try
            {
                // Làm sạch ComboBox Mã nhân viên
                cboMaNhanVien.SelectedIndex = -1;

                // Làm sạch TextBox Tên nhân viên
                txtTenNhanVien.Text = string.Empty;

                // Đặt lại DateTimePicker Ngày bắt đầu và Ngày kết thúc về ngày hiện tại
                dtpNgayBatDau.Value = DateTime.Now;
                dtpNgayKetThuc.Value = DateTime.Now;
            }
            catch
            {
                // Bỏ trống catch để không hiển thị bất kỳ thông báo nào
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
           
                this.Close();
            
        }

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNhanVien.DataSource == null || cboMaNhanVien.SelectedValue == null)
                return;

            if (int.TryParse(cboMaNhanVien.SelectedValue?.ToString(), out int maNhanVien))
            {
                // Tìm tên nhân viên tương ứng từ cơ sở dữ liệu
                var nhanVien = db.tblNhanvien
                                 .Where(nv => nv.MaNhanvien == maNhanVien)
                                 .Select(nv => nv.TenNhanvien)
                                 .FirstOrDefault();

                // Gán tên nhân viên vào TextBox
                txtTenNhanVien.Text = nhanVien ?? string.Empty;
            }
            else
            {
                txtTenNhanVien.Text = string.Empty; // Gán giá trị rỗng nếu không hợp lệ
            }
        }

        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
            var nhanVienList = db.tblNhanvien
                         .Select(nv => new { nv.MaNhanvien, nv.TenNhanvien })
                         .ToList();

            cboMaNhanVien.DataSource = nhanVienList;
            cboMaNhanVien.DisplayMember = "MaNhanvien"; // Hiển thị Mã nhân viên
            cboMaNhanVien.ValueMember = "MaNhanvien";   // Giá trị là Mã nhân viên
            cboMaNhanVien.SelectedIndex = -1; // Không chọn gì ban đầu
        }

        private void frmDoanhThu_FormClosing(object sender, FormClosingEventArgs e)
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
