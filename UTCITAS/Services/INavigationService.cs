namespace UTCITAS.Services;

public interface INavigationService
{
    Task NavigateToMiCuentaAsync();
    Task PopAsync();
}