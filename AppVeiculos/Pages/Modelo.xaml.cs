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

}