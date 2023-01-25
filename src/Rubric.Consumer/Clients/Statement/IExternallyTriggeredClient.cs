namespace Rubric.Consumer.Clients.Statement;

public interface IExternallyTriggeredClient : IClient
{
    public IAsyncEnumerable<Stream> GetStatementData(string url);
    public Task<bool> CheckStructureStatementData(string url);
}