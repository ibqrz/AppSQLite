namespace AppVeiculos;

public partial class Cadastro : ContentPage
{
	public Cadastro()
	{
		InitializeComponent();
	}

	private async void btnCadastroOnClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Cadastrado(a)", "Cadastro realizado!", "Ok");
	}

	private async void btnVoltarOnClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("//MainPage");
    }
}