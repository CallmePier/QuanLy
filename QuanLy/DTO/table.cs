using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.DTO
{
    public class table
    {
        private int ID;
        private string name;
        private string status;

        public int Id 
        {
            get { return ID; } set { ID = value; }
        }
        public string Name 
        {
            get { return name; } set { name = value; }
        }
        public string Status {
            get { return status; } set { status = value; }
        }

        public table() { }

        public table(int id, string name, string status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
        public table(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
            
        }
    }
}
