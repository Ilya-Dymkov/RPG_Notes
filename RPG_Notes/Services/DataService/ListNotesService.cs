using RPG_Notes.Data;
using RPG_Notes.Models;
using RPG_Notes.Services.Source;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Notes.Services.DataService;

public class ListNotesService : IDataService<int, ListNotes>
{
    private readonly DataDbContext _dbContext = new();

    public IAsyncEnumerable<ListNotes> GetAllAsync() =>
        _dbContext.ListNotes.AsAsyncEnumerable();

    public async Task<ListNotes?> GetAsync(int key) =>
        await _dbContext.ListNotes.FindAsync(key);

    public async Task AddAsync(ListNotes value)
    {
        await _dbContext.ListNotes.AddAsync(value);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(string name) =>
        await AddAsync(new ListNotes(((await GetAllAsync().LastOrDefaultAsync())?.Id ?? 0) + 1, name));

    public async Task UpdateAsync(ListNotes value)
    {
        var list = await GetAsync(value.Id);

        if (list != null)
            _dbContext.Remove(list);

        await AddAsync(value);
    }

    public async Task UpdateAsync(int id, string name) =>
        await UpdateAsync(new(id, name));

    public async Task RemoveAsync(int key)
    {
        var list = await GetAsync(key);

        if (list != null)
        {
            _dbContext.ListNotes.Remove(list);
            await _dbContext.SaveChangesAsync();
        }
    }
}
