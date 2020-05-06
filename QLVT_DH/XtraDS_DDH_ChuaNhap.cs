using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLVT_DH
{
    public partial class XtraDS_DDH_ChuaNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraDS_DDH_ChuaNhap()
        {
            InitializeComponent();
            sp_DS_DDH_ChuaNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            sp_DS_DDH_ChuaNhapTableAdapter.Fill(ds1.sp_DS_DDH_ChuaNhap);
            if(Program.mChinhanh == 0)
            {
                lblChiNhanh.Text = "TP HCM";
            }
            else
            {
                lblChiNhanh.Text = "TP CẦN THƠ";
            }
        }

    }
}
