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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainLayout));
            headerPnl = new Panel();
            titleBackgroundPanel = new Panel();
            BtnClose = new PictureBox();
            titleLabel = new Label();
            logoPictureBox = new PictureBox();
            contentPanel = new Panel();
            bottomBorder = new Panel();
            leftBorder = new Panel();
            processingInfo = new Label();
            labelWelcomeMsg = new Label();
            PanelUpload = new Panel();
            InitProcess = new Button();
            SelectCompaniesReset = new Button();
            SelectCompanies = new Button();
            SelectRoomsReset = new Button();
            SelectRooms = new Button();
            SelectStudentReset = new Button();
            SelectStudent = new Button();
            panel1 = new Panel();
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
            headerPnl.Size = new Size(507, 52);
            headerPnl.TabIndex = 0;
            // 
            // titleBackgroundPanel
            // 
            titleBackgroundPanel.BackColor = Color.Black;
            titleBackgroundPanel.Controls.Add(BtnClose);
            titleBackgroundPanel.Controls.Add(titleLabel);
            titleBackgroundPanel.Dock = DockStyle.Fill;
            titleBackgroundPanel.Location = new Point(53, 0);
            titleBackgroundPanel.Name = "titleBackgroundPanel";
            titleBackgroundPanel.Size = new Size(454, 52);
            titleBackgroundPanel.TabIndex = 1;
            titleBackgroundPanel.MouseMove += titleBackgroundPanel_MouseMove;
            // 
            // BtnClose
            // 
            BtnClose.Image = Properties.Resources.closeapp2;
            BtnClose.Location = new Point(416, 12);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(26, 30);
            BtnClose.SizeMode = PictureBoxSizeMode.CenterImage;
            BtnClose.TabIndex = 4;
            BtnClose.TabStop = false;
            BtnClose.Click += BtnClose_Click;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(6, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(266, 30);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "BOT-Helper (BWV Aachen)";
            // 
            // logoPictureBox
            // 
            logoPictureBox.BackColor = Color.Black;
            logoPictureBox.Dock = DockStyle.Left;
            logoPictureBox.Image = Properties.Resources.icons8_chatbot_30;
            logoPictureBox.Location = new Point(0, 0);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(53, 52);
            logoPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            logoPictureBox.TabIndex = 0;
            logoPictureBox.TabStop = false;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = SystemColors.Control;
            contentPanel.Controls.Add(panel1);
            contentPanel.Controls.Add(bottomBorder);
            contentPanel.Controls.Add(leftBorder);
            contentPanel.Controls.Add(processingInfo);
            contentPanel.Controls.Add(labelWelcomeMsg);
            contentPanel.Controls.Add(PanelUpload);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 52);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(507, 463);
            contentPanel.TabIndex = 1;
            // 
            // bottomBorder
            // 
            bottomBorder.BackColor = Color.Black;
            bottomBorder.Dock = DockStyle.Bottom;
            bottomBorder.Location = new Point(0, 458);
            bottomBorder.Name = "bottomBorder";
            bottomBorder.Size = new Size(506, 5);
            bottomBorder.TabIndex = 9;
            // 
            // leftBorder
            // 
            leftBorder.BackColor = Color.Black;
            leftBorder.Dock = DockStyle.Right;
            leftBorder.Location = new Point(506, 0);
            leftBorder.Name = "leftBorder";
            leftBorder.Size = new Size(1, 463);
            leftBorder.TabIndex = 8;
            // 
            // processingInfo
            // 
            processingInfo.AutoSize = true;
            processingInfo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            processingInfo.ForeColor = Color.FromArgb(255, 128, 0);
            processingInfo.Location = new Point(53, 413);
            processingInfo.Name = "processingInfo";
            processingInfo.Size = new Size(403, 21);
            processingInfo.TabIndex = 7;
            processingInfo.Text = "Verarbeitung läuft. Bitte das Programm nicht beenden.";
            processingInfo.Visible = false;
            // 
            // labelWelcomeMsg
            // 
            labelWelcomeMsg.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelWelcomeMsg.Location = new Point(28, 11);
            labelWelcomeMsg.Name = "labelWelcomeMsg";
            labelWelcomeMsg.Size = new Size(457, 134);
            labelWelcomeMsg.TabIndex = 6;
            labelWelcomeMsg.Text = resources.GetString("labelWelcomeMsg.Text");
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
            PanelUpload.Location = new Point(12, 148);
            PanelUpload.Name = "PanelUpload";
            PanelUpload.Size = new Size(479, 241);
            PanelUpload.TabIndex = 5;
            // 
            // InitProcess
            // 
            InitProcess.BackColor = Color.Silver;
            InitProcess.FlatStyle = FlatStyle.Flat;
            InitProcess.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            InitProcess.Location = new Point(12, 180);
            InitProcess.Name = "InitProcess";
            InitProcess.Size = new Size(456, 50);
            InitProcess.TabIndex = 4;
            InitProcess.Text = "Verarbeitung starten";
            InitProcess.UseVisualStyleBackColor = false;
            InitProcess.Click += InitProcess_Click;
            // 
            // SelectCompaniesReset
            // 
            SelectCompaniesReset.BackColor = Color.Silver;
            SelectCompaniesReset.FlatStyle = FlatStyle.Flat;
            SelectCompaniesReset.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectCompaniesReset.Location = new Point(418, 68);
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
            SelectCompanies.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            SelectCompanies.Location = new Point(12, 68);
            SelectCompanies.Name = "SelectCompanies";
            SelectCompanies.Size = new Size(456, 50);
            SelectCompanies.TabIndex = 1;
            SelectCompanies.Text = "Veranstalterliste hochladen";
            SelectCompanies.UseVisualStyleBackColor = false;
            SelectCompanies.Click += SelectCompanies_Click;
            // 
            // SelectRoomsReset
            // 
            SelectRoomsReset.BackColor = Color.Silver;
            SelectRoomsReset.FlatStyle = FlatStyle.Flat;
            SelectRoomsReset.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectRoomsReset.Location = new Point(418, 124);
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
            SelectRooms.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            SelectRooms.Location = new Point(12, 124);
            SelectRooms.Name = "SelectRooms";
            SelectRooms.Size = new Size(456, 50);
            SelectRooms.TabIndex = 3;
            SelectRooms.Text = "Raumliste hochladen";
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
            SelectStudent.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            SelectStudent.Location = new Point(12, 12);
            SelectStudent.Name = "SelectStudent";
            SelectStudent.Size = new Size(456, 50);
            SelectStudent.TabIndex = 0;
            SelectStudent.Text = "Wahlliste hochladen";
            SelectStudent.UseVisualStyleBackColor = false;
            SelectStudent.Click += BtnSelectStudent_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1, 458);
            panel1.TabIndex = 10;
            // 
            // MainLayout
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 515);
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
            contentPanel.PerformLayout();
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
        private Label labelWelcomeMsg;
        private Label processingInfo;
        private Panel bottomBorder;
        private Panel leftBorder;
        private Panel panel1;
    }
}