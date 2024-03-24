using RPG_Notes.ObservableCollections;
using System.Windows.Controls;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for ListNotesView.xaml
/// </summary>
public partial class ListNotesView : UserControl
{
    public ListNotesView()
    {
        DataContext = this;

        InitializeComponent();

        Notes = new(new(1, "First List"));
        TitleNote.ListNotes = Notes;
    }

    public ObservableListNotes Notes { get; set; }
}
