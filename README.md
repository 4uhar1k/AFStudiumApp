Exclusively for all TU Dortmund students:
LSF, BOSS, separate websites for event registration—if you're tired of juggling all these platforms, the AFStudium app (Alles für Studium) is the perfect solution. 
It combines all the essential features for managing your studies comfortably, offers a user-friendly interface, and even allows you to send messages to fellow students.

The app is not yet fully completed, and here are some aspects that are still under development:

1) Design:

1.1) The app interface will be redesigned to improve UI/UX.

1.2) There's a known bug with the ScrollView on some pages.

2) Technical aspects:
   
2.1) Interaction with Studienleistung will be improved in the near future.

2.2) The list of events on the MainPage will be updated to ensure users cannot accidentally register for the same event twice.

2.3) The app will integrate a local SQLite database for more convenient and secure user data storage.

How to test the application:

1) Clone this repository to your PC.
2) Clone the AFStudiumDBAPI repository from this link: https://github.com/4uhar1k/AFStudiumDBAPI
3) Combine both repositories into a single Visual Studio solution.
4) Right-click on the AFStudiumAPIClient project, select Build Dependencies, then Project Dependencies, and check the box for AFStudiumDBAPI.
5) Right-click on the AFStudium project, select Build Dependencies, then Project Dependencies, and check the box for AFStudiumAPIClient.
6) Use the MySQL database attached to this repository (afstudiumdb.sql).

If you got any troubles feel free to text me.

Have fun by testing!
