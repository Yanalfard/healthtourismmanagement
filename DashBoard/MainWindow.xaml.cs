using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DashBoard.Models.Regular;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Properties
        /// <summary>
        /// The user that has logged in
        /// </summary>
        public TblUserPass LoggedInUser { get; set; }
        public DataView TheDataView { get; set; }
        public TicketPage TheTicketPage { get; set; }
        public NewsPage TheNewsPage { get; set; }
        public NewsEditor TheNewsEditor { get; set; }
        public PatientView ThePatientView { get; set; }
        #endregion

        #region Arrived

        public ObservableCollection<TblSection> ArrSections;
        public ObservableCollection<TblPatient> ArrPatient;
        public ObservableCollection<TblHospital> ArrHospital;
        public ObservableCollection<TblSickness> ArrSickness;
        public ObservableCollection<TblDoctor> ArrDoctor;
        public ObservableCollection<TblImage> ArrImage;
        public ObservableCollection<TblCountry> ArrCountry;
        public ObservableCollection<TblCity> ArrCity;
        public ObservableCollection<TblOperation> ArrOperations;
        public ObservableCollection<TblUserPass> ArrUserPass;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            TblUserPass loggedInUser = new TblUserPass(-1, "حسن", "Hassan", true);
            this.LoggedInUser = loggedInUser;

            FrameDataView.Content = TheDataView = new DataView(this, loggedInUser);
            FrameTickets.Content = TheTicketPage = new TicketPage(this, loggedInUser);
            FrameNews.Content = TheNewsPage = new NewsPage(this, loggedInUser);
            FrameNewsEditor.Content = TheNewsEditor = new NewsEditor(this, loggedInUser);
            FramePatientView.Content = ThePatientView = new PatientView(this, loggedInUser);


            #region Arrived
            ArrSections = TheDataView.ArrSections;
            ArrPatient = TheDataView.ArrPatient;
            ArrHospital = TheDataView.ArrHospital;
            ArrSickness = TheDataView.ArrSickness;
            ArrDoctor = TheDataView.ArrDoctor;
            ArrImage = TheDataView.ArrImage;
            ArrCountry = TheDataView.ArrCountry;
            ArrCity = TheDataView.ArrCity;
            ArrOperations = TheDataView.ArrOperations;
            ArrUserPass = TheDataView.ArrUserPass;
            #endregion

            TxtUserName.Text = loggedInUser.Username;
        }

        public MainWindow(TblUserPass loggedInUser)
        {
            InitializeComponent();
            this.LoggedInUser = loggedInUser;

            FrameDataView.Content = TheDataView = new DataView(this, loggedInUser);
            FrameTickets.Content = TheTicketPage = new TicketPage(this, loggedInUser);
            FrameNews.Content = TheNewsPage = new NewsPage(this, loggedInUser);
            FrameNewsEditor.Content = TheNewsEditor = new NewsEditor(this, loggedInUser);
            FramePatientView.Content = ThePatientView = new PatientView(this, loggedInUser);

            #region Arrived
            ArrSections = TheDataView.ArrSections;
            ArrPatient = TheDataView.ArrPatient;
            ArrHospital = TheDataView.ArrHospital;
            ArrSickness = TheDataView.ArrSickness;
            ArrDoctor = TheDataView.ArrDoctor;
            ArrImage = TheDataView.ArrImage;
            ArrCountry = TheDataView.ArrCountry;
            ArrCity = TheDataView.ArrCity;
            ArrOperations = TheDataView.ArrOperations;
            ArrUserPass = TheDataView.ArrUserPass;
            #endregion

            TxtUserName.Text = loggedInUser.Username;
        }

        #region Helpers
        public void CheckWindowState()
        {
            #region ToolBar
            //var doc = this.Resources["pathRestore"] as Canvas;
            //((Run)LogicalTreeHelper.FindLogicalNode(doc, "run")).Text = "example text";

            if (this.WindowState == WindowState.Maximized)
                BtnMaximize.Content = MenuBar.FindResource("PathRestore");

            else if (this.WindowState == WindowState.Normal)
                BtnMaximize.Content = MenuBar.FindResource("PathMaximize");



            #endregion
        }

        public void ShowPreloader(Border control)
        {
            control.Child.Visibility = Visibility.Visible;
            DoubleAnimation opacity = new DoubleAnimation(1, TimeSpan.FromMilliseconds(400));
            opacity.EasingFunction = new QuadraticEase();
            control.BeginAnimation(OpacityProperty, opacity);
            DoubleAnimation width = new DoubleAnimation(21, 42, TimeSpan.FromMilliseconds(200));
            opacity.EasingFunction = new QuadraticEase();
            control.BeginAnimation(WidthProperty, width);
        }

        public void ShowPreloader()
        {
            BrdPreLoader.Child.Visibility = Visibility.Visible;
            DoubleAnimation opacity = new DoubleAnimation(1, TimeSpan.FromMilliseconds(400));
            opacity.EasingFunction = new QuadraticEase();
            BrdPreLoader.BeginAnimation(OpacityProperty, opacity);
            DoubleAnimation width = new DoubleAnimation(21, 42, TimeSpan.FromMilliseconds(200));
            opacity.EasingFunction = new QuadraticEase();
            BrdPreLoader.BeginAnimation(WidthProperty, width);
        }
        public void HidePreloader(Border control)
        {
            control.Child.Visibility = Visibility.Hidden;
            DoubleAnimation opacity = new DoubleAnimation(0, TimeSpan.FromMilliseconds(400));
            opacity.EasingFunction = new QuadraticEase();
            control.BeginAnimation(OpacityProperty, opacity);
            DoubleAnimation width = new DoubleAnimation(42, 21, TimeSpan.FromMilliseconds(200));
            opacity.EasingFunction = new QuadraticEase();
            control.BeginAnimation(WidthProperty, width);
        }

        public void HidePreloader()
        {
            BrdPreLoader.Child.Visibility = Visibility.Hidden;
            DoubleAnimation opacity = new DoubleAnimation(0, TimeSpan.FromMilliseconds(400));
            opacity.EasingFunction = new QuadraticEase();
            BrdPreLoader.BeginAnimation(OpacityProperty, opacity);
            DoubleAnimation width = new DoubleAnimation(42, 21, TimeSpan.FromMilliseconds(200));
            opacity.EasingFunction = new QuadraticEase();
            BrdPreLoader.BeginAnimation(WidthProperty, width);
        }
        #endregion

        #region Top
        /// <summary>
        /// Has a switch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
                if (e.ClickCount == 2)
                {
                    if (this.WindowState == WindowState.Maximized)
                        this.WindowState = WindowState.Normal;
                    else if (this.WindowState == WindowState.Normal)
                        this.WindowState = WindowState.Maximized;
                    CheckWindowState();
                }
            }
            catch
            {

            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                CheckWindowState();
                //btnMaximize.Content = new Image() { Stretch = Stretch.UniformToFill, Margin = new Thickness(8), Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Menubar/Restore.png")) };
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                CheckWindowState();
                //btnMaximize.Content = new Image() { Stretch = Stretch.UniformToFill, Margin = new Thickness(8), Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Menubar/Max.png")) };
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BlurryWindow_StateChanged(object sender, EventArgs e)
        {
            CheckWindowState();
        }

        private void BtnGrids_Click(object sender, RoutedEventArgs e)
        {
            CollapseFrames();
            FrameDataView.Visibility = Visibility.Visible;
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            CollapseFrames();
            FrameTickets.Visibility = Visibility.Visible;
        }

        private void BtnNews_Click(object sender, RoutedEventArgs e)
        {
            CollapseFrames();
            FrameNews.Visibility = Visibility.Visible;
        }

        private void BtnPatientView_Click(object sender, RoutedEventArgs e)
        {
            CollapseFrames();
            FramePatientView.Visibility = Visibility.Visible;
        }

        public void CollapseFrames()
        {

            FrameNewsEditor.Visibility = Visibility.Collapsed;
            FrameNews.Visibility = Visibility.Collapsed;
            FrameDataView.Visibility = Visibility.Collapsed;
            FrameTickets.Visibility = Visibility.Collapsed;
            FrameTemp.Visibility = Visibility.Collapsed;
            FramePatientView.Visibility = Visibility.Collapsed;
        }

        #endregion


    }
}
