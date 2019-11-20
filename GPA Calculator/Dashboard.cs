using System;
using System.Collections.Generic;
using System.Drawing;
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

            loginButton.Enabled = false;
            loginButton.ForeColor = Color.Gray;
            editUserButton.Enabled = false;
            editUserButton.ForeColor = Color.Gray;
            deleteUserButton.Enabled = false;
            deleteUserButton.ForeColor = Color.Gray;
            UpdateBinding();
        }

        public void UpdateBinding()
        {
            peopleFoundListbox.DataSource = users;
            peopleFoundListbox.DisplayMember = "FullInfo";
        }

        private void searchButton_Click(object sender, EventArgs e)
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
            HttpWebResponse response = hac.login(elements[1], elements[2], out var container); //starts with second element bc first one is name
            //List<Course> courses = hac.getCourses(container, response.ResponseUri);
            hac.GetCourses(container, response.ResponseUri);//logs in and fetches grades
            //foreach (Course course in courses)
            //{ Console.WriteLine($"{course.course} - {course.courseAverage}"); }
            Hide();
            ViewStats viewStats = new ViewStats();
            viewStats.ShowDialog();
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

        private void peopleFoundListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loginButton.Enabled = true;
            loginButton.ForeColor = SystemColors.ControlText;
            editUserButton.Enabled = true;
            editUserButton.ForeColor = SystemColors.ControlText;
            deleteUserButton.Enabled = true;
            deleteUserButton.ForeColor = SystemColors.ControlText;
        }
    }
}