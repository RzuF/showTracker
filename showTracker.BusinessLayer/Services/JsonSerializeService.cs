using Newtonsoft.Json;
using showTracker.BusinessLayer.Interfaces;

namespace showTracker.BusinessLayer.Services
{
    public class JsonSerializeService : IJsonSerializeService
    {
        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public (bool success, T obj) TryDeserializeObject<T>(string json)
        {
            var obj = DeserializeObject<T>(json);
            return obj != null ? (true, obj) : (false, default(T));
        }
    }
}
