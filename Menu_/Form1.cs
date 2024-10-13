namespace Menu_
{
    public partial class Form1 : Form
    {
        private bool isSaved;
        private string filename;
        public Form1()
        {
            InitializeComponent();

            foreach (string recentFile in File.ReadAllLines(@"C:\Users\verai\Downloads\Menu_\Menu_\bin\Debug\net8.0-windows\recent.txt"))
            {
                ToolStripMenuItem item = new(recentFile);
                item.Click += Item_Click;
                recentFilesToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            filename = item.Text;
            textBox1.Text = File.ReadAllText(item.Text);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckChanges())
                textBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            CheckChanges();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            filename = dialog.FileName;
            textBox1.Text = File.ReadAllText(dialog.FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FontDialog dialog = new FontDialog();

            if (dialog.ShowDialog() != DialogResult.OK) return;
            textBox1.Font = dialog.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() != DialogResult.OK) return;
            textBox1.ForeColor = dialog.Color;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckChanges()) Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(!(CheckChanges())) e.Cancel = true;
        }

        public void Save()
        {
            if (File.Exists(filename))
            {
                isSaved = true;
                File.WriteAllText(filename, textBox1.Text);
            }
            else
            {
                SaveAs();
            }
        }
        public void SaveAs()
        {
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() != DialogResult.OK) return;
            filename = dialog.FileName;
            File.WriteAllText(dialog.FileName, textBox1.Text);
            isSaved = true;
        }

        public bool CheckChanges()
        {
            if (!(isSaved || textBox1.Text == ""))
            {
                DialogResult result = MessageBox.Show("You have unsaved changes.\nSave youre file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) Save();
            }
            return true;
        }
    }


}
