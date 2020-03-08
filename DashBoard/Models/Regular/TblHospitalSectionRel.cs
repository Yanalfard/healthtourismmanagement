namespace DashBoard.Models.Regular
{
    public class TblHospitalSectionRel
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public int SectionId { get; set; }
        public int DoctorId { get; set; }

        public TblHospitalSectionRel(int id)
        {
            this.Id = id;
        }

		public TblHospitalSectionRel(int id, int hospitalId, int sectionId, int doctorId)
        {
            this.Id = id;
            HospitalId = hospitalId;
            SectionId = sectionId;
            DoctorId = doctorId;
        }
        public TblHospitalSectionRel(int hospitalId, int sectionId, int doctorId)
        {
            HospitalId = hospitalId;
            SectionId = sectionId;
            DoctorId = doctorId;
        }

        public TblHospitalSectionRel()
        {
            
        }
    }
}