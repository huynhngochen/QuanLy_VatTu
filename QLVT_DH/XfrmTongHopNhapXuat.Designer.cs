namespace QLVT_DH
{
    partial class XfrmTongHopNhapXuat
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NGAYBD = new System.Windows.Forms.DateTimePicker();
            this.NGAYKT = new System.Windows.Forms.DateTimePicker();
            this.btnInBC = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(151, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "BẢNG TỔNG HỢP NHẬP XUẤT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(80, 144);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "TỪ NGÀY : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.label3.Location = new System.Drawing.Point(80, 216);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "ĐẾN NGÀY : ";
            // 
            // NGAYBD
            // 
            this.NGAYBD.Location = new System.Drawing.Point(248, 138);
            this.NGAYBD.Margin = new System.Windows.Forms.Padding(5);
            this.NGAYBD.Name = "NGAYBD";
            this.NGAYBD.Size = new System.Drawing.Size(331, 29);
            this.NGAYBD.TabIndex = 3;
            this.NGAYBD.ValueChanged += new System.EventHandler(this.NGAYBD_ValueChanged);
            // 
            // NGAYKT
            // 
            this.NGAYKT.Location = new System.Drawing.Point(248, 210);
            this.NGAYKT.Margin = new System.Windows.Forms.Padding(5);
            this.NGAYKT.Name = "NGAYKT";
            this.NGAYKT.Size = new System.Drawing.Size(331, 29);
            this.NGAYKT.TabIndex = 4;
            // 
            // btnInBC
            // 
            this.btnInBC.ForeColor = System.Drawing.Color.Teal;
            this.btnInBC.Location = new System.Drawing.Point(192, 294);
            this.btnInBC.Margin = new System.Windows.Forms.Padding(5);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(129, 48);
            this.btnInBC.TabIndex = 5;
            this.btnInBC.Text = "IN BÁO CÁO";
            this.btnInBC.UseVisualStyleBackColor = true;
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.ForeColor = System.Drawing.Color.Teal;
            this.btnThoat.Location = new System.Drawing.Point(390, 294);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(129, 48);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "THOÁT";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // XfrmTongHopNhapXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 439);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.NGAYKT);
            this.Controls.Add(this.NGAYBD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "XfrmTongHopNhapXuat";
            this.Text = "frmTongHopNhapXuat";
            this.Load += new System.EventHandler(this.XfrmTongHopNhapXuat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInBC;
        private System.Windows.Forms.Button btnThoat;
        public System.Windows.Forms.DateTimePicker NGAYBD;
        public System.Windows.Forms.DateTimePicker NGAYKT;
    }
}