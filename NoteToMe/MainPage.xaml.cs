using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;

namespace NoteToMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        const string EMAIL_ADDRESS_KEY = "EmailAddress";

        IsolatedStorageSettings _isolatedStorageSettings;


        public MainPage()
        {
            InitializeComponent();

            _isolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;
            if (_isolatedStorageSettings.Contains(EMAIL_ADDRESS_KEY)) Address.Text = _isolatedStorageSettings[EMAIL_ADDRESS_KEY].ToString();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            _isolatedStorageSettings[EMAIL_ADDRESS_KEY] = Address.Text;

            base.OnNavigatedFrom(e);
        }

        private void Send_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var subject = "Send to me";
            var to = this.Address.Text;
            var message = this.Message.Text;
            
            var emailComposeTask = new EmailComposeTask
            {
                Subject = subject,
                To = to,
                Body = message
            };

            this.Message.Text = "";
            emailComposeTask.Show();
        }
    }
}
