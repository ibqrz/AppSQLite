using AppVeiculos.Helpers;
using AppVeiculos.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq;
using System.Threading.Tasks;

namespace AppVeiculos;

public partial class Marca : ContentPage
{
    public Marca()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarMarcasNoContainer(string.Empty);
    }

    private async void btnAddMarcaOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMarca());
    }

    private async Task CarregarMarcasNoContainer(string searchTerm = "")
    {
        if (cardsContainer == null)
        {
            Console.WriteLine("ERRO: cardsContainer é nulo. XAML pode não ter sido inicializado.");
            return;
        }

        cardsContainer.Children.Clear();

        List<Marcas> marcas;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            marcas = await App.Db.GetAllMarca();
        }
        else
        {
            marcas = await App.Db.SearchMarca(searchTerm);
        }

        if (marcas != null && marcas.Any())
        {
            foreach (var mar in marcas)
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
                            new RowDefinition { Height = GridLength.Auto }
                        }
                    }
                };

                var gridContent = (Grid)frame.Content;

                var image = new Image
                {
                    Source = "protection.png",
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start,
                };
                gridContent.Children.Add(image);


                var labelId = new Label
                {
                    Text = $"ID: {mar.marId}",
                    TextColor = Colors.White,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelId, 1);
                Grid.SetRow(labelId, 0);
                gridContent.Children.Add(labelId);


                var labelMarca = new Label
                {
                    Text = $"Marca: {mar.marNome}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelMarca, 1);
                Grid.SetRow(labelMarca, 1);
                gridContent.Children.Add(labelMarca);


                var labelObs = new Label
                {
                    Text = $"Obs: {mar.marObs}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                Grid.SetColumn(labelObs, 1);
                Grid.SetRow(labelObs, 2);
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
                                var editMarcaPage = new EditMarca();
                                editMarcaPage.BindingContext = mar;
                                await Navigation.PushAsync(editMarcaPage);
                            })
                        },
                        new ImageButton
                        {
                            Source = "cancel.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                bool confirm = await DisplayAlert("Confirmação", $"Deseja realmente excluir a marca '{mar.marNome}'?", "Sim", "Não");
                                if (confirm)
                                {
                                    await App.Db.DeleteMarca(mar.marId);
                                    await DisplayAlert("Excluída", "A marca foi deletada!", "Ok");
                                    await CarregarMarcasNoContainer(SearchBarMarca.Text); 
                                }
                            })
                        }
                    }
                };
                Grid.SetRow(buttonsLayout, 3);
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
        await CarregarMarcasNoContainer(q);
    }
}