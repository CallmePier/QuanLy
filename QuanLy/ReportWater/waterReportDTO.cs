using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLy.ReportWater
{
    public class waterReportDTO
    {
        public string Name1 { get; set; }

        public DateTime? DateCheckIn { get; set; }

        public DateTime? DateCheckOut { get; set; }

        public float totalPrice {  get; set; }
        public int Discount { get; set; }
    }
}
