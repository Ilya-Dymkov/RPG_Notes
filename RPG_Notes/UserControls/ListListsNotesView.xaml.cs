using RPG_Notes.Extensions;
using RPG_Notes.ObservableCollections;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for ListListsNotesView.xaml
/// </summary>
public partial class ListListsNotesView : UserControl, INotifyPropertyChanged
{
    public ListListsNotesView()
    {
        DataContext = this;
        InitializeComponent();

        Lists = new();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private ScrollViewer _scrollViewer;

    private ObservableListListsNotes lists;

    public ObservableListListsNotes Lists
    {
        get => lists;
        set
        {
            lists = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void lvLists_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0)
        {
            _scrollViewer ??= lvLists.GetChildOfType<ScrollViewer>() ??
                    throw new Exception("Scroll Viewer was not found!");

            if (e.Delta < 0)
                _scrollViewer.LineRight();
            else
                _scrollViewer.LineLeft();

            e.Handled = true;
        }
    }
}
