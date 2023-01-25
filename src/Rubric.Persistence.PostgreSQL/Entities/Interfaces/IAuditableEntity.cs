using Rubric.Persistence.PostgreSQL.Models;

namespace Rubric.Persistence.PostgreSQL.Entities.Interfaces;

public interface IAuditableEntity<T> : IEntity<T> where T : struct
{
    public AuditInformation AuditInformation { get; set; }
}