using System.Windows;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for BlurOutMessageBox.xaml
    /// </summary>
     public partial class BlurOutMessageBox
    {
        public BlurOutMessageBoxViewModel ArrViewModel = new BlurOutMessageBoxViewModel();

        public BlurOutMessageBox()
        {
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            InitializeComponent();
        }

        public MessageBoxResult Show(string message)
        {
            ArrViewModel = new BlurOutMessageBoxViewModel(message);
            BtnAccept.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            BtnReject.Visibility = Visibility.Collapsed;

            this.DataContext = ArrViewModel;
            InitializeComponent();
            this.ShowDialog();
            return ArrViewModel.Result;
        }

        public MessageBoxResult Show(string message, string acceptText)
        {
            ArrViewModel = new BlurOutMessageBoxViewModel(message, acceptText);
            BtnAccept.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            BtnReject.Visibility = Visibility.Collapsed;
            this.DataContext = ArrViewModel;
            InitializeComponent();
            this.ShowDialog();
            return ArrViewModel.Result;

        }

        public MessageBoxResult Show(string title, string message, string acceptText)
        {
            ArrViewModel = new BlurOutMessageBoxViewModel(title, message, acceptText);
            BtnAccept.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            BtnReject.Visibility = Visibility.Collapsed;
            this.DataContext = ArrViewModel;
            InitializeComponent();
            this.ShowDialog();
            return ArrViewModel.Result;
        }

        public MessageBoxResult Show(string title, string message, string acceptText, string rejectText)
        {
            ArrViewModel = new BlurOutMessageBoxViewModel(title, message, acceptText, rejectText);
            BtnAccept.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Collapsed;
            BtnReject.Visibility = Visibility.Visible;
            this.DataContext = ArrViewModel;
            InitializeComponent();
            this.ShowDialog();
            return ArrViewModel.Result;
        }

        public MessageBoxResult Show(string title, string message, string acceptText, string rejectText, string cancelText)
        {
            ArrViewModel = new BlurOutMessageBoxViewModel(title, message, acceptText, rejectText, cancelText);
            BtnAccept.Visibility = Visibility.Visible;
            BtnCancel.Visibility = Visibility.Visible;
            BtnReject.Visibility = Visibility.Visible;
            this.DataContext = ArrViewModel;
            InitializeComponent();
            this.ShowDialog();
            return ArrViewModel.Result;
        }


        private void BlurryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ArrViewModel;
            BtnAccept.DataContext = ArrViewModel;
            BtnCancel.DataContext = ArrViewModel;
            BtnReject.DataContext = ArrViewModel;
            TbMessage.DataContext = ArrViewModel;
            TbTitle.DataContext = ArrViewModel;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            ArrViewModel.Result = MessageBoxResult.Yes;
            this.Close();
        }

        private void BtnReject_Click(object sender, RoutedEventArgs e)
        {
            ArrViewModel.Result = MessageBoxResult.No;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ArrViewModel.Result = MessageBoxResult.Cancel;
            this.Close();
        }
    }
}
