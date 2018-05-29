using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Models;
using WeatherApp.Services.Location;
using WeatherApp.Services.Weather;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WeatherApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public ILocationService<LocationInfo> LocationService;
        public IWeatherApiClient WeatherApiClient;

        bool isBusy = false;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        string title = string.Empty;

        protected BaseViewModel()
        {
            WeatherApiClient = new WeatherApiClient();
            LocationService = new LocationService();
        }

        protected BaseViewModel(ILocationService<LocationInfo> locationService, IWeatherApiClient weatherApiClient)
        {
            WeatherApiClient = weatherApiClient;
            LocationService = locationService;
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected async Task ExecuteCommand(Func<Task> func)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                await func.Invoke();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}