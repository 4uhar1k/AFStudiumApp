using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
using Org.BouncyCastle.Tls;

namespace AFStudiumApp;

public partial class ModulesPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
    public ModulesViewModel thisContext { get; set; }
	public ModulesPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		thisContext = new ModulesViewModel(_apiClient);
        thisContext.IsStudent = (thisContext.CurRole == "student" | thisContext.CurRole == "admin");
        thisContext.IsTeacher = (thisContext.CurRole == "teacher" | thisContext.CurRole == "admin");
        BindingContext = thisContext;
        
    }
    public async void AddModul(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddModulPage(_apiClient));
    }

	public async void ShowMyModul(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count > 0)
		{
			var SelectedSubject = (Subject)e.CurrentSelection[0];
			await Navigation.PushAsync(new AboutModulPage(_apiClient, SelectedSubject, thisContext.IsTeacher));
            MySubjectsCollection.SelectedItem = null;
        }
	}
    public async void ShowModul(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var SelectedSubject = (Subject)e.CurrentSelection[0];
            await Navigation.PushAsync(new AboutModulPage(_apiClient, SelectedSubject, false));
            SubjectsCollection.SelectedItem = null;
        }
    }
    public async void EditModule(object sender, EventArgs e)
    {
        Button EditBtn = (Button)sender;
        Subject sub = (Subject)EditBtn.CommandParameter;
        
        await Navigation.PushAsync(new AddModulPage(_apiClient, sub));
    }
}