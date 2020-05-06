namespace QLVT_DH
{
    partial class Xfrm_HoatDongNhanVien
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label mANVLabel;
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbChiNhanh = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbHangNhap = new System.Windows.Forms.RadioButton();
            this.rbHangXuat = new System.Windows.Forms.RadioButton();
            this.txtNgayBD = new System.Windows.Forms.DateTimePicker();
            this.txtNgayKT = new System.Windows.Forms.DateTimePicker();
            this.dS = new QLVT_DH.DS();
            this.bdsNV = new System.Windows.Forms.BindingSource(this.components);
            this.dSNVTableAdapter = new QLVT_DH.DSTableAdapters.DSNVTableAdapter();
            this.tableAdapterManager = new QLVT_DH.DSTableAdapters.TableAdapterManager();
            this.cmbNV = new System.Windows.Forms.ComboBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            mANVLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsNV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXuat
            // 
            this.btnXuat.Location = new System.Drawing.Point(192, 233);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(86, 33);
            this.btnXuat.TabIndex = 0;
            this.btnXuat.Text = "Xuất Báo Cáo";
            this.btnXuat.UseVisualStyleBackColor = true;
            this.btnXuat.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(326, 233);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(66, 33);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CHI NHÁNH: ";
            // 
            // cmbChiNhanh
            // 
            this.cmbChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChiNhanh.FormattingEnabled = true;
            this.cmbChiNhanh.Location = new System.Drawing.Point(257, 39);
            this.cmbChiNhanh.Name = "cmbChiNhanh";
            this.cmbChiNhanh.Size = new System.Drawing.Size(164, 21);
            this.cmbChiNhanh.TabIndex = 3;
            this.cmbChiNhanh.SelectedIndexChanged += new System.EventHandler(this.cmbChiNhanh_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ngày Bắt Đầu: ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ngày Kết Thúc: ";
            // 
            // rbHangNhap
            // 
            this.rbHangNhap.AutoSize = true;
            this.rbHangNhap.Location = new System.Drawing.Point(192, 198);
            this.rbHangNhap.Name = "rbHangNhap";
            this.rbHangNhap.Size = new System.Drawing.Size(80, 17);
            this.rbHangNhap.TabIndex = 13;
            this.rbHangNhap.TabStop = true;
            this.rbHangNhap.Text = "Hàng Nhập";
            this.rbHangNhap.UseVisualStyleBackColor = true;
            // 
            // rbHangXuat
            // 
            this.rbHangXuat.AutoSize = true;
            this.rbHangXuat.Location = new System.Drawing.Point(313, 198);
            this.rbHangXuat.Name = "rbHangXuat";
            this.rbHangXuat.Size = new System.Drawing.Size(76, 17);
            this.rbHangXuat.TabIndex = 14;
            this.rbHangXuat.TabStop = true;
            this.rbHangXuat.Text = "Hàng Xuất";
            this.rbHangXuat.UseVisualStyleBackColor = true;
            // 
            // txtNgayBD
            // 
            this.txtNgayBD.Location = new System.Drawing.Point(257, 111);
            this.txtNgayBD.Name = "txtNgayBD";
            this.txtNgayBD.Size = new System.Drawing.Size(200, 20);
            this.txtNgayBD.TabIndex = 16;
            this.txtNgayBD.ValueChanged += new System.EventHandler(this.txtNgayBD_ValueChanged);
            // 
            // txtNgayKT
            // 
            this.txtNgayKT.Location = new System.Drawing.Point(257, 146);
            this.txtNgayKT.Name = "txtNgayKT";
            this.txtNgayKT.Size = new System.Drawing.Size(200, 20);
            this.txtNgayKT.TabIndex = 17;
            // 
            // mANVLabel
            // 
            mANVLabel.AutoSize = true;
            mANVLabel.Location = new System.Drawing.Point(164, 78);
            mANVLabel.Name = "mANVLabel";
            mANVLabel.Size = new System.Drawing.Size(41, 13);
            mANVLabel.TabIndex = 18;
            mANVLabel.Text = "MANV:";
            // 
            // dS
            // 
            this.dS.DataSetName = "DS";
            this.dS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bdsNV
            // 
            this.bdsNV.DataMember = "DSNV";
            this.bdsNV.DataSource = this.dS;
            // 
            // dSNVTableAdapter
            // 
            this.dSNVTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ChiNhanhTableAdapter = null;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.CTDDHTableAdapter = null;
            this.tableAdapterManager.CTPNTableAdapter = null;
            this.tableAdapterManager.CTPXTableAdapter = null;
            this.tableAdapterManager.DatHangTableAdapter = null;
            this.tableAdapterManager.KhoTableAdapter = null;
            this.tableAdapterManager.NhanVienTableAdapter = null;
            this.tableAdapterManager.PhieuNhapTableAdapter = null;
            this.tableAdapterManager.PhieuXuatTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = QLVT_DH.DSTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.VattuTableAdapter = null;
            // 
            // cmbNV
            // 
            this.cmbNV.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bdsNV, "HOTEN", true));
            this.cmbNV.DataSource = this.bdsNV;
            this.cmbNV.DisplayMember = "MANV";
            this.cmbNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNV.FormattingEnabled = true;
            this.cmbNV.Location = new System.Drawing.Point(257, 75);
            this.cmbNV.Name = "cmbNV";
            this.cmbNV.Size = new System.Drawing.Size(121, 21);
            this.cmbNV.TabIndex = 20;
            this.cmbNV.ValueMember = "HOTEN";
            this.cmbNV.SelectedIndexChanged += new System.EventHandler(this.cmbNV_SelectedIndexChanged);
            // 
            // txtHoTen
            // 
            this.txtHoTen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bdsNV, "HOTEN", true));
            this.txtHoTen.Location = new System.Drawing.Point(402, 76);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.ReadOnly = true;
            this.txtHoTen.Size = new System.Drawing.Size(100, 20);
            this.txtHoTen.TabIndex = 21;
            // 
            // Xfrm_HoatDongNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 317);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.cmbNV);
            this.Controls.Add(mANVLabel);
            this.Controls.Add(this.txtNgayKT);
            this.Controls.Add(this.txtNgayBD);
            this.Controls.Add(this.rbHangXuat);
            this.Controls.Add(this.rbHangNhap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbChiNhanh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXuat);
            this.Name = "Xfrm_HoatDongNhanVien";
            this.Text = "Xfrm_DanhSachNv";
            this.Load += new System.EventHandler(this.Xfrm_DanhSachNv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsNV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbChiNhanh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbHangNhap;
        private System.Windows.Forms.RadioButton rbHangXuat;
        private System.Windows.Forms.DateTimePicker txtNgayBD;
        private System.Windows.Forms.DateTimePicker txtNgayKT;
        private DS dS;
        private System.Windows.Forms.BindingSource bdsNV;
        private DSTableAdapters.DSNVTableAdapter dSNVTableAdapter;
        private DSTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ComboBox cmbNV;
        private System.Windows.Forms.TextBox txtHoTen;
    }
}