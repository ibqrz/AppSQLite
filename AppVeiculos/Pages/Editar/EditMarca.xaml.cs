using Microsoft.Maui.Controls;
using AppVeiculos.Models; 
using AppVeiculos.Helpers; 

namespace AppVeiculos;

public partial class EditMarca : ContentPage
{
    public EditMarca()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is Marcas marcaParaEditar)
        {
            etrMarca.Text = marcaParaEditar.marNome;
            edtOBSMarca.Text = marcaParaEditar.marObs;
        }
        else
        {
            await DisplayAlert("Erro", "Não foi possível carregar a marca para edição.", "OK");
        }
    }

    private async void btnEdtMarcaOnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is Marcas marcaParaAtualizar)
        {
            marcaParaAtualizar.marNome = etrMarca.Text;
            marcaParaAtualizar.marObs = edtOBSMarca.Text;

            await App.Db.UpdateMarca(marcaParaAtualizar);
            await DisplayAlert("Atenção", "Marca editada!", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Nenhuma marca válida para atualizar.", "OK");
        }
    }
}