using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            this.Load += Form3_Load;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;    
            richTextBox1.DetectUrls = true;    


            richTextBox1.Text = "Статус проекта: В разработке..." + Environment.NewLine +
                                "Документация README: https://github.com/GodwynCornelia/TFLandC/blob/main/README.md";


            richTextBox1.LinkClicked -= richTextBox1_LinkClicked;
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                // Открываем ссылку в браузере
                Process.Start(new ProcessStartInfo
                {
                    FileName = e.LinkText,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии ссылки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
