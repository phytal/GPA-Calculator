using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPA_Calculator
{
    public partial class EditUser : Form
    {
        public EditUser()
        {
            InitializeComponent();

            editUserNameText.Text = Variables.UserInfo.Item1;
            editUserUsernameText.Text = Variables.UserInfo.Item2;
            editUserPasswordText.Text = Variables.UserInfo.Item3;
        }

        private void createNewUserButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.InsertUser(editUserUsernameText.Text, editUserPasswordText.Text, editUserNameText.Text);

            editUserUsernameText.Text = "";
            editUserPasswordText.Text = "";
            editUserNameText.Text = "";

        }

        private void createUserCancelButton_Click(object sender, EventArgs e)
        {
            Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
        }
    }
}
