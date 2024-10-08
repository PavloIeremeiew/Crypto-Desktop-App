﻿using CryptoApp.Core;
using CryptoApp.FixedData.Const;
using CryptoApp.FixedData.Enum;
using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Globalization;
using CryptoApp.Localization;
using System.Windows;

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

        private Language _selectedLanguage = Language.English;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                switch (_selectedLanguage)
                {

                    case Language.Ukrainian:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk-UA");
                        break;
                    case Language.English:
                    default:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
                        break;
                     
                }
                OnPropertyChanged();
                LoadView();
                NavigationService.CurrentView.LoadView();
            }
        }

        private string _title = string.Empty;
        public string Title 
        {  
            get => _title;
            set {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _search = string.Empty;
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
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
        public RelayCommand SwitchThemeCommand { get; set; }

        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();
        public ObservableCollection<Language> Languages { get; set; } = new() { Language.English, Language.Ukrainian };
        private List<CryptoCurrency> _currenciesList { get; set; } = new();
        private bool _isThemeLight = false;

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

            SwitchThemeCommand = new RelayCommand(o => SwitchTheme(), o => true);


            _ = LoadList();
            NavigationService.NavigateTo<HomeViewModel>();
            LoadView();
        }
        public override void LoadView()
        {
            base.LoadView();
            Title =  Resource.Title;
            Search = Resource.Search;
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

        private void SwitchTheme()
        {
            _isThemeLight = !_isThemeLight;
            string newThemeName, oldThemeName;
            if (_isThemeLight)
            {
                oldThemeName = MainConstants.DarkThemePath;
                newThemeName = MainConstants.LightThemePath;
            }
            else
            {
                newThemeName = MainConstants.DarkThemePath;
                oldThemeName = MainConstants.LightThemePath;
            }
            ResourceDictionary newTheme = (ResourceDictionary)Application.LoadComponent(new Uri(newThemeName, UriKind.Relative));
            ResourceDictionary oldTheme = (ResourceDictionary)Application.LoadComponent(new Uri(oldThemeName, UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.Remove(oldTheme);
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}
