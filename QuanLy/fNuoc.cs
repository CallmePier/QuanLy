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
    public partial class fNuoc : Form
    {
        public fNuoc()
        {
            InitializeComponent();
            loadListFoodtoAdd();
        }
        #region methods
        void LoadListFood(string name)
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood(name);
        }
        void loadListFoodtoAdd()
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFoodtoAdd();
        }
        void refeshFood()
        {
            txbID.Text = "";
            txbFoodName.Text = "";
            cmbFoodCategory.Text = "";
            nmPrice.Value = 0;
        }
        void LoadComboboxFoodcategory(ComboBox cb)
        {
            cb.DataSource = FoodDAO.Instance.loadFoodcategoryCombobox();
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
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
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "D1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "11";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // tao tieu de cot
            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "ID";
            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "Tên Thức Uống";
            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Danh Mục";
            cl3.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "Tổng Tiền";
            cl4.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "D3");
            rowHead.Font.Bold = true;

            //Ke vien

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            //Thiet lap mau men
            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // tao mang database
            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            // chuyen du lieu database vao mang doi tuong
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
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

        #endregion

        #region envents
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
        private void btnExitWater_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không ?", " Thông báo!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }

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
                        MessageBox.Show("Đã thêm dữ liệu thành công vào bảng", "Thông báo!", MessageBoxButtons.OK);
                        loadListFoodtoAdd();
                        refeshFood();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Đã có lỗi !");
                }
            }
            else
                return;
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
                    string sql = "DELETE  dbo.food where id = " + dtgvFood.Rows[dtgvFood.CurrentCell.RowIndex].Cells[0].Value.ToString() + "";

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

        private void btnEditFood_Click(object sender, EventArgs e)
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

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            DataColumn col1 = new DataColumn("id");
            DataColumn col2 = new DataColumn("name");
            DataColumn col3 = new DataColumn("idCategory");
            DataColumn col4 = new DataColumn("price");
            


            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            

            foreach (DataGridViewRow dtgvRow in dtgvFood.Rows)
            {
                DataRow dtRow = dataTable.NewRow();
               dtRow[0] = dtgvRow.Cells[0].Value;
                dtRow[1] = dtgvRow.Cells[1].Value;
                dtRow[2] = dtgvRow.Cells[2].Value;
                dtRow[3] = dtgvRow.Cells[3].Value;
                
                dataTable.Rows.Add(dtRow);
            }
            ExportFile(dataTable, "Danh sach", "DANH SÁCH TÔNG CÁC THỨC UỐNG");




            //dtgvFood.SelectAll();
            //DataObject copydata = dtgvFood.GetClipboardContent();
            //if (copydata != null)
            //{
            //    Clipboard.SetDataObject(copydata);
            //}
            //Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            //xlapp.Visible = true;
            //Microsoft.Office.Interop.Excel.Workbook xlwbook;
            //Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            //object miseddata = System.Reflection.Missing.Value;
            //xlwbook = xlapp.Workbooks.Add(miseddata);

            //xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlwbook.Worksheets.get_Item(1);
            //Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            //xlr.Select();

            //xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            loadListFoodtoAdd();
        }

        private void btnfindFood_Click(object sender, EventArgs e)
        {
            LoadListFood(txbfindFood.Text.Trim());
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txbID.Text = "";
            txbFoodName.Text = "";
            cmbFoodCategory.Text = "";
            nmPrice.Value = 0;
        }
        #endregion

        private void fNuoc_Load(object sender, EventArgs e)
        {
            LoadComboboxFoodcategory(cmbFoodCategory);
        }
    }
}
