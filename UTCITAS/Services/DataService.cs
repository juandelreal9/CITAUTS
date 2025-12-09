using UTCITAS.Models;

namespace UTCITAS.Services;

public class DataService : IDataService
{
    private string _nombreUsuario = "Invitado";
    private string _correoUsuario = "invitado@utcitas.com";
    private List<Cita> _citas = new List<Cita>();
    private int _siguienteId = 1;

    // MÉTODOS DE USUARIO
    public void GuardarUsuario(string nombre, string correo)
    {
        _nombreUsuario = nombre;
        _correoUsuario = correo;
    }

    public (string Nombre, string Correo) ObtenerUsuario()
    {
        return (_nombreUsuario, _correoUsuario);
    }

    public bool TieneUsuario()
    {
        return !string.IsNullOrEmpty(_nombreUsuario) && _nombreUsuario != "Invitado";
    }

    // MÉTODOS DE CITAS
    public void AgregarCita(Cita cita)
    {
        cita.Id = _siguienteId++;
        cita.EstadoFecha = DateTime.Now;
        cita.Estado = StatusCita.Programada;
        _citas.Add(cita);
    }

    public List<Cita> ObtenerCitas()
    {
        return new List<Cita>(_citas);
    }

    public Cita? ObtenerCitaPorId(int citaId)
    {
        return _citas.FirstOrDefault(c => c.Id == citaId);
    }

    public void ActualizarCita(Cita citaActualizada)
    {
        var citaExistente = _citas.FirstOrDefault(c => c.Id == citaActualizada.Id);
        if (citaExistente != null)
        {
            citaExistente.Servicio = citaActualizada.Servicio;
            citaExistente.Fecha = citaActualizada.Fecha;
            citaExistente.Hora = citaActualizada.Hora;
            citaExistente.Email = citaActualizada.Email;
            citaExistente.Estado = citaActualizada.Estado;
            citaExistente.EstadoFecha = DateTime.Now;
        }
    }

    public void EliminarCita(int citaId)
    {
        var cita = _citas.FirstOrDefault(c => c.Id == citaId);
        if (cita != null)
        {
            _citas.Remove(cita);
        }
    }
}