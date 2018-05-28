using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Services.Location
{
    public interface ILocationService<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync();
    }
}
