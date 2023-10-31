    using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.DAO
{
    public class billInforDAO
    {
        private static billInforDAO instance;

        public static billInforDAO Instance
        {
            get { if (instance == null) instance = new billInforDAO(); return billInforDAO.instance; }
            private set { billInforDAO.instance = value; }
        }
        private billInforDAO()
        {

        }
        public List<billInfor> getListBillInfor(int id)
        {
            List<billInfor> listBillinFor = new List<billInfor>();

            DataTable data = DataProvider.Instance.ExcuteQuery("select *from dbo.billInfor where idBill = " + id);

            foreach (DataRow item in data.Rows) 
            {
                billInfor info = new billInfor(item);
                listBillinFor.Add(info);    
            }
            return listBillinFor;
        }
        public void InsertBillInfor(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExcuteNonQuery("USP_InsertBillInfor @idBill , @idFood , @count", new object[] { idBill , idFood , count });
        }
        public void  deleteInforBillbyFoodID(int id)
        {
            DataProvider.Instance.ExcuteQuery("delete dbo.billInfor where idFood = " + id);
        }

    }
}
