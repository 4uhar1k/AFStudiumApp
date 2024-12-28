using AFStudiumAPIClient;
//using AFStudiumApp.Models;
using System.Collections.ObjectModel;
using AFStudiumAPIClient.Models.ApiModels;
using AFStudiumApp.ViewModels;
namespace AFStudiumApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<User> AllUsers { get; set; }
        //public SqlConnectionBase ConnectionBase { get; set; }
        public readonly AFStudiumAPIClientService _apiClient;
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");


        public MainPage(AFStudiumAPIClientService apiClient)
        {
            InitializeComponent();
            
            //ConnectionBase = new SqlConnectionBase();
            AllUsers = new ObservableCollection<User>();
            //LoadUsers();
            _apiClient = apiClient;
            BindingContext = this;
            if (!File.Exists(CurUserPath))
            {
                GoToLogin();
            }
            //ViewModelBase viewModelBase = new ViewModelBase();
            //if (viewModelBase.IsUserLogged().Result == false)
            //{

            //}
        }

        /* public async void LoadUsers()
         {
             //AllUsers = ConnectionBase.GetUsers().Result;


          //   User newUser = new User() { MatrikelNum = 257518, Email = "a", Password="b", Name="c", Surname = "d", Course="AngInf", Semester=3 };
            // await _apiClient.PostUser(newUser);
             //await DisplayAlert("", "successfully added", "OK");


             //await DisplayAlert("", "success", "OK");

         var users = await _apiClient.GetUsers();
             UsersCollection.ItemsSource = users;   
         }
        */
        public async void TestAddUser(object sender,  EventArgs e)
        {
            User u = new User { MatrikelNum = 333333, Email = "ah", Password = "b", Name = "c", Surname = "student2", Course = "e", Semester = 99, Role = "student" };
            //_apiClient.PostConnection(333333, 13);
            //var users = await _apiClient.GetConnectionsByUserId(333333);
            //UsersCollection.ItemsSource = users;            //User testuser = new User() { MatrikelNum = 1 , Email="test", Password="testpass", Name="testname", Surname="testsurname", Course="testcourse", Semester=1};
            Message m = new Message() { MessageId = 1, SendFrom = 100000, SendTo = 257518, MessageHeader="ti dolboeb", MessageText="koncheniy", MessageTime=DateTime.Now };
            await _apiClient.PostMessage(m);
        }

        public async void GoToModules(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModulesPage(_apiClient));
        }

        public async void GoToExams(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsPage(_apiClient, "Klausur"));
        }
        public async void GoToLectures(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsPage(_apiClient, "Vorlesung"));
        }
        public async void GoToExercises(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsPage(_apiClient, "Uebung"));
        }
        public async void GoToLogin()
        {
            await Navigation.PushModalAsync(new LoginPage(_apiClient));
        }
    }

}
