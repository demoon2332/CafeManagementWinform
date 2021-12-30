using CafeManagement.DAO;
using CafeManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = CafeManagement.DTO.Menu;

namespace CafeManagement
{
    public partial class fTableManager : Form
    {
        public int idStaff = 1;
        public string username = "";
        public fTableManager(string username)
        {
            this.username = username;
            InitializeComponent();
            //set Information for Staff
            Staff currentStaff = StaffDAO.Instance.GetStaffByUsername(username);
            if (currentStaff == null)
            {
                MessageBox.Show("Cant find users");
                return;
            }
            idStaff = currentStaff.ID;
            usernameTxt.Text += " "+currentStaff.Username.ToString();

            //LoadSeat();
            choosePaymentBox.SelectedIndex = 0;

            //hiding Admin tab :
            if (idStaff != 1)
            {
                adminToolStripMenuItem.Visible = false;
            }
        }
        void LoadBill()
        {
            
        }

        void LoadCategory()
        {
            List<Category> list = CategoryDAO.Instance.GetListCategory();


            selectCateBox.DataSource = list.ToArray<Category>();
            selectCateBox.DisplayMember = "Name";
        }


        void LoadItemListByCateGoryId(int id)
        {
            List<Item> listItem = ItemDAO.Instance.GetItemsByCategoryId(id);

            selectItemBox.DataSource = listItem;
            selectItemBox.DisplayMember = "Name";
        }

        void ShowBill(int id)
        {
            listViewBill.Items.Clear();
            totalPayTxt.Clear();
            if (id == 1)
            {
                btnCheckOut.Tag = "Take away";
            }
            else
            {
                btnCheckOut.Tag = "";
                List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

                float totalPay = 0;
                foreach(Menu item in listBillInfo)
                {
                    ListViewItem viewItem = new ListViewItem(item.Name.ToString());
                    viewItem.SubItems.Add(item.Quantity.ToString());
                    viewItem.SubItems.Add(item.Price.ToString());
                    viewItem.SubItems.Add(item.TotalPrice.ToString());             
                    listViewBill.Items.Add(viewItem);
                    totalPay += item.TotalPrice;
                }
                totalPayTxt.Text = totalPay.ToString("C"); 
            }
        }

        void loadItem(int id)
        {
            if (listViewLastBill.Items.Count > listViewBill.Items.Count)
            {
                int lastIndex = listViewLastBill.Items.Count - 1;
                listViewLastBill.Items.RemoveAt(lastIndex);
            }
            //listViewLastBill.Items.Clear();
            Item item = ItemDAO.Instance.GetItemById(id);
            ListViewItem viewItem = new ListViewItem(item.Name.ToString());
            viewItem.SubItems.Add(item.Quantity.ToString());
            viewItem.SubItems.Add(item.Price.ToString());
            listViewLastBill.Items.Add(viewItem);
        }
        
        void addItem(int id)
        {
            Item item = ItemDAO.Instance.GetItemById(id);
            ListViewItem viewItem = new ListViewItem(item.Name.ToString());
            viewItem.SubItems.Add(quantityBox.Value.ToString());
            viewItem.SubItems.Add(item.Price.ToString());
            viewItem.Tag = item.Id.ToString();
            // tag --> use to add to BillInfo 

            // get item's quantity in billTable if the item is already added to table 
            string itemName = selectItemBox.Text;
            int orderQuantity = Int16.Parse(quantityBox.Value.ToString());
            Item tempItem = ItemDAO.Instance.GetItemById(id);
            int quantityInStorage = tempItem.Quantity;
            if(quantityInStorage == 0)
            {
                MessageBox.Show(itemName + " is sold out !");
                return;
            }
            else if(orderQuantity > quantityInStorage)
            {
                MessageBox.Show("You just order : " + orderQuantity + "\nThere are only " + quantityInStorage.ToString() + " in storage");
                return;
            }
            foreach (ListViewItem i in listViewBill.Items)
            {
                if (i.SubItems[0].Text.ToString() == itemName)
                {
                    
                    //can not update quantity
                    int currentQuantity = Int16.Parse(i.SubItems[1].Text.ToString()) + orderQuantity;
                    if(currentQuantity > quantityInStorage)
                    {
                        MessageBox.Show("You just order : "+currentQuantity+"\nThere are only " + quantityInStorage.ToString() + " in storage");
                        return;
                    }

                    // can update quantity
                    i.SubItems[1].Text = (Int16.Parse(i.SubItems[1].Text.ToString()) + orderQuantity).ToString();
                    // after update quantity --> update totalPay
                    float itemPrice = (float)Convert.ToDouble(i.SubItems[2].Text.ToString());
                    //  extraPay is the money from new duplicate item to added ( price * quantity)
                    float extraPay = itemPrice * orderQuantity;
                    totalPayTxt.Text = ((float)Convert.ToDouble(totalPayTxt.Text.ToString()) + extraPay).ToString();
                    return;
                }
            }

            listViewBill.Items.Add(viewItem);
            

            float total = 0;
            if(totalPayTxt.Text.Length > 0)
            {
                total = (float)Convert.ToDouble(totalPayTxt.Text.ToString());
            }
            total += (float)Convert.ToDouble(item.Price * Int16.Parse(quantityBox.Value.ToString()));
            totalPayTxt.Text = total.ToString();

            // decrease quantity of item, quantity = quantity - 1;
        }

        void updateQuantityInStorage()
        {
            foreach (ListViewItem i in listViewBill.Items)
            {
                int itemId = Int16.Parse(i.Tag.ToString());
                int itemQuantity = Int16.Parse(i.SubItems[1].Text);
                ItemDAO.Instance.decreaseQuantity(itemQuantity, itemId);
            }
        }


        bool writeBill()
        {
            int type = choosePaymentBox.SelectedIndex;
            int idSeat = 1;
            BillDAO.Instance.InsertBill(idSeat, idStaff, type);

            return false;
        }
        bool writeBillInfo(int idBill)
        {
            foreach (ListViewItem item in listViewBill.Items)
            {
                if(item.Tag == null)
                {
                    MessageBox.Show("TAG IS NULL");
                    return false;
                }
                int idItem = Int16.Parse(item.Tag.ToString());
                int quantity = Int16.Parse(item.SubItems[1].Text.ToString());
                if (quantity < 1 )
                    return false;
                try
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, idItem, quantity);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error: Something went wrong , we can't make this bill");
                    return false;
                }   
            }
            return true;
        }

        void checkOut()
        {
            writeBill();
            Bill b = BillDAO.Instance.GetLastestBill();
            writeBillInfo(b.ID);
            // update quantity to storage
            updateQuantityInStorage();

            // clear input ui
            clearBill();
        }

        void clearBill()
        {
            listViewBill.Items.Clear();
            listViewLastBill.Items.Clear();
            quantityBox.Value = 1;
            choosePaymentBox.SelectedIndex = 0;
            totalPayTxt.Text = "";
        }




        //Event handle:
        void btnSeat_Click(object sender,EventArgs e)
        {
            string seatId = ((sender as Button).Tag.ToString());
            listViewBill.Tag = seatId;

            ShowBill(Int16.Parse(seatId));
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(username);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            if (idStaff == 1)
            {
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                return;
            }
        }

        private void selectCateBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = 0;
            selectItemBox.Text = "";
            
            ComboBox box = sender as ComboBox;
            if (box.SelectedItem == null)
            {
                return;
            }
            Category selected = box.SelectedItem as Category;
            if(selected == null)
            {
                return;
            }
            id = selected.Id;
            LoadItemListByCateGoryId(id);
        }

        private void selectCateBox_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            int quantity = (int)quantityBox.Value;
            if (btnAddItem.Tag==null)
                return;
            //Item item = ItemDAO.Instance.GetItemById(Int16.Parse(btnAddItem.Tag.ToString()));
            addItem(Int16.Parse(btnAddItem.Tag.ToString()));
            // reset quantity
            quantityBox.Value = 1;

        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void selectItemBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = 0;
            ComboBox box = sender as ComboBox;
            if (box.SelectedItem == null)
            {
                return;
            }
            Item selected = box.SelectedItem as Item;
            if (selected == null)
            {
                return;
            }
            id = selected.Id;
            btnAddItem.Tag = id.ToString();
            loadItem(id);
        }

        private void listViewBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            //edit item ( edit row ) 
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = listViewBill.SelectedItems.Count > 0 ? listViewBill.SelectedItems[0] : null;
            if (item != null)
            {
                
                DialogResult result = MessageBox.Show("Delete " + item.SubItems[0].Text + "?", "Confirm Remove Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    float newTotalPay = (float)Convert.ToDouble(totalPayTxt.Text.ToString());
                    int quantity = Int16.Parse(listViewBill.SelectedItems[0].SubItems[1].Text.ToString());
                    float price = (float)Convert.ToDouble(listViewBill.SelectedItems[0].SubItems[2].Text.ToString());
                    newTotalPay = newTotalPay - price * quantity;

                    totalPayTxt.Text = newTotalPay.ToString();
                    

                    listViewBill.Items.Remove(listViewBill.SelectedItems[0]);
                }
                else
                {
                    return;
                }
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            String bill = "Your bill Detail : \n";
            foreach (ListViewItem i in listViewBill.Items)
            {
                int itemId = Int16.Parse(i.Tag.ToString());
                int itemQuantity = Int16.Parse(i.SubItems[1].Text);
                //ItemDAO.Instance.decreaseQuantity(itemQuantity, itemId);
                bill += " + "+i.SubItems[1].Text + ": " + "\t"+i.SubItems[0].Text+"  "+i.SubItems[2].Text+" VNĐ\n";
            }
            // add totalPay at the end :
            bill += "\n --> Total to pay : "+totalPayTxt.Text+" VNĐ";
            // add payment at the end : 
            bill += "\n\nPayment : " + choosePaymentBox.Text;
            bill += "\nStaff : " + username+"\n";
            if (MessageBox.Show(bill+"\nCheck it carefully before continuing ! ", "Confirm Check Out", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                checkOut();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the Bill ?", "Confirm Clear Bill", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                clearBill();
            }
            else
            {
                return;
            }
        }
    }
}
