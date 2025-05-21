namespace AppVeiculos;

public partial class AddModelo : ContentPage
{
	public AddModelo()
	{
		InitializeComponent();
	}

	private async void btnAddModeloOnClicked(object? sender, EventArgs e)
	{
		string modelo = etrModelo.Text;
		string obsModelo = edtOBSModelo.Text;

		// salvar no banco de dados
	
		if (string.IsNullOrWhiteSpace(modelo))
		{
			DisplayAlert("ERRO", "O campo 'Modelo' precisa ser preenchido!", "OK");
			return;
		}

		await DisplayAlert("Modelo Adicionado", $"Modelo: {modelo}\nObservações: {obsModelo}", "OK");
        await Shell.Current.GoToAsync("//Modelo");

        etrModelo.Text = string.Empty;
		edtOBSModelo.Text = string.Empty;

	}
}