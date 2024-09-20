using CryptoApp.MVVM.Model;
using CryptoApp.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace CryptoApp.Services.Realization
{
    public class CryptoСurrenciesCollection : ICryptoСurrenciesCollection
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private List<CryptoCurrency>? _cryptoCurrencies;
        private CryptoCurrency? _selectedCryptoCurrency;
        public async Task<List<CryptoCurrency>> GetCryptoCurrencies()
        {
           
                if (_cryptoCurrencies == null)
                {
                    await LoadСurrencyAsync();
                }

                return _cryptoCurrencies!;
        }

        public CryptoCurrency SelectedCryptoCurrency
        {
            get
            {
                if (_selectedCryptoCurrency == null)
                {
                    _selectedCryptoCurrency = _cryptoCurrencies![0];
                }

                return _selectedCryptoCurrency;
            }

            private set
            {
                _selectedCryptoCurrency = value;
            }
        }

        public CryptoСurrenciesCollection(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void SetSelectedCryptoCurrencyById(string id)
        {
            _selectedCryptoCurrency = _cryptoCurrencies!.FirstOrDefault(c => c.id == id);
        }

        public async Task LoadСurrencyAsync()
        {
            var client = _httpClientFactory.CreateClient("CryptoСurrenciesList");
            var response = await client.GetAsync(String.Empty);
            response.EnsureSuccessStatusCode();

            var rootObj = await response.Content.ReadFromJsonAsync<CryptoСurrenciesList>();
            _cryptoCurrencies = rootObj!.data;

        }
    }
}
