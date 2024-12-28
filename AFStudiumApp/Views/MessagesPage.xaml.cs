using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;

namespace AFStudiumApp;

public partial class MessagesPage : ContentPage
{
	private readonly AFStudiumAPIClientService _apiClient;
	public MessagesViewModel thisContext { get; set; }
	public MessagesPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
		thisContext = new MessagesViewModel(_apiClient);
		BindingContext = thisContext;
	}

	public async void Update(object sender, EventArgs e)
	{
		thisContext.LoadMessages();
	}
}