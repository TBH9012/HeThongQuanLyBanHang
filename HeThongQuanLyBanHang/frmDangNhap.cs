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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTK.Text.Length == 0 && txtMK.Text.Length == 0)
            {
                MessageBox.Show("Tài khoản và mật khẩu không được bỏ trống!");
            }
            else if (txtTK.Text.Length == 0)
            {
                MessageBox.Show("Tài khoản không được bỏ trống!");
            }
            else if (txtMK.Text.Length == 0)
            {
                MessageBox.Show("Mật khẩu không được bỏ trống!");
            }
            else
            {
                if (txtTK.Text == "admin" && txtMK.Text == "123")
                {
                    this.Hide();
                    frmmain f1 = new frmmain();
                    f1.ShowDialog();
                    this.Close();
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("Tài khoản không đúng!");
                }
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '*';
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
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