using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class LecturesPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiService;
	public LecturesPage(AFStudiumAPIClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
		BindingContext = new ModulesViewModel(_apiService);
	}
}