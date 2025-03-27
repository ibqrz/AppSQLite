namespace AppVeiculos;

public partial class EdtVeiculo : ContentPage
{
	public EdtVeiculo()
	{
		InitializeComponent();
	}

    private async void btnEdtVeiculoOnClicked(object? sender, EventArgs e)
    {
        string veiculo = etrVeiculo.Text;
        // int ano = etrAnoFab; 
        // string obsVeiculo = edtObsVeiculos.Text;

        // salvar no banco de dados

        if (string.IsNullOrWhiteSpace(veiculo))
        {
            DisplayAlert("ERRO", "Os campos 'camposEx' precisam estar preenchidos!", "Ok");
            return;
        }

        await DisplayAlert("Ve�culo Editado", $"Ve�culo: {veiculo}", "OK");

        etrVeiculo.Text = string.Empty;

    }
}