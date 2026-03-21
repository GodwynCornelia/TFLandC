using System.Threading.Channels;
using System.Collections.Generic;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {

        private string filePath = "";
        private bool changed = false;

        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();
        private bool isOperating = false;

        private List<Token> lastAnalysisResults = new List<Token>();

        public Form1()
        {
            InitializeComponent();


            this.Text = "Íîâûé äîêóìåíò — Ðåäàêòîð";
            richTextBox1.Visible = false;

            undoStack.Push("");

        }

        private void ShowEditor()
        {
            if (!richTextBox1.Visible)
            {
                richTextBox1.Visible = true;
            }
        }

        //âñïîìîãàòåëüíûå ìåòîäû äëÿ ðàáîòû ñ ôàéëîì
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
            var res = MessageBox.Show("Ñîõðàíèòü èçìåíåíèÿ â ôàéëå?", "Âíèìàíèå", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

        //ðåàëèçàöèÿ êíîïîê ðàáîòû ñ ôàéëàìè
        #region 
        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Confirm())
            {
                richTextBox1.Clear();
                filePath = "";
                changed = false;
                ShowEditor();
            }
        }

        private void îòêðûòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Confirm())
            {
                OpenFileDialog ofd = new OpenFileDialog { Filter = "Text|*.txt" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = File.ReadAllText(ofd.FileName);
                    filePath = ofd.FileName;
                    changed = false;
                    ShowEditor();
                }
            }
        }

        private void ñîõðàíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath)) ñîõðàíèòüÊàêToolStripMenuItem_Click(sender, e);
            else
            {
                File.WriteAllText(filePath, richTextBox1.Text);
                changed = false;
            }
        }

        private void ñîõðàíèòüÊàêToolStripMenuItem_Click(object sender, EventArgs e)
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
            îòêðûòüToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ñîõðàíèòüToolStripMenuItem_Click(sender, e);
        }
        #endregion

        //ðàáîòà ñ ðàçäåëîì ïðàâêè
        #region
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            changed = true;

            if (!isOperating)
            {
                if (redoStack.Count > 0) redoStack.Clear();
                if (undoStack.Count == 0 || richTextBox1.Text != undoStack.Peek())
                {
                    undoStack.Push(richTextBox1.Text);
                }
            }
        }
        private void îòìåíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 1)
            {
                isOperating = true;
                redoStack.Push(undoStack.Pop());

                richTextBox1.Text = undoStack.Peek();

                richTextBox1.SelectionStart = richTextBox1.Text.Length;

                isOperating = false;
            }
        }


        private void âåðíóòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                isOperating = true;

                string state = redoStack.Pop();
                undoStack.Push(state);

                richTextBox1.Text = state;

                richTextBox1.SelectionStart = richTextBox1.Text.Length;

                isOperating = false;
            }
        }

        private void âûðåçàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Cut();
        }

        private void êîïèðîâàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Copy();
        }

        private void âñòàâèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) richTextBox1.Paste();
        }

        private void óäàëèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void âûäåëèòüÂñToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            îòìåíèòüToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            âåðíóòüToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            êîïèðîâàòüToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            âûðåçàòüToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            âñòàâèòüToolStripMenuItem_Click(sender, e);
        }
        #endregion
        //âûâîä ñïðàâî÷íûõ îêîí
        #region
        private void îÏðîãðàììåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 aboutForm = new Form2();
            aboutForm.ShowDialog();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            îÏðîãðàììåToolStripMenuItem_Click(sender, e);
        }

        private void âûçîâÑïðàâêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 aboutForm = new Form3();
            aboutForm.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            âûçîâÑïðàâêèToolStripMenuItem_Click(sender, e);
        }
        #endregion


        private void ñïèñîêËèòåðàòîðûToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void èñõîäíûéÊîäëToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Tag is Token token)
            {

                int charIndex = richTextBox1.GetFirstCharIndexFromLine(token.Line - 1) + token.StartPos;

                richTextBox1.Focus();
                richTextBox1.Select(charIndex, token.EndPos - token.StartPos);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ïóñêToolStripMenuItem_Click(sender, e);
        }

        private void ïóñêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner();
            lastAnalysisResults = scanner.Analyze(richTextBox1.Text);
            dataGridView1.Rows.Clear();
            foreach (var t in lastAnalysisResults)
            {
                int rowIdx = dataGridView1.Rows.Add(t.Code, t.Type, t.Lexeme, $"Ë:{t.Line} Ï:{t.StartPos}");
                dataGridView1.Rows[rowIdx].Tag = t;

                if (t.Code == 99)
                    dataGridView1.Rows[rowIdx].DefaultCellStyle.BackColor = Color.MistyRose;
            }

            Parser parser = new Parser(lastAnalysisResults);
            parser.Parse();

            dgvErrors.Rows.Clear();
            lblErrCount.Text = $"Îøèáêè ñèíòàêñè÷åñêîãî àíàëèçà (âñåãî: {parser.SyntaxErrors.Count}):";

            foreach (var err in parser.SyntaxErrors)
            {
                int rowIdx = dgvErrors.Rows.Add(
                    err.Lexeme,
                    $"Ñòð: {err.Line}, Ïîç: {err.StartPos}",
                    err.Type
                );
                dgvErrors.Rows[rowIdx].Tag = err;
            }

            if (parser.SyntaxErrors.Count == 0 && lastAnalysisResults.Count > 0)
            {
                MessageBox.Show("Ñèíòàêñè÷åñêèé àíàëèç çàâåðøåí. Îøèáîê íå íàéäåíî!", "Óñïåõ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvErrors.Rows[e.RowIndex].Tag is Token err)
            {
                int charIndex = richTextBox1.GetFirstCharIndexFromLine(err.Line - 1) + err.StartPos;
                richTextBox1.Focus();
                int len = Math.Max(1, err.EndPos - err.StartPos);
                richTextBox1.Select(charIndex, len);
            }
        }
    }
}
