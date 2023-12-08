using Microsoft.VisualBasic;
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
        private double cookieTotalCounter = 0;

        private int pointerCounter = 0;
        private double pointerPrice = 15;
        private const double pointerBasePrice = 15;
        private int pointerBonus = 1;
        private int pointerBonusCount = 0;


        private int grannyCounter = 0;
        private double grannyPrice = 100;
        private const double grannyBasePrice = 100;
        private int grannyBonus = 1;
        private int grannyBonusCount = 0;


        private int farmCounter = 0;
        private double farmPrice = 1100;
        private const double farmBasePrice = 1100;
        private int farmBonus = 1;
        private int farmBonusCount = 0;


        private int mineCounter = 0;
        private double minePrice = 12000;
        private const double mineBasePrice = 12000;
        private int mineBonus = 1;
        private int mineBonusCount = 0;


        private int factoryCounter = 0;
        private double factoryPrice = 130000;
        private const double factoryBasePrice = 130000;
        private int factoryBonus = 1;
        private int factoryBonusCount = 0;


        private int bankCounter = 0;
        private double bankPrice = 1400000;
        private const double bankBasePrice = 1400000;
        private int bankBonus = 1;
        private int bankBonusCount = 0;


        private int templeCounter = 0;
        private double templePrice = 20000000;
        private const double templeBasePrice = 20000000;
        private int templeBonus = 1;
        private int templeBonusCount = 0;


        private readonly DispatcherTimer PassiveIncomeTimer = new DispatcherTimer();

        private bool passiveIncome = false;

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
            ButtonEnabler();
            ButtonVisibilityEnabler();
        }
        public void AddPassiveIncome(int counter, double ammount)
        {
            cookieCounter += counter * ammount;
            cookieTotalCounter += counter * ammount;
        }
        public void AddAllPassiveIncome()
        {
            AddPassiveIncome(pointerCounter, 0.001);
            AddPassiveIncome(grannyCounter, 0.01);
            AddPassiveIncome(farmCounter, 0.08);
            AddPassiveIncome(mineCounter, 0.47);
            AddPassiveIncome(factoryCounter, 2.60);
            AddPassiveIncome(bankCounter, 14);
            AddPassiveIncome(templeCounter, 78);
        }

        public void NumberFormat()
        {
            double miljoen = 1000000;
            double miljard = 1000000000;
            double biljoen = 1000000000000;
            double biljard = 1000000000000000;
            double triljoen = 1000000000000000000;

            if (cookieCounter >= miljoen && cookieCounter < miljard)
            {
                LblCookieCount.Content = $"{cookieCounter / miljoen:F3} Miljoen Cookies";
                this.Title = $"{cookieCounter / miljoen:F3} Miljoen Cookies";
            }
            else if (cookieCounter >= miljard && cookieCounter < biljoen)
            {
                LblCookieCount.Content = $"{cookieCounter / miljard:F3} Miljard Cookies";
                this.Title = $"{cookieCounter / miljard:F3} Miljard Cookies";
            }
            else if (cookieCounter >= biljoen && cookieCounter < biljard)
            {
                LblCookieCount.Content = $"{cookieCounter / biljoen:F3} Biljoen Cookies";
                this.Title = $"{cookieCounter / biljoen:F3} Biljoen Cookies";
            }
            else if (cookieCounter >= biljard && cookieCounter < triljoen)
            {
                LblCookieCount.Content = $"{cookieCounter / biljard:F3} Biljard Cookies";
                this.Title = $"{cookieCounter / biljard:F3} Biljard Cookies";
            }
            else if (cookieCounter >= triljoen)
            {
                LblCookieCount.Content = $"{cookieCounter / triljoen:F3} Triljoen Cookies";
                this.Title = $"{cookieCounter / triljoen:F3} Triljoen Cookies";
            }
            else
            {
                LblCookieCount.Content = $"{Math.Floor(cookieCounter).ToString("N0").Replace('.', ' ')} Cookies";
                this.Title = $"{Math.Floor(cookieCounter).ToString("N0").Replace('.', ' ')} Cookies";
            }
        }

        public void Animations()
        {
            ColorAnimationUsingKeyFrames colorAnimation = new ColorAnimationUsingKeyFrames()
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = TimeSpan.FromMilliseconds(6000)
            };
            colorAnimation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Red, KeyTime.FromPercent(0.0)));
            colorAnimation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Blue, KeyTime.FromPercent(0.5)));
            colorAnimation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Green, KeyTime.FromPercent(1.0)));

            LblPassiveIncomePerSecond.Foreground = new SolidColorBrush(Colors.Red);

            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 24,
                To = 40,
                Duration = TimeSpan.FromMilliseconds(250),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
            };

            LblPassiveIncomePerSecond.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            LblPassiveIncomePerSecond.BeginAnimation(Label.FontSizeProperty, doubleAnimation);
        }

        private void ImgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ClickAnimation();
                cookieCounter++;
                cookieTotalCounter++;
                UpdateCookieDisplay();
                ButtonEnabler();
                ButtonVisibilityEnabler();
            }
        }
        private void UpdateCookieDisplay()
        {
            NumberFormat();

            LblPointerPrice.Content = pointerPrice;
            LblGrannyPrice.Content = grannyPrice;
            LblFarmPrice.Content = farmPrice;
            LblMinePrice.Content = minePrice;
            LblFactoryPrice.Content = factoryPrice;
            LblBankPrice.Content = bankPrice;
            LblTemplePrice.Content = templePrice;

            LblPointerCounter.Content = pointerCounter;
            LblGrannyCounter.Content = grannyCounter;
            LblFarmCounter.Content = farmCounter;
            LblMineCounter.Content = mineCounter;
            LblFactoryCounter.Content = factoryCounter;
            LblBankCounter.Content = bankCounter;
            LblTempleCounter.Content = templeCounter;

            LblPointerBonus.Content = $"{pointerBonus}X ➔ {pointerBonus * 2}X";
            LblGrannyBonus.Content = $"{grannyBonus}X ➔ {grannyBonus * 2}X";
            LblFarmBonus.Content = $"{farmBonus}X ➔ {farmBonus * 2}X";
            LblMineBonus.Content = $"{mineBonus}X ➔ {mineBonus * 2}X";
            LblFactoryBonus.Content = $"{factoryBonus}X ➔ {factoryBonus * 2}X";
            LblBankBonus.Content = $"{bankBonus}X ➔ {bankBonus * 2}X";
            LblTempleBonus.Content = $"{templeBonus}X ➔ {templeBonus * 2}X";

            LblPassiveIncomePerSecond.Content =
                $"+{(pointerCounter * 0.1 * pointerBonus) + (grannyCounter * 1 * grannyBonus) + (farmCounter * 8 * farmBonus) + (mineCounter * 47 * mineBonus) + (factoryCounter * 260 * factoryBonus) + (bankCounter * 1400 * bankBonus) + (templeCounter * 7800 * templeBonus)}/s";
        }

        public void ButtonEnabler()
        {
            BtnPointer.IsEnabled = cookieCounter >= pointerPrice;
            BtnGranny.IsEnabled = cookieCounter >= grannyPrice;
            BtnFarm.IsEnabled = cookieCounter >= farmPrice;
            BtnMine.IsEnabled = cookieCounter >= minePrice;
            BtnFactory.IsEnabled = cookieCounter >= factoryPrice;
            BtnBank.IsEnabled = cookieCounter >= bankPrice;
            BtnTemple.IsEnabled = cookieCounter >= templePrice;
        }
        public void ButtonVisibilityEnabler()
        {
            BtnPointer.Visibility = cookieTotalCounter >= pointerBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnGranny.Visibility = cookieTotalCounter >= grannyBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnFarm.Visibility = cookieTotalCounter >= farmBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnMine.Visibility = cookieTotalCounter >= mineBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnFactory.Visibility = cookieTotalCounter >= factoryBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnBank.Visibility = cookieTotalCounter >= bankBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnTemple.Visibility = cookieTotalCounter >= templeBasePrice ? Visibility.Visible : Visibility.Hidden;

            TabBonus.Visibility = BtnPointer.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;

            BtnPointerBonus.Visibility = BtnPointer.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnGrannyBonus.Visibility = BtnGranny.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnFarmBonus.Visibility = BtnFarm.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnMineBonus.Visibility = BtnMine.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnFactoryBonus.Visibility = BtnFactory.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBankBonus.Visibility = BtnBank.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnTempleBonus.Visibility = BtnTemple.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;

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
            factoryPrice = UpdatePrice(factoryBasePrice, factoryCounter);
            bankPrice = UpdatePrice(bankBasePrice, bankCounter);
            templePrice = UpdatePrice(templeBasePrice, templeCounter);

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
                    StackpanelVisualizeItems("Pointer");
                    break;
                case "Granny":
                    cookieCounter -= grannyPrice;
                    grannyCounter++;
                    StackpanelVisualizeItems("Granny");
                    break;
                case "Farm":
                    cookieCounter -= farmPrice;
                    farmCounter++;
                    StackpanelVisualizeItems("Farm");
                    break;
                case "Mine":
                    cookieCounter -= minePrice;
                    mineCounter++;
                    StackpanelVisualizeItems("Mine");
                    break;
                case "Factory":
                    cookieCounter -= factoryPrice;
                    factoryCounter++;
                    StackpanelVisualizeItems("Factory");
                    break;
                case "Bank":
                    cookieCounter -= bankPrice;
                    bankCounter++;
                    StackpanelVisualizeItems("Bank");
                    break;
                case "Temple":
                    cookieCounter -= templePrice;
                    templeCounter++;
                    StackpanelVisualizeItems("Temple");
                    break;
            }
            if (passiveIncome == false)
            {
                passiveIncome = true;
                Animations();
            }
            UpdateAllPrices();
            UpdateCookieDisplay();
            ButtonEnabler();
        }
        private void LblBakeryName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string name = "";
            while (string.IsNullOrWhiteSpace(name))
            {
                name = Interaction.InputBox("Enter a new name for the bakery");
            }
            LblBakeryName.Content = name;
        }

        public void StackpanelVisualizeItems(string itemName)
        {
            StackPanel stackpanel = FindName($"Stackpanel{itemName}") as StackPanel;
            if (stackpanel.Visibility == Visibility.Collapsed)
            {
                stackpanel.Visibility = Visibility.Visible;
            }

            BitmapImage bitmap = new BitmapImage(new Uri($"Assets/Images/{itemName}.png", UriKind.RelativeOrAbsolute));
            Image image = new Image();
            image.Source = bitmap;
            image.Width = 80;

            StackPanel stackpanelItems = FindName($"Stackpanel{itemName}Items") as StackPanel;
            stackpanelItems.Children.Add(image);
        }

        private void BtnBonusBuy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
