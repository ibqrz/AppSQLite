using AppVeiculos.Helpers;
using AppVeiculos.Models;
using Microsoft.Maui.Controls;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVeiculos;

public partial class Veiculos : ContentPage
{
    private List<Marcas> ? _marcas;
    private List<AppVeiculos.Models.Modelo> ? _modelos;

    public Veiculos()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadReferencedData();
        await CarregarVeiculoNoContainer(string.Empty);
    }

    private async Task LoadReferencedData()
    {
        if (App.Db == null)
        {
            Console.WriteLine("ERRO: App.Db não foi inicializado. Certifique-se de que seu App.xaml.cs inicializa o banco de dados.");
            return;
        }

        try
        {
            _marcas = await App.Db.GetAllMarca();
            _modelos = await App.Db.GetAllModelo();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar dados de referência: {ex.Message}");
            await DisplayAlert("Erro", "Não foi possível carregar as marcas e modelos. Tente novamente.", "Ok");
        }
    }

    private async void btnAddVeiculoOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddVeiculo());
    }

    private async Task CarregarVeiculoNoContainer(string searchTerm = "")
    {
        if (cardsContainer == null)
        {
            Console.WriteLine("ERRO: cardsContainer é nulo. XAML pode não ter sido inicializado.");
            return;
        }

        cardsContainer.Children.Clear();

        List<Veiculo> veiculos;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            veiculos = await App.Db.GetAllVeiculo();
        }
        else
        {
            veiculos = await App.Db.SearchVeiculo(searchTerm);
        }

        if (veiculos != null && veiculos.Any())
        {
            foreach (var vei in veiculos)
            {
                var frame = new Frame
                {
                    BackgroundColor = Color.FromArgb("#404040"),
                    Padding = 10,
                    WidthRequest = 350,
                    CornerRadius = 10,
                    Margin = new Thickness(10),
                    HasShadow = true,
                    Content = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star }
                        },
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto }
                        }
                    }
                };

                var gridContent = (Grid)frame.Content;

                var image = new Image
                {
                    Source = "vehicles.png",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                };
                gridContent.Children.Add(image);
                Grid.SetColumn(image, 0);
                Grid.SetRow(image, 0);
                Grid.SetRowSpan(image, 2);


                var labelId = new Label
                {
                    Text = $"ID: {vei.veiId}",
                    TextColor = Colors.White,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelId, 1);
                Grid.SetRow(labelId, 0);
                gridContent.Children.Add(labelId);


                var labelVeiculo = new Label
                {
                    Text = $"Veículo: {vei.veiNome}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelVeiculo, 1);
                Grid.SetRow(labelVeiculo, 1);
                gridContent.Children.Add(labelVeiculo);


                var labelAnoFab = new Label
                {
                    Text = $"Ano: {vei.veiAnoFab}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelAnoFab, 1);
                Grid.SetRow(labelAnoFab, 2);
                gridContent.Children.Add(labelAnoFab);

                var marcaAssociada = _marcas?.FirstOrDefault(m => m.marId == vei.codMar);
                var modeloAssociado = _modelos?.FirstOrDefault(m => m.modId == vei.codMod);

                var labelCodMar = new Label
                {
                    Text = $"Marca:  {(marcaAssociada != null ? marcaAssociada.marNome : "N/A")}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelCodMar, 1);
                Grid.SetRow(labelCodMar, 3);
                gridContent.Children.Add(labelCodMar);


                var labelCodMod = new Label
                {
                    Text = $"Modelo: {(modeloAssociado != null ? modeloAssociado.modNome : "N/A")}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelCodMod, 1);
                Grid.SetRow(labelCodMod, 4);
                gridContent.Children.Add(labelCodMod);


                var labelObs = new Label
                {
                    Text = $"Obs: {vei.veiObs}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                Grid.SetColumn(labelObs, 1);
                Grid.SetRow(labelObs, 5);
                gridContent.Children.Add(labelObs);


                var buttonsLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.End,
                    Spacing = 10,
                    Margin = new Thickness(0, 10, 0, 0),
                    Children =
                    {
                        new ImageButton
                        {
                            Source = "edit.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                var editVeiculoPage = new EdtVeiculo();
                                editVeiculoPage.BindingContext = vei;
                                await Navigation.PushAsync(editVeiculoPage);
                            })
                        },
                        new ImageButton
                        {
                            Source = "cancel.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                bool confirm = await DisplayAlert("Confirmação", $"Deseja realmente excluir o veículo '{vei.veiNome}'?", "Sim", "Não");
                                if (confirm)
                                {
                                    await App.Db.DeleteVeiculo(vei.veiId);
                                    await DisplayAlert("Excluída", "O veículo foi deletado!", "Ok");
                                    await CarregarVeiculoNoContainer(SearchBarVeiculo.Text);
                                }
                            })
                        }
                    }
                };
                Grid.SetRow(buttonsLayout, 6);
                Grid.SetColumn(buttonsLayout, 0);
                Grid.SetColumnSpan(buttonsLayout, 2);
                gridContent.Children.Add(buttonsLayout);

                cardsContainer.Children.Add(frame);
            }
        }
    }

    private async void txtSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        await CarregarVeiculoNoContainer(q);
    }
}