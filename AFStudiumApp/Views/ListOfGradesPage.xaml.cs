using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class ListOfGradesPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public ModulesViewModel thisContext { get; set; }
	public ListOfGradesPage(AFStudiumAPIClientService apiClient, Event e)
	{
		InitializeComponent();
		_apiClient = apiClient;
		thisContext = new ModulesViewModel(_apiClient);
		thisContext.LoadStudentsOfEvent(e.EventId);
        BindingContext = thisContext;
	}

	public async void GoBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}