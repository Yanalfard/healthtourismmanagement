using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainData { get; set; }
        public string MainDataRtf { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblNews(TblNews news, HttpStatusCode statusEffect)
        {
            Id = news.Id;
            Title = news.Title;
            MainData = news.MainData;
            MainDataRtf = news.MainDataRtf;

            StatusEffect = statusEffect;
        }

        public DtoTblNews()
        {
        }
    }
}