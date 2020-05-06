using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;

namespace QLVT_DH
{
    public partial class Xtra_TongHopNhapXuat : DevExpress.XtraReports.UI.XtraReport
    {
        public Xtra_TongHopNhapXuat(DateTime NGAYBD, DateTime NGAYKT)
        {
            InitializeComponent();
            ds1.EnforceConstraints = false;
            this.sp_TongHopNhapXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sp_TongHopNhapXuatTableAdapter.Fill(ds1.sp_TongHopNhapXuat, NGAYBD, NGAYKT);
        }

    }
}
