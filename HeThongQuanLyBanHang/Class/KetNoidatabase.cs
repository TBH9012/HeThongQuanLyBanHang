using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using HeThongQuanLyBanHang.Class;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace HeThongQuanLyBanHang.Class
{
    internal class KetNoidatabase
    {
        private static Model1 Con;  // Khai báo đối tượng DbContext

        public static void Connect()
        {
            try
            {
                Con = new Model1();  // Khởi tạo DbContext
                MessageBox.Show("Kết nối thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối với cơ sở dữ liệu: " + ex.Message);
            }
        }

        public static void Disconnect()
        {
            if (Con != null)
            {
                Con.Dispose();  // Đóng DbContext
            }
        }
      
    }
}
