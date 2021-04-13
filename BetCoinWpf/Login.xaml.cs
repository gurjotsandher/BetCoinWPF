using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace BetCoinWpf
{
    /// <summary>  
    /// Interaction logic for MainWindow.xaml  
    /// </summary>   
    public partial class Login : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "1OmgRtbk8eXUDANWlGJxS1ybeoWQP2aO4WgYkXXu",
            BasePath = "https://betcoin-5ffb1-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        /*
         * Initializes the firebase client object and registration object.
         */
        public Login()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
        }
        Registration registration = new Registration();
        public string textBoxEmailProperty
        {
            get
            {
                return textBoxEmail.Text;
            }
        }

        public string textBoxPasswordProperty
        {
            get
            {
                return passwordBox1.Password.ToString();
            }
        }
        /*
         * Login button, ensures username and password are both entered, then consults firebase DB
         * for accounts matching the information.
         */
        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxEmail.Text) || string.IsNullOrEmpty(passwordBox1.Password.ToString()))
            {

                MessageBox.Show("Please enter both username and password.");
            }
            else
            {

                FirebaseResponse response = client.Get("Users/");
                Console.WriteLine(response);
                Dictionary<Object, User> result = response.ResultAs<Dictionary<Object, User>>();

                foreach (var get in result)
                {
                    string userresult = get.Value.Username;
                    string passresult = get.Value.Password;

                    if (textBoxEmail.Text == userresult)
                    {

                        if (passwordBox1.Password.ToString() == passresult)
                        {
                            User player = new User(get.Value.Username, get.Value.Password, get.Value.Bank, get.Value.Iban, get.Value.Id, get.Value.Balance);

                            MessageBox.Show("Welcome " + textBoxEmail.Text + " you currently have " + player.Balance);

                            Game game = new Game(player);
                            game.Show();
                            
                            this.Hide();
                            

                        }

                    }
                }
            }
        }
        /*
         * Opens registration page.
         */
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }
    }
}