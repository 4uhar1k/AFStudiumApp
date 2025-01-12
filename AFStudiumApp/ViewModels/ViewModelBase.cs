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
        public string curname, cursurname;
        protected readonly AFStudiumAPIClientService _apiClient;
        public User CurUser { get; set; }
        // public bool IsLoggedIn { get; set; }

        

        public ViewModelBase(AFStudiumAPIClientService apiClient)
        {
            CurUser = new User();
            _apiClient = apiClient;
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
            using (StreamReader sr = new StreamReader(CurUserPath))
            {
                
                CurName = sr.ReadLine();
                CurSurName = sr.ReadLine();
                
                sr.Close();
            }
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
