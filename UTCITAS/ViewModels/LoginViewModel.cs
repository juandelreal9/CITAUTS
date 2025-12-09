using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UTCITAS.Services;

namespace UTCITAS.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;

    private string? _correo;
    private string? _contrasena;

    public ICommand LoginCommand { get; }

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        LoginCommand = new Command(async () => await ExecuteLoginCommand());
    }

    private async Task ExecuteLoginCommand()
    {
        if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Campos incompletos",
                "Por favor ingresa tu correo y contraseña.",
                "OK");
            return;
        }

        await Application.Current!.MainPage!.DisplayAlert(
            "Bienvenido",
            "Has iniciado sesión como invitado",
            "Continuar");

        await _navigationService.NavigateToMiCuentaAsync();
    }

    public string? Correo
    {
        get => _correo;
        set
        {
            if (_correo != value)
            {
                _correo = value;
                OnPropertyChanged();
            }
        }
    }

    public string? Contrasena
    {
        get => _contrasena;
        set
        {
            if (_contrasena != value)
            {
                _contrasena = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}