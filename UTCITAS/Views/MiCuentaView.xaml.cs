using Microsoft.Extensions.DependencyInjection;

namespace UTCITAS.Views;

public partial class MiCuentaView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public MiCuentaView(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    private async void BtnGenerarCita_Clicked(object sender, EventArgs e)
    {
        var generarCitaView = _serviceProvider.GetRequiredService<GenerarCitaView>();
        await Navigation.PushAsync(generarCitaView);
    }

    private async void BtnModificarCita_Clicked(object sender, EventArgs e)
    {
        var modificarCitaView = _serviceProvider.GetRequiredService<ModificarCitaView>();
        await Navigation.PushAsync(modificarCitaView);
    }

    private async void BtnMiPerfil_Clicked(object sender, EventArgs e)
    {
        var miPerfilView = _serviceProvider.GetRequiredService<MiPerfilView>();
        await Navigation.PushAsync(miPerfilView);
    }

    private async void BtnAyuda_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AyudaView());
    }
}