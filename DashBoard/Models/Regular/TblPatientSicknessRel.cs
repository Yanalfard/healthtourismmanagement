namespace DashBoard.Models.Regular
{
    public class TblPatientSicknessRel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SicknessId { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public string BeforeCureDesc { get; set; }
        public string AfterCureDesc { get; set; }

        public TblPatientSicknessRel(int id)
        {
            this.Id = id;
        }

		public TblPatientSicknessRel(int id, int patientId, int sicknessId, int doctorId, int hospitalId, string beforeCureDesc, string afterCureDesc)
        {
            this.Id = id;
            PatientId = patientId;
            SicknessId = sicknessId;
            DoctorId = doctorId;
            HospitalId = hospitalId;
            BeforeCureDesc = beforeCureDesc;
            AfterCureDesc = afterCureDesc;
        }
        public TblPatientSicknessRel(int patientId, int sicknessId, int doctorId, int hospitalId, string beforeCureDesc, string afterCureDesc)
        {
            PatientId = patientId;
            SicknessId = sicknessId;
            DoctorId = doctorId;
            HospitalId = hospitalId;
            BeforeCureDesc = beforeCureDesc;
            AfterCureDesc = afterCureDesc;
        }

        public TblPatientSicknessRel()
        {
            
        }
    }
}