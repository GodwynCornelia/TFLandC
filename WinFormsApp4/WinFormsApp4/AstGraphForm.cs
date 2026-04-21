using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class AstGraphForm : Form
    {
        private List<ConstDeclStr> _nodes;

        private const int nodeWidth = 130;
        private const int nodeHeight = 40;

        public AstGraphForm(List<ConstDeclStr> nodes)
        {
            this.Text = "Визуализация AST Дерева";
            this.Size = new Size(1200, 800);
            this.AutoScroll = true;
            this._nodes = nodes;
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int currentRootX = 450;
            int startY = 50;

            foreach (var node in _nodes)
            {
                DrawFullTree(g, node, currentRootX, startY);
                currentRootX += 600;
            }
        }

        private void DrawFullTree(Graphics g, ConstDeclStr root, int x, int y)
        {
            int levelHeight = 90;
            int branchWidth = 150;

            int x1 = x - (int)(branchWidth * 1.5);
            int x2 = x - (int)(branchWidth * 0.5);
            int x3 = x + (int)(branchWidth * 0.5);
            int x4 = x + (int)(branchWidth * 1.5);

            int yLevel1 = y + levelHeight;
            int yLevel2 = yLevel1 + levelHeight;

            DrawNode(g, x, y, "ConstDeclStr", "");

            int rootBottomX = x + nodeWidth / 2;
            int rootBottomY = y + nodeHeight;

            DrawEdge(g, rootBottomX, rootBottomY, x1 + nodeWidth / 2, yLevel1);
            DrawNode(g, x1, yLevel1, "name:", $"\"{root.Name}\"");

            DrawEdge(g, rootBottomX, rootBottomY, x2 + nodeWidth / 2, yLevel1);
            DrawNode(g, x2, yLevel1, "modifiers:", "\"const\"");

            DrawEdge(g, rootBottomX, rootBottomY, x3 + nodeWidth / 2, yLevel1);
            DrawNode(g, x3, yLevel1, "type: StrType", "");

            DrawEdge(g, rootBottomX, rootBottomY, x4 + nodeWidth / 2, yLevel1);
            DrawNode(g, x4, yLevel1, "str: BodyString", "");

            DrawEdge(g, x3 + nodeWidth / 2, yLevel1 + nodeHeight, x3 + nodeWidth / 2, yLevel2);
            DrawNode(g, x3, yLevel2, "name:", "\"&str\"");

            if (root.Cases != null && root.Cases.Count > 0)
            {
                DrawEdge(g, x4 + nodeWidth / 2, yLevel1 + nodeHeight, x4 + nodeWidth / 2, yLevel2);
                DrawNode(g, x4, yLevel2, "str:", $"\"{root.Cases[0].Name}\"");
            }
        }

        private void DrawNode(Graphics g, int x, int y, string title, string value)
        {
            Rectangle rect = new Rectangle(x, y, nodeWidth, nodeHeight);

            g.DrawRectangle(Pens.Black, rect);
            Font font = new Font("Consolas", 9, FontStyle.Regular);
            string content = string.IsNullOrEmpty(value) ? title : $"{title} {value}";

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(content, font, Brushes.Black, rect, sf);
        }

        private void DrawEdge(Graphics g, int x1, int y1, int x2, int y2)
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                AdjustableArrowCap arrow = new AdjustableArrowCap(4, 4);
                pen.CustomEndCap = arrow;

                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        private void AstGraphForm_Load(object sender, EventArgs e)
        {

        }
    }
}