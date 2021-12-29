using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DTO
{
    class Staff
    {

        private int id;
        private string displayName;
        private string username;
        private string phone;
        private int gender;
        private string birth;

        public Staff(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DisplayName = row["displayName"].ToString();
            this.Username = row["username"].ToString();
            this.Phone = row["phone"].ToString();
            this.Gender = (int)row["gender"];
            this.Birth = row["birth"].ToString();
        }
        public Staff(int id,string displayName, string username,string phone, int gender,string birth)
        {
            this.ID = id;
            this.DisplayName = displayName;
            this.Username = username;
            this.Phone = phone;
            this.Gender = gender;
            this.Birth = birth;
        }


        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string Birth
        {
            get { return birth; }
            set { birth = value; }
        }
    }
}
