using Microsoft.Win32;
using QuanLy.DAO;
using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace QuanLy
{
    public partial class fStaff : Form
    {
        public fStaff()
        {
            InitializeComponent();
            //LoadAcountList();
            LoadDateTimePickerBill();
            
            //LoadListBillByDate(dtpkFormDate.Value, dtpkFormtoDat.Value);
        }
        /*void loadAcountList()
        {

            //string query = "select *from dbo.account";

            //string query = "exec dbo.USP_GetAccountByUserName @userName";

            //DataProvider provider = new DataProvider();

            //dtgvAccount.DataSource = provider.ExcuteQuery(query, new object[]{"admin"});
            //dtgvAccount.DataSource = provider.ExcuteQuery(query);

            //dtgvAccount.DataSource = DataProvider.Instance.ExcuteQuery(query, new object[] {"staff"});

        }*/

        #region methods
        void LoadListBillByDate(DateTime checkin, DateTime checkout)
        {
            dtgvBill.DataSource = BILLDAO.Instance.GetListByDate(checkin, checkout);
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFormDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkFormtoDat.Value = dtpkFormDate.Value.AddMonths(1).AddDays(-1);
        }
        #endregion
        void LoadListFood(string name)
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood(name);
        }
        void loadListFoodtoAdd()
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFoodtoAdd();
        }
        void loadlistCategorytoAdd()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategortoADD();
        }
        void LoadListCategory(string name)
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory(name);
        }
        void refeshFood()
        {
            txbID.Text = "";
            txbFoodName.Text = "";
            cmbFoodCategory.Text = "";
            nmPrice.Value = 0;
        }
        void refeshCategory()
        {
            txbCtegoryID.Text = "";
            txbCategory.Text = "";     
        }
        void loadAccout()
        {
            dtgvAccount.DataSource = AcountDAO.Instance.GetListAccount();
        }

        #region events
        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkFormtoDat.Value);
        }

        private void dtpkFormDate_ValueChanged(object sender, EventArgs e)
        {
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tbFoodCategory_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e) // Show category
        {
            loadlistCategorytoAdd();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dtbvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void fStaff_Load(object sender, EventArgs e)
        {
            LoadComboboxFoodcategory(cmbFoodCategory);
        }
        void LoadComboboxFoodcategory(ComboBox cb)
        {
            cb.DataSource = FoodDAO.Instance.loadFoodcategoryCombobox();
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
        }
        void loadComboboxTable(ComboBox cb)
        {
            cb.DataSource = tableDAO.Instance.loadStatusCombobox();
            cb.DisplayMember = "status";
            cb.ValueMember= "id";
            
        }
        private void btnShowFood_Click(object sender, EventArgs e) // tim kiem
        {
            LoadListFood(txbfindFood.Text.Trim());
        }
        #endregion

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgvFood.Rows[e.RowIndex];
                txbID.Text = row.Cells[0].Value.ToString();
                txbFoodName.Text = row.Cells[1].Value.ToString();
                LoadComboboxFoodcategory(cmbFoodCategory);
                cmbFoodCategory.SelectedValue = row.Cells[2].Value.ToString();
                nmPrice.Text = row.Cells[3].Value.ToString();
            }
        }

        private void dtgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgvCategory.Rows[e.RowIndex];
                txbCategory.Text = row.Cells[1].Value.ToString();
                txbCtegoryID.Text = row.Cells[0].Value.ToString();
            }
        }

        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowStaff_Click(object sender, EventArgs e)
        {
            dtgvAccount.DataSource = AcountDAO.Instance.GetListAccount();
        }

        private void dtgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgvAccount.Rows[e.RowIndex];
                tbUsername.Text = row.Cells[0].Value.ToString();
                tbDislayName.Text = row.Cells[1].Value.ToString();
                txbPassWordAcc.Text = row.Cells[2].Value.ToString();
                cbStaffType.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            dtgvTable.DataSource = tableDAO.Instance.GetListTable();
        }

        private void dtgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgvTable.Rows[e.RowIndex];
                txbIDTable.Text = row.Cells[0].Value.ToString();
                //loadComboboxTable(cbTableStatus);
                tbTableName.Text = row.Cells[1].Value.ToString();
                cbTableStatus.Text = row.Cells[2].Value.ToString();
            }
        }
        #region EditFood
        private void btnAddfodd_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thêm mục này không ? ", " Thông báo!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");

                try
                {
                    conn.Open();
                    string sql = "insert dbo.food values ('" + txbFoodName.Text + "' , " + cmbFoodCategory.SelectedValue.ToString() + " , '" + nmPrice.Value + "') ";

                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Them thanh cong", "Thong bao", MessageBoxButtons.OK);
                        loadListFoodtoAdd();
                        refeshFood();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Loi !");
                }
            }
            else
                return;
        }
        private void btnChangeFood_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn sửa mục này không ? ", " Thông báo!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
                try
                {
                    conn.Open();
                    string sql = "UPDATE  dbo.food SET name = N'" + txbFoodName.Text + "',idCategory = " + cmbFoodCategory.SelectedValue.ToString() + " ,price = " + nmPrice.Value + "  where id = " + dtgvFood.Rows[dtgvFood.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";

                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK);
                        loadListFoodtoAdd();
                        refeshFood();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Đã có lỗi xảy ra!");
                }
            }
            else
            {
                return;
            }
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (dtgvFood.Rows.Count == 0)
            {
                return;
            }

            DialogResult dr = MessageBox.Show("Có chắc chắn xóa dòng dữ liệu này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
                try
                {
                    conn.Open();
                    string sql = "DELETE  dbo.food where id = "+ dtgvFood.Rows[dtgvFood.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";

                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Đã xóa thàng công !", "Thông báo!", MessageBoxButtons.OK);
                        loadListFoodtoAdd();
                        
                        refeshFood();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Đã có lỗi xảy ra !");
                }
            }
            else
                return;
        }

        private void btnfindFood_Click(object sender, EventArgs e)
        {
            LoadListFood(txbfindFood.Text.Trim());
        }
        #endregion
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        #region EditCategory
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
            try
            {
                conn.Open();
                string sql = "insert dbo.foodCategory values ('" + txbCategory.Text + "') ";

                SqlCommand command = new SqlCommand(sql, conn);
                int kq = (int)command.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Them thanh cong", "Thong bao", MessageBoxButtons.OK);
                    loadlistCategorytoAdd();
                    refeshCategory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loi !");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dtgvCategory.Rows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Có chắc chắn xóa dòng dữ liệu này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
                try
                {
                    conn.Open();
                    string sql = "DELETE dbo.foodCategory where id = " + dtgvCategory.Rows[dtgvCategory.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";

                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Xoa thanh cong", "Thong bao", MessageBoxButtons.OK);
                        loadlistCategorytoAdd();
                        refeshFood();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Loi !");
                }
            }
            else
                return;
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
            try
            {
                conn.Open();
                string sql = "UPDATE dbo.foodCategory SET name = N'" + txbCategory.Text + "' where id = " + dtgvCategory.Rows[dtgvCategory.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";

                SqlCommand command = new SqlCommand(sql, conn);
                int kq = (int)command.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Sua thanh cong", "Thong bao", MessageBoxButtons.OK);
                    loadlistCategorytoAdd();
                    refeshCategory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loi !");
            }
        }

        private void tpBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txbfindFood_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory(txbSearchCategory.Text.Trim());
        }
        #endregion
        #region EditTable
        void refeshTable()
        {
            txbIDTable.Text = "";
            tbTableName.Text = "";
            cbTableStatus.Text = "";
        }
        void loadListTabletoShow()
        {
            dtgvTable.DataSource = tableDAO.Instance.GetListTabletoShow();
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");

            try
            {
                conn.Open();
                string sql = "insert tableFood values ('" + tbTableName.Text + "' , N'" + cbTableStatus.Text + "') ";

                SqlCommand command = new SqlCommand(sql, conn);
                int kq = (int)command.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm thành công", "Thông báo!", MessageBoxButtons.OK);
                    loadListTabletoShow();
                    refeshTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Đã có lỗi xảy ra!");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (dtgvTable.Rows.Count == 0)
            {
                return;
            }

            DialogResult dr = MessageBox.Show("Có chắc chắn xóa dòng dữ liệu này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
                try
                {
                    conn.Open();
                    string sql = "DELETE tableFood where id = " + dtgvTable.Rows[dtgvTable.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";

                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Đã xóa thành công", "Thông báo!", MessageBoxButtons.OK);
                        loadListTabletoShow();
                        refeshTable();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Đã có lỗi xảy ra !");
                }
            }
            else
                return;
        }
        private void button2_Click(object sender, EventArgs e)  // Button Edit Table
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn sửa mục này không ? ", " Thông báo!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
                try
                {
                    conn.Open();
                    string sql = "UPDATE tableFood SET name = N'" + tbTableName.Text + "' ,status = " + cbTableStatus.SelectedItem.ToString() + " WHERE id = " + dtgvTable.Rows[dtgvTable.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";
                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Đã sửa thành công", "Thông báo !", MessageBoxButtons.OK);
                        
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Đã có lỗi xảy ra !");
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        private void cbTableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region EditAccount

        #endregion

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");

            try
            {
                conn.Open();
                string sql = "insert account values (N'" + tbUsername.Text + "' , N'" + tbDislayName.Text + "' ,'" + txbPassWordAcc.Text + "', '" + cbStaffType.Text + "') ";

                SqlCommand command = new SqlCommand(sql, conn);
                int kq = (int)command.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm thành công dữ liệu vào bảng", "Thông báo!", MessageBoxButtons.OK);
                    loadListTabletoShow();
                    refeshTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Đã có lỗi xảy ra!");
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (dtgvAccount.Rows.Count == 0)
            {
                return;
            }

            DialogResult dr = MessageBox.Show("Có chắc chắn xóa dòng dữ liệu này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection("Data Source=18CM\\SQLDEV;Database=doan;Integrated Security=false;User ID=sa;Password=1234");
                try
                {
                    conn.Open();
                    string sql = "DELETE account where userName = N'" + dtgvAccount.Rows[dtgvAccount.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'";

                    SqlCommand command = new SqlCommand(sql, conn);
                    int kq = (int)command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Dữ liệu đã được xóa", "Thông báo!", MessageBoxButtons.OK);
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Đã có lỗi xảy ra!");
                }
            }
            else
                return;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            loadAccout();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnExitWater_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ra mục này không ? ", "Thông báo !", MessageBoxButtons.YesNo);
            switch(dr)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void btnExitCategory_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ra mục này không ? ", "Thông báo !", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void btnExitTable_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ra mục này không ? ", "Thông báo !", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void btnExitAccout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ra mục này không ? ", "Thông báo !", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
            }
        }
        public void ExportFile(System.Data.DataTable dataTable, string sheetName, string title)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //tao moi 1 excel workbook

            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;

            //tao phan tieu de
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "E1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "11";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // tao tieu de cot
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "Tên Bàn";
            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "Ngày Vào";
            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Ngày ra";
            cl3.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "Tổng Tiền";
            cl4.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "Giảm giá";
            cl5.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "E3");
            rowHead.Font.Bold = true;

            //Ke vien

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            //Thiet lap mau men
            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // tao mang database
            object[,] arr = new object[dataTable.Rows.Count , dataTable.Columns.Count];

            // chuyen du lieu database vao mang doi tuong
            for(int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for(int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }

            // thiet lap vung du lieu
            int rowStart = 4;
            int colStart = 1;
            int rowEnd = rowStart + dataTable.Rows.Count - 2;
            int columEnd = dataTable.Columns.Count;

            //o bat dau du lieu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, colStart];
            //o ket thuc dien du lieu 

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columEnd];

            // lay ve vung du lieu
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            // dien vao vung du lieu da thiet lap

            range.Value2 = arr;

            // ke vien
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // can giua bang
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            dtgvBill.SelectAll();
            DataObject copydata = dtgvBill.GetClipboardContent();
            if (copydata != null)
            {
                Clipboard.SetDataObject(copydata);
            }
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlwbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object miseddata = System.Reflection.Missing.Value;
            xlwbook = xlapp.Workbooks.Add(miseddata);

            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlwbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            xlr.Select();

            xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);



            //System.Data.DataTable dataTable = new System.Data.DataTable();
            //DataColumn col1 = new DataColumn("name");
            //DataColumn col2 = new DataColumn("dateCheckin");
            //DataColumn col3 = new DataColumn("dateCheckout");
            //DataColumn col4 = new DataColumn("totalPrice");
            //DataColumn col5 = new DataColumn("discount");


            //dataTable.Columns.Add(col1);
            //dataTable.Columns.Add(col2);
            //dataTable.Columns.Add(col3);
            //dataTable.Columns.Add(col4);
            //dataTable.Columns.Add(col5);

            //foreach(DataGridViewRow dtgvRow in dtgvBill.Rows)
            //{
            //    DataRow dtRow = dataTable.NewRow();
            //    dtRow[0] = dtgvRow.Cells[0].Value;
            //    dtRow[1] = dtgvRow.Cells[1].Value;
            //    dtRow[2] = dtgvRow.Cells[2].Value;
            //    dtRow[3] = dtgvRow.Cells[3].Value;
            //    dtRow[4] = dtgvRow.Cells[4].Value;

            //    dataTable.Rows.Add(dtRow);
            //}
            //ExportFile(dataTable, "Danh sach", "Danh Sach Thanh Toan");

        }

        private void cmbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}