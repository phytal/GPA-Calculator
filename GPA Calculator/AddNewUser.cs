using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        private void directLoginButton_Click(object sender, EventArgs e)
        {
            HAC hac = new HAC();
            HttpWebResponse response = hac.login(newUserUsernameText.Text, newUserPasswordText.Text, out var container); //starts with second element bc first one is name
            hac.GetCourses(container, response.ResponseUri);//logs in and fetches grades

            //foreach (Course assignment in assignments)
            //{ Console.WriteLine(assignment); }
        }
    }
}
