namespace SaasProductImporter
{
    using Autofac;
    using Autofac.Configuration;
    using Microsoft.Extensions.Configuration;
    using SaasProductImporter.Application.Configs;
    using SaasProductImporter.Application.Factories;
    using SaasProductImporter.DataAccess;
    using SaasProductImporter.Services.Parsers.InputParsers;
    using SaasProductImporter.Services.Parsers.ProductParsers;
    using SaasProductImporter.Services.Serializers;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Settings.ConfigPath)
                       .AddJsonFile("AppSettings.json")
                       .Build();

            var container = ConfigureContainer(configuration);

            ///var inputString = "import capterra feed-products/capterra.yaml";
            Console.WriteLine("Enter input:");
            var inputString = Console.ReadLine();

            var inputParser = container.Resolve<IUserInputParser>();
            var companyName = inputParser.GetCompanyName(inputString);
            var fileName = inputParser.GetFileName(inputString);

            StartProcessing(container, companyName, fileName);
        }

        private static void StartProcessing(
            IContainer container,
            string companyName,
            string fileName)
        {
            var productParser = container.ResolveKeyed<IProductParser>(companyName);
            var dataAccessFactory = container.Resolve<IDataAccessFactory>();
            var productsRoot = productParser.DeserializeFileContent(fileName);
            var dataAccess = dataAccessFactory.Create();

            productParser.PrintProductDetails(productsRoot);
            dataAccess.Insert(productsRoot);
        }

        private static IContainer ConfigureContainer(IConfigurationRoot configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationModule(configuration));
            builder.RegisterType<AppSettings>().As<IAppSettings>().WithParameter("configuration", configuration);
            builder.RegisterType<UserInputParser>().As<IUserInputParser>().SingleInstance();
            builder.RegisterType<SoftwareAdviceParser>().Named<IProductParser>("softwareadvice").SingleInstance();
            builder.RegisterType<CapterraParser>().Named<IProductParser>("capterra").SingleInstance();
            builder.RegisterType<MongoDataAccess>().Named<IDataAccess>("mongo").SingleInstance();
            builder.RegisterType<MySqlDataAccess>().Named<IDataAccess>("mysql").SingleInstance();
            builder.RegisterType<DataAccessFactory>().As<IDataAccessFactory>().SingleInstance();
            builder.RegisterType<SerializerFactory>().As<ISerializerFactory>().SingleInstance();
            builder.RegisterType<JsonSerializer>().Named<ISerializer>("json").SingleInstance();
            builder.RegisterType<YamlSerializer>().Named<ISerializer>("yaml").SingleInstance();
            return builder.Build();
        }
    }
}
