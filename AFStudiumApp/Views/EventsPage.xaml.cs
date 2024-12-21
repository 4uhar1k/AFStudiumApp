using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

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
	public void blabla()
	{
		SubjectsCollection.ItemsSource = thisContext.Exams;
	}
}