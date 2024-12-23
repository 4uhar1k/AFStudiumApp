using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumAPIClient;


namespace AFStudiumApp;

public partial class LoginPage : ContentPage
{
    private readonly AFStudiumAPIClientService _apiClient;
    public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
    public LoginPage(AFStudiumAPIClientService apiClient)
	{
		InitializeComponent();
		_apiClient = apiClient;
	}

	public async void LogIn(object sender, EventArgs e)
	{

		var user = await _apiClient.GetUserByEmailNPass(loginentry.Text, passentry.Text);
		if (user !=null)
		{
			//File.Create(CurUserPath);
			
			using (StreamWriter sw = new StreamWriter(CurUserPath, false))
			{
				sw.WriteLine(user.MatrikelNum);
				sw.WriteLine(user.Email);
				sw.WriteLine(user.Password);
				sw.WriteLine(user.Name);
				sw.WriteLine(user.Surname);
				sw.WriteLine(user.Course);
				sw.WriteLine(user.Semester);
				sw.WriteLine(user.Role);
				//sw.WriteLine(user.Password);
				sw.Close();
			}
			await DisplayAlert("", $"Success: {user.MatrikelNum} {user.Name} {user.Surname}", "OK");
			await Navigation.PopModalAsync();
		}
		else
		{
            await DisplayAlert("", "No user found", "OK");
        }
	}
}