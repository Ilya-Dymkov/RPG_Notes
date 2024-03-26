using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPG_Notes.Services.Source;

public interface IDataService<TKey, TValue>
{
    IAsyncEnumerable<TValue> GetAllAsync();
    Task<TValue?> GetAsync(TKey key);
    Task AddAsync(TValue value);
    Task UpdateAsync(TValue value);
    Task RemoveAsync(TKey key);
}
