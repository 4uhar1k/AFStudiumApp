using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFStudiumApp.ViewModels
{
    public class MessagesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public string CurEmail, CurPass, CurName, CurSurname, CurCourse, CurRole;
        public int CurMatrikel, CurSemester;
        private readonly AFStudiumAPIClientService _apiService;
        public ObservableCollection<Message> SentMessages { get; set; }
        public ObservableCollection<Message> ReceivedMessages { get; set; }
        public MessagesViewModel(AFStudiumAPIClientService apiService)
        {
            _apiService = apiService;
            SentMessages = new ObservableCollection<Message>();
            ReceivedMessages = new ObservableCollection<Message>();
            GetUsersInfo();
            LoadMessages();
        }

        public async void LoadMessages()
        {
            var messages = await _apiService.GetMessages();
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    if (message.SendFrom == CurMatrikel)
                        SentMessages.Add(message);
                    else if (message.SendTo == CurMatrikel)
                        ReceivedMessages.Add(message);
                }
            }
        }
        public void GetUsersInfo()
        {
            //int CurMatrikel = 0;
            using (StreamReader sr = new StreamReader(CurUserPath))
            {
                CurMatrikel = Int32.Parse(sr.ReadLine());
                CurEmail = sr.ReadLine();
                CurPass = sr.ReadLine();
                CurName = sr.ReadLine();
                CurSurname = sr.ReadLine();
                CurCourse = sr.ReadLine();
                CurSemester = Int32.Parse(sr.ReadLine());
                CurRole = sr.ReadLine();
                sr.Close();
            }
            //IsStudent = (CurRole == "student" | CurRole == "admin");
            //IsTeacher = (CurRole == "teacher" | CurRole == "admin");
            //if (CurRole == "student")
            //{
            //    IsStudent = true;
            //    IsTeacher = false;
            //}
            //else if (CurRole == "teacher")
            //{
            //    IsStudent = false;
            //    IsTeacher = true;
            //}
            //else
            //{
            //    IsStudent = true;
            //    IsTeacher = true;
            //}

        }
    }
}
