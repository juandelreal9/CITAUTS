using Microsoft.Extensions.DependencyInjection;

namespace UTCITAS;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        var loginView = serviceProvider.GetRequiredService<Views.LoginView>();
        MainPage = new NavigationPage(loginView);
    }
}