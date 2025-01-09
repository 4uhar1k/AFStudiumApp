using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
using System.Data;

namespace AFStudiumApp;

public partial class EventsPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiService;
	//private readonly string _eventType;
	public ModulesViewModel thisContext { get; set; }
	
    public EventsPage(AFStudiumAPIClientService apiService, string EventType)
	{
		InitializeComponent();
		_apiService = apiService;
		//_eventType = EventType;
		thisContext = new ModulesViewModel(_apiService);
		thisContext.IsStudent = (thisContext.CurRole == "student" | thisContext.CurRole == "admin");
		thisContext.IsTeacher = false;//(thisContext.CurRole == "teacher" | thisContext.CurRole == "admin");
        BindingContext = thisContext;
		//AddEventBtn.IsVisible = false;
		switch (EventType)
		{
            case ("Vorlesung"):
                SubjectsCollection.ItemsSource = thisContext.Lectures;
				//TeachersSubjectsCollection.ItemsSource = thisContext.MyLectures;
				break;
			case ("Uebung"):
                SubjectsCollection.ItemsSource = thisContext.Exercises;
                //TeachersSubjectsCollection.ItemsSource = thisContext.MyExercises;
                break;
			case ("Klausur"):
				SubjectsCollection.ItemsSource = thisContext.Exams;
                //TeachersSubjectsCollection.ItemsSource = thisContext.MyExams;
                break;
			default:
                SubjectsCollection.ItemsSource = thisContext.Exercises;
				break;

        }
		if (thisContext.IsStudent)
			SubjectsCollection.SelectionChanged += ShowMyEvent;
		//if (_eventType == "Vorlesung")
		//	SubjectsCollection.BindingContext = thisContext.Lectures;


	}

    public EventsPage(AFStudiumAPIClientService apiService, bool isExam)
    {
		InitializeComponent();
		_apiService = apiService;
		thisContext = new ModulesViewModel(_apiService);
        thisContext.IsStudent = (thisContext.CurRole == "student" | thisContext.CurRole == "admin");
        thisContext.IsTeacher = (thisContext.CurRole == "teacher" | thisContext.CurRole == "admin");
        BindingContext = thisContext;
		if (thisContext.CurRole == "student")
		{
            GetMyEvents(isExam);
			SubjectsCollection.IsVisible = false;
        }
		else
		{
            if (isExam)
                SubjectsCollection.ItemsSource = thisContext.MyExams;
            else
                SubjectsCollection.ItemsSource = thisContext.MyEvents;
        }
		
		
    }
	public async void GetMyEvents(bool isExam)
	{
        var myevents = await _apiService.GetConnectionsByUserId(thisContext.CurMatrikel);
		List<Event> notexams = new List<Event>();
		List<Event> exams = new List<Event>();
		foreach (Event e in myevents)
		{
			if (e.EventType != "Klausur")
				notexams.Add(e);
			else
				exams.Add(e);
		}
		if (isExam == false)
            MySubjectsCollection.ItemsSource = notexams;
		else
            MySubjectsCollection.ItemsSource = exams;
    }
    /*public void SearchEvent(object sender, EventArgs e)
	{
		SearchBar searchBar = (SearchBar)sender;
		SubjectsCollection.ItemsSource = DataService.GetSearchResults(searchBar.Text);
	}*/

    public async void EditEvent(object sender, EventArgs e)
	{
		Button EditBtn = (Button)sender;
		Event ev = (Event)EditBtn.CommandParameter;
		//object sub = _apiService.GetSubjectById(ev.SubjectId);
		await Navigation.PushAsync(new AddEventPage(_apiService, ev, true));
	}

    public async void ShowMyEvent(object sender, SelectionChangedEventArgs e)
    {
        //Button EditBtn = (Button)sender;
        //Event ev = (Event)EditBtn.CommandParameter;
        //object sub = _apiService.GetSubjectById(ev.SubjectId);
        if (e.CurrentSelection.Count > 0)
        {
            var SelectedEvent = (Event)e.CurrentSelection[0];
            await Navigation.PushAsync(new AddEventPage(_apiService, SelectedEvent, thisContext.IsTeacher));
        }
        
    }
}