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
        public int subjectid, eventid, studentsamount, CurMatrikel, createdperson, credits;
        public string subjectname, faculty, location, CurName, CurSurname, eventname, eventtype, CurEmail, CurPass, CurCourse, CurRole, weeklyeventtxt;
        public bool isstudent, isteacher, permitrequired;
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public int? CurSemester;
        public TimeSpan begintime, endtime;
        public DateTime date;
        public event PropertyChangedEventHandler? PropertyChanged;
        public bool OldPermitRequired { get; set; }
        public TimeSpan BeginTime
        {
            get => begintime;
            set
            {
                if (begintime != value)
                {
                    begintime = value;
                    OnPropertyChanged(nameof(BeginTime));
                }
            }
        }
        public TimeSpan EndTime
        {
            get => endtime;
            set
            {
                if (endtime != value)
                {
                    endtime = value;
                    OnPropertyChanged(nameof(EndTime));
                }
            }
        }
        public string WeeklyEventText
        {
            get => weeklyeventtxt;
            set
            {
                if (weeklyeventtxt != value)
                {
                    weeklyeventtxt = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Subject> AllSubjects { get; set; }
        public ObservableCollection<Subject> MySubjects { get; set; }
        public ObservableCollection<User> Students { get; set; }
        public ObservableCollection<Grades> GradesOfEvent { get; set; }
        public ObservableCollection<Event> EventsOfSubject { get; set; }
        public ObservableCollection<Event> Exams { get; set; }
        public ObservableCollection<Event> Lectures { get; set; }
        public ObservableCollection<Event> Exercises { get; set; }
        public ObservableCollection<Event> Permits { get; set; }
        public ObservableCollection<Event> MyEvents { get; set; }
        public ObservableCollection<Event> MyLectures { get; set; }
        public ObservableCollection<Event> MyExercises { get; set; }
        public ObservableCollection<Event> MyExams { get; set; }
        public ObservableCollection<Event> MondayEvents { get; set; }
        public ObservableCollection<Event> TuesdayEvents { get; set; }

        public ObservableCollection<Event> WednesdayEvents { get; set; }

        public ObservableCollection<Event> ThursdayEvents { get; set; }

        public ObservableCollection<Event> FridayEvents { get; set; }

        public ObservableCollection<ObservableCollection<Event>> EventsOfWeekCollection { get; set; } 

        public ICommand AddSubject { get; set; }
        public ICommand DeleteSubject { get; set; }
        public ICommand EditSubject { get; set; }
        public ICommand AddEvent { get; set; }
        public ICommand AddEventForStudent { get; set; }
        public ICommand DeleteEvent { get; set; }
        public ICommand CloseEvent { get; set; }
        public ICommand DeleteEventForStudent { get; set; }
        public ICommand EditEvent { get; set; }
        public ICommand UpdateGrades { get; set; }
        public Subject SubjectToEdit { get; set; }
        public ModulesViewModel(AFStudiumAPIClientService apiClient)
        {
            _apiClient = apiClient;            
            AllSubjects = new ObservableCollection<Subject>();
            MySubjects = new ObservableCollection<Subject>();
            Students = new ObservableCollection<User>();
            GradesOfEvent = new ObservableCollection<Grades>();
            EventsOfSubject = new ObservableCollection<Event>();
            Exams = new ObservableCollection<Event>();
            Lectures = new ObservableCollection<Event>();
            Exercises = new ObservableCollection<Event>();
            Permits = new ObservableCollection<Event>();
            MyEvents = new ObservableCollection<Event>();
            MyLectures = new ObservableCollection<Event>();
            MyExercises = new ObservableCollection<Event>();
            MyExams = new ObservableCollection<Event>();
            MondayEvents = new ObservableCollection<Event>();
            TuesdayEvents = new ObservableCollection<Event>();
            WednesdayEvents = new ObservableCollection<Event>();
            ThursdayEvents = new ObservableCollection<Event>();
            FridayEvents = new ObservableCollection<Event>();
            EventsOfWeekCollection = new ObservableCollection<ObservableCollection<Event>>();//new ObservableCollection<Event>[5] { new ObservableCollection<Event>(), new ObservableCollection<Event>(), new ObservableCollection<Event>(), new ObservableCollection<Event>(), new ObservableCollection<Event>() };
            //EventsOfWeekCollection = new ObservableCollection<Event>();
            SubjectToEdit = new Subject();
            Event SelectedEvent = new Event();
            //BeginTime = new TimeSpan(0, 0, 0);
            //EndTime = new TimeSpan(1, 0, 0);
            GetUsersInfo();
            LoadSubjects();
            LoadEventsOfSubject();
            LoadEventsByDays();
            isstudent = (CurRole == "student" | CurRole == "admin");
            isteacher = (CurRole == "teacher" | CurRole == "admin");


            AddSubject = new Command(() =>
            {        
                Subject NewSubject = new Subject() { SubjectName = SubjectName, Faculty = Faculty, CreatedPerson = CurMatrikel };
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

                AddEventAsync(true);
                
               
            }, () => EventType != "" & EventType!= null & Location!="" & Location!=null );
            AddEventForStudent = new Command((object e) =>
            {
                SelectedEvent = (Event)e;
                _apiClient.PostConnection(CurMatrikel, SelectedEvent.EventId, 3);
                if (SelectedEvent.EventType == "Klausur")
                    _apiClient.PostGrades(CurMatrikel, SelectedEvent.EventId, "");
                SelectedEvent.StudentsAmount++;
                _apiClient.PutEvent(SelectedEvent);
            });
            EditEvent = new Command(() =>
            {
                //Event e = new Event() {EventId = EventId, SubjectId = SubjectId, EventName = EventName, EventType = EventType, CreatedPerson = CurMatrikel, Date = Date.ToString("dd.MM.yyyy"), Time = $"{BeginTime.ToString()}-{EndTime.ToString()}", Credits = Credits, Location = Location, PermitRequired = PermitRequired };

                //_apiClient.PutEvent(e);
                
                AddEventAsync(false);
            });
            DeleteEvent = new Command((object e) =>
            {
                Event EventToDelete = (Event)e;
                DeleteConnectionsWithEvent(EventToDelete);
            });
            CloseEvent = new Command(() =>
            {
                CloseEventAsync(EventId);
            });
            DeleteEventForStudent = new Command((object e) =>
            {
                Event EventToDelete = (Event)e;
                _apiClient.DeleteConnection(CurMatrikel, EventToDelete.EventId);
                if (SelectedEvent.EventType == "Klausur")
                    _apiClient.DeleteGrade(CurMatrikel, EventToDelete.EventId);
                EventToDelete.StudentsAmount--;
                _apiClient.PutEvent(EventToDelete);
            });
            UpdateGrades = new Command(() =>
            {
                foreach (Grades grade in GradesOfEvent)
                {
                    _apiClient.PutGrade(grade);
                    //Students.Add(await _apiClient.GetUserByMatrikelNum(connection.StudentId));
                }
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
                if (subject.CreatedPerson == CurMatrikel)
                    MySubjects.Add(subject);
                else
                    AllSubjects.Add(subject);
            }
        }
        public async Task LoadStudents(int eid)
        {
            var users = await _apiClient.GetUsers();
            var usersofevent = await _apiClient.GetConnectionsByEventId(eid);
            foreach (User user in users)
            {
                if (user.Role=="student" & usersofevent.Where(n=> n.StudentId == user.MatrikelNum).FirstOrDefault() == null)
                    Students.Add(user);
            }
        }
        public async Task LoadStudentsOfEvent(int eid)
        {
            var users = await _apiClient.GetGradesByEventId(eid);
            foreach (Grades connection in users)
            {
                GradesOfEvent.Add(connection);
                //Students.Add(await _apiClient.GetUserByMatrikelNum(connection.StudentId));
            }
        }
        public async Task CloseEventAsync(int eid)
        {
            var EventToFind = await _apiClient.GetEventById(eid);
            if (EventToFind != null)
            {
                //EventToClose = (Event)EventToFind;
                var ConOfEvents = await _apiClient.GetConnectionsByEventId(EventToFind.EventId);
                foreach (Connections cons in ConOfEvents)
                {
                    await _apiClient.DeleteConnection(cons.StudentId, cons.EventId);                    
                }
                if (EventToFind.EventType == "Klausur")
                {
                    var GradesOfEvent = await _apiClient.GetGradesByEventId(EventToFind.EventId);
                    foreach (Grades cons in GradesOfEvent)
                    {
                        await _apiClient.DeleteGrade(cons.StudentId, EventToFind.EventId);
                    }
                }
                    
                EventToFind.StudentsAmount = 0;
                await _apiClient.PutEvent(EventToFind);
           }
        }
        public async Task LoadEventsByDays()
        {
            
            var allevents = await _apiClient.GetConnectionsByUserId(CurMatrikel);
            ObservableCollection<Event>[] arrayoflists = new ObservableCollection<Event>[5] {new ObservableCollection<Event>(), new ObservableCollection<Event>(), new ObservableCollection<Event>(), new ObservableCollection<Event>(), new ObservableCollection<Event>() };
            foreach (Event connection in allevents)
            {
                if (connection.Date.Contains("Montag"))
                {
                    MondayEvents.Add(connection);
                    //EventsOfWeekCollection[0].Add(connection);
                }
                if (connection.Date.Contains("Dienstag"))
                {
                    TuesdayEvents.Add(connection);
                    //EventsOfWeekCollection[1].Add(connection);
                }
                if (connection.Date.Contains("Mittwoch"))
                {
                    WednesdayEvents.Add(connection);
                    //EventsOfWeekCollection[2].Add(connection);
                }
                if (connection.Date.Contains("Donnerstag"))
                {
                    ThursdayEvents.Add(connection);
                    //EventsOfWeekCollection[3].Add(connection);
                }
                if (connection.Date.Contains("Freitag"))
                {
                    FridayEvents.Add(connection);
                    //EventsOfWeekCollection[4].Add(connection);
                }
            }
            EventsOfWeekCollection.Add(MondayEvents);
            EventsOfWeekCollection.Add(TuesdayEvents);
            EventsOfWeekCollection.Add(WednesdayEvents);
            EventsOfWeekCollection.Add(ThursdayEvents);
            EventsOfWeekCollection.Add(FridayEvents);
            //EventsOfWeekCollection = new ObservableCollection<Event>(arrayoflists.SelectMany(x => x));
        }
        public async Task AddEventAsync(bool isAdding)
        {
            Event sl = new Event();
            Event e = new Event() { SubjectId = SubjectId, EventName = EventName, EventType = EventType, CreatedPerson = CurMatrikel, StudentsAmount = StudentsAmount, Time = $"{BeginTime.ToString(@"hh\:mm")}-{EndTime.ToString(@"hh\:mm")}", Credits = Credits, Location = Location, PermitRequired = PermitRequired};
            if (WeeklyEventText != "" & WeeklyEventText != null)
                e.Date = WeeklyEventText;
            else
                e.Date = Date.ToString("dd.MM.yyyy");
            if (isAdding)
            {
                if (PermitRequired)
                {
                    sl.SubjectId = SubjectId;
                    sl.EventName = $"Studienleistung für {EventName}";
                    sl.EventType = "Studienleistung";
                    sl.CreatedPerson = CurMatrikel;
                    sl.Date = "";
                    sl.Time = "";
                    sl.Location = "";
                    await _apiClient.PostEvent(sl);
                    var events = await _apiClient.GetEventsBySubjectId(SubjectId);
                    Event studienleistung = events.Where(n => n.EventType == "Studienleistung").FirstOrDefault();
                    e.PermitionEvent = studienleistung.EventId;
                }   
                else if (e.EventType == "Studienleistung")
                {
                    e.Date = "";
                    e.Time = "";
                    e.Location = "";

                }
                await _apiClient.PostEvent(e);
                Connections ConOfCreator = new Connections() { EventId = e.EventId, StudentId = CurMatrikel, Status = 1 };
            }                
            else
            {
                e.EventId = EventId;
                if (OldPermitRequired & !PermitRequired)
                {
                    var events = await _apiClient.GetEventsBySubjectId(SubjectId);
                    await _apiClient.DeleteEvent(events.Where(n => n.EventType == "Studienleistung").FirstOrDefault().EventId);
                }
                else if (!OldPermitRequired & PermitRequired)
                {
                    sl.SubjectId = SubjectId;
                    sl.EventName = $"Studienleistung für {EventName}";
                    sl.EventType = "Studienleistung";
                    sl.CreatedPerson = CurMatrikel;
                    sl.Date = "";
                    sl.Time = "";
                    sl.Location = "";
                    await _apiClient.PostEvent(sl);
                    var events = await _apiClient.GetEventsBySubjectId(SubjectId);
                    Event studienleistung = events.Where(n => n.EventType == "Studienleistung").FirstOrDefault();
                    e.PermitionEvent = studienleistung.EventId;
                }
                else if (e.EventType == "Studienleistung")
                {
                    e.Date = "";
                    e.Time = "";
                    e.Location = "";

                }
                await _apiClient.PutEvent(e);

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
                foreach (Connections se in listofconnections)
                {
                    await _apiClient.DeleteConnection(se.StudentId, se.EventId);
                }
            }
            if (e.PermitRequired)
                await _apiClient.DeleteEvent(e.PermitionEvent);
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
                    if (e.EventType == "Klausur"/* & !MyExams.Contains(e)*/)
                        Exams.Add(e);
                    else if (e.EventType == "Vorlesung"/* & !MyLectures.Contains(e)*/)
                        Lectures.Add(e);
                    else if (e.EventType == "Übung" )/*& !MyExercises.Contains(e))*/
                        Exercises.Add(e);
                    //else if (e.EventType == "Studienleistung")
                    //    Permits.Add(e);
                }
                var myevents = await _apiClient.GetMyEvents(CurMatrikel);
                foreach (Event e in myevents)
                {
                    if (e.EventType != "Klausur")
                    {
                        MyEvents.Add(e);
                        switch (e.EventType)
                        {
                            case "Vorlesung":
                                MyLectures.Add(e);

                                break;
                            case "Übung":
                                MyExercises.Add(e);
                                break;

                        }
                    }

                    else
                        MyExams.Add(e);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        //public async Task<bool> IsPermitted(Event e)
        //{
        //    return true;
        //}

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
        public int Credits
        {
            get => credits;
            set
            {
                if (credits != value)
                {
                    credits = value;
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
        public string Location
        {
            get => location;
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
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
        
        public bool PermitRequired
        {
            get => permitrequired;
            set
            {
                if (permitrequired != value)
                {
                    permitrequired = value;
                    OnPropertyChanged(nameof(PermitRequired));
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
            ((Command)AddEventForStudent).ChangeCanExecute();            
        }

    }
}
