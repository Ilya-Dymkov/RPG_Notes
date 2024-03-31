using Microsoft.EntityFrameworkCore;
using RPG_Notes.Data;
using RPG_Notes.Models;
using RPG_Notes.Services.Source;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Notes.Services.DataService;

public class NoteService : IDataService<int, Note>
{
    private readonly DataDbContext _dbContext = new();

    public IAsyncEnumerable<Note> GetAllAsync() =>
        _dbContext.Notes.AsAsyncEnumerable();

    public async Task<Note?> GetAsync(int key) =>
        await _dbContext.Notes.FindAsync(key);

    public async Task AddAsync(Note value)
    {
        await _dbContext.Notes.AddAsync(value);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(int listId, string text) =>
        await AddAsync(new(((await GetAllAsync().LastOrDefaultAsync())?.Id ?? 0) + 1, listId, text));

    public async Task UpdateAsync(Note value)
    {
        var note = await GetAsync(value.Id);

        if (note != null)
            note.Text = value.Text;
        else
            await _dbContext.AddAsync(value);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, int listId, string text) =>
        await UpdateAsync(new(id, listId, text));

    public async Task RemoveAsync(int key)
    {
        var note = await GetAsync(key);

        if (note != null)
        {
            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();
        }
    }
}
