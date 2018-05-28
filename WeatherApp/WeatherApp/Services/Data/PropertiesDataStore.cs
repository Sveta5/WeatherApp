using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WeatherApp.Services.Data
{
    public class PropertiesDataStore<T> : IDataStore<T>
    {
        private readonly string PropertyName;

        public PropertiesDataStore(string propertyName)
        {
            this.PropertyName = propertyName;
        }
        
        public async Task<bool> StoreData(T objectToStore)
        {
            if (Application.Current.Properties.Keys.Contains(PropertyName))
            { 
            Application.Current.Properties.Remove(PropertyName);
            }
            var value = JsonConvert.SerializeObject(objectToStore);
            Application.Current.Properties.Add(PropertyName, value);

            await Application.Current.SavePropertiesAsync();
            return await Task.FromResult(true);
        }

        public async Task<T> GetData()
        {
            Application.Current.Properties.TryGetValue(PropertyName, out var value);
            string valueAsString = "";
            if (value != null) valueAsString = value.ToString();

            T result = JsonConvert.DeserializeObject<T>(valueAsString);

            return await Task.FromResult(result); 
        }
    }
}
