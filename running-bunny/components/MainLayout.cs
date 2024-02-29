using running_bunny.Business;
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

        public MainLayout()
        {
            InitializeComponent();
        }

        private void InitProcess_Click(object sender, EventArgs e)
        {
            if(FilePaths.Count != 3)
            {
                MessageBox.Show("Sie müssen alle benötigten Daten voher hochladen.");
                return;
            }
            foreach (KeyValuePair<string, string> file in FilePaths)
            {
                string key = file.Key;
                string value = file.Value;
                switch (key)
                {
                    case "SelectStundent":
                        //
                        break;
                    case "SelectRooms":
                        //
                        break;
                    case "SelectCompanies":
                        //
                        break;
                    default:
                        break;
                }
                //var verarbeitung = new Verarbeitung();
                //verarbeitung.run(FilePath, null, null);
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
                    btn.Enabled = false;
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
            FilePaths.Remove("SelectStudent");
            SelectStudent.Text = "Schülerliste hochladen";
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
            FilePaths.Remove("SelectRooms");
            SelectRooms.Text = "Raumliste hochladen";
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
            FilePaths.Remove("SelectCompanies");
            SelectCompanies.Text = "Unternehmensliste hochladen";
        }
    }
}