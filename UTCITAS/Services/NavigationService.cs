using UTCITAS.Views;
using Microsoft.Extensions.DependencyInjection;

namespace UTCITAS.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task NavigateToMiCuentaAsync()
    {
        if (Application.Current?.MainPage?.Navigation != null)
        {
            var miCuentaPage = _serviceProvider.GetRequiredService<MiCuentaView>();
            await Application.Current.MainPage.Navigation.PushAsync(miCuentaPage);
        }
    }

    public Task PopAsync()
    {
        return Application.Current!.MainPage!.Navigation.PopAsync();
    }
}