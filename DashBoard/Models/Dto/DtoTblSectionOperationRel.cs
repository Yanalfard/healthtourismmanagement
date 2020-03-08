using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblSectionOperationRel
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int OperationId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblSectionOperationRel(TblSectionOperationRel sectionOperationRel, HttpStatusCode statusEffect)
        {
            Id = sectionOperationRel.Id;
            SectionId = sectionOperationRel.SectionId;
            OperationId = sectionOperationRel.OperationId;

            StatusEffect = statusEffect;
        }

        public DtoTblSectionOperationRel()
        {
        }
    }
}