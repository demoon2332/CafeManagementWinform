using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DTO
{
    public class Menu
    {
        private String name;
        private int quantity;
        private float price,totalPrice;

        public Menu(string name,int quantity,float price,float total)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
            this.TotalPrice = TotalPrice;
        }
        public Menu(DataRow row)
        {
            this.Name = row["name"].ToString();
            this.Quantity = (int)row["quantity"];
            this.Price = (float)Convert.ToDouble(row["price"]);
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"]);
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

    }
}
