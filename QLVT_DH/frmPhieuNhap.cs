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
    public partial class frmPhieuNhap : Form
    {
        int vitri = 0;
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void phieuNhapBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DS.DSMaDDH' table. You can move, or remove it, as needed.
            this.dSMaDDHTableAdapter.Fill(this.DS.DSMaDDH);
            // TODO: This line of code loads data into the 'DS.DSMaPN' table. You can move, or remove it, as needed.
            this.dSMaPNTableAdapter.Fill(this.DS.DSMaPN);
            DS.EnforceConstraints = false;

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.DS.CTPN);

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);

            this.dSKHOTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSKHOTableAdapter.Fill(this.DS.DSKHO);

            this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSNVTableAdapter.Fill(this.DS.DSNV);

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
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);

                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Fill(this.DS.CTPN);

                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Fill(this.DS.Vattu);

                this.dSKHOTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSKHOTableAdapter.Fill(this.DS.DSKHO);

                this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSNVTableAdapter.Fill(this.DS.DSNV);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsPN.Position;
            gcPN.Enabled = false;
            bdsPN.AddNew();
            btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPH.Enabled = true;
            cmbHoTen.Text = Program.mHoten;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String lenh = "EXEC [sp_KTMaPN] '" + txtMaPN.Text.Trim() + "'";
            SqlDataReader kt = Program.ExecSqlDataReader(lenh);
            if (kt == null) return;
            kt.Read();
            int k = (int)kt.GetInt32(0);
            kt.Close();
            if (k != 0)
            {
                MessageBox.Show("Mã Phiếu Nhập bị trùng !", "", MessageBoxButtons.OK);
                txtMaPN.Focus();
                return;
            }
            if (txtMaPN.Text.Trim() == "")
            {
                MessageBox.Show("Mã Phiếu Nhập không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtMaPN.Focus();
                return;
            }
            if(txtNgay.Text.Trim() == "")
            {
                MessageBox.Show("Ngày nhập không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtNgay.Focus();
                return;
            }
            if ( txtMaSoDDH.Text.Trim() == "")
            {
                MessageBox.Show("Mã Đơn Đặt Hàng không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtMaSoDDH.Focus();
                return;
            }
            try
            {
                bdsPN.EndEdit();
                bdsPN.ResetCurrentItem();
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);
                btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
                gcPN.Enabled = true;
                MessageBox.Show("Ghi Phiếu Nhập thành công!!", "Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi Phiếu Nhập!!", "Message", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsCTPN.Count > 0)
            {
                MessageBox.Show("PHIẾU NHẬP NÀY KHÔNG THỂ XÓA!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xóa Phiếu Nhập Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsPN.RemoveCurrent();
                    this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);
                    MessageBox.Show("Xóa thành công!!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa Phiếu Nhập." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            bdsPN.CancelEdit();
            bdsPN.Position = vitri;
            gcPN.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnThem_CTPN_Click(object sender, EventArgs e)
        {
            bdsCTPN.AddNew();
        }

        private void btnGhi_CTPN_Click(object sender, EventArgs e)
        {
            String MaVT = ((DataRowView)bdsCTPN[bdsCTPN.Position])["MAVT"].ToString().Trim();
            String DonGia = ((DataRowView)bdsCTPN[bdsCTPN.Position])["DONGIA"].ToString().Trim();
            String SL = ((DataRowView)bdsCTPN[bdsCTPN.Position])["SOLUONG"].ToString().Trim();
            //int row = gvCTPN.CurrentCell.RowIndex;
            //Kiểm tra số lượng trong Pn có lớn hơn trong DDH hay ko
            Program.KetNoi();
            SqlCommand sqlCommand = new SqlCommand("sp_KiemTraSoLuongVT", Program.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@maDDH", SqlDbType.NChar).Value = txtMaSoDDH.Text.ToString();
            sqlCommand.Parameters.Add("@maVT", SqlDbType.NChar).Value = MaVT;
          
            int check = Program.execStoreProcedureWithReturnValue(sqlCommand);
            // MessageBox.Show(txtMaSoDDH.Text.ToString() + MaVT + check.ToString(), "Message", MessageBoxButtons.OK);
            if (txtMaPN_CTPN.Equals("") == true)
            {
                MessageBox.Show("Mã PN không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (txtMaVT_CTPN.Equals("") == true)
            {
                MessageBox.Show("Mã VT không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (check == 0)
            {
                MessageBox.Show("Vật tư này chưa được đặt, Không thể nhập về!!", "Báo Lỗi", MessageBoxButtons.OK);
                return;
            }
            else
            {
                if (int.Parse(SL.ToString()) > check)
                {
                    MessageBox.Show("Số Lượng không được lớn hơn số lượng đã đặt!!", "Message", MessageBoxButtons.OK);
                    txtSoLuong_CTPN.DataGridView.Focus();
                    return;
                }
            }
            if (SL.Equals("") == true)
            {
                MessageBox.Show("Số Lượng không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(SL.ToString().Trim()) <= 0)
            {
                MessageBox.Show("Số Lượng phải > 0!!", "Message", MessageBoxButtons.OK);
                return;
            }
            
            if (DonGia.Equals("") == true)
            {
                MessageBox.Show("Đơn giá không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(DonGia.ToString()) <= 0)
            {
                MessageBox.Show("Đơn giá phải > 0!!", "Message", MessageBoxButtons.OK);
                return;
            }
            try
            {
                bdsCTPN.EndEdit();
                bdsCTPN.ResetCurrentItem();
                
                vattuTableAdapter.Fill(this.DS.Vattu);
                //Cập nhật số lượng tồn bên VT
                string maVT;
                for (int i = 0; i < bdsCTPN.Count; i++)
                {
                    maVT = ((DataRowView)bdsCTPN[i])["MAVT"].ToString().Trim();
                    this.DS.Vattu.FindByMAVT(maVT).SOLUONGTON += (int)((DataRowView)bdsCTPN[i])["SOLUONG"];
                }
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Update(this.DS.Vattu);
                
                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Update(this.DS.CTPN);
                MessageBox.Show("Ghi CTPN thành công!!", "Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi CT phiếu nhập!!", "Message", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnXoa_CTPN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Xóa CTPN Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsCTPN.RemoveCurrent();
                    this.cTPNTableAdapter.Update(this.DS.CTPN);
                    MessageBox.Show("Xóa thành công!!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa C T Phiếu Nhập." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_CTPN_Click(object sender, EventArgs e)
        {
            this.cTPNTableAdapter.Update(this.DS.CTPN);
            bdsCTPN.CancelEdit();
        }

        private void cmbHoTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Text = cmbHoTen.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbTenKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaKho.Text = cmbTenKho.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbMaSoDDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               // txtMaSoDDH.Text = cmbMaSoDDH.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
