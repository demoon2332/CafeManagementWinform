using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DTO
{
    public class Item
    {
        private int id,idCate,quantity;
        private string name;
        private float price;
        
        public Item(int id,int idCate,int quantity,string name,float price)
        {
            this.Id = id;
            this.IdCate = idCate;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }

        public Item(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdCate = (int)row["idCategory"];
            this.Name = row["name"].ToString();
            this.Price = (float) Convert.ToDouble(row["price"]);
            this.Quantity = (int)row["quantity"];
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int IdCate
        {
            get { return idCate; }
            set { idCate = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public float Price
        {
            get { return price; }
            set { price = value; }
        }



    }
}
