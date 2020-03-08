using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblPatientSicknessRel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SicknessId { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public string BeforeCureDesc { get; set; }
        public string AfterCureDesc { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblPatientSicknessRel(TblPatientSicknessRel patientSicknessRel, HttpStatusCode statusEffect)
        {
            Id = patientSicknessRel.Id;
            PatientId = patientSicknessRel.PatientId;
            SicknessId = patientSicknessRel.SicknessId;
            DoctorId = patientSicknessRel.DoctorId;
            HospitalId = patientSicknessRel.HospitalId;
            BeforeCureDesc = patientSicknessRel.BeforeCureDesc;
            AfterCureDesc = patientSicknessRel.AfterCureDesc;

            StatusEffect = statusEffect;
        }

        public DtoTblPatientSicknessRel()
        {
        }
    }
}