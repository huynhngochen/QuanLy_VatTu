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
    public partial class frmPhieuXuat : Form
    {
        int vitri = 0;
        public frmPhieuXuat()
        {
            InitializeComponent();
        }

        private void phieuXuatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPX.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);

            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.DS.CTPX);

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);

            this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSNVTableAdapter.Fill(this.DS.DSNV);

            this.dSKHOTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dSKHOTableAdapter.Fill(this.DS.DSKHO);

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
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);

                this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPXTableAdapter.Fill(this.DS.CTPX);

                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Fill(this.DS.Vattu);

                this.dSNVTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSNVTableAdapter.Fill(this.DS.DSNV);

                this.dSKHOTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dSKHOTableAdapter.Fill(this.DS.DSKHO);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsPX.Position;
            gcPX.Enabled = false;
            bdsPX.AddNew();
            btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPH.Enabled = true;
            cmbHoTen.Text = Program.mHoten;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String lenh = "EXEC [sp_KTMaPX] '" + txtMaPX.Text.Trim() + "'";
            SqlDataReader kt = Program.ExecSqlDataReader(lenh);
            if (kt == null) return;
            kt.Read();
            int k = (int)kt.GetInt32(0);
            kt.Close();
            if (k != 0)
            {
                MessageBox.Show("Mã Phiếu Xuất bị trùng !", "", MessageBoxButtons.OK);
                txtMaPX.Focus();
                return;
            }
            if (txtMaPX.Text.Trim() == "")
            {
                MessageBox.Show("Mã Phiếu Xuất không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtMaPX.Focus();
                return;
            }
            if (txtNgay.Text.Trim() == "")
            {
                MessageBox.Show("Ngày Xuất không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtNgay.Focus();
                return;
            }
            if (txtHoTenKH.Text.Trim() == "")
            {
                MessageBox.Show("Họ Tên Khách Hàng không được rỗng!!", "Message", MessageBoxButtons.OK);
                txtHoTenKH.Focus();
                return;
            }
            try
            {
                bdsPX.EndEdit();
                bdsPX.ResetCurrentItem();
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);
                btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
                gcPX.Enabled = true;
                MessageBox.Show("Ghi Phiếu Xuất thành công!!", "Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi Phiếu Xuất!!", "Message", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsCTPX.Count > 0)
            {
                MessageBox.Show("PHIẾU XUẤT NÀY KHÔNG THỂ XÓA!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xóa Phiếu Xuất Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsPX.RemoveCurrent();
                    this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);
                    MessageBox.Show("Xóa thành công!!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa Phiếu Xuất." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
            bdsPX.CancelEdit();
            bdsPX.Position = vitri;
            gcPX.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;

        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnThem_CTPX_Click(object sender, EventArgs e)
        {
            bdsCTPX.AddNew();
        }

        private void btnGhi_CTPX_Click(object sender, EventArgs e)
        {
            String MaVT = ((DataRowView)bdsCTPX[bdsCTPX.Position])["MAVT"].ToString().Trim();
            String SL = ((DataRowView)bdsCTPX[bdsCTPX.Position])["SOLUONG"].ToString().Trim();
            //int row = gvCTPN.CurrentCell.RowIndex;
            //Kiểm tra số lượng trong Pn có lớn hơn trong DDH hay ko
            Program.KetNoi();
            SqlCommand sqlCommand = new SqlCommand("sp_KiemTra_SLTon", Program.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@maVT", SqlDbType.NChar).Value = MaVT;

            int soluongTon = Program.execStoreProcedureWithReturnValue(sqlCommand);
            if (txtMaPX_CTPX.Equals("") == true)
            {
                MessageBox.Show("Mã PX không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (txtSoLuong_CTPX.Equals("") == true)
            {
                MessageBox.Show("Số Lượng không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(SL.ToString()) > soluongTon)
            {
                MessageBox.Show("Không đủ số lượng để xuất. Vui Lòng kiểm tra lại!!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (txtDonGia_CTPX.Equals("") == true)
            {
                MessageBox.Show("Đơn Giá không được rỗng!!", "Message", MessageBoxButtons.OK);
                return;
            }
            try
            {
                bdsCTPX.EndEdit();
                bdsCTPX.ResetCurrentItem();
                vattuTableAdapter.Fill(this.DS.Vattu);
                //cập nhật lại số lượng tồn bên VT
                string maVT;
                for (int i = 0; i < bdsCTPX.Count; i++)
                {
                    maVT = ((DataRowView)bdsCTPX[i])["MAVT"].ToString().Trim();
                    this.DS.Vattu.FindByMAVT(maVT).SOLUONGTON -= (int)((DataRowView)bdsCTPX[i])["SOLUONG"];
                }
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Update(this.DS.Vattu);

                this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPXTableAdapter.Update(this.DS.CTPX);
                MessageBox.Show("Ghi CTPX thành công!!", "Message", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi CT phiếu xuất!!", "Message", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnXoa_CTPX_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Xóa CTPX Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsCTPX.RemoveCurrent();
                    this.cTPXTableAdapter.Update(this.DS.CTPX);
                    MessageBox.Show("Xóa thành công!!", "Message", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa C T Phiếu Xuất." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_CTPX_Click(object sender, EventArgs e)
        {
            this.cTPXTableAdapter.Update(this.DS.CTPX);
            bdsCTPX.CancelEdit();
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
    }
}
