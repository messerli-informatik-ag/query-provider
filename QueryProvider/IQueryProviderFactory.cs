using Messerli.ServerCommunication;

namespace Messerli.QueryProvider
{
    public interface IQueryProviderFactory
    {
        QueryProvider Create();

        QueryProvider Create(ObjectToResolve objectToResolve);
    }
}