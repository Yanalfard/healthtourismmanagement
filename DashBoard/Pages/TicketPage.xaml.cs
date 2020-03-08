using System.Windows;
using System.Windows.Controls;
using DashBoard.Models.Regular;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        public MainWindow MainWindow { get; set; }
        public TblUserPass LoggedInUser { get; set; }

        public TicketPage()
        {
            InitializeComponent();
        }

        public TicketPage(MainWindow mainWindow, TblUserPass loggedInUser)
        {
            this.MainWindow = mainWindow;
            this.LoggedInUser = loggedInUser;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
