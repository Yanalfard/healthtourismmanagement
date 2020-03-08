using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public bool NowActive { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblDoctor(TblDoctor doctor, HttpStatusCode statusEffect)
        {
            Id = doctor.Id;
            Name = doctor.Name;
            SectionId = doctor.SectionId;
            NowActive = doctor.NowActive;

            StatusEffect = statusEffect;
        }

        public DtoTblDoctor()
        {
        }
    }
}