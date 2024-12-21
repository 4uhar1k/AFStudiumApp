using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class AddEventPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public AddEventPage(AFStudiumAPIClientService apiClient, Subject subject)
	{
		InitializeComponent();
		_apiClient = apiClient;
		ModulesViewModel thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectName = subject.SubjectName;
		thisContext.SubjectId = subject.SubjectId;
		BindingContext = thisContext;
	}
	public AddEventPage(AFStudiumAPIClientService apiClient, object e)
    {
        InitializeComponent();
        _apiClient = apiClient;
		Event SelectedEvent = (Event)e;
		//Subject SelectedSubject = _apiClient.GetSubjectById(SelectedEvent.SubjectId).Result;
        ModulesViewModel thisContext = new ModulesViewModel(_apiClient);

        thisContext.SubjectName = "smth";
        thisContext.EventType = SelectedEvent.EventType;
        BindingContext = thisContext;
    }
}