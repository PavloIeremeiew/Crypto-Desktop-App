using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public InfoViewModel(ICryptoСurrenciesCollection collection)
        {
            _collection = collection;
        }
        private async Task LoadInfo()
        {
            Currency = await _collection.GetSelectedCryptoCurrency();
        }
        public override void LoadView()
        {
            _ = LoadInfo();
            base.LoadView();
        }
    }
}
