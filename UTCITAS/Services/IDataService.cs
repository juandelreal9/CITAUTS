using UTCITAS.Models;

namespace UTCITAS.Services;

public interface IDataService
{
    // Métodos de Usuario
    void GuardarUsuario(string nombre, string correo);
    (string Nombre, string Correo) ObtenerUsuario();
    bool TieneUsuario();

    // Métodos de Citas
    void AgregarCita(Cita cita);
    List<Cita> ObtenerCitas();
    Cita? ObtenerCitaPorId(int citaId);
    void ActualizarCita(Cita cita);
    void EliminarCita(int citaId);
}