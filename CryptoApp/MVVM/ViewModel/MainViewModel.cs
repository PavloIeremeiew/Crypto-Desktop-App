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
        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToHomeViewComand { get; set; }
        public RelayCommand NavigateToInfoViewComand { get; set; }
        public RelayCommand FilterListCommand { get; set; }
        public RelayCommand ClearFilterTextCommand { get; set; }

        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();
        private List<CryptoCurrency> _currenciesList { get; set; } = new();

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
            FilterListCommand = new RelayCommand(o => FilterPeople(), o => true);
            ClearFilterTextCommand = new RelayCommand(o =>
            {
                FilterText = string.Empty;
                FilterPeople();
            }, o => true);


            _ = LoadList();
            NavigationService.NavigateTo<HomeViewModel>();
        }

        private async Task LoadList()
        {
            _currenciesList = await _collection.GetCryptoCurrencies();
            PrintList(_currenciesList);
        }
        private void PrintList(List<CryptoCurrency> list)
        {
            Сurrencies.Clear();
            foreach (var currency in list)
            {
                Сurrencies.Add(currency);
            }
        }
        private void FilterPeople()
        {
            List<CryptoCurrency> list;
            if (string.IsNullOrEmpty(FilterText))
            {
                list = _currenciesList;
            }
            else
            {
                list = _currenciesList.Where(p =>
                    p.name!.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    p.symbol!.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            PrintList(list);
        }

    }
}
