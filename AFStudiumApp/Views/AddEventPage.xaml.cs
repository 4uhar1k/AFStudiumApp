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

	public async void AddStudents (object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ListOfStudentsPage(_apiClient, thisContext.EventId));
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
        Picker picker = (Picker)sender;
        if (picker.SelectedItem != null)
        {
            if (picker.SelectedItem.ToString() == "Klausur")
            {
                PermitReqCheck.IsVisible = true;
            }
            else
                PermitReqCheck.IsVisible = false;
        }


    }

	public void IsExamPicked(object sender, EventArgs e)
	{
		
	}

	public async void MessageClicked(object sender, EventArgs e)
	{
		if (MessageHeaderEntry.IsVisible == false)
		{
			ReceiverEntry.IsVisible = true;
            MessageHeaderEntry.IsVisible = true;
            MessageEntry.IsVisible = true;
        }
		else
		{
			Message message = new Message() { EventId = thisContext.EventId, SendFrom = thisContext.CurMatrikel, SendTo = Int32.Parse(ReceiverEntry.Text), MessageHeader = MessageHeaderEntry.Text, MessageText = MessageEntry.Text, MessageTime = DateTime.Now };
			await _apiClient.PostMessage(message);
			await DisplayAlert("", "Nachricht gesendet", "OK");
            ReceiverEntry.IsVisible = false;
            MessageHeaderEntry.IsVisible = false;
            MessageEntry.IsVisible = false;
        }
		
	}

	public void EventChecked(object sender, CheckedChangedEventArgs e)
	{
		RadioButton rb = (RadioButton)sender;
		switch (rb.Content)
		{
			case "Täglich":
                DaysOfWeekCollection.IsVisible = false;
                EventDatePicker.IsVisible = false;
				break;
			case "Wochentlich":
                DaysOfWeekCollection.IsVisible = true;
                EventDatePicker.IsVisible = false;
				break;
			case "Einzeln":
                DaysOfWeekCollection.IsVisible = false;
                EventDatePicker.IsVisible = true;
				break;
        }


	}
    public void WeeklyEventChecked(object sender, CheckedChangedEventArgs e)
    {
        
    }
    public void UniqueEventChecked(object sender, CheckedChangedEventArgs e)
    {
       
    }



}

