namespace running_bunny
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPnl = new System.Windows.Forms.Panel();
            this.titleBackgroundPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.proccessPanel = new System.Windows.Forms.Panel();
            this.processLabel2 = new System.Windows.Forms.Label();
            this.processBtn = new System.Windows.Forms.Button();
            this.processLabel1 = new System.Windows.Forms.Label();
            this.uploadPanel = new System.Windows.Forms.Panel();
            this.openFileDialogBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.headerPnl.SuspendLayout();
            this.titleBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.contentPanel.SuspendLayout();
            this.proccessPanel.SuspendLayout();
            this.uploadPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPnl
            // 
            this.headerPnl.BackColor = System.Drawing.Color.DodgerBlue;
            this.headerPnl.Controls.Add(this.titleBackgroundPanel);
            this.headerPnl.Controls.Add(this.logoPictureBox);
            this.headerPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPnl.Location = new System.Drawing.Point(0, 0);
            this.headerPnl.Name = "headerPnl";
            this.headerPnl.Size = new System.Drawing.Size(584, 46);
            this.headerPnl.TabIndex = 0;
            // 
            // titleBackgroundPanel
            // 
            this.titleBackgroundPanel.Controls.Add(this.titleLabel);
            this.titleBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBackgroundPanel.Location = new System.Drawing.Point(75, 0);
            this.titleBackgroundPanel.Name = "titleBackgroundPanel";
            this.titleBackgroundPanel.Size = new System.Drawing.Size(509, 46);
            this.titleBackgroundPanel.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(3, 7);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(259, 32);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Project Running Bunny";
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.logoPictureBox.Image = global::running_bunny.Properties.Resources.running_bunny;
            this.logoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(75, 46);
            this.logoPictureBox.TabIndex = 0;
            this.logoPictureBox.TabStop = false;
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.proccessPanel);
            this.contentPanel.Controls.Add(this.uploadPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 46);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(584, 515);
            this.contentPanel.TabIndex = 1;
            // 
            // proccessPanel
            // 
            this.proccessPanel.Controls.Add(this.processLabel2);
            this.proccessPanel.Controls.Add(this.processBtn);
            this.proccessPanel.Controls.Add(this.processLabel1);
            this.proccessPanel.Location = new System.Drawing.Point(12, 161);
            this.proccessPanel.Name = "proccessPanel";
            this.proccessPanel.Size = new System.Drawing.Size(522, 139);
            this.proccessPanel.TabIndex = 3;
            this.proccessPanel.Visible = false;
            // 
            // processLabel2
            // 
            this.processLabel2.AutoSize = true;
            this.processLabel2.Location = new System.Drawing.Point(12, 31);
            this.processLabel2.Name = "processLabel2";
            this.processLabel2.Size = new System.Drawing.Size(206, 15);
            this.processLabel2.TabIndex = 2;
            this.processLabel2.Text = "Möchten Sie die Verarbeitung starten?";
            // 
            // processBtn
            // 
            this.processBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.processBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processBtn.ForeColor = System.Drawing.Color.White;
            this.processBtn.Location = new System.Drawing.Point(12, 55);
            this.processBtn.Name = "processBtn";
            this.processBtn.Size = new System.Drawing.Size(94, 23);
            this.processBtn.TabIndex = 1;
            this.processBtn.Text = "Verarbeiten";
            this.processBtn.UseVisualStyleBackColor = false;
            this.processBtn.Click += new System.EventHandler(this.processBtn_Click);
            // 
            // processLabel1
            // 
            this.processLabel1.AutoSize = true;
            this.processLabel1.Location = new System.Drawing.Point(12, 12);
            this.processLabel1.Name = "processLabel1";
            this.processLabel1.Size = new System.Drawing.Size(16, 15);
            this.processLabel1.TabIndex = 0;
            this.processLabel1.Text = "...";
            // 
            // uploadPanel
            // 
            this.uploadPanel.Controls.Add(this.openFileDialogBtn);
            this.uploadPanel.Controls.Add(this.label1);
            this.uploadPanel.Location = new System.Drawing.Point(12, 16);
            this.uploadPanel.Name = "uploadPanel";
            this.uploadPanel.Size = new System.Drawing.Size(522, 139);
            this.uploadPanel.TabIndex = 2;
            // 
            // openFileDialogBtn
            // 
            this.openFileDialogBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.openFileDialogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openFileDialogBtn.ForeColor = System.Drawing.Color.White;
            this.openFileDialogBtn.Location = new System.Drawing.Point(12, 30);
            this.openFileDialogBtn.Name = "openFileDialogBtn";
            this.openFileDialogBtn.Size = new System.Drawing.Size(94, 23);
            this.openFileDialogBtn.TabIndex = 1;
            this.openFileDialogBtn.Text = "Durchsuchen";
            this.openFileDialogBtn.UseVisualStyleBackColor = false;
            this.openFileDialogBtn.Click += new System.EventHandler(this.openFileDialogBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bitte die .csv-Datei hochladen";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPnl);
            this.Name = "Form1";
            this.Text = "Running Bunny App <dev>";
            this.headerPnl.ResumeLayout(false);
            this.titleBackgroundPanel.ResumeLayout(false);
            this.titleBackgroundPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.contentPanel.ResumeLayout(false);
            this.proccessPanel.ResumeLayout(false);
            this.proccessPanel.PerformLayout();
            this.uploadPanel.ResumeLayout(false);
            this.uploadPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel headerPnl;
        private Panel titleBackgroundPanel;
        private Label titleLabel;
        private PictureBox logoPictureBox;
        private Panel contentPanel;
        private Panel uploadPanel;
        private Button openFileDialogBtn;
        private Label label1;
        private Panel proccessPanel;
        private Label processLabel2;
        private Button processBtn;
        private Label processLabel1;
    }
}