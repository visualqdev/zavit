namespace zavit.Infrastructure.Core.Serialization
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string value);
        string Serialize<T>(T value);
    }
}