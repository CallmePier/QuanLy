using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.DTO
{
    public class BILL
    {
        private int id;

        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;

        private int status;
        private int discount;

        public int Id { 
            get { return id; } set {  id = value; }
        }
        public DateTime? DateCheckIn { 
            get { return dateCheckIn; } set { dateCheckIn = value; }
        }
        public DateTime? DateCheckOut { 
            get { return dateCheckOut; } set { dateCheckOut = value; }
        }
        public int Status {
            get { return status; } set {  status = value; }
        }
        public int Discount { 
            get { return discount; } set {  discount = value; }
        }
        public BILL() { }
        public BILL(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status,int discount=0)
        {
            this.Id =id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
        }
        public BILL(DataRow row)
        {
            this.Id = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckin"];
            var dataCheckOutTemp = row["dateCheckout"];
            if (dataCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)dataCheckOutTemp;
            }
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }

    }
}
