using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblHospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserPassId { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblHospital(TblHospital hospital, HttpStatusCode statusEffect)
        {
            Id = hospital.Id;
            Name = hospital.Name;
            UserPassId = hospital.UserPassId;
            Percentage = hospital.Percentage;
            Description = hospital.Description;
            Longitude = hospital.Longitude;
            Latitude = hospital.Latitude;

            StatusEffect = statusEffect;
        }

        public DtoTblHospital()
        {
        }
    }
}