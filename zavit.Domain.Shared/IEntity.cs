namespace zavit.Domain.Shared
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}