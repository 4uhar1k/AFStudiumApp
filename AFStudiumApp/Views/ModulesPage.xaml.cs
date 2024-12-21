using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class ModulesPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public ModulesPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		BindingContext = new ModulesViewModel(_apiClient);

	}

	public async void AddModul(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddModulPage(_apiClient));
	}

	public async void ShowModul(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count > 0)
		{
			var SelectedSubject = (Subject)e.CurrentSelection[0];
			await Navigation.PushAsync(new AboutModulPage(_apiClient, SelectedSubject));
		}
	}
    public async void EditModule(object sender, EventArgs e)
    {
        Button EditBtn = (Button)sender;
        Subject sub = (Subject)EditBtn.CommandParameter;
        
        await Navigation.PushAsync(new AddModulPage(_apiClient, sub));
    }
}