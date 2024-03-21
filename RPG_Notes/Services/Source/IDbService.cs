using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPG_Notes.Services.Source;

public interface IDbService<TKey, TValue>
{
    IAsyncEnumerable<TKey> GetAllAsync();
    Task<TValue> GetAsync(TKey key);
    Task AddAsync(TValue value);
    Task UpdateAsync(TValue value);
    Task RemoveAsync(TKey key);
}
