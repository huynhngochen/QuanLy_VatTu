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

namespace QLVT_DH
{
    public partial class frmNV : Form
    {
        int vitri = 0;
        String macn = "";
        public frmNV()
        {
            InitializeComponent();
        }

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void FormNV_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;
            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.dS.NhanVien);
            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dS.DatHang);
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dS.PhieuNhap);
            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dS.PhieuXuat);

            macn = ((DataRowView)bdsNV[0])["MACN"].ToString();
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;
            if(Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPH.Enabled = btnTaiLai.Enabled = false;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
            }
            vitri = bdsNV.Position;

            if (bdsNV.Count == 0)
            {
                btnXoa.Enabled = false;
            }
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbChiNhanh.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                Program.servername = cmbChiNhanh.SelectedValue.ToString();
            }

            if(cmbChiNhanh.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            if(Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "Message", MessageBoxButtons.OK);
            }
            else
            {
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Fill(this.dS.NhanVien);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dS.DatHang);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dS.PhieuNhap);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dS.PhieuXuat);

                macn = ((DataRowView)bdsNV[0])["MACN"].ToString();
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsNV.Position;
            groupBox1.Enabled = true;
            gcNV.Enabled = false;
            bdsNV.AddNew();
            txtMaCN.Text = macn;
            btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPH.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Mã NV không được rỗng!", "Message", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return;
            }
            String lenh = "EXEC [sp_KTMaNV] '" + txtMaNV.Text.Trim() + "'";
            SqlDataReader kt = Program.ExecSqlDataReader(lenh);
            if (kt == null) return;
            kt.Read();
            int i = (int)kt.GetInt32(0);
            kt.Close();
            if (i != 0)
            {
                MessageBox.Show("Mã Nhân Viên bị trùng !", "", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return;
            }
            //for(int i=0; i < gvNV.DataRowCount; i++)
            //{
            //    if(txtMaNV.Text.Trim().Equals(gvNV.GetRowCellValue(i, "MANV").ToString().Trim()) == true)
            //    {
            //        MessageBox.Show("Mã Nhân Viên Bị Trùng.", "Message", MessageBoxButtons.OK);
            //        txtMaNV.Focus();
            //        return;
            //    }
            //}
            if (txtHo.Text.Trim() == "")
            {
                MessageBox.Show("Họ NV không được rỗng!", "Message", MessageBoxButtons.OK);
                txtHo.Focus();
                return;
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên NV không được rỗng!", "Message", MessageBoxButtons.OK);
                txtTen.Focus();
                return;
            }
            if (txtDC.Text.Trim() == "")
            {
                MessageBox.Show("Địa Chỉ NV không được rỗng!", "Message", MessageBoxButtons.OK);
                txtDC.Focus();
                return;
            }
            if (float.Parse(txtLuong.Text) < 4000000)
            {
                MessageBox.Show("Lương NV phải >= 4000000 !", "Message", MessageBoxButtons.OK);
                txtLuong.Focus();
                return;
            }
            try
            {
                bdsNV.EndEdit();
                bdsNV.ResetCurrentItem();
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Update(this.dS.NhanVien);
                btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
                gcNV.Enabled = true;
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Lỗi Ghi Nhân Viên." + ex.Message, "Message", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsDDH.Count > 0)
            {
                MessageBox.Show("Nhân Viên đã lập Đơn Đặt Hàng!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (bdsPN.Count > 0)
            {
                MessageBox.Show("Nhân Viên đã lập Phiếu Nhập!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (bdsPX.Count > 0)
            {
                MessageBox.Show("Nhân Viên đã lập Phiếu Xuất!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xóa NV Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsNV.RemoveCurrent();
                    this.nhanVienTableAdapter.Update(this.dS.NhanVien);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa Nhân Viên." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // vitri = bdsNV.Position;
            this.nhanVienTableAdapter.Fill(this.dS.NhanVien);
            bdsNV.CancelEdit();
            bdsNV.Position = vitri;
            gcNV.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.nhanVienTableAdapter.Fill(this.dS.NhanVien);
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xtra_DanhSachNV rpt = new Xtra_DanhSachNV();
            rpt.lbChiNhanh.Text = cmbChiNhanh.Text;
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void gcNV_Click(object sender, EventArgs e)
        {

        }

        private void btnTaoLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmTaoLogin formTaoLogin = new frmTaoLogin(Program.mGroup);
            formTaoLogin.ShowDialog();
            if (formTaoLogin.thongTinLogin != null)
            {
                string maNV = ((DataRowView)bdsNV.Current)["MANV"].ToString().Trim();
                formTaoLogin.thongTinLogin.UserName = maNV;
                int  kq = Program.taoLogin(formTaoLogin.thongTinLogin);
                if (kq == 0)
                {
                    MessageBox.Show("Tạo Login thành công");
                    return;
                }
                else  {
                    MessageBox.Show("LOGIN ĐÃ TẠO TÀI KHOẢN OR TÊN LOGIN BỊ TRÙNG!!");
                    return;
                }
            }
            formTaoLogin.Close();
        }
    }
}
