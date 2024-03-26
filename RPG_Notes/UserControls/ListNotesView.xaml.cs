using RPG_Notes.ObservableCollections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            TitleNote.ListNotes = value;

            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
