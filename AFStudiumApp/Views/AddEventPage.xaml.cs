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
	public Event SelectedEvent { get; set; }
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
		AddStudentBtn.IsVisible = false;
		SendMessageBtn.IsVisible = false;
		AddGradesBtn.IsVisible = false;
        AddBtn.Command = thisContext.AddEvent;
    }
	public AddEventPage(AFStudiumAPIClientService apiClient, Event e, bool isAllowed)
    {
        InitializeComponent();
        _apiClient = apiClient;
		SelectedEvent = e;
		subject = new Subject();
		DateTime dt = new DateTime();
		//var tasksubject = _apiClient.GetSubjects();//.Result.Where(n=> n.SubjectId == SelectedEvent.SubjectId).FirstOrDefault();(Task<Subject>)s
		if (SelectedEvent.EventType != "Klausur")
            AddGradesBtn.IsVisible = false;


        thisContext = new ModulesViewModel(_apiClient);
		thisContext.EventId = SelectedEvent.EventId;
		thisContext.SubjectId = SelectedEvent.SubjectId;
		thisContext.EventName = SelectedEvent.EventName;//GetSubject(SelectedEvent).Result;
        thisContext.EventType = SelectedEvent.EventType;
		thisContext.Credits = SelectedEvent.Credits;
		thisContext.StudentsAmount = SelectedEvent.StudentsAmount;
		if (SelectedEvent.EventType != "Studienleistung")
		{
            if (DateTime.TryParseExact(SelectedEvent.Date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
            {
                thisContext.Date = DateTime.ParseExact(SelectedEvent.Date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                EventRadioBtn3.IsChecked = true;
            }
            else
            {
                thisContext.WeeklyEventText = SelectedEvent.Date;
                if (SelectedEvent.Date == "Montag Dienstag Mittwoch Donnerstag Freitag")
                    EventRadioBtn1.IsChecked = true;
                else
                    EventRadioBtn2.IsChecked = true;
            }

            thisContext.BeginTime = TimeSpan.Parse(SelectedEvent.Time.Split('-')[0]);
            thisContext.EndTime = TimeSpan.Parse(SelectedEvent.Time.Split('-')[1]);
        }
        else
        {
            
            EventPicker.IsEnabled = false;
            PermitReqCheck.IsVisible = false;
            CreditsEntry.IsVisible = false;
            LocationEntry.IsVisible = false;
            HaufigkeitsStack.IsVisible = false;
            WeeklyEventLabel.IsVisible = false;
            MondayCheck.IsVisible = false;
            TuesdayCheck.IsVisible = false;
            WednesdayCheck.IsVisible = false;
            ThursdayCheck.IsVisible = false;
            FridayCheck.IsVisible = false;
            EventDatePicker.IsVisible = false;
            BeginTimePicker.IsVisible = false;
            EndTimePicker.IsVisible = false;
            SendMessageBtn.IsVisible = false;
            AddGradesBtn.IsVisible = false;
        }
        
		
		thisContext.Location = SelectedEvent.Location;
		thisContext.PermitRequired = SelectedEvent.PermitRequired;
		thisContext.OldPermitRequired = SelectedEvent.PermitRequired;
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
				//DaysOfWeekCollection.IsVisible = false;
				MondayCheck.IsVisible = false;
				TuesdayCheck.IsVisible = false;
				WednesdayCheck.IsVisible = false;
				ThursdayCheck.IsVisible = false;
				FridayCheck.IsVisible = false;
                EventDatePicker.IsVisible = false;
				thisContext.WeeklyEventText = "Montag Dienstag Mittwoch Donnerstag Freitag";
				WeeklyEventLabel.IsVisible = false;
				break;
			case "Wochentlich":
                //DaysOfWeekCollection.IsVisible = true;
                MondayCheck.IsVisible = thisContext.IsTeacher;
                TuesdayCheck.IsVisible = thisContext.IsTeacher;
                WednesdayCheck.IsVisible = thisContext.IsTeacher;
                ThursdayCheck.IsVisible = thisContext.IsTeacher;
                FridayCheck.IsVisible = thisContext.IsTeacher;
                EventDatePicker.IsVisible = false;
                thisContext.WeeklyEventText = "";
                if (MCheck.IsChecked)
                {
                    thisContext.WeeklyEventText += "Montag ";
                }
                if (TCheck.IsChecked)
                {
                    thisContext.WeeklyEventText += "Dienstag ";
                }
                if (WCheck.IsChecked)
                {
                    thisContext.WeeklyEventText += "Mittwoch ";
                }
                if (ThCheck.IsChecked)
                {
                    thisContext.WeeklyEventText += "Donnerstag ";
                }
                if (FCheck.IsChecked)
                {
                    thisContext.WeeklyEventText += "Freitag ";
                }
                /*for (int i = 0; i < DaysOfWeekCollection.SelectedItems.Count; i++)
				{
                    thisContext.WeeklyEventText += $" {DaysOfWeekCollection.SelectedItems[i]}";
				}*/
                WeeklyEventLabel.IsVisible = true;				
                break;
			case "Einzeln":
                //DaysOfWeekCollection.IsVisible = false;
                MondayCheck.IsVisible = false;
                TuesdayCheck.IsVisible = false;
                WednesdayCheck.IsVisible = false;
                ThursdayCheck.IsVisible = false;
                FridayCheck.IsVisible = false;
                EventDatePicker.IsVisible = true;
                thisContext.WeeklyEventText = "";
                WeeklyEventLabel.IsVisible = false;
                break;
        }


	}
    public async void AddGrades(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ListOfGradesPage(_apiClient, SelectedEvent));
	}

	public void UpdateWeeklyLabel(object sender, CheckedChangedEventArgs e)
	{
        thisContext.WeeklyEventText = "";
		if (MCheck.IsChecked)
		{
			thisContext.WeeklyEventText += "Montag ";
        }
        if (TCheck.IsChecked)
        {
            thisContext.WeeklyEventText += "Dienstag ";
        }
        if (WCheck.IsChecked)
        {
            thisContext.WeeklyEventText += "Mittwoch ";
        }
        if (ThCheck.IsChecked)
        {
            thisContext.WeeklyEventText += "Donnerstag ";
        }
        if (FCheck.IsChecked)
        {
            thisContext.WeeklyEventText += "Freitag ";
        }
        /*if (e.CurrentSelection.Count > 0)
		{
			for (int i = 0; i < DaysOfWeekCollection.SelectedItems.Count; i++) 
			{
                thisContext.WeeklyEventText += $" {DaysOfWeekCollection.SelectedItems[i]}";
            }

			//for (int i = 0; i < 5; i++)
			//{
				//if (DaysOfWeekCollection.SelectedItems[i] == "" )
			//}
			

        }*/
    }

}

