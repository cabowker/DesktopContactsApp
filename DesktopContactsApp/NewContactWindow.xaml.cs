using System.IO;
using System.Windows;
using DesktopContactsApp.Classes;
using SQLite;

namespace DesktopContactsApp;

public partial class NewContactWindow : Window
{
    public NewContactWindow()
    {
        InitializeComponent();

        Owner = Application.Current.MainWindow;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
    }

    private void SaveButton(object sender, RoutedEventArgs e)
    {
     //Save Contact
     Contact contact = new Contact()
     {
         Name = nameTextBox.Text,
         Email = eMailTextBox.Text,
         Phone = phoneNumberTextBox.Text
     };


     using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
     {
         connection.CreateTable<Contact>();
         connection.Insert(contact);
     }
     
     Close();
    }
}