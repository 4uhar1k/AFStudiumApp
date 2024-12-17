using AFStudiumAPIClient.Models.ApiModels;
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

        public User CurUser { get; set; }
       // public bool IsLoggedIn { get; set; }

        public ViewModelBase()
        {
           // CurUserBase = new SqliteConnectionBase();
            
            //IsLoggedIn = IsUserLogged().Result;
        }
        public async Task<bool> IsUserLogged()
        {
           // CurUserConnection = CurUserBase.CreateConnection();
            //CurUser = await CurUserConnection.Table<User>().FirstAsync();
            if (CurUser == null)
                return false;
            return true;
        }

        public void OnPropertyChanged([CallerMemberName] string prop ="" )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( prop ) );
        }
    }
}
