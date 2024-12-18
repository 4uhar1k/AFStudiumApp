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
}