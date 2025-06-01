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
            await DisplayAlert("Erro", "N�o foi poss�vel carregar a marca para edi��o.", "OK");
        }
    }

    private async void btnEdtMarcaOnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is Marcas marcaParaAtualizar)
        {
            marcaParaAtualizar.marNome = etrMarca.Text;
            marcaParaAtualizar.marObs = edtOBSMarca.Text;

            await App.Db.UpdateMarca(marcaParaAtualizar);
            await DisplayAlert("Aten��o", "Marca editada!", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Nenhuma marca v�lida para atualizar.", "OK");
        }
    }
}