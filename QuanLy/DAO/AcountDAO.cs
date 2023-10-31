using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.DAO
{
    public class AcountDAO
    {
        private static AcountDAO instance;
        public AcountDAO() { 

        }
        public static AcountDAO Instance {
            get { if (instance == null) instance = new AcountDAO(); return AcountDAO.instance; }
            private set { AcountDAO.instance = value; }
        }
        public bool login(string username, string password)
        {
            string query = " USP_Login @userName , @passWord";
            
            DataTable result = DataProvider.Instance.ExcuteQuery(query,new object[] {username, password});
           

            return result.Rows.Count > 0;
        }
        public QuanLy.DTO.Account1 GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("Select *from dbo.account where userName = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new DTO.Account1(item);
            }
            return null;
        }
        public bool updateAccount(string userName, string displayName, string passWord, string newpassWord)
        {
            int result = DataProvider.Instance.ExcuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @passWord , @newPassword", new object[] {userName , displayName , passWord , newpassWord});
            return result > 0;
        }
        public List<Account1> GetListAccount()
        {
            List<Account1> list = new List<Account1>();
            string query = "select *from account";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                Account1 account = new Account1(item);
                list.Add(account);
            }
            return list;
        }
        public List<Account1> GetSearchAccount(string name)
        {
            List<Account1> list = new List<Account1>();
            string query = "select *from account WHERE userName LIKE N'%"+name+"%'";

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Account1 acc = new Account1(item);
                list.Add(acc);
            }
            return list;
        }

    }
}
