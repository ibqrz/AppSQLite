using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppVeiculos;

public partial class AddVeiculo : ContentPage
{
	public AddVeiculo()
	{
		InitializeComponent();
	}

    private async void btnAddVeiculoOnClicked(object? sender, EventArgs e)
    {
        string veiculo = etrVeiculo.Text;
        // int ano = etrAnoFab; 
        // string obsVeiculo = edtObsVeiculos.Text;

        if (string.IsNullOrWhiteSpace(veiculo))
        {
            DisplayAlert("ERRO", "Os campos 'camposEx' precisam estar preenchidos!", "Ok");
            return;
        }

        await DisplayAlert("Veículo Adicionado", $"Veícluo: {veiculo}", "OK");

        etrVeiculo.Text = string.Empty;

    }

}