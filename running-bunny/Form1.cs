using System.Runtime.CompilerServices;

namespace running_bunny
{
    public partial class Form1 : Form
    {
        static String FilePath = string.Empty;
        public Form1()
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
            processLabel1.Text = OpenCsvFileDialog(); // Need to cut to filename only
            proccessPanel.Visible = true;
            proccessPanel.Location = new Point(x, y);
        }

        static string OpenCsvFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    FilePath = openFileDialog.FileName; 
                    return openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("Please select a valid .csv file.", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }

        private void processBtn_Click(object sender, EventArgs e)
        {
            if(File.Exists(FilePath))
            {
                // Starting point for anna, emanuel & kevin
            }
            
            
        }
    }
}