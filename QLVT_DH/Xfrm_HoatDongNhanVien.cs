using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QLVT_DH
{
    
    public partial class Xfrm_HoatDongNhanVien : Form
    {
        String macn = "";
        public Xfrm_HoatDongNhanVien()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Xtra_HoatDongNhanVien rpt;
            if (rbHangNhap.Checked)
                rpt = new Xtra_HoatDongNhanVien(1, txtNgayBD.Value, txtNgayKT.Value, int.Parse(cmbNV.Text.Trim()));
            else
                rpt = new Xtra_HoatDongNhanVien(0, txtNgayBD.Value, txtNgayKT.Value, int.Parse(cmbNV.Text.Trim()));

          // rpt.lbMaNV.Text = cmbNV.Text;
           // rpt.lbHoTen.Text = txtHoTen.Text;
           /// rpt.lbMaCN.Text = cmbChiNhanh.Text;

            ReportPrintTool report = new ReportPrintTool(rpt);
            report.ShowPreviewDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Xfrm_DanhSachNv_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.DSNV' table. You can move, or remove it, as needed.
            dS.EnforceConstraints = false;
            this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSNVTableAdapter.Fill(this.dS.DSNV);
            rbHangNhap.Checked = true;

            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;

            //macn = ((DataRowView)bdsNV[0])["MACN"].ToString();
            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
            }
            else cmbChiNhanh.Enabled = false;

           /* if (bdsNV.Count == 0)
            {
                MessageBox.Show("Danh sách nhân viên rỗng", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            for (int i = 0; i < bdsNV.Count; i++)
            {
                cmbNV.Items.Add(((DataRowView)bdsNV[i])["MANV"]);
            }*/
            cmbNV.SelectedIndex = 0;
            txtNgayKT.MinDate = txtNgayBD.Value.AddDays(1);
        }


        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                Program.servername = cmbChiNhanh.SelectedValue.ToString();
            }
            //if (bdsNV.Count <= 0) return;
            //Program.servername = cmbChiNhanh.SelectedValue.ToString();
            if (cmbChiNhanh.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "Message", MessageBoxButtons.OK);
            }
            else
            {
                this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSNVTableAdapter.Fill(this.dS.DSNV);

                //macn = ((DataRowView)bdsNV[0])["MACN"].ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtNgayBD_ValueChanged(object sender, EventArgs e)
        {
            txtNgayKT.MinDate = txtNgayBD.Value.AddDays(1);
        }

        private void cmbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }
        
        private void cmbNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataRowView tro = (DataRowView)bdsNV[bdsNV.Find("MANV", cmbNV.Text)];
           // txtHoTen.Text = tro["HO"] + " " + tro["TEN"];
        }
    }
}
