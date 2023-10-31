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
    public partial class fAccount : Form
    {
        private Account1 loginAccount;

        public Account1 LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; changeAccount(loginAccount); }
        }

        #region events
        public fAccount(Account1 acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateAccount();
        }
        private void fAccount_Load(object sender, EventArgs e)
        {
        }
        private event EventHandler<AccountEvent> updateAccount1;
        public event EventHandler<AccountEvent> newUpdateAccount
        {
            add { updateAccount1 += value; }
            remove { updateAccount1 -= value; }
        }
        #endregion
        #region methods
        void changeAccount(Account1 acc)
        {
            txbUsername.Text = LoginAccount.UserName;
            txbTenhienthi.Text = LoginAccount.DisplayName;
        }
        void updateAccount()
        {
            string displayName = txbTenhienthi.Text;
            string passWord = txbPassword.Text;
            string newpasWord = txbNewPassword.Text;
            string renewpassWord = txbReNewPassword.Text;
            string userName = txbUsername.Text;
            if (!newpasWord.Equals(renewpassWord))
            {
                MessageBox.Show("Mật khẩu sai !");
            }
            else
            {
                if (AcountDAO.Instance.updateAccount(userName,displayName,passWord ,newpasWord))
                {
                    MessageBox.Show("Cập nhật thành công !");
                    if(updateAccount1 != null) 
                    {
                        updateAccount1(this, new AccountEvent(AcountDAO.Instance.GetAccountByUserName(userName)));
                    }                           
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu !");
                }
            }

        }
        #endregion

        private void checkbShow1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkbShow1.Checked == true)
                {
                    txbNewPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txbNewPassword.UseSystemPasswordChar = true;
                }
            }
            catch
            {
                MessageBox.Show("Không thể hiện mật khẩu của bạn!", "Thông báo!", MessageBoxButtons.OK);
            }
        }

        private void checkbShow2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkbShow2.Checked == true)
                {
                    txbReNewPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txbReNewPassword.UseSystemPasswordChar = true;
                }
            }
            catch
            {
                MessageBox.Show("Không thể hiện mật khẩu của bạn!", "Thông báo!", MessageBoxButtons.OK);
            }
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account1 acc;

        public Account1 Acc
        {
            get { return acc; }
            set { acc = value; }
        }
        public AccountEvent(Account1 acc) 
        {
            this.Acc = acc;
        }
    }
}
