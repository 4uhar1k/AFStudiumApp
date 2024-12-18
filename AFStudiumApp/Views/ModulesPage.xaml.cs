using AFStudiumAPIClient;
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
}