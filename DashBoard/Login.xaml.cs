using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DashBoard.Models.Regular;
using DashBoard.Models.Dto;
using System.Windows.Media.Animation;
using DashBoard.ApiDecoder;
using HelthTourismServer.ApiDecoder;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        const string Password = "110ff8d5d2ec47b98b1d53fc3d2bb4e1b517864b502e2f8d1f4cf4b10c017cee";
        const string Username = "yanal";

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        DragMove();
        //        if (e.ClickCount == 2)
        //        {
        //            if (this.WindowState == WindowState.Maximized)
        //                this.WindowState = WindowState.Normal;
        //            else if (this.WindowState == WindowState.Normal)
        //                this.WindowState = WindowState.Maximized;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        private void TxtSignUp_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            BtnLogin.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(TxtUsername.Text))
            {
                TxtUsernameAlarm.Text = "نام کاربری برای ورود لازم است";
                TxtUsernameAlarm.Visibility = Visibility.Visible;
                BtnLogin.IsEnabled = true;
                return;
            }
            TxtUsernameAlarm.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                TxtPasswordAlarm.Text = "گذرواژه برای ورود لازم است";
                TxtPasswordAlarm.Visibility = Visibility.Visible;
                BtnLogin.IsEnabled = true;
                return;
            }
            TxtPasswordAlarm.Visibility = Visibility.Collapsed;
            ShowPreloader(BrdPreLoader);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username,Password);
            UserPassCore userPassCore = new UserPassCore(token);

            DtoTblUserPass nameCheck = await userPassCore.SelectUserPassByUsername(TxtUsername.Text);
            if (nameCheck == null)
            {
                TxtUsernameAlarm.Text = "نام کاربری اشتباه است";
                TxtUsernameAlarm.Visibility = Visibility.Visible;
                BtnLogin.IsEnabled = true;
                HidePreloader(BrdPreLoader);
                return;
            }
            TxtUsernameAlarm.Visibility = Visibility.Collapsed;

            DtoTblUserPass attempt = await userPassCore.SelectUserPassByUsername(TxtUsername.Text);
            if (attempt.Password != TxtPassword.Text)
            {
                TxtPasswordAlarm.Text = "گذرواژه اشتباه است";
                TxtPasswordAlarm.Visibility = Visibility.Visible;
                BtnLogin.IsEnabled = true;
                HidePreloader(BrdPreLoader);
                return;
            }
            TxtPasswordAlarm.Visibility = Visibility.Collapsed;
            BtnLogin.IsEnabled = true;

            MainWindow mainWindow = new MainWindow(new TblUserPass(attempt.Id, attempt.Username, attempt.Password, attempt.IsHelthApple));
            mainWindow.Show();
            this.Close();
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
    }
}
