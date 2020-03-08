using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblSickness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblSickness(TblSickness sickness, HttpStatusCode statusEffect)
        {
            Id = sickness.Id;
            Name = sickness.Name;
            SectionId = sickness.SectionId;

            StatusEffect = statusEffect;
        }

        public DtoTblSickness()
        {
        }
    }
}