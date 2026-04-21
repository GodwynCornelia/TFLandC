using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public Form1()
        {
            InitializeComponent();
            this.Text = "Новый документ — Редактор";
            richTextBox1.Visible = false;
            undoStack.Push("");
            dgvErrors.CellClick += dgvErrors_CellClick;
        }

        private void ShowEditor()
        {
            if (!richTextBox1.Visible)
            {
                richTextBox1.Visible = true;
            }
        }

        #region Вспомогательные методы файла
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
            if (!Confirm()) e.Cancel = true;
            base.OnFormClosing(e);
        }
        #endregion

        #region Кнопки управления файлами
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

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) => Save();
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e) => SaveAs();
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        private void toolStripButton1_Click(object sender, EventArgs e) => создатьToolStripMenuItem_Click(sender, e);
        private void toolStripButton2_Click(object sender, EventArgs e) => открытьToolStripMenuItem_Click(sender, e);
        private void toolStripButton3_Click(object sender, EventArgs e) => сохранитьToolStripMenuItem_Click(sender, e);
        #endregion

        #region Правка и Undo/Redo
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

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e) { if (richTextBox1.SelectionLength > 0) richTextBox1.Cut(); }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) { if (richTextBox1.SelectionLength > 0) richTextBox1.Copy(); }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e) { if (Clipboard.ContainsText()) richTextBox1.Paste(); }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectedText = "";
        private void выделитьВсToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectAll();

        private void toolStripButton4_Click(object sender, EventArgs e) => отменитьToolStripMenuItem_Click(sender, e);
        private void toolStripButton5_Click(object sender, EventArgs e) => вернутьToolStripMenuItem_Click(sender, e);
        private void toolStripButton6_Click(object sender, EventArgs e) => копироватьToolStripMenuItem_Click(sender, e);
        private void toolStripButton7_Click(object sender, EventArgs e) => вырезатьToolStripMenuItem_Click(sender, e);
        private void toolStripButton8_Click(object sender, EventArgs e) => вставитьToolStripMenuItem_Click(sender, e);
        #endregion

        #region Справка
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) => new Form2().ShowDialog();
        private void toolStripButton11_Click(object sender, EventArgs e) => оПрограммеToolStripMenuItem_Click(sender, e);
        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e) => new Form3().ShowDialog();
        private void toolStripButton10_Click(object sender, EventArgs e) => вызовСправкиToolStripMenuItem_Click(sender, e);
        #endregion

        private void toolStripButton9_Click(object sender, EventArgs e) => пускToolStripMenuItem_Click(sender, e);

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dgvErrors.Rows.Clear();
                dgvSemanticErrors.Rows.Clear();
                rtbAstTree.Clear();

                Scanner scanner = new Scanner();
                List<Token> tokens = scanner.Analyze(richTextBox1.Text);
                foreach (var t in tokens)
                {
                    int rowIdx = dataGridView1.Rows.Add(t.Code, t.Type, t.Lexeme, $"Л:{t.Line} П:{t.StartPos}");
                    dataGridView1.Rows[rowIdx].Tag = t;
                }

                Parser parser = new Parser(tokens);
                List<EnumDeclNode> allNodes = parser.Parse();

                foreach (var err in parser.SyntaxErrors)
                {
                    int rowIndex = dgvErrors.Rows.Add(
                        err.Lexeme,
                        $"Стр:{err.Line}, Поз:{err.StartPos}",
                        err.Type
                    );

                    dgvErrors.Rows[rowIndex].Tag = err;
                }
                SemanticAnalyzer semantic = new SemanticAnalyzer();
                semantic.Analyze(allNodes);

                foreach (var sErr in semantic.Errors)
                {
                    dgvSemanticErrors.Rows.Add(sErr.Message, $"Стр:{sErr.Line}, Поз:{sErr.Position}");
                }

                List<EnumDeclNode> nodesToDraw = new List<EnumDeclNode>();
                HashSet<string> seenNames = new HashSet<string>();

                foreach (var node in allNodes)
                {
                    if (!string.IsNullOrEmpty(node.Name))
                    {
                        if (!seenNames.Contains(node.Name))
                        {
                            nodesToDraw.Add(node);
                            seenNames.Add(node.Name);
                        }
                    }
                }

                if (parser.SyntaxErrors.Count == 0)
                {
                    if (nodesToDraw.Count > 0)
                    {
                        AstVisualizer visualizer = new AstVisualizer();
                        rtbAstTree.Text = visualizer.GenerateTreeText(nodesToDraw);
                    }
                    else if (allNodes.Count > 0)
                    {
                        rtbAstTree.Text = "Дерево пусто: все найденные конструкции содержат семантические ошибки.";
                    }
                }
                else
                {
                    rtbAstTree.Text = "Построение дерева прервано: исправьте синтаксические ошибки.";
                }

                int totalErrors = parser.SyntaxErrors.Count + semantic.Errors.Count;

                if (lblErrCount != null)
                {
                    lblErrCount.Text = $"Ошибок: {totalErrors}";
                    lblErrCount.ForeColor = totalErrors > 0 ? Color.Red : Color.Green;
                }

                if (tabControl1 != null && tabControl1.TabCount > 1)
                {
                    tabControl1.SelectedIndex = (totalErrors > 0) ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при выполнении анализа: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Переход по клику на таблицу
        private void dgvErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvErrors.Rows[e.RowIndex].Tag is Token t) HighlightToken(t);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvErrors.Rows[e.RowIndex].Tag is Token token)
                {
                    richTextBox1.Focus();

                    try
                    {
                        int lineStartIndex = richTextBox1.GetFirstCharIndexFromLine(token.Line - 1);

                        if (lineStartIndex != -1)
                        {
                            int selectionStart = lineStartIndex + token.StartPos;
                            int selectionLength = Math.Max(1, token.Lexeme.Length);
                            richTextBox1.Select(selectionStart, selectionLength);

                            richTextBox1.ScrollToCaret();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при выделении текста: " + ex.Message);
                    }
                }
            }
        }

        private void HighlightToken(Token t)
        {
            try
            {
                int lineStartIndex = richTextBox1.GetFirstCharIndexFromLine(t.Line - 1);
                if (lineStartIndex == -1) return;
                int start = lineStartIndex + t.StartPos;
                richTextBox1.Focus();
                richTextBox1.Select(start, t.Lexeme.Length);
                richTextBox1.ScrollToCaret();
            }
            catch { }
        }
        #endregion

        #region Меню документации
        private void постановкаЗадачиToolStripMenuItem_Click_1(object sender, EventArgs e) => new InfoForm("Постановка задачи", "Task.html").ShowDialog();
        private void грамматикаToolStripMenuItem_Click_1(object sender, EventArgs e) => new InfoForm("Грамматика", "Grammar.html").ShowDialog();
        private void классификацияГрамматикиToolStripMenuItem_Click_1(object sender, EventArgs e) => new InfoForm("Классификация", "Classification.html").ShowDialog();
        private void методАнализаToolStripMenuItem_Click_1(object sender, EventArgs e) => new InfoForm("Метод анализа", "Method.html").ShowDialog();
        private void тестовыйПримерToolStripMenuItem_Click_1(object sender, EventArgs e) => new InfoForm("Пример", "Tests.html").ShowDialog();
        private void списокЛитераторыToolStripMenuItem_Click(object sender, EventArgs e) => new InfoForm("Литература", "References.html").ShowDialog();

        private void исходныйКодлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/GodwynCornelia/TFLandC/blob/main/README.md",
                UseShellExecute = true
            });
        }
        #endregion

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void показатьASTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner();
            List<Token> tokens = scanner.Analyze(richTextBox1.Text);
            Parser parser = new Parser(tokens);
            List<EnumDeclNode> allNodes = parser.Parse();

            List<EnumDeclNode> validNodes = new List<EnumDeclNode>();
            HashSet<string> seenNames = new HashSet<string>();
            foreach (var node in allNodes)
            {
                if (!string.IsNullOrEmpty(node.Name) && !seenNames.Contains(node.Name))
                {
                    validNodes.Add(node);
                    seenNames.Add(node.Name);
                }
            }

            if (validNodes.Count > 0 && parser.SyntaxErrors.Count == 0)
            {
                AstGraphForm graphForm = new AstGraphForm(validNodes);
                graphForm.Show();
            }
            else
            {
                MessageBox.Show("Сначала исправьте ошибки в коде!", "Внимание");
            }
        }
    }
}