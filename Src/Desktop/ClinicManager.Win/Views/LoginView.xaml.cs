using System.Windows;
using ClinicManager.Win.Presentation.Mvvm;

namespace ClinicManager.Win.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();

        DataContextChanged += (_, e) =>
        {
            if (e.OldValue is IWindowCloseRequester oldVm) oldVm.RequestClose -= OnRequestClose;
            if (e.NewValue is IWindowCloseRequester newVm) newVm.RequestClose += OnRequestClose;
        };
    }

    private void OnRequestClose(bool? result) => DialogResult = result;
}
