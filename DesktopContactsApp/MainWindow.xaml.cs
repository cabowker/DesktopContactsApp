using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopContactsApp.Classes;
using SQLite;

namespace DesktopContactsApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    List<Contact> contacts;
    public MainWindow()
    {
        InitializeComponent();

        contacts = new List<Contact>();
        ReadDataBase();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
       //Link this button to open new window
       NewContactWindow newContactWindow = new NewContactWindow();
       newContactWindow.ShowDialog();
       
       ReadDataBase();
    }

    void ReadDataBase()
    {
        using (SQLite.SQLiteConnection conn = new SQLiteConnection(App.databasePath))
        {
            conn.CreateTable<Contact>();
            contacts = conn.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();
        }

        if (contacts != null)
        {
            contactsListView.ItemsSource = contacts;
        }
    }

    private void SearchTextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox searchTextBox = sender as TextBox;

        var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
        
        //This is the same above but not done in LINQ....
        /*var fitleredList2 = (from c2 in contacts
            where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
            orderby c2.Email
            select c2).ToList();*/
        
        contactsListView.ItemsSource = filteredList;
    }

    private void ContactsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Contact selectContact = (Contact)contactsListView.SelectedItem;

        if (selectContact != null)
        {
            ContactDetailsView contactDetailsView = new ContactDetailsView(selectContact);
            contactDetailsView.ShowDialog();
            
            ReadDataBase();

        }
    }
}