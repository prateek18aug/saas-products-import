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
            var filePath = inputParser.GetFilePath(inputString);

            StartProcessing(container, companyName, filePath);
        }

        private static void StartProcessing(
            IContainer container,
            string companyName,
            string filePath)
        {
            var productSource = container.ResolveKeyed<IProductParser>(companyName);
            var dataAccessFactory = container.Resolve<IDataAccessFactory>();
            var productRoot = productSource.DeserializeFileContent(filePath);
            var dataAccess = dataAccessFactory.Create();

            productSource.PrintProductDetails(productRoot);
            dataAccess.Insert(productRoot);
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
            return builder.Build();
        }
    }
}
