using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace AFStudiumApp;

public partial class AboutModulPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public Subject thisSubject;
	public AboutModulPage(AFStudiumAPIClientService apiClient, Subject subject)
	{
		InitializeComponent();
		_apiClient = apiClient;
		thisSubject = subject;
		ModulesViewModel thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectName = subject.SubjectName;
		thisContext.SubjectId = subject.SubjectId;
		thisContext.LoadEventsOfSubject();
		BindingContext = thisContext;
	}

	public async void AddEvent(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddEventPage(_apiClient, thisSubject));
	}
}