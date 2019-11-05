using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace GPA_Calculator
{
    public partial class Dashboard : Form
    {
        List<User> users = new List<User>();
        public Dashboard()
        {
            InitializeComponent();
            
            UpdateBinding();
        }

        public void UpdateBinding()
        {
            peopleFoundListbox.DataSource = users;
            peopleFoundListbox.DisplayMember = "FullInfo";
        }

        private void searchButtonn_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();

            users = db.GetUser(nameBox.Text);

            UpdateBinding();
        }

        private void addNewUserButton_Click(object sender, EventArgs e)
        {
            Hide();
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            string text = peopleFoundListbox.GetItemText(peopleFoundListbox.SelectedItem);

            string pattern = @"\s--\s";
            string[] elements = System.Text.RegularExpressions.Regex.Split(text, pattern);
            db.DeleteUser(elements[0], elements[1], elements[2]);

            UpdateBinding();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string text = peopleFoundListbox.GetItemText(peopleFoundListbox.SelectedItem);
            string pattern = @"\s--\s";
            string[] elements = System.Text.RegularExpressions.Regex.Split(text, pattern); //gets username, password, name from list

            HAC hac = new HAC();
            CookieContainer container;
            HttpWebResponse response = hac.login(elements[1], elements[2], out container); //starts with second element bc first one is name
            List<Assignment> assignments = hac.getAssignments(container, response.ResponseUri);//logs in and fetches grades
            
            foreach (Assignment assignment in assignments)
            { Console.WriteLine(assignment); }
        }

        private void editUserButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            string text = peopleFoundListbox.GetItemText(peopleFoundListbox.SelectedItem);

            string pattern = @"\s--\s";
            string[] elements = System.Text.RegularExpressions.Regex.Split(text, pattern);
            db.EditUser(elements[0], elements[1], elements[2]);
            Variables.UserInfo = new Tuple<string, string, string>(elements[0], elements[1], elements[2]);
            Hide();
            EditUser editUser = new EditUser();
            editUser.ShowDialog();
            UpdateBinding();
        }

    }
}