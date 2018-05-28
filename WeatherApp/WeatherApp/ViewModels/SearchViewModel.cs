using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<LocationInfo> LocationInfos { get; set; }

        public SearchViewModel()
        {
            Title = "Location search";
            LocationInfos = new ObservableCollection<LocationInfo>();
        }

        public async Task SearchForLocation(string postalCode)
        {
            await ExecuteCommand(async () =>
            {
                LocationInfos.Clear();

                var items = await WeaterApiClient.SearchLocation(postalCode);
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