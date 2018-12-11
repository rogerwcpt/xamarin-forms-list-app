using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Globalization;
using SkiaSharp;
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

    public class ContactCell: ImageCell {

        public ContactCell() {
            this.SetBinding(TextProperty, "Name");
            this.SetBinding(DetailProperty, "Email");
            this.SetBinding(ImageSourceProperty, "Initial", BindingMode.OneWay, new CharToAvatarConverter());
        }
    }

    public class ContactListPage : ContentPage {
        public ContactListPage() {
            Title = "List";
            Content = new ListView {
                RowHeight = 65,
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


    public class CharToAvatarConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int bitmapSize = 200;
            var bitmap = new SKBitmap(bitmapSize, bitmapSize);
            var paint = new SKPaint() {
                Color = SKColors.Beige
            };
            var paintText = new SKPaint() {
                TextSize = 64.0f,
                IsAntialias = true
            };
            var width = paintText.MeasureText(value.ToString());
            var canvas = new SKCanvas(bitmap);
            canvas.DrawCircle(bitmapSize/2, bitmapSize/2, 80, paint);
            canvas.DrawText(value.ToString(), (bitmapSize / 2) - (width / 2), 120, paintText);
            return new SKBitmapImageSource {
                Bitmap = bitmap
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
