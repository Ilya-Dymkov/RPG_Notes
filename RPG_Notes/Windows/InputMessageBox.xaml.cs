using System;
using System.Windows;

namespace RPG_Notes.Windows;

/// <summary>
/// Interaction logic for InputMessageBox.xaml
/// </summary>
public partial class InputMessageBox : Window
{
    public InputMessageBox()
    {
        InitializeComponent();
    }

    public Action<string> InputText { get; set; }

    private bool _goodName = false;

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_goodName && !string.IsNullOrEmpty(tbInputBox.Text))
            InputText(tbInputBox.Text);
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
        _goodName = true;
        Close();
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
