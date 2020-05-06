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

namespace QLVT_DH
{
    public partial class frmKho : Form
    {
        int vitri = 0;
        string macn = "";
        public frmKho()
        {
            InitializeComponent();
        }

        private void khoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKho.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void btnXoa_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.dS.Kho);

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dS.DatHang);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dS.PhieuNhap);

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dS.PhieuXuat);


            macn = ((DataRowView)bdsKho[0])["MACN"].ToString();
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;
            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPH.Enabled = btnTaiLai.Enabled = false;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
            }
            vitri = bdsKho.Position;

            if (bdsKho.Count == 0)
            {
                btnXoa.Enabled = false;
            }

        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                Program.servername = cmbChiNhanh.SelectedValue.ToString();
            }

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
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.dS.Kho);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dS.DatHang);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dS.PhieuNhap);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dS.PhieuXuat);

                macn = ((DataRowView)bdsKho[0])["MACN"].ToString();
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsKho.Position;           
            groupBox1.Enabled = true;
            gcKho.Enabled = false;
            bdsKho.AddNew();
            txtMaCN.Text = macn;
            btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPH.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaKho.Text.Trim() == "")
            {
                MessageBox.Show("Mã Kho không được rỗng!", "Message", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return;
            }
            String lenh = "EXEC [sp_KTMaKho] '" + txtMaKho.Text.Trim() + "'";
            SqlDataReader kt = Program.ExecSqlDataReader(lenh);
            if (kt == null) return;
            kt.Read();
            int i = (int)kt.GetInt32(0);
            kt.Close();
            if (i != 0)
            {
                MessageBox.Show("Mã Kho bị trùng !", "", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return;
            }
            //for (int i = 0; i < gvKho.DataRowCount; i++)
            //{
            //    if (txtMaKho.Text.Trim().Equals(gvKho.GetRowCellValue(i, "MAKHO").ToString().Trim()) == true)
            //    {
            //        MessageBox.Show("Mã Kho Bị Trùng.", "Message", MessageBoxButtons.OK);
            //        txtMaKho.Focus();
            //        return;
            //    }
            //}
            if (txtTenKho.Text.Trim() == "")
            {
                MessageBox.Show("Tên Kho không được rỗng!", "Message", MessageBoxButtons.OK);
                txtTenKho.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa Chỉ Kho không được rỗng!", "Message", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return;
            }
            try
            {
                bdsKho.EndEdit();
                bdsKho.ResetCurrentItem();
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Update(this.dS.Kho);
                btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
                gcKho.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi Kho!!" + ex.Message, "Message", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsDDH.Count > 0)
            {
                MessageBox.Show("Kho đã lập Đơn Đặt Hàng!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (bdsPN.Count > 0)
            {
                MessageBox.Show("Kho đã lập Phiếu Nhập!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (bdsPX.Count > 0)
            {
                MessageBox.Show("Kho đã lập Phiếu Xuất!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xóa Kho Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsKho.RemoveCurrent();
                    this.khoTableAdapter.Update(this.dS.Kho);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa Kho!!" + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.khoTableAdapter.Fill(this.dS.Kho);
            bdsKho.CancelEdit();
            bdsKho.Position = vitri;
            gcKho.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.khoTableAdapter.Fill(this.dS.Kho);
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}
