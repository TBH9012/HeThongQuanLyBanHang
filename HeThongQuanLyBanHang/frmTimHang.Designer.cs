namespace HeThongQuanLyBanHang
{
    partial class frmTimHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimHang));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvDanhSachHang = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnTimLaiHang = new System.Windows.Forms.Button();
            this.btnTimKiemHang = new System.Windows.Forms.Button();
            this.txtTKTenHang = new System.Windows.Forms.TextBox();
            this.txtTKMaHang = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHang)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1055, 366);
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
            this.splitContainer1.Panel1.Controls.Add(this.dgvDanhSachHang);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.btnDong);
            this.splitContainer1.Panel2.Controls.Add(this.btnBoQua);
            this.splitContainer1.Panel2.Controls.Add(this.btnTimLaiHang);
            this.splitContainer1.Panel2.Controls.Add(this.btnTimKiemHang);
            this.splitContainer1.Panel2.Controls.Add(this.txtTKTenHang);
            this.splitContainer1.Panel2.Controls.Add(this.txtTKMaHang);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1055, 366);
            this.splitContainer1.SplitterDistance = 626;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvDanhSachHang
            // 
            this.dgvDanhSachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachHang.Location = new System.Drawing.Point(0, 0);
            this.dgvDanhSachHang.Name = "dgvDanhSachHang";
            this.dgvDanhSachHang.Size = new System.Drawing.Size(626, 366);
            this.dgvDanhSachHang.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(16, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Lưu ý: Hãy nhập đúng mã hàng và tên hàng!";
            // 
            // btnDong
            // 
            this.btnDong.Image = global::HeThongQuanLyBanHang.Properties.Resources.close;
            this.btnDong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDong.Location = new System.Drawing.Point(337, 164);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Image = global::HeThongQuanLyBanHang.Properties.Resources.cancel;
            this.btnBoQua.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBoQua.Location = new System.Drawing.Point(231, 164);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(75, 23);
            this.btnBoQua.TabIndex = 6;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnTimLaiHang
            // 
            this.btnTimLaiHang.Image = global::HeThongQuanLyBanHang.Properties.Resources.reload;
            this.btnTimLaiHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimLaiHang.Location = new System.Drawing.Point(125, 164);
            this.btnTimLaiHang.Name = "btnTimLaiHang";
            this.btnTimLaiHang.Size = new System.Drawing.Size(75, 23);
            this.btnTimLaiHang.TabIndex = 5;
            this.btnTimLaiHang.Text = "Tìm lại";
            this.btnTimLaiHang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimLaiHang.UseVisualStyleBackColor = true;
            this.btnTimLaiHang.Click += new System.EventHandler(this.btnTimLaiHang_Click);
            // 
            // btnTimKiemHang
            // 
            this.btnTimKiemHang.Image = global::HeThongQuanLyBanHang.Properties.Resources.find;
            this.btnTimKiemHang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimKiemHang.Location = new System.Drawing.Point(19, 164);
            this.btnTimKiemHang.Name = "btnTimKiemHang";
            this.btnTimKiemHang.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiemHang.TabIndex = 4;
            this.btnTimKiemHang.Text = "Tìm kiếm";
            this.btnTimKiemHang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimKiemHang.UseVisualStyleBackColor = true;
            this.btnTimKiemHang.Click += new System.EventHandler(this.btnTimKiemHang_Click);
            // 
            // txtTKTenHang
            // 
            this.txtTKTenHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTKTenHang.Location = new System.Drawing.Point(93, 99);
            this.txtTKTenHang.Name = "txtTKTenHang";
            this.txtTKTenHang.Size = new System.Drawing.Size(319, 20);
            this.txtTKTenHang.TabIndex = 3;
            // 
            // txtTKMaHang
            // 
            this.txtTKMaHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTKMaHang.Location = new System.Drawing.Point(93, 64);
            this.txtTKMaHang.Name = "txtTKMaHang";
            this.txtTKMaHang.Size = new System.Drawing.Size(319, 20);
            this.txtTKMaHang.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tên hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mã hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Thông tin tìm kiếm hàng";
            // 
            // frmTimHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 366);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTimHang";
            this.Text = "Tìm kiếm hàng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTimHang_FormClosing);
            this.Load += new System.EventHandler(this.frmTimHang_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDanhSachHang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnTimLaiHang;
        private System.Windows.Forms.Button btnTimKiemHang;
        private System.Windows.Forms.TextBox txtTKTenHang;
        private System.Windows.Forms.TextBox txtTKMaHang;
    }
}