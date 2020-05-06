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
    public partial class frmDonDatHang : Form
    {
        int vitri = 0;
        String macn = "";
        public frmDonDatHang()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDDH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmDonDatHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DS.DSMaDDH' table. You can move, or remove it, as needed.
            this.dSMaDDHTableAdapter.Fill(this.DS.DSMaDDH);

            DS.EnforceConstraints = false;

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.DS.DatHang);

            this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSNVTableAdapter.Fill(this.DS.DSNV);

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.DS.CTDDH);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);

            this.dSKHOTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSKHOTableAdapter.Fill(this.DS.DSKHO);

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);


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

            if (bdsDDH.Count == 0)
            {
                btnXoa.Enabled = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.DS.DatHang);

                this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSNVTableAdapter.Fill(this.DS.DSNV);

                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);

                this.dSKHOTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSKHOTableAdapter.Fill(this.DS.DSKHO);

                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Fill(this.DS.Vattu);
            }
        }

        private void hOTENComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Text = cmbHoTen.SelectedValue.ToString();
            }
            catch(Exception ex)
            {

            }            
        }

        private void btnThemVT_Click(object sender, EventArgs e)
        {
            bdsCTDDH.AddNew();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsDDH.Position;
            gcDDH.Enabled = false;
            bdsDDH.AddNew();
            btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPH.Enabled = true;
            cmbHoTen.Text = Program.mHoten;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String lenh = "EXEC [sp_KTMaDDH] '" + txtMaDDH.Text.Trim() + "'";
            SqlDataReader kt = Program.ExecSqlDataReader(lenh);
            if (kt == null) return;
            kt.Read();
            int k = (int)kt.GetInt32(0);
            kt.Close();
            if (k != 0)
            {
                MessageBox.Show("Mã Đơn Đặt Hàng bị trùng !", "", MessageBoxButtons.OK);
                txtMaDDH.Focus();
                return;
            }
            if (txtMaDDH.Text.Trim() == "")
            {
                MessageBox.Show("Mã Đơn Đặt Hàng không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtMaDDH.Focus();
                return;
            }
            if (txtNCC.Text.Trim() == "")
            {
                MessageBox.Show("Nhà cung cấp không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtNCC.Focus();
                return;
            }
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Mã NV không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return;
            }
            if (txtMaKho.Text.Trim() == "")
            {
                MessageBox.Show("Mã Kho không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtMaKho.Focus();
                return;
            }
            try
            {
                bdsDDH.EndEdit();
                bdsDDH.ResetCurrentItem();
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Update(this.DS.DatHang);
                btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
                gcDDH.Enabled = true;
                MessageBox.Show("Ghi đơn đặt hàng thành công!!", "Message", MessageBoxButtons.OK);
            }
            catch(Exception ex) {

                MessageBox.Show("Lỗi ghi đơn đặt hàng!!", "Message", MessageBoxButtons.OK);
            }

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsPN.Count > 0)
            {
                MessageBox.Show("Đơn đặt hàng đã được lập Phiếu Nhập!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xóa DDH Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsDDH.RemoveCurrent();
                    this.datHangTableAdapter.Update(this.DS.DatHang);
                    MessageBox.Show("Xóa thành công!!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa D D H." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.datHangTableAdapter.Fill(this.DS.DatHang);
            bdsDDH.CancelEdit();
            bdsDDH.Position = vitri;
            gcDDH.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.datHangTableAdapter.Fill(this.DS.DatHang);
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnGhiVT_Click(object sender, EventArgs e)
        {
            
            if (txtMADDH_CTDH.Equals("") == true)
            {
                MessageBox.Show("Mã DDH không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (txtMaVT_CTDH.Equals("") == true)
            {
                MessageBox.Show("Mã VT không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            int row = gvCTDDH.CurrentCell.RowIndex;
            if (gvCTDDH.Rows[row].Cells[2].Value.ToString().Trim().Equals("")== true)
            {
                MessageBox.Show("Số Lượng không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(gvCTDDH.Rows[row].Cells[2].Value.ToString().Trim()) <= 0)
            {
                MessageBox.Show("Số Lượng phải > 0!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (gvCTDDH.Rows[row].Cells[3].Value.ToString().Trim().Equals("") == true)
            {
                MessageBox.Show("Đơn giá không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(gvCTDDH.Rows[row].Cells[3].Value.ToString().Trim()) <= 0)
            {
                MessageBox.Show("Đơn giá phải > 0!!", "Message", MessageBoxButtons.OK);
                return;
            }
            try
            {
                bdsCTDDH.EndEdit();
                bdsCTDDH.ResetCurrentItem();
                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Update(this.DS.CTDDH);
                MessageBox.Show("Ghi CTDDH thành công!!", "Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi CT đặt hàng!!", "Message", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnXoaVT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Xóa CTDDH Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsCTDDH.RemoveCurrent();
                    this.cTDDHTableAdapter.Update(this.DS.CTDDH);
                    MessageBox.Show("Xóa thành công!!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa C T D D H." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPHVT_Click(object sender, EventArgs e)
        {
            this.cTDDHTableAdapter.Update(this.DS.CTDDH);
            bdsCTDDH.CancelEdit();
            bdsCTDDH.Position = vitri;
            gcDDH.Enabled = true;
        }

        private void tENKHOComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTenKho_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        private void gvCTDDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gvCTDDH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
