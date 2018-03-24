namespace showTracker.BusinessLayer.Interfaces
{
    public interface IJsonSerializeService
    {
        string SerializeObject<T>(T obj);
        T DeserializeObject<T>(string json);
        (bool success, T obj) TryDeserializeObject<T>(string json);
    }
}
