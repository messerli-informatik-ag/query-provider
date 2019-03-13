using System.Linq.Expressions;

namespace Update.Client.ServerCommunication.QueryProvider
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