using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class ProfilePage : ContentPage
{
    public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
    private readonly AFStudiumAPIClientService _apiClient;
    public ProfilePage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
        _apiClient = apiClient;
        BindingContext = new ViewModelBase(apiClient);
	}

    public async void GoToModules(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EventsPage(_apiClient, false));
    }

    public async void GoToExams(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EventsPage(_apiClient, true));
    }
    public async void GoToLectures(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EventsPage(_apiClient, "Vorlesung"));
    }
    public async void LogOut(object sender, EventArgs e)
    {
        File.Delete(CurUserPath);
        await Navigation.PushModalAsync(new LoginPage(_apiClient));
    }
}