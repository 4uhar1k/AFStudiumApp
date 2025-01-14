using AFStudiumAPIClient;
using AFStudiumAPIClient.Models.ApiModels;
using Microsoft.Maui.Devices.Sensors;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AFStudiumApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        // public SqliteConnectionBase CurUserBase { get; set; }
        // public ISQLiteAsyncConnection CurUserConnection { get; set; }
        public string CurUserPath = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public string curname, cursurname, curmatrikel, curemail, curpass;
        protected readonly AFStudiumAPIClientService _apiClient;
        public User CurUser { get; set; }
        // public bool IsLoggedIn { get; set; }

        

        public ViewModelBase(/*AFStudiumAPIClientService apiClient*/)
        {
            CurUser = new User();
            //_apiClient = apiClient;
            GetUsersInfo();
            // CurUserBase = new SqliteConnectionBase();

            //IsLoggedIn = IsUserLogged().Result;
           
        }
        //public async Task<bool> IsUserLogged()
        //{
        //   // CurUserConnection = CurUserBase.CreateConnection();
        //    //CurUser = await CurUserConnection.Table<User>().FirstAsync();
        //    if (CurUser == null)
        //        return false;
        //    return true;
        //}
        public async void GetUsersInfo()
        {
            try
            {
                using (StreamReader sr = new StreamReader(CurUserPath))
                {
                    curmatrikel = sr.ReadLine();
                    curemail = sr.ReadLine();
                    curpass = sr.ReadLine();
                    CurName = sr.ReadLine();
                    CurSurName = sr.ReadLine();

                    sr.Close();
                }
            }
            catch { }
        }

        public string CurName
        {
            get => curname;
            set
            {
                if (curname != value)
                {
                    curname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CurSurName
        {
            get => cursurname;
            set
            {
                if (cursurname != value)
                {
                    cursurname = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop ="" )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( prop ) );
        }
    }
}
