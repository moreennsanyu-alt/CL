using ClinicManager.Win.Presentation.Mvvm;
using Prism.Commands;
using Prism.Regions;

namespace ClinicManager.Win.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    public MainViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        Title = "Main";
        NavigateCommand = new DelegateCommand<string>(Navigate);
    }

    public DelegateCommand<string> NavigateCommand { get; }

    private void Navigate(string viewName) => _regionManager.RequestNavigate(RegionNames.Content, viewName);
}
