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
        private readonly ICryptoСurrenciesCollection _collection;
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
        public RelayCommand LoadListComand { get; set; }

        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();

        public MainViewModel(INavigationService navigationService, ICryptoСurrenciesCollection collection)
        {
            NavigationService = navigationService;
            NavigateToHomeViewComand = new RelayCommand(o => { NavigationService.NavigateTo<HomeViewModel>(); }, o => true);
            NavigateToInfoViewComand = new RelayCommand(o => { NavigationService.NavigateTo<InfoViewModel>(); }, o => true);

            _collection = collection;
            _=LoadList();
        }

        private async Task LoadList()
        {
            Сurrencies.Clear();
            var list = await _collection.GetCryptoCurrencies();
            foreach (var currency in list)
            {
                Сurrencies.Add(currency);
            }
        }
    }
}
