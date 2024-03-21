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

    private string placeholder;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Placeholder
    {
        get { return placeholder; }
        set
        {
            placeholder = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
