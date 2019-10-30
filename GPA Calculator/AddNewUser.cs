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
    public partial class AddNewUser : Form
    {
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void createNewUserButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            db.InsertUser(newUserUsernameText.Text, newUserPasswordText.Text, newUserNameText.Text);

            newUserUsernameText.Text = "";
            newUserPasswordText.Text = "";
            newUserNameText.Text = "";
        }

        private void createUserCancelButton_Click(object sender, EventArgs e)
        {
            Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
        }
    }
}
