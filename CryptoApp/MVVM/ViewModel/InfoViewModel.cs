using CryptoApp.Core;
using CryptoApp.Localization;
using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.MVVM.ViewModel
{
    public class InfoViewModel : Core.ViewModel
    {
        
        private ICryptoСurrenciesCollection _collection;
        
        private CryptoCurrency? _currency;
        public CryptoCurrency Currency 
        {
            get => _currency!;
            set
            {
                _currency = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand UrlCommand { get; set; }

        private string _avarage = string.Empty;
        public string Avarage
        {
            get => _avarage;
            set
            {
                _avarage = value;
                OnPropertyChanged();
            }
        }

        private string _cap = string.Empty;
        public string Cap
        {
            get => _cap;
            set
            {
                _cap = value;
                OnPropertyChanged();
            }
        }

        private string _last24hours = string.Empty;
        public string Last24hours
        {
            get => _last24hours;
            set
            {
                _last24hours = value;
                OnPropertyChanged();
            }
        }

        private string _market = string.Empty;
        public string Market
        {
            get => _market;
            set
            {
                _market = value;
                OnPropertyChanged();
            }
        }

        private string _more = string.Empty;
        public string More
        {
            get => _more;
            set
            {
                _more = value;
                OnPropertyChanged();
            }
        }

        private string _price = string.Empty;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string _supply = string.Empty;
        public string Supply
        {
            get => _supply;
            set
            {
                _supply = value;
                OnPropertyChanged();
            }
        }

        private string _volume = string.Empty;
        public string Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnPropertyChanged();
            }
        }


        public InfoViewModel(ICryptoСurrenciesCollection collection)
        {
            _collection = collection;
            UrlCommand = new RelayCommand(o =>  System.Diagnostics.Process.Start(new ProcessStartInfo(Currency.explorer!) { UseShellExecute = true }), o => true);
        }
        private async Task LoadInfo()
        {
            Currency = await _collection.GetSelectedCryptoCurrency();
        }
        public override void LoadView()
        {
            _ = LoadInfo();
            base.LoadView();

            Avarage = Resource.AVERAGE;
            Cap = Resource.CAP+":";
            Last24hours = Resource.LAST24HOURS;
            Market = Resource.MARKET;
            More = Currency.explorer is not null? Resource.more+"...": string.Empty;
            Price = Resource.PRICE+":";
            Supply = Resource.SUPPLY +":";
            Volume = Resource.VOLUME + ":";



        }
    }
}
