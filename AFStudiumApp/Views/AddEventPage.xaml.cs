using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;

using Org.BouncyCastle.Tls;

namespace AFStudiumApp;

public partial class AddEventPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public ModulesViewModel thisContext { get; set; }
	public Subject subject { get; set; }
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
	public AddEventPage(AFStudiumAPIClientService apiClient, Event e, bool isAllowed)
    {
        InitializeComponent();
        _apiClient = apiClient;
		Event SelectedEvent = e;
		subject = new Subject();

		//var tasksubject = _apiClient.GetSubjects();//.Result.Where(n=> n.SubjectId == SelectedEvent.SubjectId).FirstOrDefault();(Task<Subject>)s
		
		
        thisContext = new ModulesViewModel(_apiClient);
		thisContext.EventId = SelectedEvent.EventId;
		thisContext.SubjectId = SelectedEvent.SubjectId;
		thisContext.EventName = SelectedEvent.EventName;//GetSubject(SelectedEvent).Result;
        thisContext.EventType = SelectedEvent.EventType;
		thisContext.IsTeacher = isAllowed;
		thisContext.IsStudent = !isAllowed;
        BindingContext = thisContext;
        GetSubject(e);
        EventNameEntry.Text = e.EventName;
		AddBtn.Command = thisContext.EditEvent;
    }

	public async void GetSubject(Event e)
	{
        //return await _apiClient.GetSubjectById(e.SubjectId);
        var tasksubject = await _apiClient.GetSubjects();
       if (tasksubject != null)
       {
            foreach (Subject sub in tasksubject)
            {
				if (sub.SubjectId == e.SubjectId)
					thisContext.SubjectName = sub.SubjectName;
            }
       }
	   
    }

	public void ChangeText(object sender, EventArgs e)
	{
		string[] eventname = EventNameEntry.Text.Split(' ');
		if (eventname.Length == 1)
		{
			EventNameEntry.Text += $" {EventPicker.SelectedItem.ToString()}";
		}
		else if (eventname.Length > 1)
		{

			//if (EventPicker.SelectedItem != null)
			//{
				EventNameEntry.Text = "";
			//          for (int i = 0; i < eventname.Length-1; i++)
			//          {
			//              EventNameEntry.Text += eventname[i];
			//          }
			EventNameEntry.Text += thisContext.SubjectName; 
            EventNameEntry.Text += $" {EventPicker.SelectedItem.ToString()}";
        }
			

        }
	}

