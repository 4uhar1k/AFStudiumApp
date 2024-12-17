namespace AFStudiumApp;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    public async void GoToModules(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModulesPage());
    }

    public async void GoToExams(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExamsPage());
    }
}