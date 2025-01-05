using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongQuanLyBanHang
{
    public partial class frmTroGiup : Form
    {
        public frmTroGiup()
        {
            InitializeComponent();
        }

        private string[] noiDungTroGiup = new string[]
{
    // Danh mục
    "1. Chất liệu: Vào 'Danh mục' > Chọn 'Chất liệu' để quản lý danh sách chất liệu.",
    "2. Hàng hóa: Vào 'Danh mục' > Chọn 'Hàng hóa' để thêm, sửa hoặc xóa thông tin hàng hóa.",
    "3. Nhân viên: Vào 'Danh mục' > Chọn 'Nhân viên' để quản lý thông tin nhân viên.",
    "4. Khách hàng: Vào 'Danh mục' > Chọn 'Khách hàng' để thêm hoặc sửa thông tin khách hàng.",

    // Hóa đơn
    "5. Hóa đơn bán: Vào 'Hóa đơn' > Chọn 'Hóa đơn bán' để xem hoặc thêm hóa đơn mới.",
    "6. In hóa đơn: Sau khi tạo hóa đơn, nhấn nút 'In' để in hóa đơn.",

    // Tìm kiếm
    "7. Tìm kiếm hóa đơn: Vào 'Tìm kiếm' > Chọn 'Hóa đơn' để tìm kiếm hóa đơn theo mã hoặc ngày.",
    "8. Tìm kiếm hàng hóa: Vào 'Tìm kiếm' > Chọn 'Hàng' để tìm kiếm hàng hóa theo tên hoặc mã.",
    "9. Tìm kiếm khách hàng: Vào 'Tìm kiếm' > Chọn 'Khách hàng' để tìm kiếm khách hàng theo tên hoặc số điện thoại.",

    // Báo cáo
    "10. Báo cáo hàng tồn: Vào 'Báo cáo' > Chọn 'Hàng tồn' để xem báo cáo tồn kho theo sản phẩm.",
    "11. Báo cáo doanh thu: Vào 'Báo cáo' > Chọn 'Doanh thu' để xem tổng doanh thu theo ngày, tháng hoặc năm.",

    // Trợ giúp
    "12. Trợ giúp: Vào 'Trợ giúp' để tìm các hướng dẫn sử dụng phần mềm."
};

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemTroGiup(); // Gọi hàm tìm kiếm khi nhấn nút
        }

        private void frmTroGiup_Load(object sender, EventArgs e)
        {
            // Hiển thị tất cả nội dung trợ giúp khi form được load
            listBoxTroGiup.Items.AddRange(noiDungTroGiup);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemTroGiup(); // Gọi hàm tìm kiếm khi TextBox thay đổi
        }

        private void TimKiemTroGiup()
        {
            string tuKhoa = txtTimKiem.Text.ToLower(); // Lấy từ khóa từ TextBox
            listBoxTroGiup.Items.Clear(); // Xóa danh sách cũ

            // Lọc nội dung chứa từ khóa
            foreach (string noiDung in noiDungTroGiup)
            {
                if (noiDung.ToLower().Contains(tuKhoa))
                {
                    listBoxTroGiup.Items.Add(noiDung);
                }
            }

            // Hiển thị thông báo nếu không tìm thấy kết quả
            if (listBoxTroGiup.Items.Count == 0)
            {
                listBoxTroGiup.Items.Add("Không tìm thấy kết quả phù hợp.");
            }
        }

        private void frmTroGiup_FormClosing(object sender, FormClosingEventArgs e)
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
