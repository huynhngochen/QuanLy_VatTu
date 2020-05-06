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
    public partial class frmTaoLogin : Form
    {
        public ThongTinLogin thongTinLogin = null;
        public frmTaoLogin(String Role)
        {
            InitializeComponent();
            if (string.Compare(Role, "CONGTY") == 0) cmbNhom.Items.Add("CONGTY");
            if (string.Compare(Role, "CHINHANH") == 0)
            {
                cmbNhom.Items.Add("CHINHANH");
                cmbNhom.Items.Add("USER");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtTenLogin.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin", "Lỗi", MessageBoxButtons.OK);
                return;
            }
            
            thongTinLogin = new ThongTinLogin
            {
                Role = cmbNhom.SelectedItem.ToString(),
                LoginName = txtTenLogin.Text.Trim(),
                Pass = txtPass.Text.Trim()
            };
            this.Hide();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            thongTinLogin = null;
            this.Close();
        }
    }
}
