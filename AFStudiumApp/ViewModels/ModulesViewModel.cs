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
        public ObservableCollection<Event> Exercises { get; set; }
        public ObservableCollection<Event> MyEvents { get; set; }
        public ObservableCollection<Event> MyExams { get; set; }
        public ICommand AddSubject { get; set; }
        public ICommand DeleteSubject { get; set; }
        public ICommand EditSubject { get; set; }
        public ICommand AddEvent { get; set; }
        public ICommand DeleteEvent { get; set; }
        public ICommand EditEvent { get; set; }
        public Subject SubjectToEdit { get; set; }
        public ModulesViewModel(AFStudiumAPIClientService apiClient)
        {
            _apiClient = apiClient;
            AllSubjects = new ObservableCollection<Subject>();
            EventsOfSubject = new ObservableCollection<Event>();
            Exams = new ObservableCollection<Event>();
            Lectures = new ObservableCollection<Event>();
            Exercises = new ObservableCollection<Event>();
            MyEvents = new ObservableCollection<Event>();
            MyExams = new ObservableCollection<Event>();
            SubjectToEdit = new Subject();
            LoadSubjects();
            LoadEventsOfSubject();
            GetUsersInfo();

            AddSubject = new Command(() =>
            {        
                Subject NewSubject = new Subject() { SubjectName = SubjectName, Faculty = Faculty };
                _apiClient.PostSubject(NewSubject);
            }, () => SubjectName!="" & SubjectName!=null & Faculty!=null);
            EditSubject = new Command(() =>
            {
                Subject NewSubject = new Subject() { SubjectId = SubjectId, SubjectName = SubjectName, Faculty = Faculty };
                _apiClient.PutSubject(NewSubject);
            });
            DeleteSubject = new Command((object s) =>
            {
                Subject SubjectToDelete = (Subject)s;
                DeleteEventsOfSubject(SubjectToDelete);
            });
            AddEvent = new Command(() =>
            {
                Event e = new Event() { SubjectId = SubjectId, EventName = EventName, EventType = EventType, CreatedPerson=$"{curname} {cursurname}", Date="Montag", Time="14:00-16:00" };
                _apiClient.PostEvent(e);
            }, () => EventType != "" & EventType!= null);
            EditEvent = new Command(() =>
            {
                Event e = new Event() { EventId = EventId, SubjectId = SubjectId, EventName = EventName, EventType = EventType, CreatedPerson = $"{curname} {cursurname}", Date = "Montag", Time = "14:00-16:00" };
                _apiClient.PutEvent(e);
            });
            DeleteEvent = new Command((object e) =>
            {
                Event EventToDelete = (Event)e;
                _apiClient.DeleteEvent(EventToDelete.EventId);
            });
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
        public async void DeleteEventsOfSubject(Subject subject)
        {
            var eventsofsub = await _apiClient.GetEventsBySubjectId(subject.SubjectId);
            if (eventsofsub != null)
            {
                foreach (Event e in eventsofsub)
                {
                    _apiClient.DeleteEvent(e.EventId);
                }
            }
           await _apiClient.DeleteSubject(subject.SubjectId);

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
                    else if (e.EventType == "Übung")
                        Exercises.Add(e);
                }
                var myevents = await _apiClient.GetMyEvents($"{CurUser.Name} {CurUser.Surname}");
                foreach (Event e in myevents)
                {
                    if (e.EventType != "Klausur")
                        MyEvents.Add(e);
                    else
                        MyExams.Add(e);
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
