using System;
using System.Windows;
using System.Windows.Controls;

namespace MultiPanel_WPF_.MVVM.View
{
    public partial class PacmanView : UserControl
    {
        public PacmanView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pacman pacman = new Pacman();
            pacman.Show();

            MainWindow mWindow = (MainWindow)Window.GetWindow(this);

            if (pacman != null)
            {
                mWindow.Close();
            }
        }
    }
}
