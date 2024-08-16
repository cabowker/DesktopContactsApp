using System.CodeDom;
using System.Windows;
using System.Windows.Controls;
using DesktopContactsApp.Classes;

namespace DesktopContactsApp.Controls;

public partial class ContactControl : UserControl
{
    public Contact Contact
    {
        get { return (Contact)GetValue(ContactProperty); }
        set {SetValue(ContactProperty, value);}
    }

    public static readonly DependencyProperty ContactProperty =
        DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), 
            new PropertyMetadata(new Contact()
                {Name = "Name Lastname", Phone = "123.456.7890", Email = "example@domain.com"}, SetText));

    private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ContactControl control = d as ContactControl;
        if (control != null)
        {
            control.NameTextBlock.Text = (e.NewValue as Contact).Name;
            control.EmailTextBlock.Text = (e.NewValue as Contact).Email;
            control.PhoneTextBlock.Text = (e.NewValue as Contact).Phone;
        }
    }

    public ContactControl()
    {
        InitializeComponent();
    }
}