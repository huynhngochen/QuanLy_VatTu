using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLVT_DH
{
    public partial class Xtra_DanhSachNV : DevExpress.XtraReports.UI.XtraReport
    {
        public Xtra_DanhSachNV()
        {
            InitializeComponent();
            sp_DanhSachNVTableAdapter.Connection.ConnectionString = Program.connstr;
            sp_DanhSachNVTableAdapter.Fill(ds1.sp_DanhSachNV);
        }

    }
}
