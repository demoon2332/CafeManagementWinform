using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DTO
{
    public class Bill
    {
        private DateTime? dateCheckIn, dateCheckOut;
        private int id, type;


        public Bill(int id,DateTime? dateCheckIn,DateTime? dateCheckOut,int type)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Type = type;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];
            string dateCheckOutTemp = row["dateCheckOut"].ToString();
            if(dateCheckOutTemp != "")
                this.DateCheckOut = (DateTime?)row["dateCheckOut"];
            this.Type = (int)row["type"];
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }
        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }
    }
}
