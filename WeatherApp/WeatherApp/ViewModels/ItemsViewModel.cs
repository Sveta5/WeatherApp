﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using WeatherApp.Models;
using WeatherApp.Views;
using System.Linq;
using WeatherApp.Services.Location;
using WeatherApp.Services.Weather;

namespace WeatherApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<LocationInfo> LocationInfos { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command DeleteItemsCommand { get; set; }

        public ItemsViewModel()
        {
            InitializeItemsViewModel();
        }

        public ItemsViewModel(ILocationService<LocationInfo> locationService, IWeatherApiClient weatherApiClient) : base(locationService, weatherApiClient)
        {
            InitializeItemsViewModel();
        }

        private void InitializeItemsViewModel()
        {
            Title = "Your Locations List";
            LocationInfos = new ObservableCollection<LocationInfo>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteItemsCommand = new Command(async location => await ExecuteDeleteLocationCommand(location));

            MessagingCenter.Subscribe<SearchPage, LocationInfo>(this, "AddItem", async (obj, item) =>
            {
                if (LocationInfos.Any(element => element.Key.Equals(item.Key))) return;
                var weather = await WeatherApiClient.GetCurrentWeather(item.Key);
                item.WeatherInfo = weather;
                LocationInfos.Add(item);
                await LocationService.AddItemAsync(item);
            });
        }
        async Task ExecuteLoadItemsCommand()
        {
            await ExecuteCommand(async () =>
            {
                var items = await LocationService.GetItemsAsync();
                foreach (var locationInfo in items)
                {
                    var weather = await WeatherApiClient.GetCurrentWeather(locationInfo.Key);
                    if (weather != null) locationInfo.WeatherInfo = weather;

                }
                if (items != null)
                {
                    LocationInfos.Clear();
                    foreach (var item in items) LocationInfos.Add(item);
                }
            }
            );
        }

        async Task ExecuteDeleteLocationCommand(object obj)
        {
            await ExecuteCommand(async () =>
             {
                 if (!(obj is LocationInfo locationObject)) return;
                 await LocationService.DeleteItemAsync(locationObject.Key);
             }
             );

            await ExecuteLoadItemsCommand();
        }
    }
}