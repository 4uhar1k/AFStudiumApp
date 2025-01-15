using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;
using AFStudiumAPIClient.Models.ApiModels;

namespace AFStudiumApp;

public partial class ListOfStudentsPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiService;
	public ModulesViewModel thisContext { get; set; }
	public int CurEventId { get; set; }
	public ListOfStudentsPage(AFStudiumAPIClientService apiService, int eid)
	{
		InitializeComponent();
		_apiService = apiService;
		CurEventId = eid;
		thisContext = new ModulesViewModel(_apiService);
		thisContext.LoadStudents(eid);
		BindingContext = thisContext;
	}

	public async void AddStudentsToEvent(object sender, EventArgs e)
	{
		if (StudentsCollection.SelectedItems.Count>0)
		{
            Event ev = await _apiService.GetEventById(CurEventId);
            foreach (User user in StudentsCollection.SelectedItems)
			{
				await _apiService.PostConnection(user.MatrikelNum, CurEventId, false);
                if (ev.EventType == "Klausur")
                    _apiService.PostGrades(user.MatrikelNum, CurEventId, "");
            }
			
			ev.StudentsAmount += StudentsCollection.SelectedItems.Count;
			await _apiService.PutEvent(ev);
			await Navigation.PopAsync();
		}
		else
		{
			await DisplayAlert("", "Waehlen Sie mind. 1 Studenten aus", "OK");
		}

	}
}