using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DH
{
    public partial class frmVatTu : Form
    {
        int vitri = 0;
        public frmVatTu()
        {
            InitializeComponent();
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsVT.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void frmVatTu_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.dS.Vattu);

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.dS.CTDDH);

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.dS.CTPN);

            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.dS.CTPX);           
            
            if (Program.mGroup == "CONGTY")
            {
                btnThem.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPH.Enabled = btnTaiLai.Enabled = false;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsVT.Position;
            bdsVT.AddNew();
            groupBox1.Enabled = true;
            gcVT.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPH.Enabled = true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(txtMaVT.Text.Trim() == "")
            {
                MessageBox.Show("Mã Vật Tư không được rỗng!", "Message", MessageBoxButtons.OK);
                txtMaVT.Focus();
                return;
            }
            for(int i =0; i < gvVT.DataRowCount; i++)
            {
                if (txtMaVT.Text.Trim().Equals(gvVT.GetRowCellValue(i, "MAVT").ToString().Trim()) == true)
                {
                    MessageBox.Show("Mã Vật Tư bị trùng. Vui Lòng Kiểm Tra Lại!", "Message", MessageBoxButtons.OK);
                    txtMaVT.Focus();
                    return;
                }
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên Vật Tư không được rỗng!", "Message", MessageBoxButtons.OK);
                txtTen.Focus();
                return;
            }
            if(int.Parse(txtSLT.Text) < 0)
            {
                MessageBox.Show("Số Lượng Tồn phải > 0 !", "Message", MessageBoxButtons.OK);
                txtSLT.Focus();
                return;
            }
            try
            {
                bdsVT.EndEdit();
                bdsVT.ResetCurrentItem();
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Update(this.dS.Vattu);
                btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
                gcVT.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi Vật Tư." + ex.Message, "Message", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bdsCTDDH.Count > 0)
            {
                MessageBox.Show("Vật Tư đã có trong CTDDH!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (bdsCTPN.Count > 0)
            {
                MessageBox.Show("Vật Tư đã có trong CTPN!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (bdsCTPX.Count > 0)
            {
                MessageBox.Show("Vật Tư đã có trong CTPX!", "Message", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xóa VT Không??", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsVT.RemoveCurrent();
                    this.vattuTableAdapter.Update(this.dS.Vattu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Xóa Vật Tư." + ex.Message, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void btnPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.vattuTableAdapter.Fill(this.dS.Vattu);
            bdsVT.CancelEdit();
            bdsVT.Position = vitri;
            gcVT.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnIn.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.vattuTableAdapter.Fill(this.dS.Vattu);
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Xtra_DanhMucVT rpt = new Xtra_DanhMucVT();
            rpt.lbHoTen.Text = Program.mHoten;
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }
    }
}
