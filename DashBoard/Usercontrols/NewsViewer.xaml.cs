using DashBoard.Models.Regular;
using DashBoard.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DashBoard.ApiDecoder;
using HelthTourismServer.ApiDecoder;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for NewsViewer.xaml
    /// </summary>
    public partial class NewsViewer : UserControl
    {
        const string Password = "110ff8d5d2ec47b98b1d53fc3d2bb4e1b517864b502e2f8d1f4cf4b10c017cee";
        const string Username = "yanal";

        /// <summary>
        /// Datamodel
        /// </summary>
        public NewsViewerDataModel DataModel { get; set; } 

        public NewsViewer()
        {
            InitializeComponent();
            DataModel = new NewsViewerDataModel();
        }


        public NewsViewer(TblNews tblNews)
        {
            DataModel = new NewsViewerDataModel();
            DataModel.News = tblNews;

            InitializeComponent();
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainRichtext.Document.Blocks.Add(new Paragraph(new Run(DataModel.News.MainData)));
        }

        private async Task<List<TblImage>> GetNewsImage()
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            NewsCore newsCore = new NewsCore(token);
            List<DtoTblImage> images = await newsCore.SelectImageByNewsId(DataModel.News.Id);

            foreach (DtoTblImage dto in images)
                DataModel.TblImages.Add(new TblImage(dto.Id, dto.Image, dto.Status));

            return new List<TblImage>();
        }

    }
}
