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
        private string _textToShow= string.Empty;
        private ICryptoСurrenciesCollection _collection;

        public string TextToShow
        {
            get => _textToShow;
            set
            {
                _textToShow = value;
                OnPropertyChanged(nameof(TextToShow));
            }
        }

        public InfoViewModel(ICryptoСurrenciesCollection collection)
        {
            _collection = collection;
        }
        private async Task LoadInfo()
        {
            TextToShow = (await _collection.GetSelectedCryptoCurrency()).symbol?? string.Empty;
        }
        public override void LoadView()
        {
            _ = LoadInfo();
            base.LoadView();
        }
    }
}
