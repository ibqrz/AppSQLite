using Microsoft.Maui.Controls;
using AppVeiculos.Models;
using AppVeiculos.Helpers;
using System.Collections.Generic;
using System.Linq;
using System; 

namespace AppVeiculos;

public partial class EdtVeiculo : ContentPage
{
    private List<Marcas>? _marcas = new List<Marcas>();
    private List<AppVeiculos.Models.Modelo>? _modelos = new List<AppVeiculos.Models.Modelo>();

    public EdtVeiculo()
    {
        InitializeComponent();

        CarregarPickers();
    }

    private async 
    Task
    CarregarPickers()
    {
        try
        {
            _marcas = await App.Db.GetAllMarca();
            if (_marcas != null && _marcas.Any())
            {
                pkMar.ItemsSource = _marcas.Select(m => m.marNome).ToList();
            }

            _modelos = await App.Db.GetAllModelo();
            if (_modelos != null && _modelos.Any())
            {
                pkMod.ItemsSource = _modelos.Select(m => m.modNome).ToList();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro ao Carregar Listas", $"Ocorreu um erro ao carregar marcas ou modelos: {ex.Message}", "OK");
            await Navigation.PopAsync(); 
        }
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_marcas == null || !_marcas.Any() || _modelos == null || !_modelos.Any())
        {
            await CarregarPickers();
        }


        if (BindingContext is Veiculo veiculoParaEditar)
        {
            etrVeiculo.Text = veiculoParaEditar.veiNome;
            edtObsVeiculo.Text = veiculoParaEditar.veiObs;
            etrAnoFab.Text = veiculoParaEditar.veiAnoFab.ToString();

            var marcaSelecionada = _marcas.FirstOrDefault(m => m.marId == veiculoParaEditar.codMar);
            if (marcaSelecionada != null)
            {
                pkMar.SelectedItem = marcaSelecionada.marNome;
            }
            else
            {
                pkMar.SelectedItem = null;
            }

            var modeloSelecionado = _modelos.FirstOrDefault(m => m.modId == veiculoParaEditar.codMod);
            if (modeloSelecionado != null)
            {
                pkMod.SelectedItem = modeloSelecionado.modNome;
            }
            else
            {
                pkMod.SelectedItem = null;
            }
        }
        else
        {
            await DisplayAlert("Erro", "Não foi possível carregar o veículo para edição.", "OK");
            await Navigation.PopAsync(); 
        }
    }

    private async void btnEditVeiculoOnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is Veiculo veiculoParaAtualizar)
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

            veiculoParaAtualizar.veiNome = etrVeiculo.Text;
            veiculoParaAtualizar.veiAnoFab = anoFab; 
            veiculoParaAtualizar.veiObs = edtObsVeiculo.Text;
            veiculoParaAtualizar.codMar = selectedMarcaId.Value;
            veiculoParaAtualizar.codMod = selectedModeloId.Value; 

            try
            {
                await App.Db.UpdateVeiculo(veiculoParaAtualizar);
                await DisplayAlert("Sucesso!", "Veículo editado!", "Ok");
                await Navigation.PopAsync(); 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao atualizar o veículo: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Erro", "Nenhum veículo válido para atualizar.", "OK");
        }
    }
}