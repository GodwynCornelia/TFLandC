namespace WinFormsApp4
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TreeNode treeNode1 = new TreeNode("Создать");
            TreeNode treeNode2 = new TreeNode("Открыть");
            TreeNode treeNode3 = new TreeNode("Сохранить");
            TreeNode treeNode4 = new TreeNode("Сохранить как");
            TreeNode treeNode5 = new TreeNode("Выход");
            TreeNode treeNode6 = new TreeNode("Файл", new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5 });
            TreeNode treeNode7 = new TreeNode("Отменить");
            TreeNode treeNode8 = new TreeNode("Вернуть");
            TreeNode treeNode9 = new TreeNode("Вырезать");
            TreeNode treeNode10 = new TreeNode("Копировать");
            TreeNode treeNode11 = new TreeNode("Вставить");
            TreeNode treeNode12 = new TreeNode("Удалить");
            TreeNode treeNode13 = new TreeNode("Выделить всё");
            TreeNode treeNode14 = new TreeNode("Правка", new TreeNode[] { treeNode7, treeNode8, treeNode9, treeNode10, treeNode11, treeNode12, treeNode13 });
            TreeNode treeNode15 = new TreeNode("Постановка задачи");
            TreeNode treeNode16 = new TreeNode("Грамматика");
            TreeNode treeNode17 = new TreeNode("Классификация грамматики");
            TreeNode treeNode18 = new TreeNode("Метод анализа");
            TreeNode treeNode19 = new TreeNode("Тестовый пример");
            TreeNode treeNode20 = new TreeNode("Список литературы");
            TreeNode treeNode21 = new TreeNode("Исходный код программы");
            TreeNode treeNode22 = new TreeNode("Текст", new TreeNode[] { treeNode15, treeNode16, treeNode17, treeNode18, treeNode19, treeNode20, treeNode21 });
            TreeNode treeNode23 = new TreeNode("Пуск");
            TreeNode treeNode24 = new TreeNode("Область редактирования");
            TreeNode treeNode25 = new TreeNode("Таблица ошибок");
            TreeNode treeNode26 = new TreeNode("Пуск и области", new TreeNode[] { treeNode23, treeNode24, treeNode25 });
            TreeNode treeNode27 = new TreeNode("Описание", new TreeNode[] { treeNode6, treeNode14, treeNode22, treeNode26 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            treeView1 = new TreeView();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(150, 46);
            label1.TabIndex = 0;
            label1.Text = "Справка";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(486, 58);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(284, 339);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, 58);
            treeView1.Name = "treeView1";
            treeNode1.Name = "Узел5";
            treeNode1.Text = "Создать";
            treeNode2.Name = "Узел6";
            treeNode2.Text = "Открыть";
            treeNode3.Name = "Узел7";
            treeNode3.Text = "Сохранить";
            treeNode4.Name = "Узел8";
            treeNode4.Text = "Сохранить как";
            treeNode5.Name = "Узел9";
            treeNode5.Text = "Выход";
            treeNode6.Name = "Узел1";
            treeNode6.Text = "Файл";
            treeNode7.Name = "Узел10";
            treeNode7.Text = "Отменить";
            treeNode8.Name = "Узел11";
            treeNode8.Text = "Вернуть";
            treeNode9.Name = "Узел12";
            treeNode9.Text = "Вырезать";
            treeNode10.Name = "Узел13";
            treeNode10.Text = "Копировать";
            treeNode11.Name = "Узел14";
            treeNode11.Text = "Вставить";
            treeNode12.Name = "Узел15";
            treeNode12.Text = "Удалить";
            treeNode13.Name = "Узел16";
            treeNode13.Text = "Выделить всё";
            treeNode14.Name = "Узел2";
            treeNode14.Text = "Правка";
            treeNode15.Name = "Узел17";
            treeNode15.Text = "Постановка задачи";
            treeNode16.Name = "Узел18";
            treeNode16.Text = "Грамматика";
            treeNode17.Name = "Узел19";
            treeNode17.Text = "Классификация грамматики";
            treeNode18.Name = "Узел20";
            treeNode18.Text = "Метод анализа";
            treeNode19.Name = "Узел21";
            treeNode19.Text = "Тестовый пример";
            treeNode20.Name = "Узел22";
            treeNode20.Text = "Список литературы";
            treeNode21.Name = "Узел23";
            treeNode21.Text = "Исходный код программы";
            treeNode22.Name = "Узел3";
            treeNode22.Text = "Текст";
            treeNode23.Name = "Узел24";
            treeNode23.Text = "Пуск";
            treeNode24.Name = "Узел25";
            treeNode24.Text = "Область редактирования";
            treeNode25.Name = "Узел26";
            treeNode25.Text = "Таблица ошибок";
            treeNode26.Name = "Узел4";
            treeNode26.Text = "Пуск и области";
            treeNode27.Name = "Узел0";
            treeNode27.Text = "Описание";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode27 });
            treeView1.Size = new Size(296, 339);
            treeView1.TabIndex = 2;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(326, 139);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(135, 137);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(treeView1);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RichTextBox richTextBox1;
        private TreeView treeView1;
        private PictureBox pictureBox1;
    }
}