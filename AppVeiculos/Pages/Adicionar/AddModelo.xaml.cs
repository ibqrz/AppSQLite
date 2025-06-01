using Microsoft.Maui.Controls;

using AppVeiculos.Models;
using AppVeiculos.Helpers;

namespace AppVeiculos;

public partial class AddModelo : ContentPage
{
    public AddModelo()
    {
        InitializeComponent();
    }

    public AddModelo(IEnumerable<AppVeiculos.Models.Modelo> mod, object modNome)
    {
        InitializeComponent();
        btnAddModelo.Clicked += btnAddModeloOnClicked;
    }

    private async void btnAddModeloOnClicked(object? sender, System.EventArgs e)
    {
        AppVeiculos.Models.Modelo mod = new AppVeiculos.Models.Modelo();
        mod.modNome = etrModelo.Text;
        mod.modObs = edtOBSModelo.Text;

        await App.Db.InsertModelo(mod);
        await DisplayAlert("Sucesso!", "Modelo adicionado.", "OK");

        OnAppearing();

        if (string.IsNullOrWhiteSpace(mod.modNome))
        {
            await DisplayAlert("ERRO", "O campo 'Modelo' precisa ser preenchido!", "Ok");
            return;
        }

        await Navigation.PopAsync();
    }
}