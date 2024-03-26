using RPG_Notes.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for NoteView.xaml
/// </summary>
public partial class NoteView : UserControl, INotifyPropertyChanged
{
    public NoteView()
    {
        DataContext = this;
        InitializeComponent();
    }

    private Note thisNote;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Note ThisNote
    {
        get => thisNote;
        set
        {
            thisNote = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
