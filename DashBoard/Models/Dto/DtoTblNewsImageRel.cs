using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblNewsImageRel
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int ImageId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblNewsImageRel(TblNewsImageRel newsImageRel, HttpStatusCode statusEffect)
        {
            Id = newsImageRel.Id;
            NewsId = newsImageRel.NewsId;
            ImageId = newsImageRel.ImageId;

            StatusEffect = statusEffect;
        }

        public DtoTblNewsImageRel()
        {
        }
    }
}