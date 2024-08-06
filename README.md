# Coin App

## Overview
This multi-page application provides a comprehensive platform for viewing and interacting with cryptocurrency data. The application features a clean user interface with multiple pages, navigation capabilities, and support for various functionalities related to cryptocurrencies.

## Features
### Main page:
By default, displays the top 10 cryptocurrencies returned by the API. You can also view a list of all currencies.

Allows users to view essential data such as name, symbol..etc.

By clicking the "Details" button you can go to a page with detailed information about the corresponding currency.

### Coin View:
On this page you can get detailed information about the currency. By default about Bitcoin. To select another currency you need to either go to it from the main page by clicking the "Details" button or enter the name of the currency in the top SearchBox

The page also displays a graph of the currency price history for the past month.


By clicking the Markets for purchasing button, you will be taken to a page where a list of markets where currencies are traded is displayed.

### Convert View:

Allows users to convert one currency to another. 
Use the combo boxes, enter the amount and press the bottom button.
There is a button for converting and changing currencies. Basic optimal functionality for currency conversion has been implemented.

### Markets View:

On this page you can see a list of all available markets.

### MarketSearch View:

On this page you can select a currency (Bitcoin by default) and see the markets on which it is traded. To do this, select the desired currency in the ComboBox

### Some other featrues:
- You can refresh all pages by clicking the "Refresh current page" button in the menu
- You can use a convenient menu to navigate and exit the application
- Supports maximizing the screen and returning it back
- Language change function: click on the gear in the upper right corner and select the desired language (English or Ukrainian)
- Possibility of searching for currency by name or code.
- Light / dark theme support - (on development stage)
- Currency charts
- Flexible tables

## Technical Details
### NuGet:
* LiveCharts.Wpf
* MathApps.Metro.IconPacks.Material
* MaterialDesignThemes
* Newtonsoft.Json

### Technologies:
* C#
* WPF (Windows Presentation Foundation) - WPF provides powerful tools for creating complex interfaces and making efficient use of system resources. Its XAML (Extensible Application Markup Language) support makes interface development more structured and modular.
* MVVM - To separate responsibilities, to improve the readability, manageability, maintainability and testability of code. Helps to clearly separate application business logic and presentation logic from the user interface.
* CoinCap (API) : https://docs.coincap.io/
* HttpClient, Asynchronous Programming, Resource Dictionaries
