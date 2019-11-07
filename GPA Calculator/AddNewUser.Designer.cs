namespace GPA_Calculator
{
    partial class AddNewUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewUser));
            this.createNewUserButton = new System.Windows.Forms.Button();
            this.createUserCancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newUserNameText = new System.Windows.Forms.TextBox();
            this.newUserUsernameText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newUserPasswordText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hacPicture = new System.Windows.Forms.PictureBox();
            this.directLoginButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hacPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // createNewUserButton
            // 
            this.createNewUserButton.Location = new System.Drawing.Point(193, 260);
            this.createNewUserButton.Name = "createNewUserButton";
            this.createNewUserButton.Size = new System.Drawing.Size(114, 23);
            this.createNewUserButton.TabIndex = 0;
            this.createNewUserButton.Text = "Save As New User";
            this.createNewUserButton.UseVisualStyleBackColor = true;
            this.createNewUserButton.Click += new System.EventHandler(this.createNewUserButton_Click);
            // 
            // createUserCancelButton
            // 
            this.createUserCancelButton.Location = new System.Drawing.Point(12, 260);
            this.createUserCancelButton.Name = "createUserCancelButton";
            this.createUserCancelButton.Size = new System.Drawing.Size(75, 23);
            this.createUserCancelButton.TabIndex = 1;
            this.createUserCancelButton.Text = "Cancel";
            this.createUserCancelButton.UseVisualStyleBackColor = true;
            this.createUserCancelButton.Click += new System.EventHandler(this.createUserCancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // newUserNameText
            // 
            this.newUserNameText.Location = new System.Drawing.Point(72, 26);
            this.newUserNameText.Name = "newUserNameText";
            this.newUserNameText.Size = new System.Drawing.Size(188, 20);
            this.newUserNameText.TabIndex = 3;
            // 
            // newUserUsernameText
            // 
            this.newUserUsernameText.Location = new System.Drawing.Point(72, 75);
            this.newUserUsernameText.Name = "newUserUsernameText";
            this.newUserUsernameText.Size = new System.Drawing.Size(188, 20);
            this.newUserUsernameText.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // newUserPasswordText
            // 
            this.newUserPasswordText.Location = new System.Drawing.Point(72, 133);
            this.newUserPasswordText.Name = "newUserPasswordText";
            this.newUserPasswordText.Size = new System.Drawing.Size(188, 20);
            this.newUserPasswordText.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            // 
            // hacPicture
            // 
            this.hacPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hacPicture.Image = ((System.Drawing.Image)(resources.GetObject("hacPicture.Image")));
            this.hacPicture.InitialImage = ((System.Drawing.Image)(resources.GetObject("hacPicture.InitialImage")));
            this.hacPicture.Location = new System.Drawing.Point(35, 173);
            this.hacPicture.Name = "hacPicture";
            this.hacPicture.Size = new System.Drawing.Size(241, 66);
            this.hacPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.hacPicture.TabIndex = 8;
            this.hacPicture.TabStop = false;
            // 
            // directLoginButton
            // 
            this.directLoginButton.Location = new System.Drawing.Point(103, 260);
            this.directLoginButton.Name = "directLoginButton";
            this.directLoginButton.Size = new System.Drawing.Size(84, 23);
            this.directLoginButton.TabIndex = 9;
            this.directLoginButton.Text = "Direct Login";
            this.directLoginButton.UseVisualStyleBackColor = true;
            this.directLoginButton.Click += new System.EventHandler(this.directLoginButton_Click);
            // 
            // AddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 295);
            this.Controls.Add(this.directLoginButton);
            this.Controls.Add(this.hacPicture);
            this.Controls.Add(this.newUserPasswordText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newUserUsernameText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newUserNameText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createUserCancelButton);
            this.Controls.Add(this.createNewUserButton);
            this.Name = "AddNewUser";
            this.Text = "AddNewUser";
            ((System.ComponentModel.ISupportInitialize)(this.hacPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createNewUserButton;
        private System.Windows.Forms.Button createUserCancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newUserNameText;
        private System.Windows.Forms.TextBox newUserUsernameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newUserPasswordText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox hacPicture;
        private System.Windows.Forms.Button directLoginButton;
    }
}