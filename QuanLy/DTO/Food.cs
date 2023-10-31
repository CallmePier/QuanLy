using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.DTO
{
    public class Food
    {
        private int id;
        private string name;
        private int idCategory;
        private float price;

        public Food(int id, string name, int idCategory, float price)
        {
            this.Id = id;
            this.Name = name;
            this.IdCategory = idCategory;
            this.Price = price;
        }

        public Food()
        {
        } 
        public Food(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.IdCategory = (int)row["idCategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }
      


        public int Id {
            get { return id; } set {  id = value; }
        }
        public string Name {
            get { return name; } set {  name = value; }
        }
        public int IdCategory {
            get { return idCategory; } set { idCategory = value; }
        }
        public float Price {
            get { return price; } set {  price = value; }
        }
    }
}
