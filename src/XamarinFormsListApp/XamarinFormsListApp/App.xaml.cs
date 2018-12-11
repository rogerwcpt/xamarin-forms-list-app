using Xamarin.Forms;
using System.Collections.Generic;
using SkiaSharp.Views.Forms;

namespace XamarinFormsListApp {
    public partial class App : Application {

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactListPage());
        }
    }

    public class Contact{
        public Contact(string name, string email) {
            Name = name;
            Email = email;
            Initial = name.Substring(0, 1);
        }

        public string Name { get; }
        public string Email { get; }
        public string Initial { get; }
    }

    public class ContactCell: ViewCell {

        public ContactCell() {
            var grid = new Grid();
            var nameLabel = new Label();
            var emailLabel = new Label();
            var frame = new Frame();
            var initialLabel = new Label();


            nameLabel.SetBinding(Label.TextProperty, "Name");
            emailLabel.SetBinding(Label.TextProperty, "Email");
            grid.Children.Add(nameLabel);
            grid.Children.Add(emailLabel);

            View = grid;
        }
    }

    public class ContactListPage : ContentPage {

        public ContactListPage() {
            Title = "List";

            Content = new ListView {
                RowHeight = 60,
                ItemTemplate = new DataTemplate(typeof(ContactCell)),
                Margin = new Thickness(10, 0, 0, 0),
                ItemsSource = new List<Contact> {
                  new Contact("Isa Tusa", "isa.tusa@me.com"),
                  new Contact("Racquel Ricciardi", "racquel.ricciardi@me.com"),
                  new Contact("Teresita Mccubbin", "teresita.mccubbin@me.com"),
                  new Contact("Rhoda Hassinger", "rhoda.hassinger@me.com"),
                  new Contact("Carson Cupps", "carson.cupps@me.com"),
                  new Contact("Devora Nantz", "devora.nantz@me.com"),
                  new Contact("Tyisha Primus", "tyisha.primus@me.com"),
                  new Contact("Muriel Lewellyn", "muriel.lewellyn@me.com"),
                  new Contact("Hunter Giraud", "hunter.giraud@me.com"),
                  }
               };
            }
        }
    }
