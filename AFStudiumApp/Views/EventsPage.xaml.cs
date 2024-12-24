using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
using System.Data;

namespace AFStudiumApp;

public partial class EventsPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiService;
	private readonly string _eventType;
	public ModulesViewModel thisContext { get; set; }
	
    public EventsPage(AFStudiumAPIClientService apiService, string EventType)
	{
		InitializeComponent();
		_apiService = apiService;
		_eventType = EventType;
		thisContext = new ModulesViewModel(_apiService);
		BindingContext = thisContext;
		switch (_eventType)
		{
            case ("Vorlesung"):
                SubjectsCollection.ItemsSource = thisContext.Lectures;
				break;
			case ("Uebung"):
                SubjectsCollection.ItemsSource = thisContext.Exercises;
				break;
			case ("Klausur"):
				SubjectsCollection.ItemsSource = thisContext.Exams;
				break;
			default:
                SubjectsCollection.ItemsSource = thisContext.Exercises;
				break;

        }

		//if (_eventType == "Vorlesung")
			//SubjectsCollection.BindingContext = thisContext.Lectures;

        
	}

    public EventsPage(AFStudiumAPIClientService apiService, bool isExam)
    {
		InitializeComponent();
		_apiService = apiService;
		thisContext = new ModulesViewModel(_apiService);
		BindingContext = thisContext;
		if (isExam)
			SubjectsCollection.ItemsSource = thisContext.MyExams;
		else
			SubjectsCollection.ItemsSource = thisContext.MyEvents;
		GetMyEvents(isExam);
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
		await Navigation.PushAsync(new AddEventPage(_apiService, ev));
	}
}