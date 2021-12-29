using CafeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DAO
{
    public class ItemDAO
    {
        private static ItemDAO instance;

        public static ItemDAO Instance
        {
            get { if (instance == null) instance = new ItemDAO(); return ItemDAO.instance; }
            private set { ItemDAO.instance = value; }
        }

        private ItemDAO() { }

        public List<Item> GetItemsByCategoryId(int id)
        {
            List<Item> list = new List<Item>();

            string query = "Select * from Item where idCategory="+id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Item i= new Item(item);
                list.Add(i);
            }

            return list;
        } 

        public Item GetItemById(int id)
        {
            
            string query = "Select * from Item where id=" + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            Item item = new Item(data.Rows[0]);

            return item;
        }

        public int decreaseQuantity(int quantity,int id)
        {
            string query = "Exec decreaseQuantity @quantity , @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query,new object[] { quantity,id });
            return result;
        }

    }
}
