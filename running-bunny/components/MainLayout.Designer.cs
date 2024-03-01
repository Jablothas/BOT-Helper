namespace running_bunny
{
    partial class MainLayout
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
            headerPnl = new Panel();
            titleBackgroundPanel = new Panel();
            BtnClose = new PictureBox();
            titleLabel = new Label();
            logoPictureBox = new PictureBox();
            contentPanel = new Panel();
            PanelUpload = new Panel();
            InitProcess = new Button();
            SelectCompaniesReset = new Button();
            SelectCompanies = new Button();
            SelectRoomsReset = new Button();
            SelectRooms = new Button();
            SelectStudentReset = new Button();
            SelectStudent = new Button();
            headerPnl.SuspendLayout();
            titleBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BtnClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            contentPanel.SuspendLayout();
            PanelUpload.SuspendLayout();
            SuspendLayout();
            // 
            // headerPnl
            // 
            headerPnl.BackColor = Color.DodgerBlue;
            headerPnl.Controls.Add(titleBackgroundPanel);
            headerPnl.Controls.Add(logoPictureBox);
            headerPnl.Dock = DockStyle.Top;
            headerPnl.Location = new Point(0, 0);
            headerPnl.Name = "headerPnl";
            headerPnl.Size = new Size(497, 52);
            headerPnl.TabIndex = 0;
            // 
            // titleBackgroundPanel
            // 
            titleBackgroundPanel.Controls.Add(BtnClose);
            titleBackgroundPanel.Controls.Add(titleLabel);
            titleBackgroundPanel.Dock = DockStyle.Fill;
            titleBackgroundPanel.Location = new Point(53, 0);
            titleBackgroundPanel.Name = "titleBackgroundPanel";
            titleBackgroundPanel.Size = new Size(444, 52);
            titleBackgroundPanel.TabIndex = 1;
            titleBackgroundPanel.MouseMove += titleBackgroundPanel_MouseMove;
            // 
            // BtnClose
            // 
            BtnClose.Image = Properties.Resources.closeapp2;
            BtnClose.Location = new Point(390, 0);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(48, 50);
            BtnClose.TabIndex = 4;
            BtnClose.TabStop = false;
            BtnClose.Click += BtnClose_Click;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(3, 8);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(192, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Running Bunny";
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = DockStyle.Left;
            logoPictureBox.Image = Properties.Resources.running_bunny;
            logoPictureBox.Location = new Point(0, 0);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(53, 52);
            logoPictureBox.TabIndex = 0;
            logoPictureBox.TabStop = false;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(PanelUpload);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 52);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(497, 630);
            contentPanel.TabIndex = 1;
            // 
            // PanelUpload
            // 
            PanelUpload.Controls.Add(InitProcess);
            PanelUpload.Controls.Add(SelectCompaniesReset);
            PanelUpload.Controls.Add(SelectCompanies);
            PanelUpload.Controls.Add(SelectRoomsReset);
            PanelUpload.Controls.Add(SelectRooms);
            PanelUpload.Controls.Add(SelectStudentReset);
            PanelUpload.Controls.Add(SelectStudent);
            PanelUpload.Location = new Point(12, 6);
            PanelUpload.Name = "PanelUpload";
            PanelUpload.Size = new Size(479, 335);
            PanelUpload.TabIndex = 5;
            // 
            // InitProcess
            // 
            InitProcess.BackColor = Color.Silver;
            InitProcess.Enabled = false;
            InitProcess.FlatStyle = FlatStyle.Flat;
            InitProcess.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            InitProcess.Location = new Point(264, 268);
            InitProcess.Name = "InitProcess";
            InitProcess.Size = new Size(204, 50);
            InitProcess.TabIndex = 10;
            InitProcess.Text = "Verarbeitung starten";
            InitProcess.TextAlign = ContentAlignment.MiddleLeft;
            InitProcess.UseVisualStyleBackColor = false;
            InitProcess.Click += InitProcess_Click;
            // 
            // SelectCompaniesReset
            // 
            SelectCompaniesReset.BackColor = Color.Silver;
            SelectCompaniesReset.FlatStyle = FlatStyle.Flat;
            SelectCompaniesReset.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectCompaniesReset.Location = new Point(418, 124);
            SelectCompaniesReset.Name = "SelectCompaniesReset";
            SelectCompaniesReset.Size = new Size(50, 50);
            SelectCompaniesReset.TabIndex = 9;
            SelectCompaniesReset.Text = "X";
            SelectCompaniesReset.UseVisualStyleBackColor = false;
            SelectCompaniesReset.Visible = false;
            SelectCompaniesReset.Click += SelectCompaniesReset_Click;
            // 
            // SelectCompanies
            // 
            SelectCompanies.BackColor = Color.Silver;
            SelectCompanies.FlatStyle = FlatStyle.Flat;
            SelectCompanies.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectCompanies.Location = new Point(12, 124);
            SelectCompanies.Name = "SelectCompanies";
            SelectCompanies.Size = new Size(400, 50);
            SelectCompanies.TabIndex = 8;
            SelectCompanies.Text = "Unternehmensliste hochladen";
            SelectCompanies.TextAlign = ContentAlignment.MiddleLeft;
            SelectCompanies.UseVisualStyleBackColor = false;
            SelectCompanies.Click += SelectCompanies_Click;
            // 
            // SelectRoomsReset
            // 
            SelectRoomsReset.BackColor = Color.Silver;
            SelectRoomsReset.FlatStyle = FlatStyle.Flat;
            SelectRoomsReset.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectRoomsReset.Location = new Point(418, 68);
            SelectRoomsReset.Name = "SelectRoomsReset";
            SelectRoomsReset.Size = new Size(50, 50);
            SelectRoomsReset.TabIndex = 7;
            SelectRoomsReset.Text = "X";
            SelectRoomsReset.UseVisualStyleBackColor = false;
            SelectRoomsReset.Visible = false;
            SelectRoomsReset.Click += SelectRoomsReset_Click;
            // 
            // SelectRooms
            // 
            SelectRooms.BackColor = Color.Silver;
            SelectRooms.FlatStyle = FlatStyle.Flat;
            SelectRooms.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectRooms.Location = new Point(12, 68);
            SelectRooms.Name = "SelectRooms";
            SelectRooms.Size = new Size(400, 50);
            SelectRooms.TabIndex = 6;
            SelectRooms.Text = "Raumliste hochladen";
            SelectRooms.TextAlign = ContentAlignment.MiddleLeft;
            SelectRooms.UseVisualStyleBackColor = false;
            SelectRooms.Click += SelectRooms_Click;
            // 
            // SelectStudentReset
            // 
            SelectStudentReset.BackColor = Color.Silver;
            SelectStudentReset.FlatStyle = FlatStyle.Flat;
            SelectStudentReset.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectStudentReset.Location = new Point(418, 12);
            SelectStudentReset.Name = "SelectStudentReset";
            SelectStudentReset.Size = new Size(50, 50);
            SelectStudentReset.TabIndex = 5;
            SelectStudentReset.Text = "X";
            SelectStudentReset.UseVisualStyleBackColor = false;
            SelectStudentReset.Visible = false;
            SelectStudentReset.Click += SelectStudentReset_Click;
            // 
            // SelectStudent
            // 
            SelectStudent.BackColor = Color.Silver;
            SelectStudent.FlatStyle = FlatStyle.Flat;
            SelectStudent.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectStudent.Location = new Point(12, 12);
            SelectStudent.Name = "SelectStudent";
            SelectStudent.Size = new Size(400, 50);
            SelectStudent.TabIndex = 4;
            SelectStudent.Text = "Schülerliste hochladen";
            SelectStudent.TextAlign = ContentAlignment.MiddleLeft;
            SelectStudent.UseVisualStyleBackColor = false;
            SelectStudent.Click += BtnSelectStudent_Click;
            // 
            // MainLayout
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 682);
            Controls.Add(contentPanel);
            Controls.Add(headerPnl);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainLayout";
            Text = "Running Bunny App <dev>";
            headerPnl.ResumeLayout(false);
            titleBackgroundPanel.ResumeLayout(false);
            titleBackgroundPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BtnClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            contentPanel.ResumeLayout(false);
            PanelUpload.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel headerPnl;
        private Panel titleBackgroundPanel;
        private Label titleLabel;
        private PictureBox logoPictureBox;
        private Panel contentPanel;
        private PictureBox BtnClose;
        private Button SelectStudent;
        private Panel PanelUpload;
        private Button SelectStudentReset;
        private Button SelectRoomsReset;
        private Button SelectRooms;
        private Button SelectCompaniesReset;
        private Button SelectCompanies;
        private Button InitProcess;
    }
}