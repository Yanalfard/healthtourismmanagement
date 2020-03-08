using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblHospitalSectionRel
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public int SectionId { get; set; }
        public int DoctorId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblHospitalSectionRel(TblHospitalSectionRel hospitalSectionRel, HttpStatusCode statusEffect)
        {
            Id = hospitalSectionRel.Id;
            HospitalId = hospitalSectionRel.HospitalId;
            SectionId = hospitalSectionRel.SectionId;
            DoctorId = hospitalSectionRel.DoctorId;

            StatusEffect = statusEffect;
        }

        public DtoTblHospitalSectionRel()
        {
        }
    }
}