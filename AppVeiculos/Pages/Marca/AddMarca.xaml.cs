using Microsoft.Maui.Controls;

using AppVeiculos.Models;
using System.Collections.ObjectModel;

namespace AppVeiculos
{
    public partial class AddMarca : ContentPage
    {
        ObservableCollection<Marca> lista = new ObservableCollection<Marca>();
        public AddMarca()
        {
            InitializeComponent();
            btnAddMarca.Clicked += btnAddMarcaOnClicked;

            listMarcas.ItemsSource = lista;
        }

        protected override async void OnAppearing()
        {
            await carregarListaMarcas();
        }

        private async Task carregarListaMarcas()
        {
            List<Marca> tmp = await App.Db.GetAllMarca();

            lista.Clear();

            foreach (Marca marca in tmp)
            {
                lista.Add(marca);
            }
        }

        private async void btnAddMarcaOnClicked(object? sender, System.EventArgs e)
        {
            Marca mar = new Marca();
            mar.marNome = etrMarca.Text;
            mar.marObs = edtOBSMarca.Text;

            // salvar no banco de dados 

            await App.Db.InsertMarca(mar);

            if (string.IsNullOrWhiteSpace(mar.marNome))
            {
                await DisplayAlert("ERRO", "O campo 'Marca' precisa ser preenchido!", "Ok");
                return;
            }
        
            await DisplayAlert("Sucesso", "Marca adicionada!", "OK");
            await Shell.Current.GoToAsync("//Marca");

            etrMarca.Text = string.Empty;
            edtOBSMarca.Text = string.Empty;

            OnAppearing();
        }
    }
}
