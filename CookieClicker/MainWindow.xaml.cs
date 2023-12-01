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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookieClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal cookieCounter = 0; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickAnimation();
            cookieCounter++;
            UpdateCookieDisplay();
        }
        private void UpdateCookieDisplay()
        {
            LblCookieCount.Content = Math.Floor(cookieCounter);
        }
        public void ClickAnimation()
        {
            // Cookie animatie = margin manipulatie en cookie image verandert
            BitmapImage bitmapImage = new BitmapImage(new Uri("Assets/Images/Cookie_Cute.png", UriKind.RelativeOrAbsolute));
            ImgCookie.Source = bitmapImage;
            ThicknessAnimation clickAnimation = new ThicknessAnimation()
            {
                To = new Thickness(15),
                Duration = TimeSpan.FromMilliseconds(110),
                FillBehavior = FillBehavior.Stop,
            };
            clickAnimation.Completed += ClickAnimation_Completed;

            ImgCookie.BeginAnimation(Image.MarginProperty, clickAnimation);
        }
        private void ClickAnimation_Completed(object sender, EventArgs e)
        {
            // Animatie complete => Originele cookie image
            BitmapImage bitmapImage = new BitmapImage(new Uri("Assets/Images/Cookie.png", UriKind.RelativeOrAbsolute));
            ImgCookie.Source = bitmapImage;
        }
    }
}
