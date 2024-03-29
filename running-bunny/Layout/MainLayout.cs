using running_bunny.Business;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace running_bunny
{
    public partial class MainLayout : Form
    {
        // Make header dragable...
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        // Globals
        private static Dictionary<string, string> FilePaths = new Dictionary<string, string>();

        public MainLayout(Icon appIcon)
        {
            Icon = appIcon;
            InitializeComponent();
        }

        private async void InitProcess_Click(object sender, EventArgs e)
        {
            if (FilePaths.Count != 3)
            {
                MessageBox.Show("Sie müssen alle benötigten Daten voher hochladen.", "Halt!");
                return;
            }
            LoadProcessAnimation(true);
            var verarbeitung = new Verarbeitung();
            await Task.Run(() => verarbeitung.run(FilePaths[nameof(SelectStudent)], FilePaths[nameof(SelectCompanies)], FilePaths[nameof(SelectRooms)]));
            LoadProcessAnimation(false);
            MessageBox.Show("Die Verarbeitung wurde erfolgreich abgeschlossen.\nSie können das Programm nun schließen.", "Erfolg!");
            processingInfo.Visible = false;
            ForwardToDownloads();
        }

        private void LoadProcessAnimation(bool status)
        {
            if(status)
            {
                processingInfo.Visible = true;
                PanelUpload.Enabled = false;
                InitProcess.Text = "Verarbeitung läuft! Bitte warten.";
                InitProcess.BackColor = Color.YellowGreen;
            }
            else
            {
                processingInfo.Visible = false;
                PanelUpload.Enabled = true;
                InitProcess.Text = "Verarbeitung starten";
                InitProcess.BackColor = Color.White;
            }
        }

        static void OpenExcelFileDialog(Button btn, Button btnReset)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            var validFileEndings = new[] { ".xls", ".xlsx", ".xls" };
            openFileDialog.Filter = "Excel Filter|*.xls;*.xlsx;*.xls|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                if (validFileEndings.Any(fileEnding => filePath.EndsWith(fileEnding, StringComparison.OrdinalIgnoreCase)))
                {
                    btn.BackColor = Color.YellowGreen;
                    btn.Text = $"{Path.GetFileName(filePath)} gespeichert.";
                    btn.Enabled = true;
                    btnReset.Visible = true;
                    FilePaths.Add(btn.Name, filePath);
                }
                else
                {
                    MessageBox.Show($"Please select a valid {string.Join(", ", validFileEndings)} file.", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void titleBackgroundPanel_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        // Here begins event logic - boring stuff. 
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnSelectStudent_Click(object sender, EventArgs e)
        {
            OpenExcelFileDialog(SelectStudent, SelectStudentReset);
        }

        private void SelectStudentReset_Click(object sender, EventArgs e)
        {
            SelectStudentReset.Visible = false;
            SelectStudent.Enabled = true;
            SelectStudent.BackColor = Color.Silver;
            FilePaths.Remove(nameof(SelectStudent));
            SelectStudent.Text = "1. Wahlliste hochladen";
        }

        private void SelectRooms_Click(object sender, EventArgs e)
        {
            OpenExcelFileDialog(SelectRooms, SelectRoomsReset);
        }

        private void SelectRoomsReset_Click(object sender, EventArgs e)
        {
            SelectRoomsReset.Visible = false;
            SelectRooms.Enabled = true;
            SelectRooms.BackColor = Color.Silver;
            FilePaths.Remove(nameof(SelectRooms));
            SelectRooms.Text = "3 .Raumliste hochladen";
        }

        private void SelectCompanies_Click(object sender, EventArgs e)
        {
            OpenExcelFileDialog(SelectCompanies, SelectCompaniesReset);
        }

        private void SelectCompaniesReset_Click(object sender, EventArgs e)
        {
            SelectCompaniesReset.Visible = false;
            SelectCompanies.Enabled = true;
            SelectCompanies.BackColor = Color.Silver;
            FilePaths.Remove(nameof(SelectCompanies));
            SelectCompanies.Text = "2. Veranstalterliste hochladen";
        }
        private void ForwardToDownloads()
        {
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            if (System.IO.Directory.Exists(downloadsPath)) Process.Start("explorer.exe", downloadsPath);
        }


    }
}
