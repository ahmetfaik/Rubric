namespace Rubric.Consumer.Clients.Statement;

public interface IInternallyTriggeredClient : IClient
{
    public Task<bool> CheckExistStatementData(DateTime checkDate);
    public IAsyncEnumerable<Stream> GetStatementData(DateTime checkDate);
    public Task<bool> CheckStructureStatementData(DateTime checkDate);
}