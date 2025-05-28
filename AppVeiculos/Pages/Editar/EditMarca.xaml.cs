namespace AppVeiculos;

public partial class EditMarca : ContentPage
{
	public EditMarca()
	{
		InitializeComponent();
	}

    private async void btnEdtMarcaOnClicked(object? sender, System.EventArgs e)
    {
        string marca = etrMarca.Text;
        string obsMarca = edtOBSMarca.Text;

        // salvar no banco de dados 

        if (string.IsNullOrWhiteSpace(marca))
        {
            await DisplayAlert("ERRO", "O campo 'Marca' precisa ser preenchido!", "Ok");
            return;
        }

        await DisplayAlert("Marca Adicionada", $"Marca: {marca}\nObservações: {obsMarca}", "OK");

        etrMarca.Text = string.Empty;
        edtOBSMarca.Text = string.Empty;
    }
}