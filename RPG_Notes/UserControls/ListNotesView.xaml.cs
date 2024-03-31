using RPG_Notes.ObservableCollections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for ListNotesView.xaml
/// </summary>
public partial class ListNotesView : UserControl, INotifyPropertyChanged
{
    public ListNotesView()
    {
        DataContext = this;
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private ObservableListNotes notes;

    public ObservableListNotes Notes
    {
        get => notes;
        set
        {
            notes = value;
            notes.AddNotesView(this);
            TitleNote.ListNotes = notes;

            OnPropertyChanged();
        }
    }

    public async Task ViewList<TNote>(ObservableCollection<TNote> noteViews) =>
        await Task.FromResult(lvNotes.ItemsSource = noteViews);

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
