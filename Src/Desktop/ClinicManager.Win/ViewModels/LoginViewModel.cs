using ClinicManager.Win.Presentation.Mvvm;
using Prism.Commands;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace ClinicManager.Win.ViewModels;

public partial class LoginViewModel(Func<string, string, Task<string>> loginFunction) 
: ViewModelBase
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Username is required.")]
    string username = "Admin";
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Password is required.")]
    string password = "123";
    
    [ObservableProperty]
    string errorMessage;
    
    public bool IsAuthSuccess { get; set; }
    
    Func<string, string, Task<string>> LoginFunction = loginFunction;

    ICurrentWindowService CurrentWindowService => GetRequiredService<ICurrentWindowService>();

    [RelayCommand]
    async Task Login() {
        ErrorMessage = await LoginFunction(Username, Password);
        if (!string.IsNullOrEmpty(ErrorMessage))
            return;
        IsAuthSuccess = true;
        CurrentWindowService.Close();
    }
    
    bool CanLogin() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
}

