using System;
using System.Windows;
using System.Windows.Controls;

namespace MultiPanel_WPF_.MVVM.View
{
    public partial class SpaceInvadersView : UserControl
    {
        public SpaceInvadersView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpaceInvaders spaceInvaders = new SpaceInvaders();
            spaceInvaders.Show();

            MainWindow mWindow = (MainWindow)Window.GetWindow(this);

            if(spaceInvaders != null)
            {
                mWindow.Close();
            }
        }
    }
}
