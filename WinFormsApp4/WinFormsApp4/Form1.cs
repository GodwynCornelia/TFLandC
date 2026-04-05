using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions; 

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void постановкаЗадачиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Здесь будет текст вашей задачи.");
        }


        private void dgvErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void ShowEditor()
        {
            if (!richTextBox1.Visible) richTextBox1.Visible = true;
        }

        #region Файловые операции
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
        private void сохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e) => SaveAs();
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();
        #endregion

        #region Правка (Undo/Redo, Буфер)
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            changed = true;
            if (!isOperating)
            {
                if (redoStack.Count > 0) redoStack.Clear();
                if (undoStack.Count == 0 || richTextBox1.Text != undoStack.Peek())
                    undoStack.Push(richTextBox1.Text);
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
        private void выделитьВсToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectAll();
        #endregion

        #region Поиск подстрок (ЛР 4)
        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboRegexSelection.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите задачу!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pattern = "";
            switch (comboRegexSelection.SelectedIndex)
            {
                case 0:
                    pattern = @"\b[\w\-]+\.(?:doc|docx|pdf|jpg|jpeg|png|gif)\b";
                    break;
                case 1:
                    pattern = @"\b(?=[а-яА-ЯёЁ]{5,})[а-яА-ЯёЁ]*чай[а-яА-ЯёЁ]*\b";
                    break;
                case 2:
                    pattern = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\/(?:[0-9]|[1-2][0-9]|3[0-2])(?::(?:[0-9]|[1-9][0-9]{1,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5]))?\b";
                    break;
            }

            try
            {
                dataGridView1.Rows.Clear();
                MatchCollection matches = Regex.Matches(richTextBox1.Text, pattern, RegexOptions.IgnoreCase);

                if (matches.Count == 0)
                {
                    MessageBox.Show("Совпадений не найдено.", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (Match match in matches)
                {
                    dataGridView1.Rows.Add(match.Value, match.Index, match.Length);
                }

                int totalRowIndex = dataGridView1.Rows.Add("Общее количество совпадений:", matches.Count.ToString(), "");

                dataGridView1.Rows[totalRowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[totalRowIndex].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1) 
            {
                if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), out int start) &&
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out int length))
                {
                    richTextBox1.Focus();
                    richTextBox1.Select(start, length);
                    richTextBox1.ScrollToCaret();
                }
            }
        }
        #endregion

        #region Справка и прочее
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) => new Form2().ShowDialog();
        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e) => new Form3().ShowDialog();
        private void toolStripButton1_Click(object sender, EventArgs e) => создатьToolStripMenuItem_Click(sender, e);
        private void toolStripButton9_Click(object sender, EventArgs e) => пускToolStripMenuItem_Click(sender, e);
        #endregion


        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void вернутьToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void вызовСправкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 aboutForm = new Form3();
            aboutForm.ShowDialog();
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 aboutForm = new Form2();
            aboutForm.ShowDialog();
        }

        private void вырезатьToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void выделитьВсToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Copy();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Cut();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) richTextBox1.Paste();
        }

        private void постановкаЗадачиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var info = new InfoForm("Постановка задачи", "Task.html");
            info.ShowDialog();
        }

        private void грамматикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm("Грамматика", "Grammar.html");
            info.ShowDialog();
        }

        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm("Классификация грамматики", "Classification.html");
            info.ShowDialog();
        }

        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm("Метод анализа", "Method.html");
            info.ShowDialog();
        }

        private void тестовыйПримерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm("Тестовый пример", "Tests.html");
            info.ShowDialog();
        }

        private void списокЛитературыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm("Список литературы", "References.html");
            info.ShowDialog();
        }

        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/GodwynCornelia/TFLandC/blob/main/README.md",
                UseShellExecute = true
            });
        }

        private void comboRegexSelection_Click(object sender, EventArgs e)
        {

        }
    }
}