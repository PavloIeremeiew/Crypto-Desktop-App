using CryptoApp.Core;
using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
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

        public RelayCommand NavigateToHomeViewComand { get; set; }
        public RelayCommand NavigateToInfoViewComand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigateToHomeViewComand = new RelayCommand(o => { NavigationService.NavigateTo<HomeViewModel>(); }, o => true);
            NavigateToInfoViewComand = new RelayCommand(o => { NavigationService.NavigateTo<InfoViewModel>(); }, o => true);

        }
    }
}
