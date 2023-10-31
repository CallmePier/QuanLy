using QuanLy.DAO;
using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy
{
    public partial class fLogin : Form
    {
        #region methods
        bool login(string username, string password)
        {
            return AcountDAO.Instance.login(username, password);
        }
        #endregion

        #region events
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bntLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string password = txbPassword.Text;
            if (login(username, password))
            {
                Account1 loginAccount = AcountDAO.Instance.GetAccountByUserName(username);
                fTableManager f = new fTableManager(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Thông tin bạn nhập không đúng!");
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ?", "Thông báo !", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(cbShowPassword.Checked == true)
                {
                    txbPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txbPassword.UseSystemPasswordChar = true;
                }
            }
            catch
            {
                MessageBox.Show("Không thể hiện mật khẩu của bạn!", "Thông báo!", MessageBoxButtons.OK);
            }
        }

        private void txbUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
