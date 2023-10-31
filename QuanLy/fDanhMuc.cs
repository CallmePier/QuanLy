using QuanLy.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy
{
    public partial class fDanhMuc : Form
    {
        public fDanhMuc()
        {
            InitializeComponent();
            loadlistCategorytoAdd();
        }
        #region methods
        void loadlistCategorytoAdd()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategortoADD();
        }
        void LoadListCategory(string name)
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory(name);
        }
        void refeshCategory()
        {
            txbCtegoryID.Text = "";
            txbCategory.Text = "";
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
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "B1");
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
            cl2.Value2 = "Tên Danh Mục";
            cl2.ColumnWidth = 25.0;
           
            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "B3");
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


        #region events
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
                    MessageBox.Show("Đã thêm thành công dữ liệu vào bảng", "Thông báo!", MessageBoxButtons.OK);
                    loadlistCategorytoAdd();
                    refeshCategory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Đã có lỗi xẩy ra!");
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
                        MessageBox.Show("Đã xóa thành công dữ liệu của bảng", "Thông báo!", MessageBoxButtons.OK);
                        loadlistCategorytoAdd();
                        refeshCategory();
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
                    MessageBox.Show("Sửa thành công dữ liệu của bảng", "Thông báo!", MessageBoxButtons.OK);
                    loadlistCategorytoAdd();
                    refeshCategory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Đã có lỗi xảy ra !");
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            DataColumn col1 = new DataColumn("id");
            DataColumn col2 = new DataColumn("name");
           
            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
           
            foreach (DataGridViewRow dtgvRow in dtgvCategory.Rows)
            {
                DataRow dtRow = dataTable.NewRow();
                dtRow[0] = dtgvRow.Cells[0].Value;
                dtRow[1] = dtgvRow.Cells[1].Value;
               

                dataTable.Rows.Add(dtRow);
            }
            ExportFile(dataTable, "Danh sach", "DANH SÁCH TỔNG CÁC DANH MỤC");
        }

        private void btnExitCategory_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không ?", " Thông báo!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadlistCategorytoAdd();
        }

        private void btnSearchWater_Click(object sender, EventArgs e)
        {
            LoadListCategory(txbSearchCategory.Text.Trim());
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
        #endregion

        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
