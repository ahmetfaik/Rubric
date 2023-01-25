namespace Rubric.Persistence.PostgreSQL.Entities.Interfaces;

public interface IEntity<T> where T : struct
{
    public T Id { get; set; }
}