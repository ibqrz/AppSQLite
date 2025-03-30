namespace AppVeiculos;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

	private async void btnLoginOnClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Login", "Login realizado!", "Ok");
	}

}