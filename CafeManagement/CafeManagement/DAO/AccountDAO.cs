using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }

        }

        private AccountDAO() {}

        public bool Login(string username,string pass)
        {
            String query = "EXEC dbo.UserLogin @username , @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {username,pass });

            return result.Rows.Count > 0;
        }
    }
}
