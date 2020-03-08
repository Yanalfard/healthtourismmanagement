using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblSection
    {
        public int Id { get; set; }
        public string SectionName { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblSection(TblSection section, HttpStatusCode statusEffect)
        {
            Id = section.Id;
            SectionName = section.SectionName;

            StatusEffect = statusEffect;
        }

        public DtoTblSection()
        {
        }
    }
}