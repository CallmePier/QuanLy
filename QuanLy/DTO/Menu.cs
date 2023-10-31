using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy.DTO
{
    public class Menu
    {
        private string name;
        private int count;
        private float price;
        private float totalPrice;

       
        public Menu(DataRow row)
        {
            this.Name = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }

        public Menu(string name, int count, float price, float totalPrice =0)
        {
            this.Name = name;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menu() { }

        public string Name {
            get { return name; } set { name = value; }
        }
        public int Count {
            get { return count; } set {  count = value; }
        }
        public float Price {
            get { return price; } set {  price = value; }
        }
        public float TotalPrice {
            get { return totalPrice; } set {  totalPrice = value; }
        }
    }
}
