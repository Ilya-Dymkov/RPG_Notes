using RPG_Notes.Windows;
using System.Windows;
using System.Windows.Controls;

namespace RPG_Notes.UserControls;

/// <summary>
/// Interaction logic for ListSetting.xaml
/// </summary>
public partial class ListSetting : UserControl
{
    public ListSetting()
    {
        InitializeComponent();
    }

    public TitleNote Title { get; set; }

    private void btnAddList_Click(object sender, RoutedEventArgs e)
    {
        var inputBox = new InputMessageBox
        {
            InputText = async (name) => await Title.ListNotes.OwnerList.AddAsync(name),
            Owner = Window.GetWindow(this)
        };
        inputBox.ShowDialog();

        Visibility = Visibility.Hidden;
    }

    private async void btnRenameList_Click(object sender, RoutedEventArgs e)
    {
        var inputBox = new InputMessageBox
        {
            InputText = (name) =>
            {
                Title.tbTitleText.Text = name;
                Title.ListNotes.ListInfo.Name = name;
            },
            Owner = Window.GetWindow(this)
        };
        inputBox.ShowDialog();

        await Title.ListNotes.OwnerList.UpdateAsync(Title.ListNotes.ListInfo);
        Visibility = Visibility.Hidden;
    }

    private async void btnDeleteList_Click(object sender, RoutedEventArgs e)
    {
        await Title.ListNotes.OwnerList.RemoveAsync(Title.ListNotes.ListInfo.Id);
    }
}
