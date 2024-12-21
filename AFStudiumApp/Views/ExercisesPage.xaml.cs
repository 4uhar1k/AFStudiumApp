using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class ExercisesPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public ExercisesPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		BindingContext = new ModulesViewModel(_apiClient);
	}
}