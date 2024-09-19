using CryptoApp.Core;

namespace CryptoApp.Services.Interfaces
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }  
        void NavigateTo<T>() where T: ViewModel;
    }
}
