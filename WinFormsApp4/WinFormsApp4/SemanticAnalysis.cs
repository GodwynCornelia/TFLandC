using System.Collections.Generic;

namespace WinFormsApp4
{
    public class SemanticError
    {
        public string Message { get; set; }
        public int Line { get; set; }
        public int Position { get; set; }

        public override string ToString()
        {
            return $"Стр: {Line}, Поз: {Position} - {Message}";
        }
    }

    public class SemanticAnalyzer
    {
        private HashSet<string> _declaredConstants = new HashSet<string>();
        public List<SemanticError> Errors { get; private set; } = new List<SemanticError>();

        public void Analyze(List<EnumDeclNode> nodes)
        {
            Errors.Clear();
            _declaredConstants.Clear();

            if (nodes == null) return;

            foreach (var node in nodes)
            {
                if (_declaredConstants.Contains(node.Name))
                {
                    Errors.Add(new SemanticError
                    {
                        Message = $"Ошибка: идентификатор '{node.Name}' уже объявлен.",
                        Line = node.Line,
                        Position = node.Position
                    });
                }
                else
                {
                    _declaredConstants.Add(node.Name);
                }
            }
        }
    }
}