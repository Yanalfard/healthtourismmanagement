using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblCity(TblCity city, HttpStatusCode statusEffect)
        {
            Id = city.Id;
            Name = city.Name;
            CountryId = city.CountryId;

            StatusEffect = statusEffect;
        }

        public DtoTblCity()
        {
        }
    }
}