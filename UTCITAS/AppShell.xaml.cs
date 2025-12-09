using UTCITAS.Views;

namespace UTCITAS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CrearCuenta), typeof(CrearCuenta));
        }
    }
}
