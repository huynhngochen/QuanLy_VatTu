using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLVT_DH
{
    public partial class Xtra_HoatDong : DevExpress.XtraReports.UI.XtraReport
    {
        public Xtra_HoatDong(int nhaphang, DateTime ngayBD, DateTime ngayKT, int maNV)
        {
            InitializeComponent();
            ds1.EnforceConstraints = false;
            this.sp_HoatDongNhanVienTableAdapter.Fill(ds1.sp_HoatDongNhanVien, nhaphang, ngayBD, ngayKT, maNV);
        }

    }
}
