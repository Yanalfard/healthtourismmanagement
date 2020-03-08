using DashBoard.Models.Regular;
using DashBoard.ViewModels;
using System.Collections.Generic;
using System.Drawing;

namespace DashBoard
{
    public class NewsViewerDataModel : BaseViewModel
    {

       
        public List<TblImage> TblImages { get; set; }

        public List<Image> Images { get => DownloadImages(); }

        //public FlowDocument Document { get; set; }

        public TblNews News { get; set; }

        public NewsViewerDataModel()
        {
            TblImages = new List<TblImage>();
            News = new TblNews();
        }


        public List<Image> DownloadImages()
        {
            if (Images.Count != 0) return Images;

            List<Image> ans = new List<Image>();

            foreach (TblImage image in TblImages)
            {

            }

            return ans;
        }

    }
}