using System.Threading.Channels;
using System.Collections.Generic;
using System.Windows.Forms;

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


            this.Text = "Новый документ — Редактор";
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

        //вспомогательные методы для работы с файлом
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
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
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
            var res = MessageBox.Show("Сохранить изменения в файле?", "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

        //реализация кнопок работы с файлами
        #region 
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Confirm())
            {
                richTextBox1.Clear();
                filePath = "";
                changed = false;
                ShowEditor();
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath)) сохранитьКакToolStripMenuItem_Click(sender, e);
            else
            {
                File.WriteAllText(filePath, richTextBox1.Text);
                changed = false;
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Текстовые файлы|*.txt" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    File.WriteAllText(filePath, richTextBox1.Text);
                    changed = false;
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, e);
        }
        #endregion

        //работа с разделом правки
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
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
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


        private void вернутьToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void выделитьВсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            отменитьToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            вернутьToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            копироватьToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            вырезатьToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            вставитьToolStripMenuItem_Click(sender, e);
        }
        #endregion
        //вывод справочных окон
        #region
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 aboutForm = new Form2();
            aboutForm.ShowDialog();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            оПрограммеToolStripMenuItem_Click(sender, e);
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 aboutForm = new Form3();
            aboutForm.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            вызовСправкиToolStripMenuItem_Click(sender, e);
        }
        #endregion


        private void списокЛитераторыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void исходныйКодлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            пускToolStripMenuItem_Click(sender, e);
        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("Введите код для анализа", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dataGridView1.Rows.Clear();
                dgvErrors.Rows.Clear();

                Scanner scanner = new Scanner();
                List<Token> tokens = scanner.Analyze(richTextBox1.Text);

                foreach (var t in tokens)
                {
                    int rowIdx = dataGridView1.Rows.Add(t.Code, t.Type, t.Lexeme, $"Л:{t.Line} П:{t.StartPos}");
                    dataGridView1.Rows[rowIdx].Tag = t;
                }

                Parser parser = new Parser(tokens);
                parser.Parse();

                if (parser.SyntaxErrors != null)
                {
                    foreach (var err in parser.SyntaxErrors)
                    {
                        int rowIdx = dgvErrors.Rows.Add(err.Lexeme, $"Стр:{err.Line}, Поз:{err.StartPos}", err.Type);
                        dgvErrors.Rows[rowIdx].Tag = err;
                    }
                }

                int errorCount = parser.SyntaxErrors?.Count ?? 0;

                if (lblErrCount != null)
                {
                    lblErrCount.Text = $"Общее количество ошибок: {errorCount}";
                    lblErrCount.ForeColor = errorCount > 0 ? Color.Red : Color.Green;
                }

                if (tabControl1 != null)
                {
                    if (errorCount > 0)
                    {
                        tabControl1.SelectedIndex = 1; 
                    }
                    else
                    {
                        tabControl1.SelectedIndex = 0;
                        MessageBox.Show("Синтаксических ошибок не обнаружено!", "Анализ завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка при анализе: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvErrors.Rows[e.RowIndex].Tag is Token err)
            {
                try
                {
                    int lineStartIndex = richTextBox1.GetFirstCharIndexFromLine(err.Line - 1);
                    if (lineStartIndex == -1) return;

                    int globalSelectionStart = lineStartIndex + err.StartPos;
                    int length = err.Lexeme.Length;

                    richTextBox1.Focus();
                    if (globalSelectionStart >= 0 && globalSelectionStart + length <= richTextBox1.Text.Length)
                    {
                        richTextBox1.Select(globalSelectionStart, length);
                        richTextBox1.ScrollToCaret();
                    }
                }
                catch {}
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Tag is Token t)
            {
                try
                {
                    int lineStartIndex = richTextBox1.GetFirstCharIndexFromLine(t.Line - 1);
                    if (lineStartIndex == -1) return;

                    int globalSelectionStart = lineStartIndex + t.StartPos;
                    int length = t.Lexeme.Length;

                    richTextBox1.Focus();
                    if (globalSelectionStart >= 0 && globalSelectionStart + length <= richTextBox1.Text.Length)
                    {
                        richTextBox1.Select(globalSelectionStart, length);
                        richTextBox1.ScrollToCaret();
                    }
                }
                catch {}
            }
        }
    }
}
