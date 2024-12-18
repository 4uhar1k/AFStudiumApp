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
        public int subjectid;
        public string subjectname, createdperson, curname, cursurname;
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public User CurUser;
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Subject> AllSubjects { get; set; }
        public ICommand AddSubject { get; set; }
        public ModulesViewModel(AFStudiumAPIClientService apiClient)
        {
            _apiClient = apiClient;
            AllSubjects = new ObservableCollection<Subject>();
            LoadSubjects();
            GetUsersInfo();

            AddSubject = new Command(() =>
            {
                

                Subject NewSubject = new Subject() { SubjectName = SubjectName, CreatedPerson = $"{curname} {cursurname}" };
                _apiClient.PostSubject(NewSubject);
            }, () => SubjectName!="" & SubjectName!=null);
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
        public string CreatedPerson
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)AddSubject).ChangeCanExecute();
        }

    }
}
