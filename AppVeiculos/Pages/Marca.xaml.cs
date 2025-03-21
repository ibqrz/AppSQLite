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

}