using System.Threading.Channels;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {

        private string filePath = "";
        private bool changed = false;
        public Form1()
        {
            InitializeComponent();


            this.Text = "Íîâûé äîêóìåíò — Ğåäàêòîğ";
            richTextBox1.TextChanged += (s, e) => changed = true;
        }
        #region 
        private void Save()
        {
            if (string.IsNullOrEmpty(filePath)) SaveAs();
            else
            {
                File.WriteAllText(filePath, richTextBox1.Text);
                changed = false;
            }
        }

        private void SaveAs()
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Òåêñòîâûå ôàéëû|*.txt|Âñå ôàéëû|*.*" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    File.WriteAllText(filePath, richTextBox1.Text);
                    changed = false;
                }
            }
        }
        private bool Confirm()
        {
            if (!changed) return true;
            var res = MessageBox.Show("Ñîõğàíèòü èçìåíåíèÿ â ôàéëå?", "Âíèìàíèå", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) { Save(); return !changed; }
            return res == DialogResult.No;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!Confirm())
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
        #endregion

        #region 
        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Confirm()) { richTextBox1.Clear(); filePath = ""; changed = false; }
        }

        private void îòêğûòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Confirm())
            {
                OpenFileDialog ofd = new OpenFileDialog { Filter = "Text|*.txt" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = File.ReadAllText(ofd.FileName);
                    filePath = ofd.FileName;
                    changed = false;
                }
            }
        }

        private void ñîõğàíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath)) ñîõğàíèòüÊàêToolStripMenuItem_Click(sender, e);
            else
            {
                File.WriteAllText(filePath, richTextBox1.Text);
                changed = false;
            }
        }

        private void ñîõğàíèòüÊàêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Òåêñòîâûå ôàéëû|*.txt" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    File.WriteAllText(filePath, richTextBox1.Text);
                    changed = false;
                }
            }
        }
        private void âûõîäToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ñîçäàòüToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            îòêğûòüToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ñîõğàíèòüToolStripMenuItem_Click(sender, e);
        }
        #endregion
        private void ñïèñîêËèòåğàòîğûToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void èñõîäíûéÊîäëToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void îÏğîãğàììåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 aboutForm = new Form2();
            aboutForm.ShowDialog();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            îÏğîãğàììåToolStripMenuItem_Click(sender, e);
        }

        private void âûçîâÑïğàâêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 aboutForm = new Form3();
            aboutForm.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            âûçîâÑïğàâêèToolStripMenuItem_Click(sender, e);
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

       
    }
}
