using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AFStudiumApp.ViewModels
{
    public class ModulesViewModel: INotifyPropertyChanged
    {
        private readonly AFStudiumAPIClientService _apiClient;
        public int subjectid, eventid, studentsamount;
        public string subjectname, faculty, curname, cursurname, eventname, eventtype;
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public User CurUser;
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Subject> AllSubjects { get; set; }
        public ObservableCollection<Event> EventsOfSubject { get; set; }
        public ObservableCollection<Event> Exams { get; set; }
        public ObservableCollection<Event> Lectures { get; set; }

        public ICommand AddSubject { get; set; }
        public ICommand AddEvent { get; set; }
        public ModulesViewModel(AFStudiumAPIClientService apiClient)
        {
            _apiClient = apiClient;
            AllSubjects = new ObservableCollection<Subject>();
            EventsOfSubject = new ObservableCollection<Event>();
            Exams = new ObservableCollection<Event>();
            Lectures = new ObservableCollection<Event>();
            LoadSubjects();
            LoadEventsOfSubject();
            GetUsersInfo();

            AddSubject = new Command(() =>
            {               

                Subject NewSubject = new Subject() { SubjectName = SubjectName, Faculty = Faculty };
                _apiClient.PostSubject(NewSubject);
            }, () => SubjectName!="" & SubjectName!=null & Faculty!=null);

            AddEvent = new Command(() =>
            {
                Event e = new Event() { SubjectId = SubjectId, EventName = $"{SubjectName} {EventType}", EventType = EventType, CreatedPerson=$"{curname} {cursurname}" };
                _apiClient.PostEvent(e);
            }, () => EventType != "" & EventType!= null);
        }
        public async void GetUsersInfo()
        {
            int CurMatrikel = 0;
            using (StreamReader sr = new StreamReader(CurUserPath))
            {
                CurMatrikel = Int32.Parse(sr.ReadLine());
                sr.Close();
            }
            CurUser = await _apiClient.GetUserByMatrikelNum(CurMatrikel);
            if (CurUser != null)
            {
                curname = CurUser.Name;
                cursurname = CurUser.Surname;
            }
        }

        public async void LoadSubjects()
        {
            var subjects = await _apiClient.GetSubjects();
            foreach (Subject subject in subjects)
            {
                AllSubjects.Add(subject);
            }
        }

        public async void LoadEventsOfSubject()
        {
            try
            {
                var events = await _apiClient.GetEvents();
                foreach (Event e in events)
                {
                    if (e.SubjectId == SubjectId)
                        EventsOfSubject.Add(e);
                    else if (e.EventType == "Klausur")
                        Exams.Add(e);
                    else if (e.EventType == "Vorlesung")
                        Lectures.Add(e);
                }
            }
            catch { }
        }

        public int SubjectId
        {
            get => subjectid;
            set
            {
                if (subjectid!=value)
                {
                    subjectid = value;
                    OnPropertyChanged();
                }    
            }
        }

        public int EventId
        {
            get => eventid;
            set
            {
                if (eventid != value)
                {
                    eventid = value;
                    OnPropertyChanged();
                }
            }
        }
        public int StudentsAmount
        {
            get => studentsamount;
            set
            {
                if (studentsamount != value)
                {
                    studentsamount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SubjectName
        {
            get => subjectname;
            set
            {
                if (subjectname != value)
                {
                    subjectname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Faculty
        {
            get => faculty;
            set
            {
                if (faculty != value)
                {
                    faculty = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EventName
        {
            get => eventname;
            set
            {
                if (eventname != value)
                {
                    eventname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EventType
        {
            get => eventtype;
            set
            {
                if (eventtype != value)
                {
                    eventtype = value;
                    OnPropertyChanged();
                }
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)AddSubject).ChangeCanExecute();
            ((Command)AddEvent).ChangeCanExecute();
        }

    }
}
