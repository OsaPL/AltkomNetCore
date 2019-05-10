using System.Collections.Generic;
using System.Threading.Tasks;

namespace utcAltkomDevices.IServices
{
    //Interfaces to be used by both client and server if needed
    public interface IEntityServices<T>
    {
        ICollection<T> Get();
        T Get(int id);
        T Get(string name);
        bool Add(T input);
        bool Update(T input);
        T Remove(int id);
    }
    public interface IEntityServicesAsync<T>
    {
        Task<ICollection<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<T> GetAsync(string name);
        Task<bool> AddAsync(T input);
        Task<bool> UpdateAsync(T input);
        Task<T> RemoveAsync(int id);
    }
}
