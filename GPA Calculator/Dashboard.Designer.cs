namespace GPA_Calculator
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.peopleFoundListbox = new System.Windows.Forms.ListBox();
            this.addNewUserButton = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.deleteUserButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(24, 13);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(65, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(250, 20);
            this.nameBox.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(321, 10);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButtonn_Click);
            // 
            // peopleFoundListbox
            // 
            this.peopleFoundListbox.FormattingEnabled = true;
            this.peopleFoundListbox.Location = new System.Drawing.Point(27, 47);
            this.peopleFoundListbox.Name = "peopleFoundListbox";
            this.peopleFoundListbox.Size = new System.Drawing.Size(369, 212);
            this.peopleFoundListbox.TabIndex = 3;
            this.peopleFoundListbox.SelectedIndexChanged += new System.EventHandler(this.peopleFoundListbox_SelectedIndexChanged);
            // 
            // addNewUserButton
            // 
            this.addNewUserButton.Location = new System.Drawing.Point(108, 265);
            this.addNewUserButton.Name = "addNewUserButton";
            this.addNewUserButton.Size = new System.Drawing.Size(147, 23);
            this.addNewUserButton.TabIndex = 4;
            this.addNewUserButton.Text = "Add New User/Direct Login";
            this.addNewUserButton.UseVisualStyleBackColor = true;
            this.addNewUserButton.Click += new System.EventHandler(this.addNewUserButton_Click);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(321, 265);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            // 
            // deleteUserButton
            // 
            this.deleteUserButton.Location = new System.Drawing.Point(27, 265);
            this.deleteUserButton.Name = "deleteUserButton";
            this.deleteUserButton.Size = new System.Drawing.Size(75, 23);
            this.deleteUserButton.TabIndex = 6;
            this.deleteUserButton.Text = "Delete User";
            this.deleteUserButton.UseVisualStyleBackColor = true;
            // 
            // Dashboard
            // 
            this.ClientSize = new System.Drawing.Size(439, 319);
            this.Controls.Add(this.deleteUserButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.addNewUserButton);
            this.Controls.Add(this.peopleFoundListbox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.nameLabel);
            this.Name = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox peopleFoundListbox;
        private System.Windows.Forms.Button addNewUserButton;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button deleteUserButton;
    }
}

