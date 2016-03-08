namespace ImageEditor
{
    partial class WindDialogBox
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.strengtTrackBar = new System.Windows.Forms.TrackBar();
            this.strengtUpDown = new System.Windows.Forms.DomainUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.directionComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.strengtTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(353, 94);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(272, 94);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Strengt";
            // 
            // strengtTrackBar
            // 
            this.strengtTrackBar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.strengtTrackBar.LargeChange = 10;
            this.strengtTrackBar.Location = new System.Drawing.Point(28, 46);
            this.strengtTrackBar.Maximum = 100;
            this.strengtTrackBar.Name = "strengtTrackBar";
            this.strengtTrackBar.Size = new System.Drawing.Size(363, 45);
            this.strengtTrackBar.TabIndex = 8;
            this.strengtTrackBar.TickFrequency = 100;
            this.strengtTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // strengtUpDown
            // 
            this.strengtUpDown.Location = new System.Drawing.Point(121, 12);
            this.strengtUpDown.Name = "strengtUpDown";
            this.strengtUpDown.Size = new System.Drawing.Size(120, 20);
            this.strengtUpDown.TabIndex = 7;
            this.strengtUpDown.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Direction";
            // 
            // directionComboBox
            // 
            this.directionComboBox.FormattingEnabled = true;
            this.directionComboBox.Items.AddRange(new object[] {
            "Right",
            "Left",
            "Up",
            "Down"});
            this.directionComboBox.Location = new System.Drawing.Point(94, 96);
            this.directionComboBox.Name = "directionComboBox";
            this.directionComboBox.Size = new System.Drawing.Size(121, 21);
            this.directionComboBox.TabIndex = 15;
            // 
            // WindDialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 130);
            this.Controls.Add(this.directionComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.strengtTrackBar);
            this.Controls.Add(this.strengtUpDown);
            this.Name = "WindDialogBox";
            this.Text = "Effects: Wind";
            ((System.ComponentModel.ISupportInitialize)(this.strengtTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar strengtTrackBar;
        public System.Windows.Forms.DomainUpDown strengtUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox directionComboBox;
    }
}