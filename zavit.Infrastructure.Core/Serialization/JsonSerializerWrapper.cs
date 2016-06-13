using Newtonsoft.Json;

namespace zavit.Infrastructure.Core.Serialization
{
    public class JsonSerializerWrapper : IJsonSerializer
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}