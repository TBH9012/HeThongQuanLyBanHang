namespace HeThongQuanLyBanHang
{
    partial class frmTimKhach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimKhach));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvDanhSachKhach = new System.Windows.Forms.DataGridView();
            this.txtTKTenKhach = new System.Windows.Forms.TextBox();
            this.txtTKMaKhach = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnTimLaiKhach = new System.Windows.Forms.Button();
            this.btnTimKiemKhach = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachKhach)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 301);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvDanhSachKhach);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtTKTenKhach);
            this.splitContainer1.Panel2.Controls.Add(this.txtTKMaKhach);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.btnDong);
            this.splitContainer1.Panel2.Controls.Add(this.btnBoQua);
            this.splitContainer1.Panel2.Controls.Add(this.btnTimLaiKhach);
            this.splitContainer1.Panel2.Controls.Add(this.btnTimKiemKhach);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(788, 301);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvDanhSachKhach
            // 
            this.dgvDanhSachKhach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachKhach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachKhach.Location = new System.Drawing.Point(0, 0);
            this.dgvDanhSachKhach.Name = "dgvDanhSachKhach";
            this.dgvDanhSachKhach.Size = new System.Drawing.Size(448, 301);
            this.dgvDanhSachKhach.TabIndex = 0;
            // 
            // txtTKTenKhach
            // 
            this.txtTKTenKhach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTKTenKhach.Location = new System.Drawing.Point(96, 91);
            this.txtTKTenKhach.Name = "txtTKTenKhach";
            this.txtTKTenKhach.Size = new System.Drawing.Size(211, 20);
            this.txtTKTenKhach.TabIndex = 14;
            // 
            // txtTKMaKhach
            // 
            this.txtTKMaKhach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTKMaKhach.Location = new System.Drawing.Point(96, 56);
            this.txtTKMaKhach.Name = "txtTKMaKhach";
            this.txtTKMaKhach.Size = new System.Drawing.Size(211, 20);
            this.txtTKMaKhach.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(15, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(232, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Lưu ý: Hãy nhập đúng mã khách và tên khách!";
            // 
            // btnDong
            // 
            this.btnDong.Image = global::HeThongQuanLyBanHang.Properties.Resources.close;
            this.btnDong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDong.Location = new System.Drawing.Point(236, 266);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 12;
            this.btnDong.Text = "Đóng";
            this.btnDong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Image = global::HeThongQuanLyBanHang.Properties.Resources.cancel;
            this.btnBoQua.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBoQua.Location = new System.Drawing.Point(236, 146);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(75, 23);
            this.btnBoQua.TabIndex = 11;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnTimLaiKhach
            // 
            this.btnTimLaiKhach.Image = global::HeThongQuanLyBanHang.Properties.Resources.reload;
            this.btnTimLaiKhach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimLaiKhach.Location = new System.Drawing.Point(127, 146);
            this.btnTimLaiKhach.Name = "btnTimLaiKhach";
            this.btnTimLaiKhach.Size = new System.Drawing.Size(75, 23);
            this.btnTimLaiKhach.TabIndex = 10;
            this.btnTimLaiKhach.Text = "Tìm lại";
            this.btnTimLaiKhach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimLaiKhach.UseVisualStyleBackColor = true;
            this.btnTimLaiKhach.Click += new System.EventHandler(this.btnTimLaiKhach_Click);
            // 
            // btnTimKiemKhach
            // 
            this.btnTimKiemKhach.Image = global::HeThongQuanLyBanHang.Properties.Resources.find;
            this.btnTimKiemKhach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimKiemKhach.Location = new System.Drawing.Point(18, 146);
            this.btnTimKiemKhach.Name = "btnTimKiemKhach";
            this.btnTimKiemKhach.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiemKhach.TabIndex = 9;
            this.btnTimKiemKhach.Text = "Tìm kiếm";
            this.btnTimKiemKhach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimKiemKhach.UseVisualStyleBackColor = true;
            this.btnTimKiemKhach.Click += new System.EventHandler(this.btnTimKiemKhach_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên khách";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã khách";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thông tin tìm kiếm khách hàng";
            // 
            // frmTimKhach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 301);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTimKhach";
            this.Text = "Tìm kiếm khách hàng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTimKhach_FormClosing);
            this.Load += new System.EventHandler(this.frmTimKhach_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachKhach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDanhSachKhach;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnTimLaiKhach;
        private System.Windows.Forms.Button btnTimKiemKhach;
        private System.Windows.Forms.TextBox txtTKTenKhach;
        private System.Windows.Forms.TextBox txtTKMaKhach;
    }
}