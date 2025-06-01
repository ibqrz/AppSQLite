using AppVeiculos.Helpers;
using AppVeiculos.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVeiculos;

public partial class AddVeiculo : ContentPage
{
    private List<Marcas>? _marcas;
    private List<AppVeiculos.Models.Modelo>? _modelos;

    public AddVeiculo()
    {
        InitializeComponent();
        CarregarMar();
        CarregarMod();
    }

    // Este construtor parece ser um resqu�cio ou est� sendo usado de forma incorreta.
    // Se a inten��o � editar um ve�culo, o BindingContext deve ser definido
    // antes de navegar para a p�gina EdtVeiculo.
    // Mantenho-o comentado ou removido para evitar confus�o no fluxo de "adi��o".
    /*
    public AddVeiculo(IEnumerable<Veiculo> vei, object veiNome)
    {
        InitializeComponent();
        btnAddVeiculo.Clicked += btnAddVeiculoOnClicked;
        // L�gica para carregar os dados do ve�culo para edi��o, se for o caso
        // Esta p�gina � AddVeiculo, ent�o este construtor n�o � apropriado para ela.
    }
    */

    private async void CarregarMar()
    {
        _marcas = await App.Db.GetAllMarca();
        if (_marcas != null && _marcas.Any())
        {
            pkMar.ItemsSource = _marcas.Select(m => m.marNome).ToList();
        }
    }

    private async void CarregarMod()
    {
        _modelos = await App.Db.GetAllModelo();
        if (_modelos != null && _modelos.Any())
        {
            pkMod.ItemsSource = _modelos.Select(m => m.modNome).ToList();
        }
    }

    private async void btnAddVeiculoOnClicked(object? sender, System.EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(etrVeiculo.Text) ||
            string.IsNullOrWhiteSpace(etrAnoFab.Text) ||
            pkMar.SelectedIndex == -1 || 
            pkMod.SelectedIndex == -1)   
        {
            await DisplayAlert("ERRO", "Todos os campos (Nome, Ano, Marca, Modelo) precisam ser preenchidos!", "Ok");
            return; 
        }

        if (!int.TryParse(etrAnoFab.Text, out int anoFab))
        {
            await DisplayAlert("ERRO", "Ano de Fabrica��o deve ser um n�mero v�lido.", "Ok");
            return;
        }

        int? selectedMarcaId = null;
        if (pkMar.SelectedIndex != -1 && _marcas != null)
        {
            selectedMarcaId = _marcas[pkMar.SelectedIndex].marId;
        }

        int? selectedModeloId = null;
        if (pkMod.SelectedIndex != -1 && _modelos != null)
        {
            selectedModeloId = _modelos[pkMod.SelectedIndex].modId;
        }

        if (!selectedMarcaId.HasValue || !selectedModeloId.HasValue)
        {
            await DisplayAlert("ERRO", "N�o foi poss�vel obter o ID da Marca ou Modelo selecionado.", "Ok");
            return;
        }

        Veiculo vei = new Veiculo
        {
            veiNome = etrVeiculo.Text,
            veiAnoFab = anoFab,
            veiObs = edtObsVeiculo.Text,
            codMar = selectedMarcaId.Value, 
            codMod = selectedModeloId.Value 
        };

        try
        {
            await App.Db.InsertVeiculo(vei);
            await DisplayAlert("Sucesso!", "Ve�culo adicionado.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao adicionar o ve�culo: {ex.Message}", "OK");
        }
    }
}