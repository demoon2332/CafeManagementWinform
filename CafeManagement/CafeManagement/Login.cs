using CafeManagement.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagement
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
           
        }



        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = usernameTxt.Text;
            string pass = passwordTxt.Text;
            string hashPass = Hash.Hash_SHA1(pass);



            if (Login(username, hashPass))
            {
                fTableManager form = new fTableManager(username);
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Wrong username or password, please try again.");
            }
        }

        bool Login(string username,string pass)
        {
            return AccountDAO.Instance.Login(username, pass);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Do you want to exit ?","Confirm exit",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
