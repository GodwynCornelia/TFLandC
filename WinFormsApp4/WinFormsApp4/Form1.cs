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


            this.Text = "Новый документ — Редактор";
            richTextBox1.Visible = false;

        }

        private void ShowEditor()
        {
            if (!richTextBox1.Visible)
            {
                richTextBox1.Visible = true;
                dataGridView1.Visible = true;
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

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo) richTextBox1.Undo();//?????????????????? некорректно работает
        }

        private void вернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo) richTextBox1.Redo();
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
            вставитьToolStripMenuItem_Click(sender, e);//???????????????????????????????????????? разобраться что конкретно требуется
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
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
