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

namespace CafeManagement
{
    public partial class fAccountProfile : Form
    {
        private string username ="";
        public fAccountProfile(string username)
        {
            this.username = username;
            InitializeComponent();
            loadInformation(username);
        }

        void loadInformation(string username)
        {
            Staff s = StaffDAO.Instance.GetStaffByUsername(username);
            if (s == null)
            {
                return;
            }
            userNameTxt.Text = s.Username;
            displayNameTxt.Text = s.DisplayName;
            phoneTxt.Text = s.Phone;
            dateBirthTxt.Text = s.Birth;

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if( this.newPassTxt.Text.ToString() != confirmPassTxt.Text.ToString())
            {
                MessageBox.Show("Password and confirm passowrd are unmatch !");
                return;
            }
            else if(this.newPassTxt.Text.Length > 0)
            {
                string newPassTxt = Hash.Hash_SHA1(this.newPassTxt.Text);
                if (StaffDAO.Instance.updatePassword(username,newPassTxt))
                {
                    
                }
                else
                {
                    MessageBox.Show("Something went wrong, can't update your new password.\nPlease try again.");
                }
            }
            string disName = displayNameTxt.Text;
            string phone = phoneTxt.Text;
            string birth = dateBirthTxt.Text;
            

            if (StaffDAO.Instance.updateStaff(username,disName,phone,birth))
            {
                MessageBox.Show("Update successfully.");
            }
            else
            {
                MessageBox.Show("Something went wrong, can't update this information.\nPlease try again.");
            }

        }
    }
}
