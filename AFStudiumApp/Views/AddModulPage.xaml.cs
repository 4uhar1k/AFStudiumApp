using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class AddModulPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public ModulesViewModel thisContext { get; set; }
	public AddModulPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		BindingContext = new ModulesViewModel(_apiClient);
	}

    public AddModulPage(AFStudiumAPIClientService apiClient, Subject s)
    {
        InitializeComponent();
        _apiClient = apiClient;
		thisContext = new ModulesViewModel(_apiClient);
		thisContext.SubjectId = s.SubjectId;
		thisContext.SubjectName = s.SubjectName;
		thisContext.Faculty = s.Faculty;
        BindingContext = thisContext;
		AddBtn.Command = thisContext.EditSubject;
    }
}