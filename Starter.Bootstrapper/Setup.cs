using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Starter.Data.Services;
using Starter.Data.Consumers;
using Starter.Data.ViewModels;
using Starter.Data.Repositories;

using Starter.Framework.Clients;
using Starter.Framework.Entities;
using Starter.Framework.Loggers;

using Starter.Repository.Repositories;

namespace Starter.Bootstrapper
{
    /// <summary>
    /// Sets up the dependency resolution for the project
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Provides means to registry different service implementations
        /// based on the setup type
        /// </summary>
        public static IServiceCollection Bootstrap(IServiceCollection services, SetupType setupType = SetupType.Debug)
        {
#if DEBUG
            Setup.Bootstrap(services);
#else
            Setup.Bootstrap(services, SetupType.Release);
#endif

            switch (setupType)
            {
                case SetupType.Release:
                    services.AddSingleton<ISettings, Settings>();

                    break;
                case SetupType.Debug:
                    services.AddSingleton<ISettings, SettingsDev>();

                    break;
                case SetupType.Test:
                    services.AddSingleton<ISettings, SettingsTest>();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(setupType), setupType, null);
            }

            services.AddTransient<IApiClient, ApiClient>();
            services.AddTransient<ILogger, ApplicationInsightsLogger>();

            services.AddTransient<ICatRepository, CatRepository>();
            //container.AddTransient<IMessageBroker<Cat>, AzureMessageBroker<Cat>>();
            services.AddTransient<IMessageBrokerConsumer, MessageBrokerConsumer>();
            services.AddTransient<ICatService, CatService>();
            services.AddTransient<IMainViewModel, MainViewModel>();

            var serviceProvider = services.BuildServiceProvider();

            IocWrapper.Instance = new IocWrapper(serviceProvider);

            return services;
        }
    }
}