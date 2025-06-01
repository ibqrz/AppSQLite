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

    // Este construtor parece ser um resquício ou está sendo usado de forma incorreta.
    // Se a intenção é editar um veículo, o BindingContext deve ser definido
    // antes de navegar para a página EdtVeiculo.
    // Mantenho-o comentado ou removido para evitar confusão no fluxo de "adição".
    /*
    public AddVeiculo(IEnumerable<Veiculo> vei, object veiNome)
    {
        InitializeComponent();
        btnAddVeiculo.Clicked += btnAddVeiculoOnClicked;
        // Lógica para carregar os dados do veículo para edição, se for o caso
        // Esta página é AddVeiculo, então este construtor não é apropriado para ela.
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
            await DisplayAlert("ERRO", "Ano de Fabricação deve ser um número válido.", "Ok");
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
            await DisplayAlert("ERRO", "Não foi possível obter o ID da Marca ou Modelo selecionado.", "Ok");
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
            await DisplayAlert("Sucesso!", "Veículo adicionado.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao adicionar o veículo: {ex.Message}", "OK");
        }
    }
}