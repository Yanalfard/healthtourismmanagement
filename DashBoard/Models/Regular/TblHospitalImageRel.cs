namespace DashBoard.Models.Regular
{
    public class TblHospitalImageRel
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public int ImageId { get; set; }

        public TblHospitalImageRel(int id)
        {
            this.Id = id;
        }

		public TblHospitalImageRel(int id, int hospitalId, int imageId)
        {
            this.Id = id;
            HospitalId = hospitalId;
            ImageId = imageId;
        }

        {
            HospitalId = hospitalId;
            ImageId = imageId;
        }

        public TblHospitalImageRel()
        {
            
        }
    }
}