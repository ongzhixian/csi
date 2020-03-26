using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Csi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private class ServiceDescription
        {
            /*
              Current choice
              {
                "ServiceType": "Csi.Extensions.Tests.Data.IAnimal, Csi.Extensions.Tests",
                "ImplementationType": "Csi.Extensions.Tests.Data.Mouse, Csi.Extensions.Tests",
                "Lifetime": "Transient"
              }
              
            vs

              {
                "ServiceType": {
                  "Name": "Csi.Extensions.Tests.Data.IAnimal",
                  "Assembly": "Csi.Extensions.Tests"
                },
                "ImplementationType": {
                  "Name": "Csi.Extensions.Tests.Data.Mouse",
                  "Assembly": "Csi.Extensions.Tests"
                },
                "Lifetime": "Transient"
              }

            */
            public string AssemblyName { get; set; }
            public string ServiceType { get; set; }
            public string ImplementationType { get; set; }
            public string Lifetime { get; set; }
        }

        // Hmm... :-(
        //private class LogEvent
        //{
        //    public static readonly EventId Remark = new EventId(100, "Remark");
        //    public static readonly EventId Start = new EventId(101, "Start");
        //    public static readonly EventId End = new EventId(102, "End:)");
        //    public static readonly EventId Invalid = new EventId(400, "Invalid");
        //}

        private class DependencyInjectionConfigurationEvent
        {

            public static readonly EventId BEGIN_CONFIGURATION = new EventId(100, "Dependency injection configuration started");
            public static readonly EventId END_CONFIGURATION = new EventId(101, "Dependency injection configuration completed");

            public static readonly EventId BEGIN_CREATE_ASSEMBLY_DICTIONARY = new EventId(102, "Assembly dictionary creation started");
            public static readonly EventId END_CREATE_ASSEMBLY_DICTIONARY = new EventId(103, "Assembly dictionary creation completed");


            public static readonly EventId SKIP_ASSEMBLY = new EventId(104, "Skip add to assembly dictionary");
            public static readonly EventId ADD_ASSEMBLY = new EventId(105, "Add to assembly dictionary");


            public static readonly EventId BEGIN_ADD_SERVICE_DESCRIPTIORS = new EventId(106, "Service descriptors registration started");
            public static readonly EventId END_ADD_SERVICE_DESCRIPTIORS = new EventId(107, "Service descriptors registration completed");


            public static readonly EventId RESOLVING_SERVICE_DESCRIPTION = new EventId(107, "Resolving service description");
            public static readonly EventId RESOLVED_SERVICE_DESCRIPTION = new EventId(107, "Resolved service description");

            public static readonly EventId INVALID_SERVICE_LIFETIME = new EventId(107, "Invalid service lifetime");
            public static readonly EventId INVALID_SERVICE_TYPE = new EventId(107, "Invalid service type");
            public static readonly EventId INVALID_IMPLEMENTATION_TYPE = new EventId(107, "Invalid implementation type");

            //public static readonly EventId Start = new EventId(101, "Start");
            //public static readonly EventId End = new EventId(102, "End:)");

            //public static readonly EventId Invalid = new EventId(400, "Invalid");
        }

        const string ConfigurationSection = "ServiceDescriptions";

        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDependencyInjection(services, configuration, null, "Test:Services");
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration, ILogger log)
        {
            ConfigureDependencyInjection(services, configuration, log, "Test:Services");
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration, ILogger log, string configSection)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (log == null)
            {
                using (ServiceProvider sp = services.BuildServiceProvider())
                using (ILoggerFactory loggerFactory = sp.GetRequiredService<ILoggerFactory>())
                {
                    log = loggerFactory.CreateLogger("IServiceCollectionExtensions");
                }
            }

            log.Log(LogLevel.Information, DependencyInjectionConfigurationEvent.BEGIN_CONFIGURATION, string.Empty);

            Dictionary<string, Assembly> assemblyDictionary = null;
            CreateAssemblyDictionary(out assemblyDictionary, log);

            // Setup DI
            log.Log(LogLevel.Trace, DependencyInjectionConfigurationEvent.BEGIN_ADD_SERVICE_DESCRIPTIORS, string.Empty);
            IEnumerable<ServiceDescription> serviceDescriptorList = configuration.GetSection(configSection).Get<IEnumerable<ServiceDescription>>();
            foreach (ServiceDescription desc in serviceDescriptorList)
            {
                log.Log(LogLevel.Information, DependencyInjectionConfigurationEvent.RESOLVING_SERVICE_DESCRIPTION, "Resolving {0} {1} {2}.", desc.Lifetime, desc.ServiceType, desc.ImplementationType);

                ServiceLifetime serviceLifetime;

                if (!Enum.TryParse<ServiceLifetime>(desc.Lifetime, out serviceLifetime))
                {
                    log.Log(LogLevel.Error, DependencyInjectionConfigurationEvent.INVALID_SERVICE_LIFETIME, "Invalid service lifetime value {0}.", desc.Lifetime);
                    continue;
                }

                string serviceTypeAssemblyName = string.Empty;
                string implementationTypeAssemblyName = string.Empty;

                string[] parsedServiceType = desc.ServiceType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] parsedImplementationType = desc.ImplementationType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (parsedServiceType.Length < 2)
                {
                    serviceTypeAssemblyName = parsedServiceType[0].Substring(0, parsedServiceType[0].LastIndexOf('.')).Trim();
                }
                else
                {
                    serviceTypeAssemblyName = parsedServiceType[1].Trim();
                }

                if (parsedImplementationType.Length < 2)
                {
                    implementationTypeAssemblyName = parsedImplementationType[0].Substring(0, parsedImplementationType[0].LastIndexOf('.')).Trim();
                }
                else
                {
                    implementationTypeAssemblyName = parsedImplementationType[1].Trim();
                }

                string serviceTypeName = parsedServiceType[0];
                string implementationTypeName = parsedImplementationType[0];

                Type serviceType = assemblyDictionary[serviceTypeAssemblyName]?.ExportedTypes.FirstOrDefault(r => r.FullName == serviceTypeName);
                if (serviceType == null)
                {
                    log.Log(LogLevel.Error, DependencyInjectionConfigurationEvent.INVALID_SERVICE_TYPE, "Invalid service serviceType value {0}.", serviceTypeName);
                    continue;
                }

                Type implementationType = assemblyDictionary[implementationTypeAssemblyName]?.ExportedTypes.FirstOrDefault(r => r.FullName == implementationTypeName);
                if (implementationType == null)
                {
                    log.Log(LogLevel.Error, DependencyInjectionConfigurationEvent.INVALID_IMPLEMENTATION_TYPE, "Invalid service implementationType value {0}.", implementationTypeName);
                    continue;
                }

                ServiceDescriptor sd = new ServiceDescriptor(
                    serviceType,
                    implementationType,
                    serviceLifetime);
                services.Add(sd);

                log.Log(LogLevel.Information, DependencyInjectionConfigurationEvent.RESOLVED_SERVICE_DESCRIPTION, "{0} as {1} ({2})", serviceTypeName, implementationTypeName, desc.Lifetime);
                
            }
            log.Log(LogLevel.Information, DependencyInjectionConfigurationEvent.END_ADD_SERVICE_DESCRIPTIORS, string.Empty);
        }

        private static Dictionary<string, Assembly> CreateAssemblyDictionary(out Dictionary<string, Assembly> assemblyDictionary, ILogger log)
        {
            log.Log(LogLevel.Trace, DependencyInjectionConfigurationEvent.BEGIN_CREATE_ASSEMBLY_DICTIONARY, "Assembly dictionary -- creating.");

            assemblyDictionary = new Dictionary<string, Assembly>();
            IEnumerable<AssemblyName> assemblyNames = null;

            assemblyNames = AppDomain.CurrentDomain.GetAssemblies().Select(r => r.GetName());
            AddAssemblyDictionary(assemblyDictionary, assemblyNames, log);

            // ZX: Probably don't need this
            assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            AddAssemblyDictionary(assemblyDictionary, Assembly.GetExecutingAssembly().GetReferencedAssemblies(), log);

            log.Log(LogLevel.Trace, DependencyInjectionConfigurationEvent.END_CREATE_ASSEMBLY_DICTIONARY, "Assembly dictionary -- created.");

            return assemblyDictionary;
        }

        private static void AddAssemblyDictionary(Dictionary<string, Assembly> assemblyDictionary, IEnumerable<AssemblyName> assemblyNames, ILogger log)
        {
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                if (assemblyDictionary.ContainsKey(assemblyName.Name))
                {
                    log.Log(LogLevel.Trace, DependencyInjectionConfigurationEvent.SKIP_ASSEMBLY, assemblyName.Name);
                    continue;
                }

                if (assemblyName.Name == "Anonymously Hosted DynamicMethods Assembly")
                    continue;

                // Else try to load assembly
                try
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    if (assembly != null)
                    {
                        log.Log(LogLevel.Trace, DependencyInjectionConfigurationEvent.ADD_ASSEMBLY, assemblyName.Name);
                        assemblyDictionary.Add(assemblyName.Name, assembly);
                        AddAssemblyDictionary(assemblyDictionary, assembly.GetReferencedAssemblies(), log);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    log.Log(LogLevel.Warning, DependencyInjectionConfigurationEvent.SKIP_ASSEMBLY, "Cannot load {0} - File not found {1}", assemblyName.Name, ex);
                }
                catch (Exception ex)
                {
                    log.Log(LogLevel.Warning, DependencyInjectionConfigurationEvent.SKIP_ASSEMBLY, "Cannot load {0} - Exception {1}", assemblyName.Name, ex);
                }

            }
        }
    }
}
