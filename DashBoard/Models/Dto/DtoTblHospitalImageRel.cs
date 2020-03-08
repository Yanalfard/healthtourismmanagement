using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblHospitalImageRel
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public int ImageId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblHospitalImageRel(TblHospitalImageRel hospitalImageRel, HttpStatusCode statusEffect)
        {
            Id = hospitalImageRel.Id;
            HospitalId = hospitalImageRel.HospitalId;
            ImageId = hospitalImageRel.ImageId;

            StatusEffect = statusEffect;
        }

        public DtoTblHospitalImageRel()
        {
        }
    }
}