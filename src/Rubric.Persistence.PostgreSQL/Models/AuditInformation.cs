namespace Rubric.Persistence.PostgreSQL.Models;

public record AuditInformation
{
    /// <summary>
    ///     CreatedDate
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    ///     LastModifiedDate
    /// </summary>
    public DateTime? LastModifiedDate { get; set; }
}