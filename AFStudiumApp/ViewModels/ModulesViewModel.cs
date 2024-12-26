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
        public int subjectid, eventid, studentsamount, CurMatrikel, createdperson;
        public string subjectname, faculty, CurName, CurSurname, eventname, eventtype, CurEmail, CurPass, CurCourse, CurRole;
        public bool isstudent, isteacher;
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public int? CurSemester;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Subject> AllSubjects { get; set; }
        public ObservableCollection<User> Students { get; set; }
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
        public ICommand AddEventForStudent { get; set; }
        public ICommand DeleteEvent { get; set; }
        public ICommand DeleteEventForStudent { get; set; }
        public ICommand EditEvent { get; set; }
        public Subject SubjectToEdit { get; set; }
        public ModulesViewModel(AFStudiumAPIClientService apiClient)
        {
            _apiClient = apiClient;            
            AllSubjects = new ObservableCollection<Subject>();
            Students = new ObservableCollection<User>();
            EventsOfSubject = new ObservableCollection<Event>();
            Exams = new ObservableCollection<Event>();
            Lectures = new ObservableCollection<Event>();
            Exercises = new ObservableCollection<Event>();
            MyEvents = new ObservableCollection<Event>();
            MyExams = new ObservableCollection<Event>();
            SubjectToEdit = new Subject();
            GetUsersInfo();
            LoadSubjects();
            LoadEventsOfSubject();
            isstudent = (CurRole == "student" | CurRole == "admin");
            isteacher = (CurRole == "teacher" | CurRole == "admin");


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
                Event e = new Event() { SubjectId = SubjectId, EventName = EventName, EventType = EventType, CreatedPerson=CurMatrikel, Date="Montag", Time="14:00-16:00" };
                _apiClient.PostEvent(e);
            }, () => EventType != "" & EventType!= null);
            AddEventForStudent = new Command((object e) =>
            {
                Event SelectedEvent = (Event)e;
                _apiClient.PostConnection(CurMatrikel, SelectedEvent.EventId);
                SelectedEvent.StudentsAmount++;
                _apiClient.PutEvent(SelectedEvent);
            });
            EditEvent = new Command(() =>
            {
                Event e = new Event() { EventId = EventId, SubjectId = SubjectId, EventName = EventName, EventType = EventType, CreatedPerson = CreatedPerson, Date = "Montag", Time = "14:00-16:00" };
                _apiClient.PutEvent(e);
            });
            DeleteEvent = new Command((object e) =>
            {
                Event EventToDelete = (Event)e;
                DeleteConnectionsWithEvent(EventToDelete);
            });
            DeleteEventForStudent = new Command((object e) =>
            {
                Event EventToDelete = (Event)e;
                _apiClient.DeleteConnection(CurMatrikel, EventToDelete.EventId);
                EventToDelete.StudentsAmount--;
                _apiClient.PutEvent(EventToDelete);
            });
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

        public async Task LoadSubjects()
        {
            var subjects = await _apiClient.GetSubjects();
            foreach (Subject subject in subjects)
            {
                AllSubjects.Add(subject);
            }
        }
        public async Task LoadStudents()
        {
            var users = await _apiClient.GetUsers();
            foreach (User user in users)
            {
                if (user.Role == "student")
                    Students.Add(user);
            }
        }
        public async Task DeleteEventsOfSubject(Subject subject)
        {
            var eventsofsub = await _apiClient.GetEventsBySubjectId(subject.SubjectId);
            if (eventsofsub != null)
            {
                foreach (Event e in eventsofsub)
                {
                    await DeleteConnectionsWithEvent(e);
                }
            }
           await _apiClient.DeleteSubject(subject.SubjectId);

        }
        public async Task DeleteConnectionsWithEvent(Event e)
        {
            var listofconnections = await _apiClient.GetConnectionsByEventId(e.EventId);
            if (listofconnections != null)
            {
                foreach (StudentsEvents se in listofconnections)
                {
                    await _apiClient.DeleteConnection(se.StudentId, se.EventId);
                }
            }
            await _apiClient.DeleteEvent(e.EventId);
        }
        public async Task LoadEventsOfSubject()
        {
            try
            {
                var events = await _apiClient.GetEvents();
                foreach (Event e in events)
                {
                    if (e.SubjectId == SubjectId)
                        EventsOfSubject.Add(e);
                    if (e.EventType == "Klausur")
                        Exams.Add(e);
                    else if (e.EventType == "Vorlesung")
                        Lectures.Add(e);
                    else if (e.EventType == "Übung")
                        Exercises.Add(e);
                }
                var myevents = await _apiClient.GetMyEvents(CurMatrikel);
                foreach (Event e in myevents)
                {
                    if (e.EventType != "Klausur")
                        MyEvents.Add(e);
                    else
                        MyExams.Add(e);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        //public async void AddEventForStudentAsync(Event e)
        //{
        //    await _apiClient.PostConnection(CurMatrikel, e.EventId);
        //}

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
        public int CreatedPerson
        {
            get => createdperson;
            set
            {
                if (createdperson != value)
                {
                    createdperson = value;
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

        public bool IsStudent
        {
            get => isstudent;
            set
            {
                if (isstudent != value)
                {
                    isstudent = value;
                    OnPropertyChanged(nameof(IsStudent));
                }
            }
        }
        public bool IsTeacher
        {
            get => isteacher;
            set
            {
                if (isteacher != value)
                {
                    isteacher = value;
                    OnPropertyChanged(nameof(IsTeacher));
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
