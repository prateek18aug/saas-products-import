namespace SaasProductImporter.Services.Serializers
{
    using YamlDotNet.Serialization;

    public class YamlSerializer : ISerializer
    {
        public T Deserialize<T>(string fileContent)
        {
            return new Deserializer().Deserialize<T>(fileContent);
        }
    }
}
