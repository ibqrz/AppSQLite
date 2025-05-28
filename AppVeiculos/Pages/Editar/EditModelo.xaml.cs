using AppVeiculos.Models;

namespace AppVeiculos;

public partial class EditModelo : ContentPage
{

    public EditModelo() 
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AppVeiculos.Models.Modelo modeloParaEditar)
        {
            etrModelo.Text = modeloParaEditar.modNome;
            edtOBSModelo.Text = modeloParaEditar.modObs;
        }
        else
        {
            DisplayAlert("Erro", "Não foi possível carregar o modelo para edição.", "OK");
        }
    }

    private async void btnEdtModeloOnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is AppVeiculos.Models.Modelo modeloParaAtualizar)
        {
            modeloParaAtualizar.modNome = etrModelo.Text;
            modeloParaAtualizar.modObs = edtOBSModelo.Text;

            await App.Db.UpdateModelo(modeloParaAtualizar);
            await DisplayAlert("Atenção", "Modelo editado!", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Nenhum modelo válido para atualizar.", "OK");
        }
    }
}