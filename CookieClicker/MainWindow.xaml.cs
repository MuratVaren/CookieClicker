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
using System.Windows.Threading;

namespace CookieClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double cookieCounter = 0;

        private int pointerCounter = 0;
        private double pointerPrice = 15;
        private const double pointerBasePrice = 15;

        private int grannyCounter = 0;
        private double grannyPrice = 100;
        private const double grannyBasePrice = 100;

        private int farmCounter = 0;
        private double farmPrice = 1100;
        private const double farmBasePrice = 1100;

        private int mineCounter = 0;
        private double minePrice = 12000;
        private const double mineBasePrice = 12000;

        private readonly DispatcherTimer PassiveIncomeTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            PassiveIncomeTimer.Tick += PassiveIncomeTimer_Tick;
            PassiveIncomeTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            PassiveIncomeTimer.Start();
        }

        private void PassiveIncomeTimer_Tick(object sender, EventArgs e)
        {
            AddAllPassiveIncome();
            UpdateCookieDisplay();
        }
        public void AddPassiveIncome(int counter, double ammount)
        {
            cookieCounter += counter * ammount;
        }
        public void AddAllPassiveIncome()
        {
            AddPassiveIncome(pointerCounter, 0.001);
            AddPassiveIncome(grannyCounter, 0.01);
            AddPassiveIncome(farmCounter, 0.08);
            AddPassiveIncome(mineCounter, 0.47);
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

        public double UpdatePrice(double baseprice, int counter)
        {
            if (counter >= 1)
            {
                return Math.Round(baseprice * Math.Pow(1.15, counter));
            }
            return baseprice;

        }
        public void UpdateAllPrices()
        {
            pointerPrice = UpdatePrice(pointerBasePrice, pointerCounter);
            grannyPrice = UpdatePrice(grannyBasePrice, grannyCounter);
            farmPrice = UpdatePrice(farmBasePrice, farmCounter);
            minePrice = UpdatePrice(mineBasePrice, mineCounter);
        }
        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonContent = button.Name.Replace("Btn", "");
            switch (buttonContent)
            {
                case "Pointer":
                    cookieCounter -= pointerPrice;
                    pointerCounter++;
                    break;
                case "Granny":
                    cookieCounter -= grannyPrice;
                    grannyCounter++;
                    break;
                case "Farm":
                    cookieCounter -= farmPrice;
                    farmCounter++;
                    break;
                case "Mine":
                    cookieCounter -= minePrice;
                    mineCounter++;
                    break;
            }
            UpdateAllPrices();
            UpdateCookieDisplay();
            ButtonEnabler();
        }
    }
}
