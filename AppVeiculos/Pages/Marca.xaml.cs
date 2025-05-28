namespace AppVeiculos;

public partial class Marca : ContentPage
{
	public Marca()
	{
		InitializeComponent();
	}

    private async void btnAddMarcaOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMarca());
    }

    private async void imgEditOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditMarca());
    }

    private async void imgDeleteOnClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Exclu�da", "A marca foi deletada!", "Ok");
    }
}