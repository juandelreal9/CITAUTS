using Microsoft.Extensions.Logging;
using UTCITAS.Services;
using UTCITAS.ViewModels;
using UTCITAS.Views;

namespace UTCITAS;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "awesomesolid");
            });

#pragma warning disable CA1416

        // Registrar Servicios como Singleton
        builder.Services.AddSingleton<IDataService, DataService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        // Registrar ViewModels como Transient
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<CrearCuentaViewModel>();
        builder.Services.AddTransient<GenerarCitaViewModel>();
        builder.Services.AddTransient<MiPerfilViewModel>();
        builder.Services.AddTransient<ModificarCitaViewModel>();

        // Registrar Views como Transient
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<CrearCuenta>();
        builder.Services.AddTransient<MiCuentaView>();
        builder.Services.AddTransient<GenerarCitaView>();
        builder.Services.AddTransient<MiPerfilView>();
        builder.Services.AddTransient<ModificarCitaView>();
        builder.Services.AddTransient<AyudaView>();

#pragma warning restore CA1416

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}