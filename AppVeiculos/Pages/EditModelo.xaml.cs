namespace AppVeiculos;

public partial class EditModelo : ContentPage
{
	public EditModelo()
	{
		InitializeComponent();
	}

    private async void btnEdtModeloOnClicked(object? sender, EventArgs e)
    {
        string modelo = etrModelo.Text;
        string obsModelo = edtOBSModelo.Text;

        // salvar no banco de dados

        if (string.IsNullOrWhiteSpace(modelo))
        {
            DisplayAlert("ERRO", "O campo 'Modelo' precisa ser preenchido!", "OK");
            return;
        }

        await DisplayAlert("Modelo Editado", $"Modelo: {modelo}\nObservações: {obsModelo}", "OK");

        etrModelo.Text = string.Empty;
        edtOBSModelo.Text = string.Empty;

    }
}