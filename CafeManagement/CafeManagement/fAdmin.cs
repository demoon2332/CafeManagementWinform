using CafeManagement.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagement
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            LoadBillList();
            LoadItemList();
            LoadCateList();
            LoadAccountList();
        }
        void LoadAccountList()
        {
            string query = "Select * from Staff";
            dataGridViewAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
           
        }

        void LoadItemList()
        {
            string query = "Select * from Item";
            dataGridViewItem.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadCateList()
        {
            string query = "Select * from Category";
            dataGridViewCate.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadBillList()
        {
            string query = "Select * from Bill";
            dataGridViewBill.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
