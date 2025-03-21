namespace AppVeiculos;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

	private async void btnCadastroOnClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Cadastro());
	}

}