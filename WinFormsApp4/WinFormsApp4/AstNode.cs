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

    public class ConstDeclStr : AstNode
    {
        public List<ConstDeclStr> Cases { get; set; } = new List<ConstDeclStr>();
    }

    public class EnumCaseNode : AstNode { }

    public class AstVisualizer
    {
        public string GenerateTreeText(List<ConstDeclStr> nodes)
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

                string value = (node.Cases.Count > 0) ? $"\"{node.Cases[0].Name}\"" : "\"\"";
                sb.AppendLine($"└── str: BodyString");
                sb.AppendLine($"    └── str: {value}");
            }
            return sb.ToString();
        }
    }
}