using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
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
		SubjectName.Text = subject.SubjectName;
	}

	public async void AddEvent(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddEventPage(_apiClient, thisSubject));
	}
}