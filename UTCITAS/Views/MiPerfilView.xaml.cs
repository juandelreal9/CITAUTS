using UTCITAS.ViewModels;

namespace UTCITAS.Views;

public partial class MiPerfilView : ContentPage
{
    private readonly MiPerfilViewModel _viewModel;

    public MiPerfilView(MiPerfilViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Recargar datos cada vez que aparece la página
        _viewModel.ActualizarCommand.Execute(null);
    }
}