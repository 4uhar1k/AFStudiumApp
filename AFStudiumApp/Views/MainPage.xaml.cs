using AFStudiumAPIClient;
//using AFStudiumApp.Models;
using System.Collections.ObjectModel;
using AFStudiumAPIClient.Models.ApiModels;
namespace AFStudiumApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<User> AllUsers { get; set; }
        public SqlConnectionBase ConnectionBase { get; set; }
        private readonly AFStudiumAPIClientService _apiClient;

        public MainPage(AFStudiumAPIClientService apiClient)
        {
            InitializeComponent();
            ConnectionBase = new SqlConnectionBase();
            AllUsers = new ObservableCollection<User>();
            //LoadUsers();
            _apiClient = apiClient;
            BindingContext = this;
        }
        
        public async void LoadUsers()
        {
            //AllUsers = ConnectionBase.GetUsers().Result;

            /* ApiService apiService = new ApiService();
             List<User> AllUsers = await apiService.GetUsersAsync();
             foreach (User user in AllUsers)
             {
                 this.AllUsers.Add(user);
             }*/
            User newUser = new User() { MatrikelNum = 257518, Email = "a", Password="b", Name="c", Surname = "d", Course="AngInf", Semester=3 };
            await _apiClient.PostUser(newUser);
            await DisplayAlert("", "successfully added", "OK");

            var users = await _apiClient.GetUsers();
            UsersCollection.ItemsSource = users;
            //await DisplayAlert("", "success", "OK");
        }

        public async void TestAddUser(object sender,  EventArgs e)
        {
            LoadUsers();
            //User testuser = new User() { MatrikelNum = 1 , Email="test", Password="testpass", Name="testname", Surname="testsurname", Course="testcourse", Semester=1};
            //ApiService apiService = new ApiService();
            //await apiService.PostEntityAsync(testuser);
            //await ConnectionBase.InsertUser(testuser);
            //ApiService apiService = new ApiService();
            //await apiService.PostEntityAsync(testuser);
        }

        
    }

}
