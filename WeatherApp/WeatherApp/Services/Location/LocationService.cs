using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services.Data;

namespace WeatherApp.Services.Location
{
    public class LocationService : ILocationService<LocationInfo>
    {
        List<LocationInfo> LocationInfos;
        public IDataStore<List<LocationInfo>> DataStore;

        public LocationService()
        {
            LocationInfos = new List<LocationInfo>();
            DataStore = new PropertiesDataStore<List<LocationInfo>>("locations");
        }

        public LocationService(IDataStore<List<LocationInfo>> dataStore)
        {
            LocationInfos = new List<LocationInfo>();
            DataStore = dataStore;
        }

        public async Task<bool> AddItemAsync(LocationInfo locationInfo)
        {
            LocationInfos.Add(locationInfo);
            await DataStore.StoreData(LocationInfos);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string key)
        {
            if (LocationInfos.Any(arg => arg.Key == key))
            {
                var locationInfo = LocationInfos.FirstOrDefault(arg => arg.Key == key);
                LocationInfos.Remove(locationInfo);
            }

            await DataStore.StoreData(LocationInfos);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<LocationInfo>> GetItemsAsync()
        {
            var data = await DataStore.GetData();
            if (data != null && data.Any())
            {
                LocationInfos = data;
            }

            return await Task.FromResult(LocationInfos);
        }
    }
}