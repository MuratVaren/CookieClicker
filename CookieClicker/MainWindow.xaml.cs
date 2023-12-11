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
        private int pointerBonusCounter = -1;
        private double pointerBonusPrice = 0;

        private int grannyCounter = 0;
        private double grannyPrice = 100;
        private const double grannyBasePrice = 100;
        private int grannyBonus = 1;
        private int grannyBonusCounter = -1;
        private double grannyBonusPrice = 0;

        private int farmCounter = 0;
        private double farmPrice = 1100;
        private const double farmBasePrice = 1100;
        private int farmBonus = 1;
        private int farmBonusCounter = -1;
        private double farmBonusPrice = 0;

        private int mineCounter = 0;
        private double minePrice = 12000;
        private const double mineBasePrice = 12000;
        private int mineBonus = 1;
        private int mineBonusCounter = -1;
        private double mineBonusPrice = 0;

        private int factoryCounter = 0;
        private double factoryPrice = 130000;
        private const double factoryBasePrice = 130000;
        private int factoryBonus = 1;
        private int factoryBonusCounter = -1;
        private double factoryBonusPrice = 0;

        private int bankCounter = 0;
        private double bankPrice = 1400000;
        private const double bankBasePrice = 1400000;
        private int bankBonus = 1;
        private int bankBonusCounter = -1;
        private double bankBonusPrice = 0;

        private int templeCounter = 0;
        private double templePrice = 20000000;
        private const double templeBasePrice = 20000000;
        private int templeBonus = 1;
        private int templeBonusCounter = -1;
        private double templeBonusPrice = 0;

        private double totalPassiveIncomePerSecond = 0;

        private readonly DispatcherTimer PassiveIncomeTimer = new DispatcherTimer();
        private readonly DispatcherTimer GoldenCookieTimer = new DispatcherTimer();
        private readonly DispatcherTimer GoldenCookieActiveTimer = new DispatcherTimer();

        private bool changedBakeryName = false;

        private bool passiveIncome = false;

        private string[,] quests = new string[,]
        {
            {"cookieCounter","68","Yo the next achievement you're gonna get is gonna be soooo coool just wait. " +
                "Trust me it's like watching avengers infinity war in imax theaters 7D ultra HDR 120 fps rgb LED on your 144HZ monitor, no cap bruh","incomplete"},
            {"cookieCounter","69","Haha 69 funny, still can't get a girlfriend though. I'm kinda lonely, everyday, every night by myself in my corner. " +
                "Crying and crying. Watching anime. Biting ice cream. Peeing my bed. just because i can. #LonerBoY","incomplete" },
            {"cookieCounter","420","YEAAAAA BlAZEEEE ITTTTTT\nActually don't drugs are bad especially bad drugs those are bat with a t yes i did not mizpel that wat ar u un aboat?","incomplete" },
            {"cookieCounter","1000000","You're a millionaire enjoy it because you'll never be rich :)","incomplete"},
            {"cookieTotalCounter","10000","You made over 10000 Cookies.\nQuite the baker you are!","incomplete"},
            {"changedBakeryName","1","Wow you changed the name of your bakery.\nCant stick to anything can you ;(","incomplete"},
            {"totalPassiveIncomePerSecond","1000","1000 cookies per second SOOOOOO FASTTTT","incomplete" },
            {"pointerCounter","30","30 pointers, you're on POINT!!!\nHaha get it! On 'POINT' since you bought pointers!\nsorry...","incomplete"},
            {"grannyCounter","25","WOW 25 grannys that's a lot of christmas gifts.\n(if u are not christian pls don't be offended)","incomplete"},
            {"farmCounter", "20","20 farms, It seems like you got a 'HERD' HA","incomplete"},
            {"mineCounter","1","You got your first mine\nYou didn't need one because you're already a gem :0","incomplete"},
            {"factoryCounter","20","You owning factories in real life, ha thats not gonna happen.","incomplete"},
            {"bankCounter","5","Don't rob a bank instead thank frank (the cookie)","incomplete"},
            {"templeCounter","3","Going to the temple, time to be gentle and relax your mental","incomplete"},
            {"pointerBonus","64","64X pointer bonus about to join granny club","incomplete" },
            {"grannyBonus","32","32X granny bonus, still having a endlife crisis","incomplete" },
            {"farmBonus","16","16X farm bonus almost legal","incomplete"},
            {"mineBonus","8","8X mine bonus elementary school kinda hard ;(","incomplete"},
            {"factoryBonus","4","4 times 4 is 16 divide that by 4 is 4","incomplete"},
            {"bankBonus","2","2 wow i remember being two but nobody knew me so they said who","incomplete"}

        };
        public Dictionary<string, double> values = new Dictionary<string, double>();

        public MainWindow()
        {
            InitializeComponent();
            PassiveIncomeTimer.Tick += PassiveIncomeTimer_Tick;
            PassiveIncomeTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            PassiveIncomeTimer.Start();

            GoldenCookieTimer.Tick += GoldenCookieTimer_Tick;
            GoldenCookieTimer.Interval = new TimeSpan(0, 1, 0);
            GoldenCookieTimer.Start();

            GoldenCookieActiveTimer.Tick += GoldenCookieActiveTimer_Tick;
            GoldenCookieActiveTimer.Interval = new TimeSpan(0, 0, 10);
        }

        public void QuestCompleteSystem()
        {
            for (int i = 0; i < quests.GetLength(0); i++)
            {
                string counterName = quests[i, 0];
                int targetValue = int.Parse(quests[i, 1]);
                if (quests[i, 3] != "complete")
                {
                    if (values.ContainsKey(counterName))
                    {
                        if (values[counterName] >= targetValue)
                        {
                            MessageBox.Show(quests[i, 2],"Mission Complete!");
                            quests[i, 3] = "complete";
                        }
                    }
                }
            }
        }
        public void QuestDictionaryVullen()
        {
            values["cookieCounter"] = cookieCounter;
            values["cookieTotalCounter"] = cookieTotalCounter;
            values["changedBakeryName"] = changedBakeryName ? 1 : 0;
            values["totalPassiveIncomePerSecond"] = totalPassiveIncomePerSecond;

            values["pointerCounter"] = pointerCounter;
            values["grannyCounter"] = grannyCounter;
            values["farmCounter"] = farmCounter;
            values["mineCounter"] = mineCounter;
            values["factoryCounter"] = factoryCounter;
            values["bankCounter"] = bankCounter;
            values["templeCounter"] = templeCounter;

            values["pointerBonus"] = pointerBonus;
            values["grannyBonus"] = grannyBonus;
            values["farmBonus"] = farmBonus;
            values["mineBonus"] = mineBonus;
            values["factoryBonus"] = factoryBonus;
            values["bankBonus"] = bankBonus;
            values["templeBonus"] = templeBonus;
        }
        private void GoldenCookieActiveTimer_Tick(object sender, EventArgs e)
        {
            if (MyCanvas.Children.Count > 0)
            {
                MyCanvas.Children.RemoveAt(0);
            }
            GoldenCookieActiveTimer.Stop();
        }

        private void GoldenCookieTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            int chance = random.Next(1,101);
            if (chance <= 30 && totalPassiveIncomePerSecond > 0)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Assets/Images/Cookie_Cute.png", UriKind.RelativeOrAbsolute));
                Image imageGoldenCookie = new Image();
                imageGoldenCookie.Source = bitmapImage;
                imageGoldenCookie.Width = 80;
                imageGoldenCookie.Height = 80;
                imageGoldenCookie.MouseDown += ImageGoldenCookie_MouseDown;

                double canvasWidth = MyCanvas.ActualWidth - imageGoldenCookie.Width;
                double canvasHeight = MyCanvas.ActualHeight - imageGoldenCookie.Height;

                Canvas.SetLeft(imageGoldenCookie, random.NextDouble() * canvasWidth);
                Canvas.SetTop(imageGoldenCookie, random.NextDouble() * canvasHeight);

                MyCanvas.Children.Add(imageGoldenCookie);
                GoldenCookieActiveTimer.Start();
            }
        }

        private void ImageGoldenCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cookieCounter += totalPassiveIncomePerSecond * 60 * 15;
            cookieTotalCounter += totalPassiveIncomePerSecond * 60 * 15;
            GoldenCookieActiveTimer.Stop();
            if (MyCanvas.Children.Count > 0)
            {
                MyCanvas.Children.RemoveAt(0);
            }
        }

        private void PassiveIncomeTimer_Tick(object sender, EventArgs e)
        {
            AddAllPassiveIncome();
            UpdateCookieDisplay();
            ButtonEnabler();
            ButtonVisibilityEnabler();
            QuestDictionaryVullen();
            QuestCompleteSystem();

        }
        public void AddPassiveIncome(int counter, double ammount, int bonus)
        {
            cookieCounter += counter * ammount * bonus;
            cookieTotalCounter += counter * ammount * bonus;
        }
        public void AddAllPassiveIncome()
        {
            AddPassiveIncome(pointerCounter, 0.001, pointerBonus);
            AddPassiveIncome(grannyCounter, 0.01, grannyBonus);
            AddPassiveIncome(farmCounter, 0.08, farmBonus);
            AddPassiveIncome(mineCounter, 0.47, mineBonus);
            AddPassiveIncome(factoryCounter, 2.60, factoryBonus);
            AddPassiveIncome(bankCounter, 14, bankBonus);
            AddPassiveIncome(templeCounter, 78, templeBonus);
        }

        public string NumberFormat(double count)
        {
            double miljoen = 1000000;
            double miljard = 1000000000;
            double biljoen = 1000000000000;
            double biljard = 1000000000000000;
            double triljoen = 1000000000000000000;

            if (count >= miljoen && count < miljard)
            {
                return  $"{count / miljoen:F3} Miljoen";
            }
            else if (count >= miljard && count < biljoen)
            {
                return $"{count / miljard:F3} Miljard";
            }
            else if (count >= biljoen && count < biljard)
            {
                return $"{count / biljoen:F3} Biljoen";
            }
            else if (count >= biljard && count < triljoen)
            {
                return $"{count / biljard:F3} Biljard";
            }
            else if (count >= triljoen)
            {
                return $"{count / triljoen:F3} Triljoen";
            }
            else
            {
                return $"{count.ToString("N0").Replace('.', ' ')}";
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
            this.Title = NumberFormat(Math.Floor(cookieCounter)) + " Cookies";
            LblCookieCount.Content = NumberFormat(Math.Floor(cookieCounter)) + " Cookies";

            LblPointerPrice.Content = NumberFormat(pointerPrice);
            LblGrannyPrice.Content = NumberFormat(grannyPrice);
            LblFarmPrice.Content = NumberFormat(farmPrice);
            LblMinePrice.Content = NumberFormat(minePrice);
            LblFactoryPrice.Content = NumberFormat(factoryPrice);
            LblBankPrice.Content = NumberFormat(bankPrice);
            LblTemplePrice.Content = NumberFormat(templePrice);

            LblPointerCounter.Content = NumberFormat(pointerCounter);
            LblGrannyCounter.Content = NumberFormat(grannyCounter);
            LblFarmCounter.Content = NumberFormat(farmCounter);
            LblMineCounter.Content = NumberFormat(mineCounter);
            LblFactoryCounter.Content = NumberFormat(factoryCounter);
            LblBankCounter.Content = NumberFormat(bankCounter);
            LblTempleCounter.Content = NumberFormat(templeCounter);

            LblPointerBonus.Content = $"{pointerBonus}X ➔ {pointerBonus * 2}X";
            LblGrannyBonus.Content = $"{grannyBonus}X ➔ {grannyBonus * 2}X";
            LblFarmBonus.Content = $"{farmBonus}X ➔ {farmBonus * 2}X";
            LblMineBonus.Content = $"{mineBonus}X ➔ {mineBonus * 2}X";
            LblFactoryBonus.Content = $"{factoryBonus}X ➔ {factoryBonus * 2}X";
            LblBankBonus.Content = $"{bankBonus}X ➔ {bankBonus * 2}X";
            LblTempleBonus.Content = $"{templeBonus}X ➔ {templeBonus * 2}X";

            LblPointerBonusPrice.Content = NumberFormat(pointerBonusPrice);
            LblGrannyBonusPrice.Content = NumberFormat(grannyBonusPrice);
            LblFarmBonusPrice.Content = NumberFormat(farmBonusPrice);
            LblMineBonusPrice.Content = NumberFormat(mineBonusPrice);
            LblFactoryBonusPrice.Content = NumberFormat(factoryBonusPrice);
            LblBankBonusPrice.Content = NumberFormat(bankBonusPrice);
            LblTempleBonusPrice.Content = NumberFormat(templeBonusPrice);

            if (totalPassiveIncomePerSecond < 1000000)
            {
                LblPassiveIncomePerSecond.Content = $"+{totalPassiveIncomePerSecond.ToString("N1").Replace('.', ' ')}/s";
            }
            else
            {
                LblPassiveIncomePerSecond.Content = $"+{NumberFormat(totalPassiveIncomePerSecond)}/s";
            }
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

            BtnBonusPointer.IsEnabled = cookieCounter >= pointerBonusPrice && pointerCounter >= 1;
            BtnBonusGranny.IsEnabled = cookieCounter >= grannyBonusPrice && grannyCounter >= 1;
            BtnBonusFarm.IsEnabled = cookieCounter >= farmBonusPrice && farmCounter >= 1;
            BtnBonusMine.IsEnabled = cookieCounter >= mineBonusPrice && mineCounter >= 1;
            BtnBonusFactory.IsEnabled = cookieCounter >= factoryBonusPrice && factoryCounter >= 1;
            BtnBonusBank.IsEnabled = cookieCounter >= bankBonusPrice && bankCounter >= 1;
            BtnBonusTemple.IsEnabled = cookieCounter >= templeBonusPrice && templeCounter >= 1;
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

            BtnBonusPointer.Visibility = BtnPointer.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusGranny.Visibility = BtnGranny.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusFarm.Visibility = BtnFarm.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusMine.Visibility = BtnMine.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusFactory.Visibility = BtnFactory.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusBank.Visibility = BtnBank.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusTemple.Visibility = BtnTemple.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
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
        public double UpdateBonusPrice(double baseprice, int counter)
        {
            if(counter < 0)
            {
                return baseprice * 100;
            }
            else
            {
                return baseprice * 100 * (5 * Math.Pow(10,counter));
            }
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

            pointerBonusPrice = UpdateBonusPrice(pointerBasePrice, pointerBonusCounter);
            grannyBonusPrice = UpdateBonusPrice(grannyBasePrice, grannyBonusCounter);
            farmBonusPrice = UpdateBonusPrice(farmBasePrice, farmBonusCounter);
            mineBonusPrice = UpdateBonusPrice(mineBasePrice, mineBonusCounter);
            factoryBonusPrice = UpdateBonusPrice(factoryBasePrice, factoryBonusCounter);
            bankBonusPrice = UpdateBonusPrice(bankBasePrice, bankBonusCounter);
            templeBonusPrice = UpdateBonusPrice(templeBasePrice, templeBonusCounter);
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
            totalPassiveIncomePerSecond =
                (pointerCounter * 0.1 * pointerBonus) + (grannyCounter * 1 * grannyBonus) +
                (farmCounter * 8 * farmBonus) + (mineCounter * 47 * mineBonus) + (factoryCounter * 260 * factoryBonus)
                + (bankCounter * 1400 * bankBonus) + (templeCounter * 7800 * templeBonus);
        }

        private void LblBakeryName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string name = "";
            while (string.IsNullOrWhiteSpace(name))
            {
                name = Interaction.InputBox("Enter a new name for the bakery");
            }
            LblBakeryName.Content = name;
            if(changedBakeryName == false)
            {
                changedBakeryName = true;
            }
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
            Button button = (Button)sender;
            string buttonContent = button.Name.Replace("BtnBonus", "");
            switch (buttonContent)
            {
                case "Pointer":
                    cookieCounter -= pointerBonusPrice;
                    pointerBonusCounter++;
                    pointerBonus *= 2;
                    BtnPointer.ToolTip = $"{0.1 * pointerBonus} Cookies per second";
                    break;
                case "Granny":
                    cookieCounter -= grannyBonusPrice;
                    grannyBonusCounter++;
                    grannyBonus *= 2;
                    BtnGranny.ToolTip = $"{1 * grannyBonus} Cookies per second";
                    break;
                case "Farm":
                    cookieCounter -= farmBonusPrice;
                    farmBonusCounter++;
                    farmBonus *= 2;
                    BtnFarm.ToolTip = $"{8 * farmBonus} Cookies per second";
                    break;
                case "Mine":
                    cookieCounter -= mineBonusPrice;
                    mineBonusCounter++;
                    mineBonus *= 2;
                    BtnMine.ToolTip = $"{47 * mineBonus} Cookies per second";
                    break;
                case "Factory":
                    cookieCounter -= factoryBonusPrice;
                    factoryBonusCounter++;
                    factoryBonus *= 2;
                    BtnFactory.ToolTip = $"{260 * factoryBonus} Cookies per second";
                    break;
                case "Bank":
                    cookieCounter -= bankBonusPrice;
                    bankBonusCounter++;
                    bankBonus *= 2;
                    BtnBank.ToolTip = $"{1400 * bankBonus} Cookies per second";
                    break;
                case "Temple":
                    cookieCounter -= templeBonusPrice;
                    templeBonusCounter++;
                    templeBonus *= 2;
                    BtnTemple.ToolTip = $"{7800 * templeBonus} Cookies per second";
                    break;
            }
            UpdateAllPrices();
            UpdateCookieDisplay();
            ButtonEnabler();
            totalPassiveIncomePerSecond = 
                (pointerCounter * 0.1 * pointerBonus) + (grannyCounter * 1 * grannyBonus) + 
                (farmCounter * 8 * farmBonus) + (mineCounter * 47 * mineBonus) + (factoryCounter * 260 * factoryBonus)
                + (bankCounter * 1400 * bankBonus) + (templeCounter * 7800 * templeBonus);
        }
    }
}
