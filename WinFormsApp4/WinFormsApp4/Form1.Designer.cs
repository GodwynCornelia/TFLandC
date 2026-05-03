namespace WinFormsApp4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            правкаToolStripMenuItem = new ToolStripMenuItem();
            отменитьToolStripMenuItem = new ToolStripMenuItem();
            вернутьToolStripMenuItem = new ToolStripMenuItem();
            вырезатьToolStripMenuItem = new ToolStripMenuItem();
            копироватьToolStripMenuItem = new ToolStripMenuItem();
            вставитьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            выделитьВсToolStripMenuItem = new ToolStripMenuItem();
            текстToolStripMenuItem = new ToolStripMenuItem();
            постановкаЗадачиToolStripMenuItem1 = new ToolStripMenuItem();
            грамматикаToolStripMenuItem = new ToolStripMenuItem();
            классификацияГрамматикиToolStripMenuItem = new ToolStripMenuItem();
            методАнализаToolStripMenuItem = new ToolStripMenuItem();
            тестовыйПримерToolStripMenuItem = new ToolStripMenuItem();
            списокЛитературыToolStripMenuItem = new ToolStripMenuItem();
            исходныйКодПрограммыToolStripMenuItem = new ToolStripMenuItem();
            пускToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            вызовСправкиToolStripMenuItem1 = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem1 = new ToolStripMenuItem();
            постановкаЗадачиToolStripMenuItem = new ToolStripMenuItem();
            вызовСправкиToolStripMenuItem = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            btnNew = new ToolStripButton();
            btnOpen = new ToolStripButton();
            btnSave = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnUndo = new ToolStripButton();
            btnRedo = new ToolStripButton();
            btnCopy = new ToolStripButton();
            btnCut = new ToolStripButton();
            btnPaste = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnRun = new ToolStripButton();
            btnHelp = new ToolStripButton();
            btnAbout = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            lblStatusPath = new ToolStripStatusLabel();
            splitContainer1 = new SplitContainer();
            richTextBox1 = new RichTextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dataGridView1 = new DataGridView();
            colSubstring = new DataGridViewTextBoxColumn();
            colPosition = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            colLength = new DataGridViewTextBoxColumn();
            tabPage2 = new TabPage();
            richTextBoxPoliz = new TextBox();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, правкаToolStripMenuItem, текстToolStripMenuItem, пускToolStripMenuItem, справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(900, 24);
            menuStrip1.TabIndex = 0;
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem, открытьToolStripMenuItem, сохранитьToolStripMenuItem, сохранитьКакToolStripMenuItem, выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            создатьToolStripMenuItem.Size = new Size(154, 22);
            создатьToolStripMenuItem.Text = "Создать";
            создатьToolStripMenuItem.Click += создатьToolStripMenuItem_Click;
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(154, 22);
            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += открытьToolStripMenuItem_Click;
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(154, 22);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            сохранитьToolStripMenuItem.Click += сохранитьToolStripMenuItem_Click;
            // 
            // сохранитьКакToolStripMenuItem
            // 
            сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            сохранитьКакToolStripMenuItem.Size = new Size(154, 22);
            сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            сохранитьКакToolStripMenuItem.Click += сохранитьКакToolStripMenuItem_Click_1;
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(154, 22);
            выходToolStripMenuItem.Text = "Выход";
            выходToolStripMenuItem.Click += выходToolStripMenuItem_Click;
            // 
            // правкаToolStripMenuItem
            // 
            правкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { отменитьToolStripMenuItem, вернутьToolStripMenuItem, вырезатьToolStripMenuItem, копироватьToolStripMenuItem, вставитьToolStripMenuItem, удалитьToolStripMenuItem, выделитьВсToolStripMenuItem });
            правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            правкаToolStripMenuItem.Size = new Size(59, 20);
            правкаToolStripMenuItem.Text = "Правка";
            // 
            // отменитьToolStripMenuItem
            // 
            отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            отменитьToolStripMenuItem.Size = new Size(147, 22);
            отменитьToolStripMenuItem.Text = "Отменить";
            отменитьToolStripMenuItem.Click += отменитьToolStripMenuItem_Click;
            // 
            // вернутьToolStripMenuItem
            // 
            вернутьToolStripMenuItem.Name = "вернутьToolStripMenuItem";
            вернутьToolStripMenuItem.Size = new Size(147, 22);
            вернутьToolStripMenuItem.Text = "Вернуть";
            вернутьToolStripMenuItem.Click += вернутьToolStripMenuItem_Click_1;
            // 
            // вырезатьToolStripMenuItem
            // 
            вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            вырезатьToolStripMenuItem.Size = new Size(147, 22);
            вырезатьToolStripMenuItem.Text = "Вырезать";
            вырезатьToolStripMenuItem.Click += вырезатьToolStripMenuItem_Click_2;
            // 
            // копироватьToolStripMenuItem
            // 
            копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            копироватьToolStripMenuItem.Size = new Size(147, 22);
            копироватьToolStripMenuItem.Text = "Копировать";
            копироватьToolStripMenuItem.Click += копироватьToolStripMenuItem_Click_1;
            // 
            // вставитьToolStripMenuItem
            // 
            вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            вставитьToolStripMenuItem.Size = new Size(147, 22);
            вставитьToolStripMenuItem.Text = "Вставить";
            вставитьToolStripMenuItem.Click += вставитьToolStripMenuItem_Click_1;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(147, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click_1;
            // 
            // выделитьВсToolStripMenuItem
            // 
            выделитьВсToolStripMenuItem.Name = "выделитьВсToolStripMenuItem";
            выделитьВсToolStripMenuItem.Size = new Size(147, 22);
            выделитьВсToolStripMenuItem.Text = "Выделать всё";
            выделитьВсToolStripMenuItem.Click += выделитьВсToolStripMenuItem_Click_1;
            // 
            // текстToolStripMenuItem
            // 
            текстToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { постановкаЗадачиToolStripMenuItem1, грамматикаToolStripMenuItem, классификацияГрамматикиToolStripMenuItem, методАнализаToolStripMenuItem, тестовыйПримерToolStripMenuItem, списокЛитературыToolStripMenuItem, исходныйКодПрограммыToolStripMenuItem });
            текстToolStripMenuItem.Name = "текстToolStripMenuItem";
            текстToolStripMenuItem.Size = new Size(48, 20);
            текстToolStripMenuItem.Text = "Текст";
            // 
            // постановкаЗадачиToolStripMenuItem1
            // 
            постановкаЗадачиToolStripMenuItem1.Name = "постановкаЗадачиToolStripMenuItem1";
            постановкаЗадачиToolStripMenuItem1.Size = new Size(231, 22);
            постановкаЗадачиToolStripMenuItem1.Text = "Постановка задачи";
            постановкаЗадачиToolStripMenuItem1.Click += постановкаЗадачиToolStripMenuItem1_Click;
            // 
            // грамматикаToolStripMenuItem
            // 
            грамматикаToolStripMenuItem.Name = "грамматикаToolStripMenuItem";
            грамматикаToolStripMenuItem.Size = new Size(231, 22);
            грамматикаToolStripMenuItem.Text = "Грамматика";
            грамматикаToolStripMenuItem.Click += грамматикаToolStripMenuItem_Click;
            // 
            // классификацияГрамматикиToolStripMenuItem
            // 
            классификацияГрамматикиToolStripMenuItem.Name = "классификацияГрамматикиToolStripMenuItem";
            классификацияГрамматикиToolStripMenuItem.Size = new Size(231, 22);
            классификацияГрамматикиToolStripMenuItem.Text = "Классификация грамматики";
            классификацияГрамматикиToolStripMenuItem.Click += классификацияГрамматикиToolStripMenuItem_Click;
            // 
            // методАнализаToolStripMenuItem
            // 
            методАнализаToolStripMenuItem.Name = "методАнализаToolStripMenuItem";
            методАнализаToolStripMenuItem.Size = new Size(231, 22);
            методАнализаToolStripMenuItem.Text = "Метод анализа";
            методАнализаToolStripMenuItem.Click += методАнализаToolStripMenuItem_Click;
            // 
            // тестовыйПримерToolStripMenuItem
            // 
            тестовыйПримерToolStripMenuItem.Name = "тестовыйПримерToolStripMenuItem";
            тестовыйПримерToolStripMenuItem.Size = new Size(231, 22);
            тестовыйПримерToolStripMenuItem.Text = "Тестовый пример";
            тестовыйПримерToolStripMenuItem.Click += тестовыйПримерToolStripMenuItem_Click;
            // 
            // списокЛитературыToolStripMenuItem
            // 
            списокЛитературыToolStripMenuItem.Name = "списокЛитературыToolStripMenuItem";
            списокЛитературыToolStripMenuItem.Size = new Size(231, 22);
            списокЛитературыToolStripMenuItem.Text = "Список литературы";
            списокЛитературыToolStripMenuItem.Click += списокЛитературыToolStripMenuItem_Click;
            // 
            // исходныйКодПрограммыToolStripMenuItem
            // 
            исходныйКодПрограммыToolStripMenuItem.Name = "исходныйКодПрограммыToolStripMenuItem";
            исходныйКодПрограммыToolStripMenuItem.Size = new Size(231, 22);
            исходныйКодПрограммыToolStripMenuItem.Text = "Исходный код программы";
            исходныйКодПрограммыToolStripMenuItem.Click += исходныйКодПрограммыToolStripMenuItem_Click;
            // 
            // пускToolStripMenuItem
            // 
            пускToolStripMenuItem.Name = "пускToolStripMenuItem";
            пускToolStripMenuItem.Size = new Size(46, 20);
            пускToolStripMenuItem.Text = "Пуск";
            пускToolStripMenuItem.Click += пускToolStripMenuItem_Click;
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { вызовСправкиToolStripMenuItem1, оПрограммеToolStripMenuItem1 });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(65, 20);
            справкаToolStripMenuItem.Text = "Справка";
            справкаToolStripMenuItem.Click += справкаToolStripMenuItem_Click;
            // 
            // вызовСправкиToolStripMenuItem1
            // 
            вызовСправкиToolStripMenuItem1.Name = "вызовСправкиToolStripMenuItem1";
            вызовСправкиToolStripMenuItem1.Size = new Size(156, 22);
            вызовСправкиToolStripMenuItem1.Text = "Вызов справки";
            вызовСправкиToolStripMenuItem1.Click += вызовСправкиToolStripMenuItem1_Click;
            // 
            // оПрограммеToolStripMenuItem1
            // 
            оПрограммеToolStripMenuItem1.Name = "оПрограммеToolStripMenuItem1";
            оПрограммеToolStripMenuItem1.Size = new Size(156, 22);
            оПрограммеToolStripMenuItem1.Text = "О программе";
            оПрограммеToolStripMenuItem1.Click += оПрограммеToolStripMenuItem1_Click;
            // 
            // постановкаЗадачиToolStripMenuItem
            // 
            постановкаЗадачиToolStripMenuItem.Name = "постановкаЗадачиToolStripMenuItem";
            постановкаЗадачиToolStripMenuItem.Size = new Size(180, 22);
            постановкаЗадачиToolStripMenuItem.Text = "Постановка задачи";
            постановкаЗадачиToolStripMenuItem.Click += постановкаЗадачиToolStripMenuItem_Click_1;
            // 
            // вызовСправкиToolStripMenuItem
            // 
            вызовСправкиToolStripMenuItem.Name = "вызовСправкиToolStripMenuItem";
            вызовСправкиToolStripMenuItem.Size = new Size(32, 19);
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(32, 19);
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(32, 32);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnSave, toolStripSeparator1, btnUndo, btnRedo, btnCopy, btnCut, btnPaste, toolStripSeparator2, btnRun, btnHelp, btnAbout });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(900, 39);
            toolStrip1.TabIndex = 1;
            // 
            // btnNew
            // 
            btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNew.Image = (Image)resources.GetObject("btnNew.Image");
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(36, 36);
            btnNew.Text = "Создать";
            btnNew.Click += создатьToolStripMenuItem_Click;
            // 
            // btnOpen
            // 
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpen.Image = (Image)resources.GetObject("btnOpen.Image");
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(36, 36);
            btnOpen.Text = "Открыть";
            btnOpen.Click += открытьToolStripMenuItem_Click;
            // 
            // btnSave
            // 
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Image = (Image)resources.GetObject("btnSave.Image");
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(36, 36);
            btnSave.Text = "Сохранить";
            btnSave.Click += сохранитьToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 39);
            // 
            // btnUndo
            // 
            btnUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnUndo.Image = (Image)resources.GetObject("btnUndo.Image");
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(36, 36);
            btnUndo.Click += отменитьToolStripMenuItem_Click;
            // 
            // btnRedo
            // 
            btnRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRedo.Image = (Image)resources.GetObject("btnRedo.Image");
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(36, 36);
            btnRedo.Click += вернутьToolStripMenuItem_Click;
            // 
            // btnCopy
            // 
            btnCopy.Image = (Image)resources.GetObject("btnCopy.Image");
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(36, 36);
            btnCopy.Click += btnCopy_Click;
            // 
            // btnCut
            // 
            btnCut.Image = (Image)resources.GetObject("btnCut.Image");
            btnCut.Name = "btnCut";
            btnCut.Size = new Size(36, 36);
            btnCut.Click += btnCut_Click;
            // 
            // btnPaste
            // 
            btnPaste.Image = (Image)resources.GetObject("btnPaste.Image");
            btnPaste.Name = "btnPaste";
            btnPaste.Size = new Size(36, 36);
            btnPaste.Click += btnPaste_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 39);
            // 
            // btnRun
            // 
            btnRun.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRun.Image = (Image)resources.GetObject("btnRun.Image");
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(36, 36);
            btnRun.Click += пускToolStripMenuItem_Click;
            // 
            // btnHelp
            // 
            btnHelp.Alignment = ToolStripItemAlignment.Right;
            btnHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnHelp.Image = (Image)resources.GetObject("btnHelp.Image");
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(36, 36);
            btnHelp.Click += вызовСправкиToolStripMenuItem_Click;
            // 
            // btnAbout
            // 
            btnAbout.Alignment = ToolStripItemAlignment.Right;
            btnAbout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAbout.Image = (Image)resources.GetObject("btnAbout.Image");
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(36, 36);
            btnAbout.Click += оПрограммеToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatusPath });
            statusStrip1.Location = new Point(0, 438);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(900, 22);
            statusStrip1.TabIndex = 3;
            // 
            // lblStatusPath
            // 
            lblStatusPath.Name = "lblStatusPath";
            lblStatusPath.Size = new Size(111, 17);
            lblStatusPath.Text = "Путь не определен";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 63);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(richTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(900, 375);
            splitContainer1.SplitterDistance = 200;
            splitContainer1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(900, 200);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(900, 171);
            tabControl1.TabIndex = 0;
            tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(892, 143);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Тетрады";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colSubstring, colPosition, Column1, colLength });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(886, 137);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // colSubstring
            // 
            colSubstring.HeaderText = "op";
            colSubstring.Name = "colSubstring";
            colSubstring.ReadOnly = true;
            // 
            // colPosition
            // 
            colPosition.HeaderText = "arg1";
            colPosition.Name = "colPosition";
            colPosition.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "arg2";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // colLength
            // 
            colLength.HeaderText = "res";
            colLength.Name = "colLength";
            colLength.ReadOnly = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(richTextBoxPoliz);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(892, 143);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "ПОЛИЗ";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBoxPoliz
            // 
            richTextBoxPoliz.Dock = DockStyle.Fill;
            richTextBoxPoliz.Location = new Point(3, 3);
            richTextBoxPoliz.Multiline = true;
            richTextBoxPoliz.Name = "richTextBoxPoliz";
            richTextBoxPoliz.ReadOnly = true;
            richTextBoxPoliz.Size = new Size(886, 137);
            richTextBoxPoliz.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 460);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Compiler";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem правкаToolStripMenuItem;
        private ToolStripMenuItem отменитьToolStripMenuItem;
        private ToolStripMenuItem вернутьToolStripMenuItem;
        private ToolStripMenuItem вырезатьToolStripMenuItem;
        private ToolStripMenuItem копироватьToolStripMenuItem;
        private ToolStripMenuItem вставитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem выделитьВсToolStripMenuItem;
        private ToolStripMenuItem текстToolStripMenuItem;
        private ToolStripMenuItem постановкаЗадачиToolStripMenuItem;
        private ToolStripMenuItem пускToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem вызовСправкиToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnUndo;
        private ToolStripButton btnRedo;
        private ToolStripButton btnCopy;
        private ToolStripButton btnCut;
        private ToolStripButton btnPaste;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnRun;
        private ToolStripButton btnHelp;
        private ToolStripButton btnAbout;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatusPath;
        private SplitContainer splitContainer1;
        private RichTextBox richTextBox1;
        private DataGridView dataGridView1;
        private ToolStripMenuItem вызовСправкиToolStripMenuItem1;
        private ToolStripMenuItem оПрограммеToolStripMenuItem1;
        private ToolStripMenuItem постановкаЗадачиToolStripMenuItem1;
        private ToolStripMenuItem грамматикаToolStripMenuItem;
        private ToolStripMenuItem классификацияГрамматикиToolStripMenuItem;
        private ToolStripMenuItem методАнализаToolStripMenuItem;
        private ToolStripMenuItem тестовыйПримерToolStripMenuItem;
        private ToolStripMenuItem списокЛитературыToolStripMenuItem;
        private ToolStripMenuItem исходныйКодПрограммыToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox richTextBoxPoliz;
        private DataGridViewTextBoxColumn colSubstring;
        private DataGridViewTextBoxColumn colPosition;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn colLength;
    }
}