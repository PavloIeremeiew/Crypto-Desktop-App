using CryptoApp.Core;
using CryptoApp.MVVM.ViewModel;
using CryptoApp.Services.Interfaces;
using CryptoApp.Services.Realization;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CryptoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
            
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient("CryptoСurrenciesList", client =>
            {
                client.BaseAddress = new Uri("https://api.coincap.io/v2/assets");
            });
            services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<InfoViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(provider => viewType => (ViewModel)provider.GetRequiredService(viewType));
        }
    }

}
