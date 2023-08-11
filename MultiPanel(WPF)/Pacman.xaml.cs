using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MultiPanel_WPF_
{
    public partial class Pacman : Window
    {
        public static Pacman PWindow;

        DispatcherTimer gameTimer = new DispatcherTimer();

        bool goLeft, goRight, goDown, goUp;
        bool noLeft, noRight, noDown, noUp;

        int speed = 8;

        Rect pacmanHitBox;

        int ghostSpeed = 10;
        int ghostMoveStep = 170;
        int currentGhostStep;
        int score = 0;

        public Pacman()
        {
            InitializeComponent();
            GameSetUp();
            PWindow = this;
        }

        private void closeApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void hideApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void movingApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                PWindow.DragMove();
            }
        }

        private void GameSetUp()
        {
            MyCanvas.Focus();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;

            ImageBrush pacmanImage = new ImageBrush();
            pacmanImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Pacman/pacman.png"));
            pacman.Fill = pacmanImage;

            ImageBrush firstGhost = new ImageBrush();
            firstGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Pacman/ghost.png"));
            firstGuy.Fill = firstGhost;

            ImageBrush secondGhost = new ImageBrush();
            secondGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Pacman/ghost.png"));
            secondGuy.Fill = secondGhost;

            ImageBrush thirdGhost = new ImageBrush();
            thirdGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Pacman/ghost.png"));
            thirdGuy.Fill = thirdGhost;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            txtScore.Content = "Печеньки: " + score;

            if (goRight)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (goLeft)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (goUp)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (goDown)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
            }

            if (goDown && Canvas.GetTop(pacman) + 85 > Application.Current.MainWindow.Height)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && Canvas.GetTop(pacman) - 5 < 1)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(pacman) - 5 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(pacman) + 55 > Application.Current.MainWindow.Width)
            {
                noRight = true;
                goRight = false;
            }

            pacmanHitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                if ((string)x.Tag == "wall")
                {
                    if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        noLeft = true;
                        goLeft = false;
                    }

                    if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        noRight = true;
                        goRight = false;
                    }

                    if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        noDown = true;
                        goDown = false;
                    }

                    if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                        noUp = true;
                        goUp = false;
                    }
                }

                if ((string)x.Tag == "coin")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        score++;
                    }
                }

                if ((string)x.Tag == "ghost")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        GameOver("Попался!");
                    }

                    if (x.Name.ToString() == "firstGuy")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - ghostSpeed);
                    }
                    else
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed);
                    }

                    currentGhostStep--;

                    if (currentGhostStep < 1)
                    {
                        currentGhostStep = ghostMoveStep;
                        ghostSpeed = -ghostSpeed;
                    }
                }
            }

            if (score == 108)
            {
                GameOver("Ты выйграл!");
            }
        }

        private void GameOver(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "Конец");

            Pacman pacman = new Pacman();
            pacman.ShowDialog();

            pacman = (Pacman)Window.GetWindow(this);

            if (pacman != null)
            {
                pacman.Close();
            }
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && noLeft == false)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;

                goLeft = true;

                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Right && noRight == false)
            {
                goLeft = goUp = goDown = false;
                noLeft = noUp = noDown = false;

                goRight = true;

                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Up && noUp == false)
            {
                goLeft = goRight = goDown = false;
                noLeft = noRight = noDown = false;

                goUp = true;

                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Down && noDown == false)
            {
                goLeft = goUp = goRight = false;
                noLeft = noUp = noRight = false;

                goDown = true;

                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
        }
    }
}
