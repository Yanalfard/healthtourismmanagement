using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblCountry(TblCountry country, HttpStatusCode statusEffect)
        {
            Id = country.Id;
            Name = country.Name;

            StatusEffect = statusEffect;
        }

        public DtoTblCountry()
        {
        }
    }
}