namespace ClinicManager.Win.Presentation.Mvvm;

/// <summary>Lets a view model ask its hosting window to close without referencing the view.</summary>
public interface IWindowCloseRequester
{
    event Action<bool?>? RequestClose;
}
