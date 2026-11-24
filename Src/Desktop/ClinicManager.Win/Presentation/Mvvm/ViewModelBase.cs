using Prism.Mvvm;

namespace ClinicManager.Win.Presentation.Mvvm;

public abstract class ViewModelBase 
    : ObservableValidator, ISupportServices 
{

    IServiceContainer serviceContainer = null;
    
    protected IServiceContainer ServiceContainer {
        get {
            if(serviceContainer == null)
                serviceContainer = new ServiceContainer(this);
            return serviceContainer; 
        }
    }
    IServiceContainer ISupportServices.ServiceContainer { get { return ServiceContainer; } }

}
