using AFStudiumAPIClient.Models.ApiModels;
using SQLite;

namespace AFStudiumApp
{
    public partial class App : Application
    {
        private readonly SqliteConnectionBase _connectionBase;

        public App(SqliteConnectionBase connectionBase)
        {
            InitializeComponent();
            //OnStart();
            MainPage = new AppShell();


            _connectionBase = connectionBase;
        }
        protected override async void OnStart()
        {
            ISQLiteAsyncConnection database = _connectionBase.CreateConnection();
            //await database.DropTableAsync<GoalDto>();
            //await database.DropTableAsync<PlayerDto>();

            await database.CreateTableAsync<User>();
            //await database.CreateTablesAsync<GoalDto, TicketDto>();

            base.OnStart();
        }
    }
}
