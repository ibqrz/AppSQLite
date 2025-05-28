using System.Collections.ObjectModel;

using AppVeiculos.Helpers;
using AppVeiculos.Models;


namespace AppVeiculos;

public partial class Modelo : ContentPage
{
    ObservableCollection<AppVeiculos.Models.Modelo> lista = new ObservableCollection<AppVeiculos.Models.Modelo>();

    /* ou - trabalha de maneira não assincrona:  
      List<Modelo> lista = new List<Modelo>(); */

    public Modelo()
	{
		InitializeComponent();

        listModelos.ItemsSource = lista;
    }

    protected override async void OnAppearing()
    {
        await carregarListaModelos();
    }

    private async Task carregarListaModelos()
    {
        List<AppVeiculos.Models.Modelo> tmp = await App.Db.GetAllModelo();

        lista.Clear();

        foreach (AppVeiculos.Models.Modelo modelo in tmp)
        {
            lista.Add(modelo);
        }
    }

    private async void btnAddModeloOnClicked(object sender, EventArgs e)
    {
        AppVeiculos.Models.Modelo mod = new AppVeiculos.Models.Modelo();
        mod.modNome = etrModelo.Text;
        mod.modObs = edtOBSModelo.Text;

        await App.Db.InsertModelo(mod);
        await DisplayAlert("Sucesso!", "Modelo adicionado.", "OK");

        OnAppearing();
    }

    private async void btnAlterar(object sender, EventArgs e)
    {
        MenuItem? selecionado = sender as MenuItem;
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
        AppVeiculos.Models.Modelo? p = selecionado.BindingContext as AppVeiculos.Models.Modelo;
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.


        await Navigation.PushAsync(page: new EditModelo { BindingContext = p });
    }

    private void listModelosItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        AppVeiculos.Models.Modelo? p = e.SelectedItem as AppVeiculos.Models.Modelo;

        p.modId.ToString();
        etrModelo.Text = p.modNome.ToString();
        edtOBSModelo.Text = p.modObs.ToString();
    }


    private async void btnRemover(object sender, EventArgs e)
    {
        MenuItem? selecionado = sender as MenuItem;
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
        AppVeiculos.Models.Modelo? p = selecionado.BindingContext as AppVeiculos.Models.Modelo;
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

        bool confirma = await DisplayAlert("Atenção", "Confirma a remoção?", "Sim", "Não");

        if (confirma == true)
        {
            await App.Db.DeleteModelo(p.modId);
            await DisplayAlert("Excluído", "O modelo foi deletado!", "Ok");

            await carregarListaModelos();
        } 
    }

    private void btnLimparOnClicked(object sender, EventArgs e)
    {
        etrModelo.Text = String.Empty;
        edtOBSModelo.Text = String.Empty;
    }

    private async void txtSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<AppVeiculos.Models.Modelo> tmp = await App.Db.SearchModelo(q);

        foreach (AppVeiculos.Models.Modelo modelo in tmp)
        {
            lista.Add(modelo);
        }
    }

}