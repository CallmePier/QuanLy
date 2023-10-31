using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLy.DAO
{
    public class BILLDAO
    {
        private static BILLDAO instance;

        public static BILLDAO Instance {
            get { if (instance == null) instance = new BILLDAO(); return BILLDAO.instance; }
            private set { BILLDAO.instance = value; }
        }
        private BILLDAO()
        {
        }
        public int getUncheckBillDAObyTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from dbo.bill where idTable = " + id + "and status = 0");
            if(data.Rows.Count > 0)
            {
                BILL bill = new BILL(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        public void checkOut(int id,int discount, float totalPrice)
        {
            string query = "update dbo.bill set dateCheckout = getdate(),  status = 1," + "discount = " + discount + ", totalPrice = " + totalPrice + "where id =" + id;
            DataProvider.Instance.ExcuteNonQuery(query);
        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExcuteNonQuery("exec USP_InsertBill @idTable", new object[] { id });
        }
        public DataTable GetListByDate(DateTime checkin, DateTime checkout)
        {
            return DataProvider.Instance.ExcuteQuery("USP_GetListBillByDate @checkin , @checkout", new object[] {checkin , checkout});
        }
        public int getMAXIDbill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("select MAX(id) from bill");
            }
            catch
            {
                return 1;
            }
            
        }
    }
}
