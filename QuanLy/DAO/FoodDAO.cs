using QuanLy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLy.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetListFoodCategoryID(int id)
        {
            List<Food> list = new List<Food>();

            string query = "select *from food where idCategory = " + id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food foods = new Food(item);
                list.Add(foods);
            }
            return list;
        }
        public List<Food> GetListFood(string name)
        {
            List<Food> list = new List<Food>();

            string query = "select *from food WHERE name LIKE N'%"+name+"%'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food foods = new Food(item);
                list.Add(foods);
            }
            return list;
        }
        public List<Food> GetListFoodtoAdd()
        {
            List<Food> list = new List<Food>();

            string query = "select *from food";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food foods = new Food(item);
                list.Add(foods);
            }
            return list;
        }
        public bool InsertFood(string name, int id, float price)
        {
            
            string query = string.Format("insert into dbo.food ( name, idCategory, price)values ( N'{0}', {1}, {2})", name , id , price);
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateFood(int idFood , string name, int id, float price)
        {

            string query = string.Format("update into dbo.food set name = N'{0}', idCategory = {1} , price = {2} , where id = {3})", name, id, price, idFood);
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            billInforDAO.Instance.deleteInforBillbyFoodID(idFood);
            string query = string.Format("delete dbo.food where id = {0})", idFood);
            int result = DataProvider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public DataTable loadFoodcategoryCombobox()
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM Foodcategory");
            return data;
        }
    }
}
