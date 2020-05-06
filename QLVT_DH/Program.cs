using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using System.Data;
using System.Data.SqlClient;

namespace QLVT_DH
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string servername;
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static SqlDataReader myReader;
        //  public static String servername = "";
        public static String username = "";
        public static String mlogin = "";
        public static String password = "";

        public static String database = "QLVT_DATHANG";
        public static String remotelogin = "HTKN";
        public static String remotepassword = "123456";
        public static String mloginDN = "";
        public static String passwordDN = "";
        public static String mGroup = "";
        public static String mHoten = "";
        public static int mChinhanh = 0;

        public static BindingSource bds_dspm = new BindingSource();
        public static frmMain frmChinh;

        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + Program.servername + ";Initial Catalog=" +
                      Program.database + ";User ID=" +
                      Program.mlogin + ";password=" + Program.password;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 600;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public static SqlCommand sqlcmd = new SqlCommand();
        public static int execStoreProcedureWithReturnValue(SqlCommand sqlcmd)
        {
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlParameter retval = sqlcmd.Parameters.Add("@return_value", SqlDbType.Int);
            retval.Direction = ParameterDirection.ReturnValue;
            try { sqlcmd.ExecuteNonQuery(); }
            catch (Exception) { }
            return int.Parse(sqlcmd.Parameters["@return_value"].Value.ToString());
        }
        public static int taoLogin(ThongTinLogin thongTinLogin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.connstr))
                using (SqlCommand cmd = new SqlCommand("sp_TaoTaiKhoan1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LGNAME", thongTinLogin.LoginName));
                    cmd.Parameters.Add(new SqlParameter("@PASS", thongTinLogin.Pass));
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", thongTinLogin.UserName));
                    cmd.Parameters.Add(new SqlParameter("@ROLE", thongTinLogin.Role));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    //int retVT = Program.sqlcmd.Parameters["@RET"].Value.ToString();
                    //  MessageBox.Show("Đã Tạo login thành công", "Message", MessageBoxButtons.OK);
                    return int.Parse(Program.sqlcmd.Parameters["@RET"].Value.ToString()); ;
                }
            }
            catch (Exception ex)
            {
              // MessageBox.Show("Tạo login không thành công", "Lỗi", MessageBoxButtons.OK);
                return 1;
            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmChinh = new frmMain();
            Application.Run(frmChinh);
          //  Application.Run(new frmDonDatHang());
        }
    }
}
