using System.Linq.Expressions;

namespace Messerli.QueryProvider
{
    public class TranslateResult
    {
        public TranslateResult(string commandText, LambdaExpression projector)
        {
            CommandText = commandText;
            Projector = projector;
        }

        public string CommandText { get; }

        public LambdaExpression Projector { get; }

    }
}
