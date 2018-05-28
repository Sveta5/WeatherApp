using System.Threading.Tasks;

namespace WeatherApp.Services.Data
{
    public interface IDataStore<T>
    {
        Task<bool> StoreData(T objectToStore);
        Task<T> GetData();
    }
}
