using CryptoApp.Core;
using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace CryptoApp.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private INavigationService? _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService!;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToHomeViewComand { get; set; }
        public RelayCommand NavigateToInfoViewComand { get; set; }

        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();

        public MainViewModel(INavigationService navigationService, IHttpClientFactory httpClientFactory)
        {
            NavigationService = navigationService;
            NavigateToHomeViewComand = new RelayCommand(o => { NavigationService.NavigateTo<HomeViewModel>(); }, o => true);
            NavigateToInfoViewComand = new RelayCommand(o => { NavigationService.NavigateTo<InfoViewModel>(); }, o => true);
            _httpClientFactory = httpClientFactory;//

            _ = LoadСurrencyAsync();
        }



        public async Task LoadСurrencyAsync()
        {
            var client = _httpClientFactory.CreateClient("CryptoСurrenciesList");
            var response = await client.GetAsync(String.Empty);
            if (response.IsSuccessStatusCode)
            {
                var rootObj = await response.Content.ReadFromJsonAsync<CryptoСurrenciesList>();
                var currencies = rootObj!.data;

                Сurrencies.Clear();
                foreach (var currency in currencies!)
                {
                    Сurrencies.Add(currency);
                }
            }
        }
    }
}
