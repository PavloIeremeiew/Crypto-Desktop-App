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

        public async Task<CryptoCurrency> GetSelectedCryptoCurrency()
        {
                if (_selectedCryptoCurrency == null)
                {
                    _selectedCryptoCurrency = (await GetCryptoCurrencies())[0];
                }

                return _selectedCryptoCurrency!;
        }

        public CryptoСurrenciesCollection(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task SetSelectedCryptoCurrencyById(string id)
        {
            _selectedCryptoCurrency = (await GetCryptoCurrencies()).FirstOrDefault(c => c.id == id);
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
