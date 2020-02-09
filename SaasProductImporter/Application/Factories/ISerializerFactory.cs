namespace SaasProductImporter.Application.Factories
{
    using SaasProductImporter.Application.Serializers;

    public interface ISerializerFactory
    {
        ISerializer Create(string key);
    }
}
