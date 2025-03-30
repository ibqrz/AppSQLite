namespace AppVeiculos;

public partial class Cadastro : ContentPage
{
	public Cadastro()
	{
		InitializeComponent();
	}

	private async void btnbtnCadastroOnClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Cadastrado(a)", "Cadastro realizado!", "Ok");
	}
}