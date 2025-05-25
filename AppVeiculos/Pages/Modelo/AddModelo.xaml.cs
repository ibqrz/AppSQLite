
using System.Collections.ObjectModel;
using SQLite;
using AppVeiculos.Models;
using Microsoft.Maui.ApplicationModel;

namespace AppVeiculos;

public partial class AddModelo : ContentPage
{
    ObservableCollection<Modelo> lista = new ObservableCollection<Modelo>();
    public AddModelo()
    {
        InitializeComponent();
        btnAddModelo.Clicked += btnAddModeloOnClicked;

        listModelo.ItemsSource = lista;
    }

    protected override async void OnAppearing()
    {
        await carregarListaModelo();
    }

    private async Task carregarListaModelo()
    {
        List<Modelo> tmp = await App.Db.GetAllModelo();

        lista.Clear();

        foreach (Modelo modelo in tmp)
        {
            lista.Add(modelo);
        }
    }

    private async void btnAddModeloOnClicked(object? sender, EventArgs e)
	{
        Modelo mod = new Modelo();
		mod.modNome = etrModelo.Text;
		mod.modObs = edtOBSModelo.Text;

        // salvar no banco de dados

        await App.Db.InsertModelo(mod);

        if (string.IsNullOrWhiteSpace(mod.modNome))
		{
			DisplayAlert("ERRO", "O campo 'Modelo' precisa ser preenchido!", "OK");
			return;
		}

		await DisplayAlert("Sucesso", "Modelo Adicionado", "OK");
        await Shell.Current.GoToAsync("//Modelo");

        etrModelo.Text = string.Empty;
		edtOBSModelo.Text = string.Empty;

        OnAppearing();
    }
}