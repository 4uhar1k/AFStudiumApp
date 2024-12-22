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

    public EventsPage(AFStudiumAPIClientService apiService)
    {
		InitializeComponent();
		_apiService = apiService;
		thisContext = new ModulesViewModel(_apiService);
		BindingContext = thisContext;
		SubjectsCollection.ItemsSource = thisContext.MyEvents;
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