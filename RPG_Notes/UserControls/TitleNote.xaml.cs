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

        lsPanel.Title = this;
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

    private async void btnReload_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        await ListNotes.ReloadNotes();
    }

    private bool _settingVisible = false;

    private void btnSetting_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_settingVisible)
        {
            lsPanel.Visibility = System.Windows.Visibility.Hidden;
            _settingVisible = false;
        }
        else
        {
            lsPanel.Visibility = System.Windows.Visibility.Visible;
            _settingVisible = true;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
