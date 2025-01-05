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
    public partial class frmmain : Form
    {
        public frmmain()
        {
            InitializeComponent();
        }

        private void frmmain_Load(object sender, EventArgs e)
        {
            Class.KetNoidatabase.Connect();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Class.KetNoidatabase.Disconnect();
            Application.Exit();
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            frmDMChatLieu frmChatLieu = new frmDMChatLieu(); //Khởi tạo đối tượng
            frmChatLieu.ShowDialog();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {

            frmDMNhanvien frmNhanvien = new frmDMNhanvien(); //Khởi tạo đối tượng
            frmNhanvien.ShowDialog();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmDMKhachHang frmNKhachhang = new frmDMKhachHang(); //Khởi tạo đối tượng
            frmNKhachhang.ShowDialog();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDonBan frmNHoadonban = new frmHoaDonBan(); //Khởi tạo đối tượng
            frmNHoadonban.ShowDialog();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            frmDMHang frmHang = new frmDMHang(); //Khởi tạo đối tượng
            frmHang.ShowDialog();
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            frmTimHDBan frmTimHDBan = new frmTimHDBan(); //Khởi tạo đối tượng
            frmTimHDBan.ShowDialog();
        }

        private void mnuHienTroGiup_Click(object sender, EventArgs e)
        {
            frmTroGiup frmTroGiup = new frmTroGiup(); //Khởi tạo đối tượng
            frmTroGiup.ShowDialog();
        }

        private void mnuFindHang_Click(object sender, EventArgs e)
        {
            frmTimHang frmTimHang = new frmTimHang(); //Khởi tạo đối tượng
            frmTimHang.ShowDialog();
        }

        private void mnuFindKhachHang_Click(object sender, EventArgs e)
        {
            frmTimKhach frmTimKhach = new frmTimKhach(); //Khởi tạo đối tượng
            frmTimKhach.ShowDialog();
        }

        private void mnuBCHangTon_Click(object sender, EventArgs e)
        {
            frmHangTon frmHangTon = new frmHangTon(); //Khởi tạo đối tượng
            frmHangTon.ShowDialog();
        }

        private void mnuBCDoanhThu_Click(object sender, EventArgs e)
        {
            frmDoanhThu frmDoanhThu = new frmDoanhThu(); //Khởi tạo đối tượng
            frmDoanhThu.ShowDialog();
        }

        private void frmmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            } } }}

