using CryptoApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Services.Interfaces
{
    public interface ICryptoСurrenciesCollection
    {
        public  Task<List<CryptoCurrency>> GetCryptoCurrencies();
        public CryptoCurrency SelectedCryptoCurrency { get; }
        public void SetSelectedCryptoCurrencyById(string Id);
    }
}
