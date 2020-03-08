namespace DashBoard.Models.Regular
{
    public class TblCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public TblCity(int id)
        {
            this.Id = id;
        }

		public TblCity(int id, string name, int countryId)
        {
            this.Id = id;
            Name = name;
            CountryId = countryId;
        }

        public TblCity(string name, int countryId)
        {
            Name = name;
            CountryId = countryId;
        }

        public TblCity()
        {
            
        }
    }
}