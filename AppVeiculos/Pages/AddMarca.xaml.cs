using Microsoft.Maui.Controls;

namespace AppVeiculos;

public partial class AddMarca : ContentPage
{
    public AddMarca()
    {
        InitializeComponent();
        btnAddMarca.Clicked += btnAddMarcaOnClicked;
    }

    private async void btnAddMarcaOnClicked(object? sender, System.EventArgs e)
    {
        string marca = etrMarca.Text;
        string obsMarca = edtOBSMarca.Text;

        // salvar no banco de dados 

        if (string.IsNullOrWhiteSpace(marca))
        {
            DisplayAlert("ERRO", "O campo 'Marca' precisa ser preenchido!", "Ok");
            return;
        }
        
        await DisplayAlert("Marca Adicionada", $"Marca: {marca}\nObservações: {obsMarca}", "OK");
            
        etrMarca.Text = string.Empty;
        edtOBSMarca.Text = string.Empty;
    }
}