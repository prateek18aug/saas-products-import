namespace SaasProductImporter.Application.Serializers
{
    public interface ISerializer
    {
        T Deserialize<T>(string fileContent);
    }
}
