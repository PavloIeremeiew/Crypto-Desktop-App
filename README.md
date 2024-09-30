# Crypto-Desktop-App
 
## About:
 Crypto is a desktop program for viewing information about cryptocurrencies.
## Feature of the program:
 The program consists of three parts:
 - `Main` - the program window that contains the navigation part, the active page and the custom header panel
   - The navigation part consists of : 
     1. button “Crypto” to go to the Home page 
     2. search field
     3. list of cryptocurrency buttons to go to the page with detailed information
     4. Button for changing the theme
     5. Language selection menu 
 - `Home` - a page that displays the top 10 cryptocurrencies.
 - `Info` is a page that displays detailed information about cryptocurrencies.
## Development process:
 1. Libraries used:
 - `Microsoft.Extensions.DependencyInjection` (to use Dependency Injection in the architecture)
 - `Microsoft.Extensions.Http` (using the abstract factory when working with the basic HttpClient)
 2. The `MVVM` project architecture was developed and the base classes were developed in the Core folder:
  - `ObservableObject` - to check for changes in property.
  - `ViewModel` - a base ViewModel class with a method for loading a View.
  - `RelayCommand` - a class for implementing button commands.
3. Implemented `API` request to coincap. For this purpose, 2 models were implemented:
  - `CryptoCurrenciesList` - a list of CryptoCurrencies and timestamp
  - `CryptoCurrency` - an object with all the characteristics of a cryptocurrency
 ![зображення](https://github.com/user-attachments/assets/0e939f02-0e9e-45bf-847e-74b6f2645b00)
4. Using Dependency Injection, 2 services were added:
- `INavigationService` - for navigation between view
- `ICryptoCurrenciesCollection` - to get data about cryptocurrencies 
5. Implementation of views functionality
6. Developed a prototype design on `Figma`:
![зображення](https://github.com/user-attachments/assets/5b6e7714-6408-47d7-9ef4-822d28c8ed90)
(The prototype has unrealized parts: `Graphic` page and currency `calculator`)
7. Development of styles for View. Converters were used in the development process:
- `LessThanZeroConverter`- to check if the value is less than zero. 
- `NullStringFormatingConverter` - to format the value in the text.
- `PercentConverter` - to format the value in percentage format.
8. Implemented theme change using `resource dictionary` / `DynamicResource`
9. Implemented language change using `Resource.resx` (the disadvantage of this implementation is the need to register all text elements in the ViewModel for dynamic change)
