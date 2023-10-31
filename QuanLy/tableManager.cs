using QuanLy.DAO;
using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Documents;
using System.Windows.Forms;
using Menu = QuanLy.DTO.Menu;

namespace QuanLy
{
    public partial class fTableManager : Form
    {
        private Account1 loginAccount;

        public Account1 LoginAccount 
        {
            get { return loginAccount; }
            set { loginAccount = value; changeAccount(loginAccount.Type); }
        }

        public fTableManager(Account1 acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadTable();
            loadCategory();
            //LoadComboboxTable(cbStwichTable);
        }

        #region method
        void changeAccount(int type)
        {
            staffToolStripMenuItem.Enabled = type == 1;
            thôngTinToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        void loadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategortoADD();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void loadFoodbyCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetListFoodCategoryID(id);

            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void loadTable()
        {
            flpTable.Controls.Clear();
            List<table> tableList = tableDAO.Instance.loadtableList();
            foreach (table item in tableList)
            {
                Button btn = new Button() { Width = tableDAO.TableWith, Height = tableDAO.tableHeigh };
                btn.Text = item.Name + Environment.NewLine + item.Status;

                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua; break;
                    default:
                        btn.BackColor = Color.LightGoldenrodYellow; break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void showBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listbillInfor = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach(Menu item in listbillInfor)
            {
                ListViewItem lsvItem = new ListViewItem(item.Name.ToString());


                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTotalPrice.Text = totalPrice.ToString("c", culture);
        }
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = tableDAO.Instance.loadtableCombobox();
            cb.DisplayMember = "name";
        }
        
        #endregion
        #region Events
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as table).Id;
            lsvBill.Tag = (sender as Button).Tag;
            showBill(tableID);
        }

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnAdddish_Click(object sender, EventArgs e)
        {
            table table1 = lsvBill.Tag as table;
            int idBill = BILLDAO.Instance.getUncheckBillDAObyTableID(table1.Id);
            int foodID = (cbFood.SelectedItem as Food).Id;
            int count = (int)mudValue.Value;

            if(idBill == -1)
            {
                BILLDAO.Instance.InsertBill(table1.Id);
                billInforDAO.Instance.InsertBillInfor(BILLDAO.Instance.getMAXIDbill(), foodID, count);
            }
            else
            {
                billInforDAO.Instance.InsertBillInfor(idBill, foodID, count);
            }
            showBill(table1.Id);
            loadTable();
        }
        private void cbStwichTable_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fStaff f = new fStaff();
            //f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount(LoginAccount);
            f.newUpdateAccount += f_newUpdateAccount;
            f.ShowDialog();
        }

        void f_newUpdateAccount(object sender, AccountEvent e)
        {
            thôngTinToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }
        private void fTableManager_Load(object sender, EventArgs e)
        {

        }
        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  //cbCategory
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.Id;

            loadFoodbyCategoryID(id);
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            table table2 = lsvBill.Tag as table;

            int idBill = BILLDAO.Instance.getUncheckBillDAObyTableID(table2.Id);
            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txtTotalPrice.Text.Split(',')[0]);
            double finaltotalPrice = totalPrice - (totalPrice/100)*discount;

            if(idBill != -1) 
            {
                if(MessageBox.Show(string.Format("Bạn muốn thanh toán cho bàn {0}\n Tổng tiền : {1} VND sau khi giảm giá {2}% ",table2.Name, finaltotalPrice, discount) , "Thông báo!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BILLDAO.Instance.checkOut(idBill, discount,(float)finaltotalPrice);
                    showBill(table2.Id);
                    loadTable();
                }

            }
            
        }
        private void btnChuyenban_Click(object sender, EventArgs e)
        {

        }
        private void nmDiscount_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadTable();
        }

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaterReport fwaterReport = new WaterReport();
            fwaterReport.ShowDialog();
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picMenu picMenu = new picMenu();
            picMenu.ShowDialog();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHoaDon fhoaDon = new fHoaDon();
            fhoaDon.ShowDialog();
        }

        private void nướcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fNuoc fnuoc = new fNuoc();
            fnuoc.ShowDialog();
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fDanhMuc fdanhmuc = new fDanhMuc();
            fdanhmuc.ShowDialog();
        }

        private void bànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQLBan fqlBan = new fQLBan();
            fqlBan.ShowDialog();
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyAccount fqlAccount = new fQuanLyAccount();
            fqlAccount.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát tài khoản ra không ?", "Thông báo !", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }
    }
}
