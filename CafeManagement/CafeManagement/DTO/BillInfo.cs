using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DTO
{
    public class BillInfo
    {
        private int id, itemId, billId,quantity;

        public BillInfo(int id,int billId,int itemId,int count)
        {
            this.ID = id;
            this.BillId = billId;
            this.ItemId = itemId;
            this.Quantity = quantity;
        }
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillId = (int)row["idBill"];
            this.ItemId = (int)row["idItem"];
            this.Quantity = (int)row["quantity"];

        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int BillId
        {
            get { return billId; }
            set { billId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
