using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        // pas beide aan om te testen.
        private double cookieCounter = 0;
        private double cookieTotalCounter = 0;

        private double totalPassiveIncomePerSecond = 0;

        // pointer waardes
        private int pointerCounter = 0;
        private double pointerPrice = 15;
        private const double pointerBasePrice = 15;
        private int pointerBonus = 1;
        private int pointerBonusCounter = -1;
        private double pointerBonusPrice = 0;
        private double pointerTotalPassiveIncome = 0;

        // granny waardes
        private int grannyCounter = 0;
        private double grannyPrice = 100;
        private const double grannyBasePrice = 100;
        private int grannyBonus = 1;
        private int grannyBonusCounter = -1;
        private double grannyBonusPrice = 0;
        private double grannyTotalPassiveIncome = 0;


        // farm waardes
        private int farmCounter = 0;
        private double farmPrice = 1100;
        private const double farmBasePrice = 1100;
        private int farmBonus = 1;
        private int farmBonusCounter = -1;
        private double farmBonusPrice = 0;
        private double farmTotalPassiveIncome = 0;


        // mine waardes
        private int mineCounter = 0;
        private double minePrice = 12000;
        private const double mineBasePrice = 12000;
        private int mineBonus = 1;
        private int mineBonusCounter = -1;
        private double mineBonusPrice = 0;
        private double mineTotalPassiveIncome = 0;


        // factory waardes
        private int factoryCounter = 0;
        private double factoryPrice = 130000;
        private const double factoryBasePrice = 130000;
        private int factoryBonus = 1;
        private int factoryBonusCounter = -1;
        private double factoryBonusPrice = 0;
        private double factoryTotalPassiveIncome = 0;


        // bank waardes
        private int bankCounter = 0;
        private double bankPrice = 1400000;
        private const double bankBasePrice = 1400000;
        private int bankBonus = 1;
        private int bankBonusCounter = -1;
        private double bankBonusPrice = 0;
        private double bankTotalPassiveIncome = 0;


        // temple waardes
        private int templeCounter = 0;
        private double templePrice = 20000000;
        private const double templeBasePrice = 20000000;
        private int templeBonus = 1;
        private int templeBonusCounter = -1;
        private double templeBonusPrice = 0;
        private double templeTotalPassiveIncome = 0;

        private int imgCookieClickAmmount = 0;
        private int imgGoldenCookieClickAmmount = 0;
        private double timeSpentInGameInSeconds = 0;

        /// <summary>
        /// Timer dat passive income geeft
        /// </summary>
        private readonly DispatcherTimer PassiveIncomeTimer = new DispatcherTimer();
        private readonly DispatcherTimer GoldenCookieTimer = new DispatcherTimer();
        private readonly DispatcherTimer GoldenCookieActiveTimer = new DispatcherTimer();
        private DispatcherTimer TimeSpentTimer = new DispatcherTimer();

        private bool changedBakeryName = false;

        private bool passiveIncome = false;

        private readonly string[,] quests = new string[,]
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
            {"farmBonus","16","16X farm bonus you can almost get your driver's license","incomplete"},
            {"mineBonus","8","8X mine bonus elementary school kinda hard ;(","incomplete"},
            {"factoryBonus","4","4 times 4 is 16 divide that by 4 is 4","incomplete"},
            {"bankBonus","2","2 wow i remember being two but nobody knew me so they said who","incomplete"}
        };
        private readonly Dictionary<string, double> questsValuesDictionary = new Dictionary<string, double>();

        private double  fallingCookiesGenerated = 0;
        private readonly MediaPlayer tapSoundPlayer = new MediaPlayer();

        /// <summary>
        /// Timers, en idleAnimatie opstarten
        /// </summary>
        public MainWindow()
        {           
            InitializeComponent();

            TimeSpentTimer.Tick += TimeSpentTimer_Tick;
            TimeSpentTimer.Interval = new TimeSpan(0, 0, 1);
            TimeSpentTimer.Start();
            
            PassiveIncomeTimer.Tick += PassiveIncomeTimer_Tick;
            PassiveIncomeTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            PassiveIncomeTimer.Start();

            GoldenCookieTimer.Tick += GoldenCookieTimer_Tick;
            GoldenCookieTimer.Interval = new TimeSpan(0, 1, 0);
            GoldenCookieTimer.Start();

            GoldenCookieActiveTimer.Tick += GoldenCookieActiveTimer_Tick;
            GoldenCookieActiveTimer.Interval = new TimeSpan(0, 0, 10);
            IdleAnimation();
        }

        private void TimeSpentTimer_Tick(object sender, EventArgs e)
        {
            timeSpentInGameInSeconds++;
        }

        /// <summary>
        /// Deze timer update om de 10ms en zorgt voor de passief inkomen opbrengts maar ook alles dat ermee te maken heeft.
        /// <para>Dus het update labels,buttons als ze enabled of visible moeten zijn, Quest systeem en vallende coekies.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassiveIncomeTimer_Tick(object sender, EventArgs e)
        {
            AddAllPassiveIncome();
            RevenuePerItem();
            UpdateWindowTitleAndLabels();
            ButtonEnabler();
            ButtonVisibilityEnabler();
            QuestDictionaryVullen();
            QuestCompleteSystem();
            FallingCookiePerPassiveIncome();
        }

        /// <summary>
        /// Timer dat een 30% kans heeft om een golden cookie te tonen via een canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoldenCookieTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            int chance = random.Next(1, 101);
            if (chance <= 30 && totalPassiveIncomePerSecond > 0)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("Assets/Images/GoldenCookie.png", UriKind.RelativeOrAbsolute));
                Image imageGoldenCookie = new Image();
                imageGoldenCookie.Source = bitmapImage;
                imageGoldenCookie.Width = random.Next(80, 130);
                imageGoldenCookie.Height = imageGoldenCookie.Width;

                // image heeft een event
                imageGoldenCookie.MouseDown += ImageGoldenCookie_MouseDown;

                // de locatie van de golden cookie is random maar past op het scherm via actual width en actual height
                double canvasWidth = CanvasGoldenCookie.ActualWidth - imageGoldenCookie.Width;
                double canvasHeight = CanvasGoldenCookie.ActualHeight - imageGoldenCookie.Height;

                Canvas.SetLeft(imageGoldenCookie, random.NextDouble() * canvasWidth);
                Canvas.SetTop(imageGoldenCookie, random.NextDouble() * canvasHeight);

                CanvasGoldenCookie.Children.Add(imageGoldenCookie);

                // Active timer starten
                GoldenCookieActiveTimer.Start();
            }
        }

        /// <summary>
        /// berekent hoeveel cookies je krijgt als je op de golden cookie klikt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageGoldenCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // je krijgt 15 min waarde van passieve inkomen
            cookieCounter += totalPassiveIncomePerSecond * 60 * 15;
            cookieTotalCounter += totalPassiveIncomePerSecond * 60 * 15;
            imgGoldenCookieClickAmmount++;
            GoldenCookieActiveTimer.Stop();
            if (CanvasGoldenCookie.Children.Count > 0)
            {
                CanvasGoldenCookie.Children.RemoveAt(0);
            }
        }
        private void GoldenCookieActiveTimer_Tick(object sender, EventArgs e)
        {
            if (CanvasGoldenCookie.Children.Count > 0)
            {
                CanvasGoldenCookie.Children.RemoveAt(0);
            }
            GoldenCookieActiveTimer.Stop();
        }


        private void ImgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // enkel de linker klik accepteren
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ClickAnimation();
                RandomClickSound();
                cookieCounter++;
                cookieTotalCounter++;
                imgCookieClickAmmount++;
                UpdateWindowTitleAndLabels();
                ButtonEnabler();
                ButtonVisibilityEnabler();
                FallingCookies();
            }
        }

        /// <summary>
        /// verwerkt de aankoop van items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            // button zoeken via sender
            Button button = (Button)sender;
            string buttonContent = button.Name.Replace("Btn", "");
            // schakelen op basis van knopInhoud en voert acties de bijhorende acties op
            // Cookiecounter - prijs
            // ItemCounter +1 
            // Stackpanel vullen met image
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
            //  Label passive income animatie starten
            if (passiveIncome == false)
            {
                passiveIncome = true;
                LabelPassiveIncomeAnimations();
            }
            UpdateAllPrices();
            UpdateWindowTitleAndLabels();
            ButtonEnabler();
            // totalpassiveIncome per seconde berekenen
            totalPassiveIncomePerSecond =
                (pointerCounter * 0.1 * pointerBonus) + (grannyCounter * 1 * grannyBonus) +
                (farmCounter * 8 * farmBonus) + (mineCounter * 47 * mineBonus) + (factoryCounter * 260 * factoryBonus)
                + (bankCounter * 1400 * bankBonus) + (templeCounter * 7800 * templeBonus);
        }

        /// <summary>
        /// Llb bakery klikken opent een dialoogvenster om de naam te veranderen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblBakeryName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string name = "";
            while (string.IsNullOrWhiteSpace(name)) // mag niet leeg zijn
            {
                name = Interaction.InputBox("Enter a new name for the bakery");
            }
            LblBakeryName.Content = name;
            if (changedBakeryName == false) // quests
            {
                changedBakeryName = true;
            }
        }

        /// <summary>
        /// verwerkt de aankoop van bonusen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBonusBuy_Click(object sender, RoutedEventArgs e)
        {
            // button zoeken via sender
            Button button = (Button)sender;
            string buttonContent = button.Name.Replace("BtnBonus", "");
            switch (buttonContent)
            {
                // schakelen op basis van knopInhoud en voert acties de bijhorende acties op
                // Cookiecounter - Bonusprijs
                // bonusCounter +1 
                // bonus = bonus *2 
                case "Pointer":
                    cookieCounter -= pointerBonusPrice;
                    pointerBonusCounter++;
                    pointerBonus *= 2;
                    break;
                case "Granny":
                    cookieCounter -= grannyBonusPrice;
                    grannyBonusCounter++;
                    grannyBonus *= 2;
                    break;
                case "Farm":
                    cookieCounter -= farmBonusPrice;
                    farmBonusCounter++;
                    farmBonus *= 2;
                    break;
                case "Mine":
                    cookieCounter -= mineBonusPrice;
                    mineBonusCounter++;
                    mineBonus *= 2;
                    break;
                case "Factory":
                    cookieCounter -= factoryBonusPrice;
                    factoryBonusCounter++;
                    factoryBonus *= 2;
                    break;
                case "Bank":
                    cookieCounter -= bankBonusPrice;
                    bankBonusCounter++;
                    bankBonus *= 2;
                    break;
                case "Temple":
                    cookieCounter -= templeBonusPrice;
                    templeBonusCounter++;
                    templeBonus *= 2;
                    break;
            }
            UpdateAllPrices();
            UpdateWindowTitleAndLabels();
            ButtonEnabler();
            // totalpassiveIncome per seconde berekenen
            totalPassiveIncomePerSecond =
                (pointerCounter * 0.1 * pointerBonus) + (grannyCounter * 1 * grannyBonus) +
                (farmCounter * 8 * farmBonus) + (mineCounter * 47 * mineBonus) + (factoryCounter * 260 * factoryBonus)
                + (bankCounter * 1400 * bankBonus) + (templeCounter * 7800 * templeBonus);
        }

        /// <summary>
        /// quests regelen via een een dictionary met de waardes en een 2d array met de quests
        /// </summary>
        public void QuestCompleteSystem()
        {
            for (int i = 0; i < quests.GetLength(0); i++)
            {
                string counterName = quests[i, 0];
                int targetValue = int.Parse(quests[i, 1]);
                string message = quests[i, 2];
                string questStatus = quests[i, 3];

                if (questStatus != "complete")
                {
                    if (questsValuesDictionary.ContainsKey(counterName))
                    {
                        if (questsValuesDictionary[counterName] >= targetValue)
                        {
                            MessageBox.Show(message,"Mission Complete!");
                            if(TabQuest.Visibility == Visibility.Collapsed)
                            {
                                TabQuest.Visibility = Visibility.Visible;
                            }
                            quests[i, 3] = "complete";
                            ListBoxItem listBoxItem = new ListBoxItem();
                            TextBlock textBlock = new TextBlock();
                            textBlock.TextWrapping = TextWrapping.Wrap;
                            if(counterName == "changedBakeryName")
                            {
                                textBlock.Text = $"Quest:\n{counterName} = {"true"}\n" +
                                    $"Quest Message:\n{message}";
                            }
                            else
                            {
                                textBlock.Text = $"Quest:\n{counterName} = {targetValue}\n" +
                                    $"Quest Message:\n{message}";
                            }
                            listBoxItem.Content = textBlock;
                            LstboxQuests.Items.Add(listBoxItem);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// dictionary met keys en values vullen
        /// </summary>
        public void QuestDictionaryVullen()
        {
            questsValuesDictionary["cookieCounter"] = cookieCounter;
            questsValuesDictionary["cookieTotalCounter"] = cookieTotalCounter;
            questsValuesDictionary["changedBakeryName"] = changedBakeryName ? 1 : 0;
            questsValuesDictionary["totalPassiveIncomePerSecond"] = totalPassiveIncomePerSecond;
            questsValuesDictionary["pointerCounter"] = pointerCounter;
            questsValuesDictionary["grannyCounter"] = grannyCounter;
            questsValuesDictionary["farmCounter"] = farmCounter;
            questsValuesDictionary["mineCounter"] = mineCounter;
            questsValuesDictionary["factoryCounter"] = factoryCounter;
            questsValuesDictionary["bankCounter"] = bankCounter;
            questsValuesDictionary["templeCounter"] = templeCounter;
            questsValuesDictionary["pointerBonus"] = pointerBonus;
            questsValuesDictionary["grannyBonus"] = grannyBonus;
            questsValuesDictionary["farmBonus"] = farmBonus;
            questsValuesDictionary["mineBonus"] = mineBonus;
            questsValuesDictionary["factoryBonus"] = factoryBonus;
            questsValuesDictionary["bankBonus"] = bankBonus;
            questsValuesDictionary["templeBonus"] = templeBonus;
        }


        private void RevenuePerItem()
        {
            pointerTotalPassiveIncome += (pointerCounter * 0.001 * pointerBonus);
            grannyTotalPassiveIncome += (grannyCounter * 0.01 * grannyBonus);
            farmTotalPassiveIncome += (farmCounter * 0.08 * farmBonus);
            mineTotalPassiveIncome += (mineCounter * 0.47 * mineBonus);
            factoryTotalPassiveIncome += (factoryCounter * 2.60 * factoryBonus);
            bankTotalPassiveIncome += (bankCounter * 14 * bankBonus);
            templeTotalPassiveIncome += (templeCounter * 78 * templeBonus);
        }


        /// <summary>
        /// Voegt passief inkomen toe op basis van het aantal tellers, de hoeveelheid per teller en de bonusfactor.
        /// </summary>
        /// <param name="counter">De aantal counters dat je hebt bijv: aantal pointers dat je hebt</param>
        /// <param name="ammount">De aantal dat een counter geeft bijv: granny geeft 1 cookie per sec</param>
        /// <param name="bonus">De bonusfactor dubbelt de waarde dat je krijgt, bijv 2x, 4x, of 6x</param>
        public void AddPassiveIncome(int counter, double ammount, int bonus)
        {
            cookieCounter += counter * ammount * bonus;
            cookieTotalCounter += counter * ammount * bonus;
            fallingCookiesGenerated += counter * ammount * bonus;
        }
        
        /// <summary>
        /// maakt gebruik van de addPassiveIncome methode per item dat we hebben
        /// </summary>
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

        /// <summary>
        /// Geeft de waardes een opgemaakt formaat op basis van hoeveel je hebt.
        /// <para>Bijvoorbeeld, 1000 wordt weergegeven als 1 000.</para>
        /// <para>Grotere getallen, zoals 1245000, worden weergegeven als 1,245 miljoen.</para>
        /// </summary>
        /// <param name="count">Het getal dat opgemaakt wordt</param>
        /// <returns>opgemaakte string</returns>
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

        /// <summary>
        /// titel en labels updaten.
        /// <para>Voor de prijzen en counters gebruiken we de NumberFormat methode</para>
        /// </summary>
        private void UpdateWindowTitleAndLabels()
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

            BtnPointer.ToolTip = $"{0.1} Cookies per second" +
                        $"\n{pointerCounter} Pointers produceren {0.1 * pointerCounter * pointerBonus} cookies per seconden" +
                        $"\n{pointerBonus} bonus verhoogt de productie met {pointerBonus * 100}%" +
                        $"\n{pointerTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";

            BtnGranny.ToolTip = $"{1} Cookies per second" +
                        $"\n{grannyCounter} Grannys produceren {1 * grannyCounter * grannyBonus} cookies per seconden" +
                        $"\n{grannyBonus} bonus verhoogt de productie met {grannyBonus * 100}%" +
                        $"\n{grannyTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";

            BtnFarm.ToolTip = $"{8} Cookies per second" +
                        $"\n{farmCounter} farms produceren {8 * farmCounter * farmBonus} cookies per seconden" +
                        $"\n{farmBonus} bonus verhoogt de productie met {farmBonus * 100}%" +
                        $"\n{farmTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";

            BtnMine.ToolTip = $"{47} Cookies per second" +
                        $"\n{mineCounter} mines produceren {47 * mineCounter * mineBonus} cookies per seconden" +
                        $"\n{mineBonus} bonus verhoogt de productie met {mineBonus * 100}%" +
                        $"\n{mineTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";

            BtnFactory.ToolTip = $"{260} Cookies per second" +
                        $"\n{factoryCounter} factories produceren {260 * factoryCounter * factoryBonus} cookies per seconden" +
                        $"\n{factoryBonus} bonus verhoogt de productie met {factoryBonus * 100}%" +
                        $"\n{factoryTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";

            BtnBank.ToolTip = $"{1400} Cookies per second" +
                        $"\n{bankCounter} banks produceren {1400 * bankCounter * bankBonus} cookies per seconden" +
                        $"\n{bankBonus} bonus verhoogt de productie met {bankBonus * 100}%" +
                        $"\n{bankTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";

            BtnTemple.ToolTip = $"{7800} Cookies per second" +
                        $"\n{templeCounter} temples produceren {7800 * templeCounter * templeBonus} cookies per seconden" +
                        $"\n{templeBonus} bonus verhoogt de productie met {templeBonus * 100}%" +
                        $"\n{templeTotalPassiveIncome.ToString("0.00")} in totaal geproduceerd";


            if (totalPassiveIncomePerSecond < 1000000)
            {
                // als passieve inkomen minder dan een miljoen is toon ik ook de komma getal
                LblPassiveIncomePerSecond.Content = $"+{totalPassiveIncomePerSecond.ToString("N1").Replace('.', ' ')}/s";
            }
            else
            {
                LblPassiveIncomePerSecond.Content = $"+{NumberFormat(totalPassiveIncomePerSecond)}/s";
            }
        }

        public void ButtonEnabler()
        {
            // Basisgebouwen knoppen worden geactiveerd als je voldoende cookies hebt om de prijs te betalen
            BtnPointer.IsEnabled = cookieCounter >= pointerPrice;
            BtnGranny.IsEnabled = cookieCounter >= grannyPrice;
            BtnFarm.IsEnabled = cookieCounter >= farmPrice;
            BtnMine.IsEnabled = cookieCounter >= minePrice;
            BtnFactory.IsEnabled = cookieCounter >= factoryPrice;
            BtnBank.IsEnabled = cookieCounter >= bankPrice;
            BtnTemple.IsEnabled = cookieCounter >= templePrice;

            // Upgrade knoppen worden geactiveerd als je voldoende cookies hebt voor de prijs en minstens 1 exemplaar van het item bezit
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
            // items zichtbaar maken als je genoeg totaal cookies had om ze te kopen
            BtnPointer.Visibility = cookieTotalCounter >= pointerBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnGranny.Visibility = cookieTotalCounter >= grannyBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnFarm.Visibility = cookieTotalCounter >= farmBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnMine.Visibility = cookieTotalCounter >= mineBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnFactory.Visibility = cookieTotalCounter >= factoryBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnBank.Visibility = cookieTotalCounter >= bankBasePrice ? Visibility.Visible : Visibility.Hidden;
            BtnTemple.Visibility = cookieTotalCounter >= templeBasePrice ? Visibility.Visible : Visibility.Hidden;

            // tabitem voor de bonus store zichtbaar maken als je minstens de eerste item kunt zien
            TabBonus.Visibility = BtnPointer.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

            // Bonusen tonen als je de items kunt zien
            BtnBonusPointer.Visibility = BtnPointer.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusGranny.Visibility = BtnGranny.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusFarm.Visibility = BtnFarm.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusMine.Visibility = BtnMine.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusFactory.Visibility = BtnFactory.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusBank.Visibility = BtnBank.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
            BtnBonusTemple.Visibility = BtnTemple.Visibility == Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
        }


        /// <summary>
        /// berekent de prijzen voor de items
        /// </summary>
        /// <param name="baseprice">De basisprijs van het item</param>
        /// <param name="counter">Het aantal dat je van het item hebt</param>
        /// <returns>De berekende prijs voor het item.</returns>
        public double UpdatePrice(double baseprice, int counter)
        {
            // als je 1 of meer hebt
            if (counter >= 1)
            {
                return Math.Round(baseprice * Math.Pow(1.15, counter));
            }
            // als je nog geen hebt
            return baseprice;
        }
        /// <summary>
        /// berekent de prijsen voor de bonusen
        /// </summary>
        /// <param name="baseprice">De basisprijs van de bonus</param>
        /// <param name="counter">het aantal bonussen</param>
        /// <returns>De berekende prijs voor de bonus</returns>
        public double UpdateBonusPrice(double baseprice, int counter)
        {
            // als je nog geen bonus hebt gekocht
            if(counter < 0)
            {
                return baseprice * 100;
            }
            else
            {
                return baseprice * 100 * (5 * Math.Pow(10,counter));
            }
        }
        /// <summary>
        /// past de methodes van updatePrice en updateBonusprice toe 
        /// </summary>
        public void UpdateAllPrices()
        {   
            // prijzen updaten
            pointerPrice = UpdatePrice(pointerBasePrice, pointerCounter);
            grannyPrice = UpdatePrice(grannyBasePrice, grannyCounter);
            farmPrice = UpdatePrice(farmBasePrice, farmCounter);
            minePrice = UpdatePrice(mineBasePrice, mineCounter);
            factoryPrice = UpdatePrice(factoryBasePrice, factoryCounter);
            bankPrice = UpdatePrice(bankBasePrice, bankCounter);
            templePrice = UpdatePrice(templeBasePrice, templeCounter);

            // bonus prijzen updaten
            pointerBonusPrice = UpdateBonusPrice(pointerBasePrice, pointerBonusCounter);
            grannyBonusPrice = UpdateBonusPrice(grannyBasePrice, grannyBonusCounter);
            farmBonusPrice = UpdateBonusPrice(farmBasePrice, farmBonusCounter);
            mineBonusPrice = UpdateBonusPrice(mineBasePrice, mineBonusCounter);
            factoryBonusPrice = UpdateBonusPrice(factoryBasePrice, factoryBonusCounter);
            bankBonusPrice = UpdateBonusPrice(bankBasePrice, bankBonusCounter);
            templeBonusPrice = UpdateBonusPrice(templeBasePrice, templeBonusCounter);
        }

        /// <summary>
        /// visuasileert de aantal items dat je hebt gekocht 
        /// </summary>
        /// <param name="itemName"></param>
        public void StackpanelVisualizeItems(string itemName)
        {
            // stackpanel zoeken via de naam van de item
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



        public void RandomClickSound()
        {
            // geluid randomizer
            Random random = new Random();
            int number = random.Next(1, 3);
            tapSoundPlayer.Open(new Uri($"Assets/Audio/tap-{number}.wav", UriKind.RelativeOrAbsolute));

            // geluid stoppen als het nog aan het spelen was
            tapSoundPlayer.Stop();
            tapSoundPlayer.Play();
        }

        /// <summary>
        /// Animatie dat zorgt voor een 360 rotatie op de cookieImage.
        /// </summary>
        public void IdleAnimation()
        {
            // new rotatie object 
            RotateTransform rotateTransform = new RotateTransform();
            ImgCookie.RenderTransform = rotateTransform;

            // rotatie via center van afbeelding
            ImgCookie.RenderTransformOrigin = new Point(0.5, 0.5);

            // simple animatie
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(6.5)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            // animatie spelen op de rotatie
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

        /// <summary>
        /// Cookie klik animatie is via margin manipulatie en cookie image veranderen
        /// </summary>
        public void ClickAnimation()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("Assets/Images/Cookie_Cute.png", UriKind.RelativeOrAbsolute));
            ImgCookie.Source = bitmapImage;

            ThicknessAnimation clickAnimation = new ThicknessAnimation()
            {
                To = new Thickness(45),
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

        /// <summary>
        /// Methode om een animatie te starten van vallende cookie met een maximum van 50 op het scherm
        /// </summary>
        public void FallingCookies()
        {
            Random random = new Random();
            BitmapImage bitmapImage = new BitmapImage(new Uri("Assets/Images/Cookie_Cute.png", UriKind.RelativeOrAbsolute));
            Image imageGoldenCookie = new Image();
            imageGoldenCookie.Source = bitmapImage;
            imageGoldenCookie.Width = random.Next(20, 50);
            imageGoldenCookie.Height = imageGoldenCookie.Width;

            // locatie van de vallende cookie is random maar past op het scherm via actual width en actual height 
            double canvasWidth = CanvasFallingCookies.ActualWidth - imageGoldenCookie.Width;
            double canvasHeight = CanvasFallingCookies.ActualHeight - imageGoldenCookie.Height;
            Canvas.SetLeft(imageGoldenCookie, random.NextDouble() * canvasWidth);
            Canvas.SetTop(imageGoldenCookie, random.NextDouble() * canvasHeight * 0.4);

            // de vallende coekie is een animatie (cookies vallen naar beneden)
            DoubleAnimation fallingCookieAnimation = new DoubleAnimation()
            {
                From = 0,
                To = CanvasFallingCookies.ActualHeight - imageGoldenCookie.Height,
                Duration = TimeSpan.FromSeconds(5),
                FillBehavior = FillBehavior.Stop,
            };
            // methode voor de einde van de animatie
            fallingCookieAnimation.Completed += FallingCookieAnimation_Completed;

            // enkel cookie toevoegen als er minder dan 50 cookies binnen de canvas zijn
            if (CanvasFallingCookies.Children.Count < 50)
            {
                CanvasFallingCookies.Children.Add(imageGoldenCookie);

                // dit zorgt ervoor dat vallende coekie animatie op de y-as plaats vindt
                imageGoldenCookie.RenderTransform = new TranslateTransform();
                imageGoldenCookie.RenderTransform.BeginAnimation(TranslateTransform.YProperty, fallingCookieAnimation);
            }
        }
        private void FallingCookieAnimation_Completed(object sender, EventArgs e)
        {
            // wanneer de animatie eindigt => verwijderen we de vallende coekie
            if (CanvasFallingCookies.Children.Count > 0)
            {
                CanvasFallingCookies.Children.RemoveAt(0);
            }
        }

        /// <summary>
        ///  zorgt voor de vallende coekie animatie op basis van passieve opbrengst
        /// </summary>
        public void FallingCookiePerPassiveIncome()
        {
            // Bij een zeer hoog passief inkomen kan de zichtbaarheid van vallende cookies haperen,
            // waarbij mogelijk minder cookies worden weergegeven dan het werkelijke aantal (bijv. 5 cookies tonen terwijl er 50 binnen de canvas zijn).
            int passiveIncome = Math.Min((int)fallingCookiesGenerated, 50);
            for (int i = 0; i < passiveIncome; i++)
            {
                FallingCookies();
                fallingCookiesGenerated--;
            }
        }

        /// <summary>
        /// een animatie dat de label vergroot en de foreground kleuren verandert
        /// </summary>
        public void LabelPassiveIncomeAnimations()
        {
            // animatie dat kleuren verandert door gebruik te maken van keyframes 
            ColorAnimationUsingKeyFrames colorAnimation = new ColorAnimationUsingKeyFrames()
            {
                AutoReverse = true, // automatisch omkeren
                RepeatBehavior = RepeatBehavior.Forever, // blijft herhalen
                Duration = TimeSpan.FromMilliseconds(6000)
            };
            // begint op rood
            colorAnimation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Red, KeyTime.FromPercent(0.0)));
            // op het midden blauw
            colorAnimation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Blue, KeyTime.FromPercent(0.5)));
            // eindt op groen
            colorAnimation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Green, KeyTime.FromPercent(1.0)));

            // label op rood zetten.
            LblPassiveIncomePerSecond.Foreground = new SolidColorBrush(Colors.Red);

            // animatie dat de grootte aan past van de label
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 24, // font size begint op 24
                To = 40, // fontsize eindigt op 40
                Duration = TimeSpan.FromMilliseconds(250),
                AutoReverse = true, // automatisch omkeren
                RepeatBehavior = RepeatBehavior.Forever, // blijft herhalen
            };

            // animaties beginnen
            LblPassiveIncomePerSecond.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            LblPassiveIncomePerSecond.BeginAnimation(Label.FontSizeProperty, doubleAnimation);
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            double totalSec = timeSpentInGameInSeconds;
            double hours = (Math.Floor(totalSec / 3600));
            double min = Math.Floor((totalSec - (hours * 3600)) / 60);
            double sec = Math.Floor(totalSec - (hours * 3600) - min * 60);

            MessageBox.Show($"Je hebt {cookieCounter.ToString("0")} cookies" +
                $"\ntotaal cookies geproduceerd: {cookieTotalCounter.ToString("0.00")}" +
                $"\nje hebt {hours} uur, {min} min, {sec} seconden besteed" +
                $"\nJe hebt {imgCookieClickAmmount} keer op de cookie geklickt" + 
                $"\nJe hebt {imgGoldenCookieClickAmmount} op de golden cookie geclickt" + 
                $"\nJe hebt {LstboxQuests.Items.Count} quests behaald");
        }
    }
}
