using System;
using System.Collections.Generic;
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

        private void peopleFoundListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        }

        private void editUserButton_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            string text = peopleFoundListbox.GetItemText(peopleFoundListbox.SelectedItem);
            //TODO: get values from the string

            string pattern = @"\s--\s";
            string[] elements = System.Text.RegularExpressions.Regex.Split(text, pattern);
            db.DeleteUser(elements[0], elements[1], elements[2]);

            UpdateBinding();
        }
    }
}