using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLVT_DH
{
    public partial class Xtra_DanhMucVT : DevExpress.XtraReports.UI.XtraReport
    {
        public Xtra_DanhMucVT()
        {
            InitializeComponent();
            sp_DanhMucVatTuTableAdapter.Connection.ConnectionString = Program.connstr;
            sp_DanhMucVatTuTableAdapter.Fill(ds2.sp_DanhMucVatTu);
        }

    }
}
