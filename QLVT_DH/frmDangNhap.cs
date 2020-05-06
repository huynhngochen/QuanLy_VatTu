using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace QLVT_DH
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLVT_DATHANGDataSet.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.qLVT_DATHANGDataSet.V_DS_PHANMANH);
            tENCNComboBox.SelectedIndex = 1;
            tENCNComboBox.SelectedIndex = 0;
            Program.frmChinh.ribbonChucNang.Visible = false;
            Program.frmChinh.ribbonDanhMuc.Visible = false;
            Program.frmChinh.ribbonBaoCao.Visible = false;
        }

        private void tENCNComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tENCNComboBox.SelectedValue != null) { 
            Program.servername = tENCNComboBox.SelectedValue.ToString();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "")
            {
                MessageBox.Show("Tài Khoản Đăng Nhập không được rỗng!", "Báo Lỗi đăng nhập", MessageBoxButtons.OK);
                txtLogin.Focus();
                return;
            }
            Program.mlogin = txtLogin.Text;
            Program.password = txtPass.Text;
            if (Program.KetNoi() == 0) return;

            Program.mChinhanh = tENCNComboBox.SelectedIndex;
            Program.bds_dspm = bdsDSPM;

            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;

             MessageBox.Show("Đăng Nhập thành công!", "", MessageBoxButtons.OK);
            
            SqlDataReader myReader;

            string strLenh = "EXEC SP_DANGNHAP '" + Program.mlogin + "'";

            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();


            Program.username = Program.myReader.GetString(0);     // Lay user name
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username, password", "", MessageBoxButtons.OK);
                return;
            }
            Program.mHoten = Program.myReader.GetString(1);
            Program.mGroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();
            Program.frmChinh.MANV.Text = "Mã Nhân Viên: " + Program.username;
            Program.frmChinh.HOTEN.Text = "Họ Tên: " + Program.mHoten;
            Program.frmChinh.NHOM.Text = "Nhóm: " + Program.mGroup;

            Program.frmChinh.ribbonChucNang.Visible = true;
            Program.frmChinh.ribbonDanhMuc.Visible = true;
            Program.frmChinh.ribbonBaoCao.Visible = true;
            //FormNV f = new FormNV();
            //f.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Program.frmChinh.MANV.Text = "";
            Program.frmChinh.HOTEN.Text = "";
            Program.frmChinh.NHOM.Text = "";
            Program.frmChinh.ribbonChucNang.Visible = false;
            Program.frmChinh.ribbonDanhMuc.Visible = false;
            Close();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
