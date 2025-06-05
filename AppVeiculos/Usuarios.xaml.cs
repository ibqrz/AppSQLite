using AppVeiculos.Models;

namespace AppVeiculos;

public partial class Usuarios : ContentPage
{
    public Usuarios()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarUsuarioNoContainer(string.Empty);
    }

    private async void btnAddUsuarioOnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddUser());
    }

    private async Task CarregarUsuarioNoContainer(string searchTerm = "")
    {
        if (cardsContainer == null)
        {
            Console.WriteLine("ERRO: cardsContainer é nulo. XAML pode não ter sido inicializado.");
            return;
        }

        cardsContainer.Children.Clear();

        List<Usuario> usuario;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            usuario = await App.Db.GetAllUsuario();
        }
        else
        {
            usuario = await App.Db.SearchUsuario(searchTerm);
        }

        if (usuario != null && usuario.Any())
        {
            foreach (var user in usuario)
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
                    Source = "imguser.png",
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
                    Text = $"ID: {user.userId}",
                    TextColor = Colors.White,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelId, 1);
                Grid.SetRow(labelId, 0);
                gridContent.Children.Add(labelId);


                var labelUsuario = new Label
                {
                    Text = $"Nome: {user.userNome}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid.SetColumn(labelUsuario, 1);
                Grid.SetRow(labelUsuario, 1);
                gridContent.Children.Add(labelUsuario);


                var labelSenha = new Label
                {
                    Text = $"Senha: {user.userSenha}",
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                Grid.SetColumn(labelSenha, 1);
                Grid.SetRow(labelSenha, 2);
                gridContent.Children.Add(labelSenha);


                var buttonsLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.End,
                    Spacing = 10,
                    Margin = new Thickness(0, 10, 0, 0),
                    Children =
                    {
                       //new ImageButton
                       // {
                            //Source = "edit.png",
                            //HeightRequest = 30,
                            //WidthRequest = 30,
                            //Command = new Command(async () =>
                            //{
                                //var editMarcaPage = new EditMarca();
                                //editMarcaPage.BindingContext = user;
                                //await Navigation.PushAsync(editMarcaPage);
                            //})
                        //},
                        new ImageButton
                        {
                            Source = "cancel.png",
                            HeightRequest = 30,
                            WidthRequest = 30,
                            Command = new Command(async () =>
                            {
                                bool confirm = await DisplayAlert("Confirmação", $"Deseja realmente excluir a marca '{user.userNome}'?", "Sim", "Não");
                                if (confirm)
                                {
                                    await App.Db.DeleteUsuario(user.userId);
                                    await DisplayAlert("Excluída", "A marca foi deletada!", "Ok");
                                    await CarregarUsuarioNoContainer(SearchBarUsuario.Text);
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
        await CarregarUsuarioNoContainer(q);
    }
}