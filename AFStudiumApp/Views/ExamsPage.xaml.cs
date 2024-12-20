using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class ExamsPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiService;
	public ExamsPage(AFStudiumAPIClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
		BindingContext = new ModulesViewModel(_apiService);
	}
}