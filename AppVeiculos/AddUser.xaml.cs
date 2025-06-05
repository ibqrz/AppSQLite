using AppVeiculos.Models;

namespace AppVeiculos;

public partial class AddUser : ContentPage
{
	public AddUser()
	{
		InitializeComponent();
	}
    public AddUser(IEnumerable<Usuario> user, object userNome)
    {
        InitializeComponent();
        btnAddUser.Clicked += btnAddUserOnClicked;
    }

    private async void btnAddUserOnClicked(object? sender, System.EventArgs e)
    {
        Usuario user = new Usuario();
        user.userNome = etrNome.Text;
        user.userSenha = etrSenha.Text;

        await App.Db.InsertUsuario(user);
        await DisplayAlert("Sucesso!", "Usuario adicionado.", "OK");

        OnAppearing();

        if (string.IsNullOrWhiteSpace(user.userNome))
        {
            await DisplayAlert("ERRO", "Os campos precisam ser preenchidos!", "Ok");
            return;
        }

        await Navigation.PopAsync();
    }
}