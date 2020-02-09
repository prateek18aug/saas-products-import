namespace SaasProductImporter.Services.Serializers
{
    public interface ISerializer
    {
        T Deserialize<T>(string fileContent);
    }
}
