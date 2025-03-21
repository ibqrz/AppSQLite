namespace AppVeiculos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLoginOnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void btnCadastrarOnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
        }
    }
}
