namespace DashBoard.Models.Regular
{
    public class TblPatientImageRel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ImageId { get; set; }

        public TblPatientImageRel(int id)
        {
            this.Id = id;
        }

		public TblPatientImageRel(int id, int patientId, int imageId)
        {
            this.Id = id;
            PatientId = patientId;
            ImageId = imageId;
        }
        public TblPatientImageRel(int patientId, int imageId)
        {
            PatientId = patientId;
            ImageId = imageId;
        }

        public TblPatientImageRel()
        {
            
        }
    }
}