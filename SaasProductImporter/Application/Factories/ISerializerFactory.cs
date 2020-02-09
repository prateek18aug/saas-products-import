namespace SaasProductImporter.Application.Factories
{
    using SaasProductImporter.Services.Serializers;

    public interface ISerializerFactory
    {
        ISerializer Create(string key);
    }
}
