using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
using Org.BouncyCastle.Tls;

namespace AFStudiumApp;

public partial class AddEventPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public ModulesViewModel thisContext { get; set; }
	public AddEventPage(AFStudiumAPIClientService apiClient, Subject subject)
	{
		InitializeComponent();
		_apiClient = apiClient;
		//ModulesViewModel thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectName = subject.SubjectName;
		thisContext.SubjectId = subject.SubjectId;
		BindingContext = thisContext;
	}
	public AddEventPage(AFStudiumAPIClientService apiClient, Event e)
    {
        InitializeComponent();
        _apiClient = apiClient;
		Event SelectedEvent = e;
		
		
		//Task<Subject> tasksubject =  (Task<Subject>)s;//_apiClient.GetSubjects().Result.Where(n=> n.SubjectId == SelectedEvent.SubjectId).FirstOrDefault();
		
        ModulesViewModel thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectName = "smth";//GetSubject(SelectedEvent).Result;
        thisContext.EventType = SelectedEvent.EventType;
        BindingContext = thisContext;
    }

	public async Task<string> GetSubject(Event e)
	{
        //return await _apiClient.GetSubjectById(e.SubjectId);
        var tasksubject = await _apiClient.GetSubjectById(e.SubjectId);
       if (tasksubject != null)
       {
            return tasksubject.SubjectName;
       }
	   else
	{
			return "";
	}
    }
}