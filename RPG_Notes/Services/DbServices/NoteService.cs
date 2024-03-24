using Microsoft.EntityFrameworkCore;
using RPG_Notes.Data;
using RPG_Notes.Models;
using RPG_Notes.Services.Source;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Notes.Services.DbServices;

public class NoteService : IDataService<int, Note>
{
    public IAsyncEnumerable<Note> GetAllAsync()
    {
        return DataDbContext.Instance.Notes.ToAsyncEnumerable();
    }

    public async Task<Note> GetAsync(int key)
    {
        return await DataDbContext.Instance.Notes.FirstAsync(n => n.Id == key);
    }

    public async Task AddAsync(Note value)
    {
        await DataDbContext.Instance.Notes.AddAsync(value);
        await DataDbContext.Instance.SaveChangesAsync();
    }

    public async Task AddAsync(int listId, string text) =>
        await AddAsync(new(((await GetAllAsync().LastOrDefaultAsync())?.Id ?? 0) + 1, listId, text));

    public async Task UpdateAsync(Note value)
    {
        var note = await DataDbContext.Instance.Notes.FindAsync(value.Id);

        if (note != null)
            DataDbContext.Instance.Notes.Remove(note);

        await AddAsync(value);
    }

    public async Task UpdateAsync(int id, int listId, string text) =>
        await UpdateAsync(new(id, listId, text));

    public async Task RemoveAsync(int key)
    {
        var note = await DataDbContext.Instance.Notes.FindAsync(key);

        if (note != null)
        {
            DataDbContext.Instance.Notes.Remove(note);
            await DataDbContext.Instance.SaveChangesAsync();
        }
    }
}
