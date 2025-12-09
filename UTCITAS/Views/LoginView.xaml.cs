using UTCITAS.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace UTCITAS.Views;

public partial class LoginView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public LoginView(LoginViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _serviceProvider = serviceProvider;
    }

    private async void BtnCrearCuenta_Clicked(object sender, EventArgs e)
    {
        var crearCuentaView = _serviceProvider.GetRequiredService<CrearCuenta>();
        await Navigation.PushAsync(crearCuentaView);
    }

    
}