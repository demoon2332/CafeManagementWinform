using CafeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance;}
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        public int GetBillBySeatID(int id)
        {
            String query = "";
            if(id == -1)
                query = "SELECT * FROM dbo.Bill where idSeat=NULL";
            else
                query = "SELECT * FROM dbo.Bill where idSeat="+id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public void InsertBill(int idSeat,int idStaff,int type = 1)
        {
            DataProvider.Instance.ExecuteNonQuery("Exec InsertBill @idSeat , @idStaff , @type ", new object[] { idSeat, idStaff, type });
        }

        public Bill GetLastestBill()
        {
            DataTable data  = DataProvider.Instance.ExecuteQuery("exec GetLastestBill");
            Bill b = new Bill(data.Rows[0]);
            

            return b;
        }

    }
}
