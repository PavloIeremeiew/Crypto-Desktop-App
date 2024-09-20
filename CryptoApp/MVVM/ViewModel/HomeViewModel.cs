using CryptoApp.FixedData.Const;
using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System.Collections.ObjectModel;

namespace CryptoApp.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        private readonly ICryptoСurrenciesCollection _collection;
        public ObservableCollection<CryptoCurrency> Сurrencies { get; set; } = new();
        public HomeViewModel(ICryptoСurrenciesCollection collection)
        {
            _collection = collection;
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
