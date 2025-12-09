using UTCITAS.Services;
using UTCITAS.ViewModels;

namespace UTCITAS.Views;

public partial class CrearCuenta : ContentPage
{
    private readonly IDataService _dataService;

    public CrearCuenta(CrearCuentaViewModel viewModel, IDataService dataService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _dataService = dataService;
    }

    private async void BntRegistrarse_Clicked(object sender, EventArgs e)
    {
        var viewModel = (CrearCuentaViewModel)BindingContext;
        string? nombreUsuario = viewModel.Nombre;
        string? correoUsuario = viewModel.Correo;
        string? contrasenaUsuario = viewModel.Contrasena;

        if (string.IsNullOrWhiteSpace(nombreUsuario) ||
            string.IsNullOrWhiteSpace(correoUsuario) ||
            string.IsNullOrWhiteSpace(contrasenaUsuario))
        {
            await DisplayAlert("Error de Registro", "Por favor, complete todos los campos.", "Aceptar");
            return;
        }

        if (!correoUsuario.Contains("@"))
        {
            await DisplayAlert("Error de Registro", "Por favor, ingresa un correo válido.", "Aceptar");
            return;
        }

        _dataService.GuardarUsuario(nombreUsuario, correoUsuario);

        await DisplayAlert("Registro Exitoso", "Su cuenta ha sido creada exitosamente.", "Aceptar");

        await Navigation.PopAsync();
    }
}