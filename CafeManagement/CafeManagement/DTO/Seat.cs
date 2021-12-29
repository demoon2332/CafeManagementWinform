using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DTO
{
    public class Seat
    {

        private int id;
        private string name;
        private int status;

        public Seat(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = Int32.Parse(row["status"].ToString());
        }

        public Seat(int id,string name,int status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
