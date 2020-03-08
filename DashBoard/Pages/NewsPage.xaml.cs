using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DashBoard.ApiDecoder;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;
using HelthTourismServer.ApiDecoder;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        const string Password = "110ff8d5d2ec47b98b1d53fc3d2bb4e1b517864b502e2f8d1f4cf4b10c017cee";
        const string Username = "yanal";

        public MainWindow MainWindow { get; set; }
        public TblUserPass LoggedInUser { get; set; }

        public NewsPage()
        {
            InitializeComponent();
        }

        public NewsPage(MainWindow mainWindow, TblUserPass loggedInUser)
        {
            this.MainWindow = mainWindow;
            this.LoggedInUser = loggedInUser;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Pluck this whenever the server was up.
                //GenerateNews();
            }
            catch (Exception ex)
            {
                new  BlurOutMessageBox().Show(ex.Message, "باشه");
            }
        }

        private async void GenerateNews()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);    
            NewsCore newsCore = new NewsCore(token);
            List<DtoTblNews> dtoNews = await newsCore.SelectAllNewss();
            foreach (DtoTblNews news in dtoNews)
            {
                TblNews tblNews = new TblNews(news.Id, news.Title, news.MainData, news.MainDataRtf);
                NewsViewer newsViewer = new NewsViewer(tblNews);
              

                StckMain.Children.Add(newsViewer);

            }
        }

        private void NewsViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewsViewer viewer = sender as NewsViewer;
            MainWindow.FrameNewsEditor.Content = MainWindow.TheNewsEditor = new NewsEditor(MainWindow, LoggedInUser,
                viewer);
            MainWindow.FrameNews.Visibility = Visibility.Collapsed;
            MainWindow.FrameNewsEditor.Visibility = Visibility.Visible;
        }

        private void BtnAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.FrameNewsEditor.Content = MainWindow.TheNewsEditor = new NewsEditor(MainWindow, LoggedInUser);
            MainWindow.FrameNews.Visibility = Visibility.Collapsed;
            MainWindow.FrameNewsEditor.Visibility = Visibility.Visible;
        }

        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            NewsCore newsCore = new NewsCore(token);

            foreach (Control ctrl in StckMain.Children)
            {
                NewsViewer viewer = ctrl as NewsViewer;
                await newsCore.DeleteNews(viewer.DataModel.News.Id);
            }
        }
    }
}
