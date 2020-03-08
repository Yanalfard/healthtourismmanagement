namespace DashBoard.Models.Regular
{
    public class TblHospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserPassId { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public TblHospital(int id)
        {
            this.Id = id;
        }

		public TblHospital(int id, string name, int userPassId, int percentage, string description, string longitude, string latitude)
        {
            this.Id = id;
            Name = name;
            UserPassId = userPassId;
            Percentage = percentage;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
        }
        public TblHospital(string name, int userPassId, int percentage, string description, string longitude, string latitude)
        {
            Name = name;
            UserPassId = userPassId;
            Percentage = percentage;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
        }

        public TblHospital()
        {
            
        }
    }
}