using RPG_Notes.Models;
using RPG_Notes.Services.DataService;
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

        FillNotes();
    }

    public ListNotes ListInfo { get; }

    public ObservableCollection<NoteView> Notes { get; private set; } = [];

    private ListNotesView _listNotesView;
    private readonly NoteService _service = new();

    private async Task FillNotes()
    {
        await foreach (var note in _service.GetAllAsync()
                                           .Where(n => n.ListId == ListInfo.Id))
            Notes.Add(new() { ThisNote = note, ControlList = this });
    }

    private async Task ViewListNotes() =>
        await _listNotesView.ViewList(Notes);

    public void AddNotesView(ListNotesView notesView) =>
        _listNotesView = notesView;

    public async Task ReloadNotes()
    {
        Notes = [];
        await FillNotes();
        await ViewListNotes();
    }

    public IAsyncEnumerable<Note> GetAllAsync() =>
        _service.GetAllAsync();

    public async Task<Note?> GetAsync(int key) =>
        await _service.GetAsync(key);

    public async Task AddAsync(Note value)
    {
        var task = _service.AddAsync(value);
        Notes.Add(new() { ThisNote = value, ControlList = this });
        await task;
    }

    public async Task AddAsync(int listId, string text) =>
        await AddAsync(new(((await GetAllAsync().LastOrDefaultAsync())?.Id ?? 0) + 1, listId, text));

    public async Task UpdateAsync(Note value) =>
        await _service.UpdateAsync(value);

    public async Task UpdateAsync(int id, int listId, string text) =>
        await UpdateAsync(new(id, listId, text));

    public async Task RemoveAsync(int key)
    {
        var task = _service.RemoveAsync(key);
        Notes.Remove(Notes.First(n => n.ThisNote.Id == key));
        await task;
    }
}
