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
    public partial class fQLBan : Form
    {
        public fQLBan()
        {
            InitializeComponent();
        }
        private void fQLBan_Load(object sender, EventArgs e)
        {
            dtgvTable.DataSource = tableDAO.Instance.GetListTable();
        }

        #region methods
        void loadListTabletoShow()
        {
            dtgvTable.DataSource = tableDAO.Instance.GetListTabletoShow();
        }
        void refeshTable()
        {
            txbIDTable.Text = "";
            tbTableName.Text = "";
            cbTableStatus.Text = "";
        }
        void SearchTalbe(string name)
        {
            dtgvTable.DataSource = tableDAO.Instance.GetListTable(name);
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
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "C1");
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
            cl2.Value2 = "Tên Bàn";
            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Trạng Thái";
            cl3.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "C3");
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

        private void btnEditTable_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            DataColumn col1 = new DataColumn("id");
            DataColumn col2 = new DataColumn("name");
            DataColumn col3 = new DataColumn("status");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);

            foreach (DataGridViewRow dtgvRow in dtgvTable.Rows)
            {
                DataRow dtRow = dataTable.NewRow();
                dtRow[0] = dtgvRow.Cells[0].Value;
                dtRow[1] = dtgvRow.Cells[1].Value;
                dtRow[2] = dtgvRow.Cells[2].Value;


                dataTable.Rows.Add(dtRow);
            }
            ExportFile(dataTable, "Danh sach", "DANH SÁCH TỔNG CÁC BÀN");
        }

        private void btnExitTable_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không ?", " Thông báo!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }

        private void btnSearchWater_Click(object sender, EventArgs e)
        {
            SearchTalbe(txbSearchCategory.Text.Trim());
        }

        private void btnReload_Click(object sender, EventArgs e)
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
        #endregion


    }
}
