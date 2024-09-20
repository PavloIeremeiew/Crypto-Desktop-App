using CryptoApp.Core;
using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System.Collections.ObjectModel;

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

        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();

        public MainViewModel(INavigationService navigationService, ICryptoСurrenciesCollection collection)
        {
            NavigationService = navigationService;
            _collection = collection;
            NavigateToHomeViewComand = new RelayCommand(o => { NavigationService.NavigateTo<HomeViewModel>(); }, o => true);
            NavigateToInfoViewComand = new RelayCommand(async o =>
            {
                await _collection.SetSelectedCryptoCurrencyById((string)o);
                NavigationService.NavigateTo<InfoViewModel>();
            }, o => true);

            
            _ = LoadList();
            NavigationService.NavigateTo<HomeViewModel>();
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
