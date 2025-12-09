using System.Security.Cryptography.X509Certificates;

namespace UTCITAS.Models;

public class Cita : ContentPage
{
    public new int Id { get; set; } // Se utiliza la palabra clave 'new' para ocultar el miembro heredado.
    public required string Servicio { get; set; } // Se agrega el modificador 'required'.
    public DateTime Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string Email { get; set; }
    public StatusCita Estado { get; set; }
    public DateTime EstadoFecha { get; set; }
}

public enum StatusCita
{
    Programada,
    Completada,
    Cancelada
}

