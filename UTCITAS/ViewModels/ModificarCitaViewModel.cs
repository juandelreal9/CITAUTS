using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UTCITAS.Models;
using UTCITAS.Services;

namespace UTCITAS.ViewModels;

public class ModificarCitaViewModel : INotifyPropertyChanged
{
    private readonly IDataService _dataService;
    private ObservableCollection<Cita> _citas;
    private Cita _citaSeleccionada;
    private bool _mostrarFormulario;

    // Campos para edición
    private string _servicio;
    private DateTime _fecha;
    private string _bloqueHorario;

    public ICommand SeleccionarCitaCommand { get; }
    public ICommand GuardarCambiosCommand { get; }
    public ICommand CancelarCitaCommand { get; }
    public ICommand CancelarEdicionCommand { get; }
    public ICommand ActualizarCommand { get; }

    public ModificarCitaViewModel(IDataService dataService)
    {
        _dataService = dataService;

        SeleccionarCitaCommand = new Command<Cita>(SeleccionarCita);
        GuardarCambiosCommand = new Command(async () => await GuardarCambios());
        CancelarCitaCommand = new Command(async () => await CancelarCita());
        CancelarEdicionCommand = new Command(CancelarEdicion);
        ActualizarCommand = new Command(CargarCitas);

        CargarCitas();
    }

    private void CargarCitas()
    {
        var listaCitas = _dataService.ObtenerCitas()
            .Where(c => c.Estado == StatusCita.Programada)
            .OrderBy(c => c.Fecha)
            .ThenBy(c => c.Hora)
            .ToList();

        Citas = new ObservableCollection<Cita>(listaCitas);
        MostrarFormulario = false;
        CitaSeleccionada = null;
    }

    private void SeleccionarCita(Cita cita)
    {
        if (cita == null) return;

        CitaSeleccionada = cita;

        // Cargar datos de la cita en los campos de edición
        Servicio = cita.Servicio;
        Fecha = cita.Fecha;
        BloqueHorario = $"{cita.Hora:HH:mm} - {cita.Hora.AddHours(1):HH:mm}";

        MostrarFormulario = true;
    }

    private async Task GuardarCambios()
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(Servicio))
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Por favor selecciona un servicio.",
                "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(BloqueHorario))
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Por favor selecciona un horario.",
                "OK");
            return;
        }

        if (Fecha.Date < DateTime.Now.Date)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                "No puedes programar una cita en una fecha pasada.",
                "OK");
            return;
        }

        // Confirmar cambios
        bool confirmar = await Application.Current.MainPage.DisplayAlert(
            "Confirmar Cambios",
            $"¿Deseas guardar los cambios?\n\n" +
            $"Servicio: {Servicio}\n" +
            $"Fecha: {Fecha:dd/MM/yyyy}\n" +
            $"Horario: {BloqueHorario}",
            "Sí, guardar",
            "Cancelar");

        if (confirmar)
        {
            // Extraer la hora del bloque horario
            var horaInicio = BloqueHorario.Split('-')[0].Trim();
            var hora = TimeOnly.Parse(horaInicio);

            // Actualizar la cita
            CitaSeleccionada.Servicio = Servicio;
            CitaSeleccionada.Fecha = Fecha;
            CitaSeleccionada.Hora = hora;

            _dataService.ActualizarCita(CitaSeleccionada);

            await Application.Current.MainPage.DisplayAlert(
                "¡Éxito!",
                "Tu cita ha sido actualizada correctamente.",
                "Aceptar");

            // Recargar lista
            CargarCitas();
        }
    }

    private async Task CancelarCita()
    {
        if (CitaSeleccionada == null) return;

        bool confirmar = await Application.Current.MainPage.DisplayAlert(
            "Cancelar Cita",
            $"¿Estás seguro de que deseas cancelar esta cita?\n\n" +
            $"Servicio: {CitaSeleccionada.Servicio}\n" +
            $"Fecha: {CitaSeleccionada.Fecha:dd/MM/yyyy}\n" +
            $"Horario: {CitaSeleccionada.Hora:HH:mm}",
            "Sí, cancelar cita",
            "No");

        if (confirmar)
        {
            _dataService.EliminarCita(CitaSeleccionada.Id);

            await Application.Current.MainPage.DisplayAlert(
                "Cita Cancelada",
                "La cita ha sido cancelada exitosamente.",
                "OK");

            // Recargar lista
            CargarCitas();
        }
    }

    private void CancelarEdicion()
    {
        MostrarFormulario = false;
        CitaSeleccionada = null;
    }

    // Propiedades
    public ObservableCollection<Cita> Citas
    {
        get => _citas;
        set { _citas = value; OnPropertyChanged(); OnPropertyChanged(nameof(TieneCitas)); }
    }

    public Cita CitaSeleccionada
    {
        get => _citaSeleccionada;
        set { _citaSeleccionada = value; OnPropertyChanged(); }
    }

    public bool MostrarFormulario
    {
        get => _mostrarFormulario;
        set { _mostrarFormulario = value; OnPropertyChanged(); OnPropertyChanged(nameof(MostrarLista)); }
    }

    public bool MostrarLista => !MostrarFormulario;

    public bool TieneCitas => Citas != null && Citas.Count > 0;

    public string Servicio
    {
        get => _servicio;
        set { _servicio = value; OnPropertyChanged(); }
    }

    public DateTime Fecha
    {
        get => _fecha;
        set { _fecha = value; OnPropertyChanged(); }
    }

    public string BloqueHorario
    {
        get => _bloqueHorario;
        set { _bloqueHorario = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}