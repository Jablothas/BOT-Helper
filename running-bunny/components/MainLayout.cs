using System.Runtime.CompilerServices;

namespace running_bunny
{
    public partial class MainLayout : Form
    {
        static String FilePath = string.Empty;
        public MainLayout()
        {
            InitializeComponent();
        }

        private void openFileDialogBtn_Click(object sender, EventArgs e)
        {
            int y;
            int x;
            uploadPanel.Visible = false;
            y = uploadPanel.Location.Y;
            x = uploadPanel.Location.X;
            processLabel1.Text = OpenExcelFileDialog(); // Need to cut to filename only
            proccessPanel.Visible = true;
            proccessPanel.Location = new Point(x, y);
        }

        static string OpenExcelFileDialog()
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
                    FilePath = filePath;
                    return filePath;
                }
                else
                {
                    MessageBox.Show($"Please select a valid {string.Join(", ", validFileEndings)} file.", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        private void processBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists(FilePath))
            {
                // Starting point for anna, emanuel & kevin
            }
        }
    }
}