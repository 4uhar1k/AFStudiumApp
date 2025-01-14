using AFStudiumApp.ViewModels;

namespace AFStudiumApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Navigated += UpdateBindingContext; 
        }

        public void UpdateBindingContext(object sender, ShellNavigatedEventArgs e)
        {
            if (Shell.Current.CurrentPage.Title == "ProfilePage")
            {
                Shell.Current.CurrentPage.BindingContext = new ViewModelBase();
            }
        }
    }
}
