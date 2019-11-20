namespace GPA_Calculator
{
    partial class ViewStats
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
            this.calculateGPAButton = new System.Windows.Forms.Button();
            this.selectClassToExclude = new System.Windows.Forms.CheckedListBox();
            this.chooseTime = new System.Windows.Forms.TabControl();
            this.infoLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.weightedGpaOutputLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.unweightedGpaOutputLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // calculateGPAButton
            // 
            this.calculateGPAButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculateGPAButton.Location = new System.Drawing.Point(359, 280);
            this.calculateGPAButton.Name = "calculateGPAButton";
            this.calculateGPAButton.Size = new System.Drawing.Size(222, 55);
            this.calculateGPAButton.TabIndex = 0;
            this.calculateGPAButton.Text = "Calculate GPA";
            this.calculateGPAButton.UseVisualStyleBackColor = true;
            this.calculateGPAButton.Click += new System.EventHandler(this.calculateGPAButton_Click);
            // 
            // selectClassToExclude
            // 
            this.selectClassToExclude.FormattingEnabled = true;
            this.selectClassToExclude.Location = new System.Drawing.Point(-4, 0);
            this.selectClassToExclude.Name = "selectClassToExclude";
            this.selectClassToExclude.Size = new System.Drawing.Size(341, 304);
            this.selectClassToExclude.TabIndex = 1;
            // 
            // chooseTime
            // 
            this.chooseTime.Location = new System.Drawing.Point(12, 12);
            this.chooseTime.Name = "chooseTime";
            this.chooseTime.SelectedIndex = 0;
            this.chooseTime.Size = new System.Drawing.Size(341, 294);
            this.chooseTime.TabIndex = 3;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(12, 315);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(323, 16);
            this.infoLabel.TabIndex = 4;
            this.infoLabel.Text = "Select which classes to exclude from GPA calculation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(359, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Weighted GPA";
            // 
            // weightedGpaOutputLabel
            // 
            this.weightedGpaOutputLabel.AutoSize = true;
            this.weightedGpaOutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weightedGpaOutputLabel.Location = new System.Drawing.Point(359, 73);
            this.weightedGpaOutputLabel.Name = "weightedGpaOutputLabel";
            this.weightedGpaOutputLabel.Size = new System.Drawing.Size(51, 25);
            this.weightedGpaOutputLabel.TabIndex = 6;
            this.weightedGpaOutputLabel.Text = "x.xx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(359, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Unweighted GPA";
            // 
            // unweightedGpaOutputLabel
            // 
            this.unweightedGpaOutputLabel.AutoSize = true;
            this.unweightedGpaOutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unweightedGpaOutputLabel.Location = new System.Drawing.Point(359, 179);
            this.unweightedGpaOutputLabel.Name = "unweightedGpaOutputLabel";
            this.unweightedGpaOutputLabel.Size = new System.Drawing.Size(51, 25);
            this.unweightedGpaOutputLabel.TabIndex = 8;
            this.unweightedGpaOutputLabel.Text = "x.xx";
            // 
            // ViewStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 345);
            this.Controls.Add(this.unweightedGpaOutputLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.weightedGpaOutputLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.chooseTime);
            this.Controls.Add(this.calculateGPAButton);
            this.Name = "ViewStats";
            this.Text = "ViewStats";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calculateGPAButton;
        private System.Windows.Forms.CheckedListBox selectClassToExclude;
        private System.Windows.Forms.TabControl chooseTime;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label unweightedGpaOutputLabel;
        private System.Windows.Forms.Label weightedGpaOutputLabel;
    }
}