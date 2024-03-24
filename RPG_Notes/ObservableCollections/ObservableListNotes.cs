using RPG_Notes.Models;
using RPG_Notes.Services.DbServices;
using RPG_Notes.Services.Source;
using RPG_Notes.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Notes.ObservableCollections;

public class ObservableListNotes : IDataService<int, Note>
{
    public ObservableListNotes(ListNotes listInfo)
    {
        ListInfo = listInfo;

        Notes = [];
        FillNotes();
    }

    public ListNotes ListInfo { get; }
    public ObservableCollection<NoteView> Notes { get; }

    private readonly NoteService _noteService = new();

    private async void FillNotes()
    {
        await foreach (var note in _noteService.GetAllAsync()
                                               .Where(n => n.ListId == ListInfo.Id))
            Notes.Add(new() { ThisNote = note });
    }

    public IAsyncEnumerable<Note> GetAllAsync()
    {
        return _noteService.GetAllAsync();
    }

    public async Task AddAsync(int listId, string text) =>
        await AddAsync(new(((await GetAllAsync().LastOrDefaultAsync())?.Id ?? 0) + 1, listId, text));

    public async Task<Note> GetAsync(int key)
    {
        return await _noteService.GetAsync(key);
    }

    public async Task AddAsync(Note value)
    {
        var task = _noteService.AddAsync(value);
        Notes.Add(new() { ThisNote = value });
        await task;
    }

    public async Task UpdateAsync(Note value)
    {
        var task = _noteService.UpdateAsync(value);
        Notes[Notes.IndexOf(Notes.First(n => n.ThisNote.Id == value.Id))] = new() { ThisNote = value };
        await task;
    }

    public async Task RemoveAsync(int key)
    {
        var task = _noteService.RemoveAsync(key);
        Notes.Remove(Notes.First(n => n.ThisNote.Id == key));
        await task;
    }
}
