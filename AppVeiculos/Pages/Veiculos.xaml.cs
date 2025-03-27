using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace AppVeiculos;

public partial class Veiculos : ContentPage
{
	public Veiculos()
	{
		InitializeComponent();
	}

	private async void btnAddVeiculoOnClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddVeiculo());
	}

	private async void imgEditOnClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EdtVeiculo());
	}

	private async void imgDeleteOnClicked(object sender, EventArgs e)
	{
		DisplayAlert("Excluído", "O veículo foi deletado!", "Ok");
	}
}