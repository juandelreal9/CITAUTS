using UTCITAS.ViewModels;

namespace UTCITAS.Views;

public partial class ModificarCitaView : ContentPage
{
    private readonly ModificarCitaViewModel _viewModel;

    public ModificarCitaView(ModificarCitaViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Recargar citas cada vez que aparece la página
        _viewModel.ActualizarCommand.Execute(null);
    }
}