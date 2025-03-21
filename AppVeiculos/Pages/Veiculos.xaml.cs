namespace AppVeiculos;

public partial class Veiculos : ContentPage
{
	public Veiculos()
	{
		InitializeComponent();
	}

	private async void btnAddVeiculoOnClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddVeiculo());
	}

}