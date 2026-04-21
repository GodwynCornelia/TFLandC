using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp4
{
    public abstract class AstNode
    {
        public string Name { get; set; }
        public int Line { get; set; }
        public int Position { get; set; }
    }

    public class EnumDeclNode : AstNode
    {
        public List<EnumCaseNode> Cases { get; set; } = new List<EnumCaseNode>();
    }

    public class EnumCaseNode : AstNode { }

    public class AstVisualizer
    {
        public string GenerateTreeText(List<EnumDeclNode> nodes)
        {
            if (nodes == null || nodes.Count == 0)
                return "AST дерево пусто.";

            StringBuilder sb = new StringBuilder();
            foreach (var node in nodes)
            {
                sb.AppendLine($"ConstDeclStr");
                sb.AppendLine($"├── name: \"{node.Name}\"");
                sb.AppendLine($"├── modifiers: \"const\"");
                sb.AppendLine($"├── type: StrType");
                sb.AppendLine($"│   └── name: \"&str\"");

                string value = node.Cases.Count > 0 ? node.Cases[0].Name : "null";
                sb.AppendLine($"└── str: BodyString");
                sb.AppendLine($"    └── str: \"{value}\"");
            }
            return sb.ToString();
        }
    }
}