using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class AddModulPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public AddModulPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		BindingContext = new ModulesViewModel(_apiClient);
	}
}