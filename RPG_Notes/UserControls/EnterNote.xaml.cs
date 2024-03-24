using RPG_Notes.ObservableCollections;
using System.Windows;
using System.Windows.Controls;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for EnterNote.xaml
/// </summary>
public partial class EnterNote : UserControl
{
    public EnterNote()
    {
        InitializeComponent();
    }

    public ObservableListNotes ListNotes { get; set; }

    private async void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(txtInput.Text))
            await ListNotes.AddAsync(ListNotes.ListInfo.Id, txtInput.Text);

        txtInput.Clear();
        txtInput.Focus();
    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        txtInput.Clear();
        txtInput.Focus();
    }

    private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtInput.Text))
            tbPlaceholder.Visibility = Visibility.Visible;
        else
            tbPlaceholder.Visibility = Visibility.Hidden;
    }
}
