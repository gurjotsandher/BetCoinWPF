///<authors>
/// Arya Koukia, Gurjot Mander, Gurjot Sandher
/// </authors>


using System;
using System.Windows;


using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace BetCoinWpf
{

    /// <summary>  
    /// Interaction logic for Registration.xaml  
    /// </summary>  
    public partial class Registration : Window
    {

        public string textBoxEmailProperty
        {
            get
            {
                return textBoxUsername.Text;
            }
        }

        public string passwordBoxProperty
        {
            get
            {
                return passwordBox1.Password.ToString();
            }
        }

        public string textBoxIdProperty
        {
            get
            {
                return textBoxID.Text;
            }
        }

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "1OmgRtbk8eXUDANWlGJxS1ybeoWQP2aO4WgYkXXu",
            BasePath = "https://betcoin-5ffb1-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        /*
         * Initializes the firebase client object.
         */
        public Registration()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
        }
        /*
         * Opens login window.
         */
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
        /*
         * Button that resets all fields.
         */
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        /*
         * Logic to reset all fields on registration page.
         */
        public void Reset()
        {
            //textBoxID.Text = "";
            textBoxUsername.Text = "";
            passwordBox1.Password = "";
        }

        /*
         * Submit button that checks if all fields are filled, then consults FireBase DB for result.
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUsername.Text) || string.IsNullOrEmpty(passwordBox1.Password.ToString()) || string.IsNullOrEmpty(textBoxID.Text) || string.IsNullOrEmpty(textBoxBalance.Text) 
                || string.IsNullOrEmpty(textBoxBank_name.Text) || string.IsNullOrEmpty(textBoxBank_IBAN.Text))
            {
                MessageBox.Show("Please fill out all of the fields.");
            }
            else
            {

                User user = new User(textBoxID.Text.ToString(), textBoxUsername.Text.ToString(), passwordBox1.Password.ToString(), textBoxBank_name.Text.ToString(), textBoxBank_IBAN.Text.ToString(), Double.Parse(textBoxBalance.Text.ToString()));

                FirebaseResponse response = client.Set("Users/" + user.Username, (User)user);

                User res = response.ResultAs<User>();
                MessageBox.Show("Your account has been registered.");

                textBoxUsername.Text = string.Empty;
                passwordBox1.Password = string.Empty;
                Login login = new Login();
                login.Show();
                Close();
            }


        }
    }
}