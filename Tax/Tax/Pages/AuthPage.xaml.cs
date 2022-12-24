using Tax.Models.User;
using System.Text.Json;

namespace Tax;

public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
	}

    private async void OnSignIn(System.Object sender, System.EventArgs e)
    {
        var client = new HTTPClient.HTTPClient();
        var email = Email.Text;
        var password = Password.Text;
        var credentials = new { email, password };

        Button.Opacity = 0.5;
        Button.InputTransparent = true;

        var user = await client.PostAsync("http://localhost:5234/api/Auth/sign-in", JsonSerializer.Serialize(credentials));

        if (user != null)
        {
            await Shell.Current.GoToAsync("//IndexPage");
        }

        Button.Opacity = 1;
        Button.InputTransparent = false;
    }
}
