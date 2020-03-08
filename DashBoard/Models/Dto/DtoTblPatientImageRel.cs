using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblPatientImageRel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ImageId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblPatientImageRel(TblPatientImageRel patientImageRel, HttpStatusCode statusEffect)
        {
            Id = patientImageRel.Id;
            PatientId = patientImageRel.PatientId;
            ImageId = patientImageRel.ImageId;

            StatusEffect = statusEffect;
        }

        public DtoTblPatientImageRel()
        {
        }
    }
}