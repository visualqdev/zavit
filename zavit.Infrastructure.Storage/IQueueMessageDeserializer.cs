namespace zavit.Infrastructure.Storage
{
    public interface IQueueMessageDeserializer<out T> where T : class
    {
        T Deserialize(byte[] message);
    }
}