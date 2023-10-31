using Microsoft.Reporting.WinForms;
using QuanLy.doanDataSet1TableAdapters;
using QuanLy.DTO;
using QuanLy.ReportWater;
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
    public partial class WaterReport : Form
    {
        

        public WaterReport()
        {
            InitializeComponent();
        }

        private void WaterReport_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.doanConnectionString;

            DataTable dt = new DataTable();
            string conn_string = Properties.Settings.Default.doanConnectionString;
            SqlConnection sqlConnect = new SqlConnection(conn_string);
            sqlConnect.Open();
            SqlCommand cmd = new SqlCommand("USP_GetListBillByDateByReprot1 '" + dtpkCheckIn.Value.ToString("yyyyMMdd") + "','" + dtpkCheckOut.Value.ToString("yyyyMMdd") + "'", sqlConnect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            rptWater.LocalReport.DataSources.Clear();
            rptWater.LocalReport.DataSources.Add(rds);
            this.rptWater.RefreshReport();
        }

        private void rptWater_Load(object sender, EventArgs e)
        {
            
        }

        private void btnTaoReport_Click(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.doanConnectionString;

            DataTable dt = new DataTable();
            string conn_string = Properties.Settings.Default.doanConnectionString;
            SqlConnection sqlConnect = new SqlConnection(conn_string);
            sqlConnect.Open();
            SqlCommand cmd = new SqlCommand("USP_GetListBillByDateByReprot1 '" + dtpkCheckIn.Value.ToString("yyyyMMdd") + "','" + dtpkCheckOut.Value.ToString("yyyyMMdd") + "'", sqlConnect);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            //rptWater.LocalReport.ReportEmbeddedResource = @"QuanLy\QuanLy\ReportWater\waterReport.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            rptWater.LocalReport.DataSources.Clear();
            rptWater.LocalReport.DataSources.Add(rds);
            this.rptWater.RefreshReport();
            //SqlCommand command = new SqlCommand();
            //command.CommandText = "";

            //command.CommandType = CommandType.StoredProcedure;
            //command.Connection = con;
            //command.Parameters.Add(new SqlParameter("@dateCheckin" , dtpkCheckOut));
            //command.Parameters.Add(new SqlParameter(" @dateCheckout", dtpkCheckOut));

            //DataSet ds = new DataSet();
            //SqlDataAdapter dap = new SqlDataAdapter(command);
            //dap.Fill(ds);

            //rptWater.ProcessingMode = ProcessingMode.Local;
            //rptWater.LocalReport.ReportPath = "./Report/waterReport.rdlc";

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ReportDataSource rds = new ReportDataSource();
            //    rds.Name = "DataSet1";
            //    rds.Value = ds.Tables[0];


            //    rptWater.LocalReport.DataSources.Clear();

            //    rptWater.LocalReport.DataSources.Add(rds);

            //    rptWater.RefreshReport();

            //}   
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
