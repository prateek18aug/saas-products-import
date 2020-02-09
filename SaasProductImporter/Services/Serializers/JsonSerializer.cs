namespace SaasProductImporter.Services.Serializers
{
    using Newtonsoft.Json;

    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string fileContent)
        {
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}
