using Microsoft.Maui.Controls;

using AppVeiculos.Models;
using AppVeiculos.Helpers;

namespace AppVeiculos;

public partial class AddMarca : ContentPage
{
    public AddMarca()
    {
        InitializeComponent();   
    }

    public AddMarca(IEnumerable<Marcas> mar, object marNome)
    {
        InitializeComponent();
        btnAddMarca.Clicked += btnAddMarcaOnClicked;
    }

    private async void btnAddMarcaOnClicked(object? sender, System.EventArgs e)
    {
        Marcas mar = new Marcas();
        mar.marNome = etrMarca.Text;
        mar.marObs = edtOBSMarca.Text;

        await App.Db.InsertMarca(mar);
        await DisplayAlert("Sucesso!", "Marca adicionada.", "OK");

        OnAppearing();

        if (string.IsNullOrWhiteSpace(mar.marNome))
        {
            await DisplayAlert("ERRO", "O campo 'Marca' precisa ser preenchida!", "Ok");
            return;
        }

        await Navigation.PopAsync();
    }
}