using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UTCITAS.ViewModels;

public class CrearCuentaViewModel : INotifyPropertyChanged
{
    private string? _nombre;
    private string? _correo;
    private string? _contrasena;

    public string? Nombre
    {
        get => _nombre;
        set { _nombre = value; OnPropertyChanged(); }
    }

    public string? Correo
    {
        get => _correo;
        set { _correo = value; OnPropertyChanged(); }
    }

    public string? Contrasena
    {
        get => _contrasena;
        set { _contrasena = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}