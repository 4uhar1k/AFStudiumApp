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
		thisContext = new ModulesViewModel(_apiClient);
		//ModulesViewModel thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectName = subject.SubjectName;
		thisContext.SubjectId = subject.SubjectId;
		
		BindingContext = thisContext;
        EventNameEntry.Text = subject.SubjectName;
    }
	public AddEventPage(AFStudiumAPIClientService apiClient, Event e)
    {
        InitializeComponent();
        _apiClient = apiClient;
		Event SelectedEvent = e;
		
		
		//Task<Subject> tasksubject =  (Task<Subject>)s;//_apiClient.GetSubjects().Result.Where(n=> n.SubjectId == SelectedEvent.SubjectId).FirstOrDefault();
		
        thisContext = new ModulesViewModel(_apiClient);
		thisContext.EventId = SelectedEvent.EventId;
		thisContext.SubjectId = SelectedEvent.SubjectId;
		thisContext.EventName = SelectedEvent.EventName;//GetSubject(SelectedEvent).Result;
        thisContext.EventType = SelectedEvent.EventType;
        BindingContext = thisContext;
		EventNameEntry.Text = e.EventName;
		AddBtn.Command = thisContext.EditEvent;
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

	public void ChangeText(object sender, EventArgs e)
	{
		string[] eventname = EventNameEntry.Text.Split(' ');
		if (eventname.Length == 1)
		{
			EventNameEntry.Text += $" {EventPicker.SelectedItem.ToString()}";
		}
		else if (eventname.Length == 2)
		{
			EventNameEntry.Text = eventname[0] + $" {EventPicker.SelectedItem.ToString()}";

        }
	}
}