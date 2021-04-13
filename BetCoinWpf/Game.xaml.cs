using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace BetCoinWpf
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "1OmgRtbk8eXUDANWlGJxS1ybeoWQP2aO4WgYkXXu",
            BasePath = "https://betcoin-5ffb1-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public DispatcherTimer dt = new DispatcherTimer();
        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;
        User Player;

        int speed = 5;

        Random rnd = new Random();

        bool gameOver;

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();

        double multiplier;

        public string multiplierTextProperty
        {
            get
            {
                return multiplierText.Content.ToString();
            }
            set
            {
                multiplierText.Content = value;
            }
        }

        public string multiplpierValueProperty
        {
            set
            {
                multiplierValue.Content = value;
            }
        }

        public string betTextBoxProperty
        {
            get
            {
                return BetTextBoxValue.Text;
            }

            set
            {
                BetTextBoxValue.Text = value;
            }
        }


        /// <summary>
        /// Game constructor, creates an instance of a game
        /// </summary>
        public Game(User user)
        {

            client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = client.Get("Users/");
            Dictionary<string, User> result = response.ResultAs<Dictionary<string, User>>();

            //balanceValue.Content = "init";

            foreach (var get in result)
            {
                //string userId = get.Value.Id.ToString().Substring(1, get.Value.Id.ToString().Length - 2) ;
                //string userId = get.Value.Id.ToString() ;
                string db_name = get.Value.Username.ToString() ;
                string logged_in_name = user.Username;


                Console.WriteLine(logged_in_name);
                Console.WriteLine(db_name);
                if (db_name.Equals(logged_in_name))
                {
                    Player = user;
                    //balance_Value.Content = get.Value.Balance.ToString();
                    //Player = new User(get.Value.Username.ToString(), get.Value.Password.ToString(), get.Value.Bank.ToString(), get.Value.Iban.ToString(), get.Value.Id.ToString(), get.Value.Balance);
                }
                
            }



            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(0.01);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.gif"));

            background.Fill = backgroundSprite;
            secondBackground.Fill = backgroundSprite;
            MyCanvas.Background = backgroundSprite;

            StartGame();
            
        }
        /*
         * Takes a users bet from text box and begins a game.
         * Begins ticking when bet is placed.
         */
        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogic logic = new GameLogic();
            dt.Interval = TimeSpan.FromMilliseconds(1);
            //BetButton_Click();
            dt.Tick += dtTicker;
            dt.IsEnabled = true;
            dt.Start();

            int bet = int.Parse(BetTextBoxValue.Text);
            double multiplier = logic.generateOneMultiplier(bet);
            multiplierValue.Content = multiplier.ToString("n2");
            WinningsValue.Content = (multiplier * bet).ToString("n2");
        }
        /*
         * Engine for the canvas WPF form.
         */
        private void GameEngine(object sender, EventArgs e)
        {

            Canvas.SetLeft(background, Canvas.GetLeft(background) - 8);
            Canvas.SetLeft(secondBackground, Canvas.GetLeft(secondBackground) - 8);

            // if the first background X position goes below -937 pixels
            if (Canvas.GetLeft(background) < -689)
            {
                // position the first background behind the second background
                // below we are setting the backgrounds left, to background2 width position
                Canvas.SetLeft(background, Canvas.GetLeft(secondBackground) + 689);
            }
            // we do the same for the background2
            // if background2 X position goes below -689
            if (Canvas.GetLeft(secondBackground) < -689)
            {
                // position the second background behind the first background
                // below we are setting background2s left position or X position to backgrounds width position
                Canvas.SetLeft(secondBackground, Canvas.GetLeft(background) + 689);
            }

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                spriteIndex += .5;
                if (spriteIndex > 8)
                {
                    spriteIndex = 1;
                }
                RunSprite(spriteIndex);
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
            }
        }
        /*
         * Starts a game by setting the canvas, adding the sprites and resetting multiplier.
         */
        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetTop(secondBackground, 1262);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 140);

            RunSprite(1);

            gameOver = false;
            multiplier = 1.0;

            /*            multiplierText.Content = Convert.ToString(multiplier) + "x";*/

            gameTimer.Start();
            balance_Value.Content = Player.Balance.ToString("n2");


        }
        /*
         * Adds sprite as a bitmap with 4 different frames.
         */
        private void RunSprite(double i)
        {

            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/rocket01.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/rocket02.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/rocket03.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/rocket04.png"));
                    break;
            }
            player.Fill = playerSprite;
        }

       

        private double increment = 1.0;
        private double inputValue = 1.0;

        /*
         * Logic for the "ticker" that is used as a way of ensuring the game is still going on, if the ame crashes,
         * displays a messagebox saying you have crashed and creates a new instance of a game.
         */
        private void dtTicker(object sender, EventArgs e)
        {
            increment += 0.001;
            
            inputValue = Math.Round(increment, 2);
            
            if (Double.Parse(multiplierText.Content.ToString()) == Double.Parse(multiplierValue.Content.ToString()))
            {
                dt.IsEnabled = false;
                int bet = int.Parse(BetTextBoxValue.Text);
                Player.Balance = Player.Balance - Double.Parse(BetTextBoxValue.Text.ToString());
                FirebaseResponse response = client.Set("Users/" + Player.Username, (User)Player);


                multiplierText.Content = "Crashed";
                //Player.Balance = Player.Balance - bet;
                dt.Stop();
                gameTimer.Stop();

                MessageBox.Show("You have crashed!");

                Game nextGame = new Game(Player);
                nextGame.Show();
                this.Close();

            }
            else
            {
                multiplierText.Content = inputValue.ToString();

            }

        }

        /*
         *Stop button logic, when the button is pressed, your bet is pulled out and your profits are shown.
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
  
            dt.Stop();
            gameTimer.Stop();
            dt.IsEnabled = false;
            gameTimer.IsEnabled = false;
            double winnings = (Double.Parse(multiplierText.Content.ToString()) * Double.Parse(BetTextBoxValue.Text.ToString()));
            if(Double.Parse(multiplierText.Content.ToString()) <= Double.Parse(multiplierValue.Content.ToString())) //winning
            {
                Player.Balance = Player.Balance + winnings - Double.Parse(BetTextBoxValue.Text.ToString());
                FirebaseResponse response = client.Set("Users/" + Player.Id, (User)Player);

            }
            else
            {

                Player.Balance -= Double.Parse(BetTextBoxValue.Text.ToString());
                FirebaseResponse response = client.Set("Users/" + Player.Id, (User)Player);

            }

            MessageBox.Show("The game is over you earned " + winnings + " balance: " + Player.Balance);
            //this.Close();
            Game nextGame = new Game(Player);
            nextGame.Show();
            this.Close();
        }
    }
}
