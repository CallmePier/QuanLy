using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.DTO
{
    public class billInfor
    {
        private int id;
        private int billdId;
        private int foodId;
        private int foodCount;

        public billInfor(int id, int billdId, int foodId, int foodCount)
        {
            this.Id = id;
            this.BilldId = billdId;
            this.FoodId = foodId;
            this.FoodCount = foodCount;
        }
        public billInfor()
        {

        }

        public int Id {
            get { return id; } set { id = value; }
        }
        public int BilldId {
            get { return billdId; } set {  billdId = value; }
        }
        public int FoodId {
            get { return foodId; } set{ foodId = value; }
        }
        public int FoodCount {
            get { return foodCount; }
            set {  foodCount = value; }
        }

        public billInfor(DataRow row)
        {
            this.Id = (int)row["id"];
            this.BilldId = (int)row["idBill"];
            this.FoodId = (int)row["idFood"];
            this.FoodCount = (int)row["count"];


        }
    }

}
