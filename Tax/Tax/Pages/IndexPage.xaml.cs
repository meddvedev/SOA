using Tax.Models.User;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace Tax;

public partial class IndexPage : ContentPage
{
    public Insurance ObservableCollection<Insurace> { get; set; }

    public IndexPage()
	{
		InitializeComponent();
        Insurance = new ObservableCollection<Insurance>();
    }

    public string Title { get; } = string.Empty;
    public string Category { get; } = string.Empty;
    public string Date { get; } = string.Empty;
    public int InsuranceAmount { get; }


	public void ObserveInsurance ()
	{
        var client = new HTTPClient.HTTPClient();
        var email = Email.Text;
        var password = Password.Text;
        var credentials = new { email, password };

        var token = await client.PostAsync("http://localhost:5234/api/Auth/getToken", JsonSerializer.Serialize(credentials));

        var insurance = await client.PostAsync("http://localhost:5234/api/insurance/getList", JsonSerializer.Serialize(token));

        Insurance.Add(insurance);
    }
}
