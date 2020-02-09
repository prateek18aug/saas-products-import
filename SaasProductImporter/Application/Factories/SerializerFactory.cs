namespace SaasProductImporter.Application.Factories
{
    using Autofac;
    using SaasProductImporter.Application.Serializers;

    public class SerializerFactory : ISerializerFactory
    {
        private readonly IComponentContext componentContext;

        public SerializerFactory(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public ISerializer Create(string key)
        {
            return componentContext.ResolveKeyed<ISerializer>(key);
        }
    }
}
