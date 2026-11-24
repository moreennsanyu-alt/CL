using ClinicManager.Win.Presentation.Mvvm;
using Prism.Commands;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.CodeGenerators;

namespace ClinicManager.Win.ViewModels;

[GenerateViewModel(ImplementISupportServices = true)]
public partial class LoginViewModel(Func<string, string, Task<string>> loginFunction) {
    [GenerateProperty]
    string username = "Admin";
    [GenerateProperty]
    string password = "123";
    [GenerateProperty]
    string errorMessage;
    public bool IsAuthSuccess { get; set; }
    Func<string, string, Task<string>> LoginFunction = loginFunction;

    ICurrentWindowService CurrentWindowService => GetRequiredService<ICurrentWindowService>();

    [GenerateCommand]
    async Task Login() {
        ErrorMessage = await LoginFunction(Username, Password);
        if (!string.IsNullOrEmpty(ErrorMessage))
            return;
        IsAuthSuccess = true;
        CurrentWindowService.Close();
    }
    bool CanLogin() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
}

