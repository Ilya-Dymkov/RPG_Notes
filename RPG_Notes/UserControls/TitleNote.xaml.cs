using RPG_Notes.ObservableCollections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for TitleNote.xaml
/// </summary>
public partial class TitleNote : UserControl, INotifyPropertyChanged
{
    public TitleNote()
    {
        DataContext = this;
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private ObservableListNotes listNotes;

    public ObservableListNotes ListNotes
    {
        get => listNotes;
        set
        {
            listNotes = value;
            EnterNote.ListNotes = listNotes;

            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
