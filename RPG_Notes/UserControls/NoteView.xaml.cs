using RPG_Notes.Models;
using RPG_Notes.ObservableCollections;
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

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableListNotes ControlList { get; set; }

    private Note thisNote;

    public Note ThisNote
    {
        get => thisNote;
        set
        {
            thisNote = value;
            OnPropertyChanged();
        }
    }

    private bool _changeMode = false;
    private bool _exitMode = false;
    private string _savedText;

    private void OffChangeMode()
    {
        _changeMode = false;
        tbTextNote.IsReadOnly = true;
        btnChange.Content = "◎";
        btnDelete.Content = "⌀";
    }

    private void OffExitMode()
    {
        _exitMode = false;
        btnDelete.Content = "x";
    }

    private async void btnChange_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_changeMode)
        {
            await ControlList.UpdateAsync(thisNote);
            OffChangeMode();
        }
        else
        {
            _savedText = tbTextNote.Text;
            _changeMode = true;
            tbTextNote.IsReadOnly = false;
            tbTextNote.Focus();
            btnChange.Content = "+";
            btnDelete.Content = "x";
        }
    }

    private async void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (_changeMode)
        {
            if (_exitMode)
            {
                OffExitMode();
                OffChangeMode();
            }
            else
            {
                tbTextNote.Text = _savedText;
                tbTextNote.Focus();
                _exitMode = true;
                btnDelete.Content = "⌂";
            }
        }
        else
            await ControlList.RemoveAsync(ThisNote.Id);
    }

    private void tbTextNote_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_exitMode)
            OffExitMode();
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
