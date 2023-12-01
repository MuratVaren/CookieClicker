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

        private int pointerCounter = 0;
        private decimal pointerPrice = 15;
        private const decimal pointerBasePrice = 15;

        private int grannyCounter = 0;
        private decimal grannyPrice = 100;
        private const decimal grannyBasePrice = 100;

        private int farmCounter = 0;
        private decimal farmPrice = 1100;
        private const decimal farmBasePrice = 1100;

        private int mineCounter = 0;
        private decimal minePrice = 12000;
        private const decimal mineBasePrice = 12000;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                ClickAnimation();
                cookieCounter++;
                UpdateCookieDisplay();
                ButtonEnabler();
            }
        }
        private void UpdateCookieDisplay()
        {
            LblCookieCount.Content = $"{Math.Floor(cookieCounter)} Cookies";
            this.Title = $"{Math.Floor(cookieCounter)} Cookies";
        }
        
        public void ButtonEnabler()
        {
            BtnPointer.IsEnabled = cookieCounter >= pointerPrice;
            BtnGranny.IsEnabled = cookieCounter >= grannyPrice;
            BtnFarm.IsEnabled = cookieCounter >= farmPrice;
            BtnMine.IsEnabled = cookieCounter >= minePrice;
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
