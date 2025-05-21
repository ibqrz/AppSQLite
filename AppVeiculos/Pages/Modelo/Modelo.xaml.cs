namespace AppVeiculos;

public partial class Modelo : ContentPage
{
	public Modelo()
	{
		InitializeComponent();
	}

    private async void btnAddModeloOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddModelo());
    }

    private async void imgEditOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditModelo());
    }

    private async void imgDeleteOnClicked(object sender, EventArgs e)
    {
        DisplayAlert("Excluído", "O modelo foi deletado!", "Ok");
    }
}