using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblImage(TblImage image, HttpStatusCode statusEffect)
        {
            Id = image.Id;
            Image = image.Image;
            Status = image.Status;

            StatusEffect = statusEffect;
        }

        public DtoTblImage()
        {
        }
    }
}