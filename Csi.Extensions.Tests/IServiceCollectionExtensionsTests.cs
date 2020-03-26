using NUnit.Framework;
using Csi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Csi.Loggers;
using System.Linq;
using Csi.Extensions.Tests.Data;

namespace Tests
{
    public class IServiceCollectionExtensionsTests
    {
        IConfigurationRoot config;
        ServiceCollection services;
        ServiceProvider sp;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // 1. Setup config
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }


        [SetUp]
        public void Setup()
        {
            // 2. Setup services
            services = new ServiceCollection();
            services.AddLogging(builder => builder.AddProvider(new TraceLoggerProvider()));
        }


        [Category("ConfigureDependencyInjection")]
        [Test(
            Author = "Ong Zhi Xian",
            Description = "Test getting service when multiple implementation of the same interface are added",
            TestOf = typeof(IServiceCollection))]
        public void MultipleServiceImplementationTest1()
        {
            // Arrange
            services.AddTransient<IAnimal, Dog>();
            services.AddTransient<IAnimal, Cat>();
            services.AddTransient<IAnimal, Mouse>();
            // 3. Make service provider
            sp = services.BuildServiceProvider();

            // Act
            IAnimal animal = sp.GetService<IAnimal>();

            // Assert
            Assert.AreEqual("squeak", animal.MakeSound());
        }


        [Category("ConfigureDependencyInjection")]
        [Test(
            Author = "Ong Zhi Xian",
            Description = "Test getting specific service when multiple implementation of the same interface are added",
            TestOf = typeof(IServiceCollection))]
        public void MultipleServiceImplementationSpecificTest1()
        {
            // Arrange
            services.AddTransient<IAnimal, Dog>();
            services.AddTransient<IAnimal, Cat>();
            services.AddTransient<IAnimal, Mouse>();
            // 3. Make service provider
            sp = services.BuildServiceProvider();

            // Act
            IAnimal animal = sp.GetServices<IAnimal>().FirstOrDefault(r => r.GetType() == typeof(Dog));

            // Assert
            Assert.AreEqual("woof", animal.MakeSound());
        }


        [Category("ConfigureDependencyInjection")]
        [Test(
            Author = "Ong Zhi Xian",
            Description = "Test getting specific service when multiple implementation of the same interface are added",
            TestOf = typeof(IServiceCollection))]
        public void ServiceMissingSpecificImplementationTest1()
        {
            // Arrange
            services.AddTransient<IAnimal, Dog>();
            //services.AddTransient<IAnimal, Cat>();
            services.AddTransient<IAnimal, Mouse>();
            // 3. Make service provider
            sp = services.BuildServiceProvider();

            // Act
            IAnimal animal = sp.GetServices<IAnimal>().FirstOrDefault(r => r.GetType() == typeof(Cat));

            // Assert
            Assert.IsNull(animal);
            //Assert.AreEqual("woof", animal.MakeSound());
        }

    }
}