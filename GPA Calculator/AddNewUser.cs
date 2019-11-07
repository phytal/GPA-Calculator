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
            CookieContainer container;
            HttpWebResponse response = hac.login(newUserUsernameText.Text, newUserPasswordText.Text, out container); //starts with second element bc first one is name
            List<Assignment> assignments = hac.getAssignments(container, response.ResponseUri);//logs in and fetches grades

            foreach (Assignment assignment in assignments)
            { Console.WriteLine(assignment); }
        }
    }
}
