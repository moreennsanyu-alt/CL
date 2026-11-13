using ClinicManager.Win.Presentation.Mvvm;
using Prism.Commands;

namespace ClinicManager.Win.ViewModels;

public class LoginViewModel : ViewModelBase, IWindowCloseRequester
{
    private string? _username;

    public LoginViewModel()
    {
        Title = "Sign in";
        LoginCommand = new DelegateCommand(Login);
    }

    public event Action<bool?>? RequestClose;

    public string? Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public DelegateCommand LoginCommand { get; }

    // TODO: authenticate through a service (Services layer) before closing.
    private void Login() => RequestClose?.Invoke(true);
}
