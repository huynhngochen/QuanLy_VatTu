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
    public partial class XfrmTongHopNhapXuat : Form
    {
        public XfrmTongHopNhapXuat()
        {
            InitializeComponent();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime ngayBD, ngayKT;
            ngayBD = NGAYBD.Value;
            ngayKT = NGAYKT.Value;
            Xtra_TongHopNhapXuat rpt = new Xtra_TongHopNhapXuat(NGAYBD.Value, NGAYKT.Value);
            rpt.lblNgayBD.Text = NGAYBD.Value.ToString();
            rpt.lblNgayKT.Text = NGAYKT.Value.ToString();
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XfrmTongHopNhapXuat_Load(object sender, EventArgs e)
        {
            NGAYKT.MinDate = NGAYBD.Value.AddDays(1);
        }

        private void NGAYBD_ValueChanged(object sender, EventArgs e)
        {
            NGAYKT.MinDate = NGAYBD.Value.AddDays(1);
        }
    }
}
