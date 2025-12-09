using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UTCITAS.Models;
using UTCITAS.Services;

namespace UTCITAS.ViewModels;

public class MiPerfilViewModel : INotifyPropertyChanged
{
    private readonly IDataService _dataService;
    private string? _nombre;
    private string? _correo;
    private ObservableCollection<Cita>? _citas;

    public ICommand ActualizarCommand { get; }

    public MiPerfilViewModel(IDataService dataService)
    {
        _dataService = dataService;
        ActualizarCommand = new Command(CargarDatos);
        CargarDatos();
    }

    private void CargarDatos()
    {
        var usuario = _dataService.ObtenerUsuario();
        Nombre = usuario.Nombre;
        Correo = usuario.Correo;

        var listaCitas = _dataService.ObtenerCitas();
        Citas = new ObservableCollection<Cita>(listaCitas.OrderByDescending(c => c.Fecha).ThenByDescending(c => c.Hora));
    }

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

    public ObservableCollection<Cita>? Citas
    {
        get => _citas;
        set { _citas = value; OnPropertyChanged(); OnPropertyChanged(nameof(TieneCitas)); }
    }

    public bool TieneCitas => Citas != null && Citas.Count > 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}