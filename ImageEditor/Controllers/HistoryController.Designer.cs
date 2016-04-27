namespace ImageEditor.Controllers
{
    partial class HistoryController
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
            this.historyListController = new System.Windows.Forms.ListView();
            this.Action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Delete = new System.Windows.Forms.Button();
            this.Clean = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // historyListController
            // 
            this.historyListController.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Action,
            this.Time});
            this.historyListController.FullRowSelect = true;
            this.historyListController.GridLines = true;
            this.historyListController.Location = new System.Drawing.Point(-2, -1);
            this.historyListController.Name = "historyListController";
            this.historyListController.Size = new System.Drawing.Size(316, 300);
            this.historyListController.TabIndex = 0;
            this.historyListController.UseCompatibleStateImageBehavior = false;
            this.historyListController.View = System.Windows.Forms.View.Details;
            // 
            // Action
            // 
            this.Action.Text = "Action";
            this.Action.Width = 167;
            // 
            // Time
            // 
            this.Time.Text = "time";
            this.Time.Width = 144;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(145, 308);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 1;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            // 
            // Clean
            // 
            this.Clean.Location = new System.Drawing.Point(226, 308);
            this.Clean.Name = "Clean";
            this.Clean.Size = new System.Drawing.Size(75, 23);
            this.Clean.TabIndex = 2;
            this.Clean.Text = "Clean";
            this.Clean.UseVisualStyleBackColor = true;
            this.Clean.Click += new System.EventHandler(this.Clean_Click);
            // 
            // HistoryController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 343);
            this.Controls.Add(this.Clean);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.historyListController);
            this.Name = "HistoryController";
            this.Text = "History";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView historyListController;
        private System.Windows.Forms.ColumnHeader Action;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.Button Clean;
        private System.Windows.Forms.Button Delete;
    }
}