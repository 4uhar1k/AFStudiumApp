using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
namespace AFStudiumApp;

public partial class TimeTablePage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiService;
	public ModulesViewModel thisContext { get; set; }
	public TimeTablePage(AFStudiumAPIClientService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
		thisContext = new ModulesViewModel(_apiService);
        //thisContext.LoadEventsByDays(); 
		BindingContext = thisContext;
	}

	
}