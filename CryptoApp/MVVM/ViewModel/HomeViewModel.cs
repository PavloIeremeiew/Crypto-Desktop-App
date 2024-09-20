using CryptoApp.Core;
using CryptoApp.FixedData.Const;
using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System.Collections.ObjectModel;

namespace CryptoApp.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        private readonly ICryptoСurrenciesCollection _collection;
        private INavigationService? _navigationService;
        public RelayCommand NavigateToInfoViewComand { get; set; }
        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();

        public INavigationService NavigationService
        {
            get => _navigationService!;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(INavigationService navigationService, ICryptoСurrenciesCollection collection)
        {
            _collection = collection;
            NavigationService = navigationService;
            NavigateToInfoViewComand = new RelayCommand(async o =>
            {
                await _collection.SetSelectedCryptoCurrencyById((string)o);
                NavigationService.NavigateTo<InfoViewModel>();
            }, o => true);
            _ = LoadList();
        }
        private async Task LoadList()
        {
            Сurrencies.Clear();
            var list = await _collection.GetCryptoCurrencies();
            list = list.Where(x => int.Parse(x.rank!) <= MainConstants.HomeListCount)
                .Take(MainConstants.HomeListCount).ToList();
            foreach (var currency in list)
            {
                Сurrencies.Add(currency);
            }
        }
    }
}
