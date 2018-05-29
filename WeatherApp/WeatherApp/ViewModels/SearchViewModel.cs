using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services.Location;
using WeatherApp.Services.Weather;

namespace WeatherApp.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<LocationInfo> LocationInfos { get; set; }

        public SearchViewModel()
        {
            InitializeSearchViewModel();
        }

        public SearchViewModel(ILocationService<LocationInfo> locationService, IWeatherApiClient weatherApiClient) : base(locationService, weatherApiClient)
        {
            InitializeSearchViewModel();
        }

        private void InitializeSearchViewModel()
        {
            Title = "Location search";
            LocationInfos = new ObservableCollection<LocationInfo>();
        }

        public async Task SearchForLocation(string postalCode)
        {
            await ExecuteCommand(async () =>
            {
                LocationInfos.Clear();

                var items = await WeatherApiClient.SearchLocation(postalCode);
                var locationInfos = items.ToList();

                if (items.ToList().Count == 0) return;

                foreach (var locationInfo in locationInfos)
                {
                    LocationInfos.Add(locationInfo);
                }
            });
        }
    }
}