using UTCITAS.ViewModels;

namespace UTCITAS.Views;

public partial class GenerarCitaView : ContentPage
{
    public GenerarCitaView(GenerarCitaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}