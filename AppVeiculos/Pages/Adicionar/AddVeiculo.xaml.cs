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
        // int ano = etrAnoFab.Text; 
        // string obsVeiculo = edtObsVeiculo.Text;

        // salvar no banco de dados

        if (string.IsNullOrWhiteSpace(veiculo))
        {
            DisplayAlert("ERRO", "Verifique se os campos estão preenchidos!", "Ok");
            return;
        }

        await DisplayAlert("Veículo Adicionado", $"Veículo: {veiculo}", "OK");

        etrVeiculo.Text = string.Empty;
        // etrAnoFab.Text = string.Empty;
        // edtObsVeiculo.Text = string.Empty;

    }

}