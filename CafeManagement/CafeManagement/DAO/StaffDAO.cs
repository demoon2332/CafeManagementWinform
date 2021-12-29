using CafeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DAO
{
    class StaffDAO
    {
        private static StaffDAO instance;

        public static StaffDAO Instance
        {
            get { if (instance == null) instance = new StaffDAO(); return StaffDAO.instance; }
            private set { StaffDAO.instance = value; }
        }

        private StaffDAO() { }

        public Staff GetStaffByUsername(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("exec getStaffByUsername @username",new object[] {username });
            if(data.Rows.Count > 0)
            {
                Staff staff = new Staff(data.Rows[0]);

                return staff;
            }
            return null;
        }

        public bool updateStaff(string username,string disName,string phone,string birth)
        {
            try
            {
                DataProvider.Instance.ExecuteNonQuery("Exec updateStaff @username , @disName , @phone , @birth ",new object[] {username, disName, phone, birth});
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool updatePassword(string username,string newPass)
        {
            try
            {
                DataProvider.Instance.ExecuteNonQuery("Exec updatePassword @username , @password ", new object[] { username, newPass});
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}
