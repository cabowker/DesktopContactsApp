using System.Windows;
using DesktopContactsApp.Classes;
using SQLite;

namespace DesktopContactsApp;

public partial class ContactDetailsView : Window
{
    Contact contact;
    public ContactDetailsView(Contact contact)
    {
        InitializeComponent();
        
        Owner = Application.Current.MainWindow;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        
        this.contact = contact;
        nameTextBox.Text = contact.Name;
        phoneNumberTextBox.Text = contact.Phone;
        eMailTextBox.Text = contact.Email;
        
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        contact.Name = nameTextBox.Text;
        contact.Phone = phoneNumberTextBox.Text;
        contact.Email = eMailTextBox.Text;
        using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
        {
            connection.CreateTable<Contact>();
            connection.Update(contact);
        }
     
        Close();
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
        {
            connection.CreateTable<Contact>();
            connection.Delete(contact);
        }
     
        Close();
    }
}