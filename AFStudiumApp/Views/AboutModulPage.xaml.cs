using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class AboutModulPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public Subject thisSubject;
    public ModulesViewModel thisContext { get; set; }

    public AboutModulPage(AFStudiumAPIClientService apiClient, Subject subject, bool isAllowed)
	{
		InitializeComponent();
		_apiClient = apiClient;
		thisSubject = subject;
		thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectName = subject.SubjectName;
		thisContext.SubjectId = subject.SubjectId;
		thisContext.IsTeacher = isAllowed;
		thisContext.IsStudent = !isAllowed;
		//thisContext.LoadEventsOfSubject();
		BindingContext = thisContext;
		if (!isAllowed)
			SubjectsCollection.SelectionChanged += ShowMyEvent;

	}

	public async void AddEvent(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddEventPage(_apiClient, thisSubject));
	}
    public async void EditEvent(object sender, EventArgs e)
    {
        Button EditBtn = (Button)sender;
        Event ev = (Event)EditBtn.CommandParameter;
        //object sub = _apiService.GetSubjectById(ev.SubjectId);
        await Navigation.PushAsync(new AddEventPage(_apiClient, ev, true));
    }

    public async void ShowMyEvent(object sender, SelectionChangedEventArgs e)
    {
        //Button EditBtn = (Button)sender;
        //Event ev = (Event)EditBtn.CommandParameter;
        //object sub = _apiService.GetSubjectById(ev.SubjectId);
        if (e.CurrentSelection.Count > 0)
        {
            var SelectedEvent = (Event)e.CurrentSelection[0];
            await Navigation.PushAsync(new AddEventPage(_apiClient, SelectedEvent, thisContext.IsTeacher));
            SubjectsCollection.SelectedItem = null;
        }

    }
}