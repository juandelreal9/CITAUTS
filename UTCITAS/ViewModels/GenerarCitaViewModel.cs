using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UTCITAS.Models;
using UTCITAS.Services;

namespace UTCITAS.ViewModels;

public class GenerarCitaViewModel : INotifyPropertyChanged
{
    private readonly IDataService _dataService;
    private string _servicio = string.Empty;
    private DateTime _fecha = DateTime.Now;
    private string _bloqueHorario = string.Empty;

    public ICommand ConfirmarCitaCommand { get; }

    public GenerarCitaViewModel(IDataService dataService)
    {
        _dataService = dataService;
        ConfirmarCitaCommand = new Command(async () => await ExecuteConfirmarCitaAsync());
    }

    private async Task ExecuteConfirmarCitaAsync()
    {
        if (string.IsNullOrWhiteSpace(Servicio))
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", "Por favor selecciona un servicio.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(BloqueHorario))
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", "Por favor selecciona un horario.", "OK");
            return;
        }

        if (Fecha.Date < DateTime.Now.Date)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", "No puedes agendar una cita en una fecha pasada.", "OK");
            return;
        }

        bool confirmar = await Application.Current!.MainPage!.DisplayAlert(
            "Confirmar Cita",
            $"¿Deseas confirmar tu cita?\n\nServicio: {Servicio}\nFecha: {Fecha:dd/MM/yyyy}\nHorario: {BloqueHorario}",
            "Sí, confirmar",
            "Cancelar");

        if (confirmar)
        {
            var horaInicio = BloqueHorario.Split('-')[0].Trim();
            var hora = TimeOnly.Parse(horaInicio);
            var usuario = _dataService.ObtenerUsuario();

            var nuevaCita = new Cita
            {
                Servicio = Servicio,
                Fecha = Fecha,
                Hora = hora,
                Email = usuario.Correo,
                Estado = StatusCita.Programada
            };

            _dataService.AgregarCita(nuevaCita);

            await Application.Current!.MainPage!.DisplayAlert(
                "¡Éxito!",
                $"Tu cita de {Servicio} ha sido confirmada para el {Fecha:dd/MM/yyyy} en el horario {BloqueHorario}.",
                "Aceptar");

            Servicio = string.Empty;
            Fecha = DateTime.Now;
            BloqueHorario = string.Empty;

            await Application.Current!.MainPage!.Navigation.PopAsync();
        }
    }

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