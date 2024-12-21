using AFStudiumAPIClient;
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
	/*public void SearchEvent(object sender, EventArgs e)
	{
		SearchBar searchBar = (SearchBar)sender;
		SubjectsCollection.ItemsSource = DataService.GetSearchResults(searchBar.Text);
	}*/

	public async void EditEvent(object sender, EventArgs e)
	{
		Button EditBtn = (Button)sender;
		await Navigation.PushAsync(new AddEventPage(_apiService, EditBtn.CommandParameter));
	}
}