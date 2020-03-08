namespace DashBoard.Models.Regular
{
    public class TblCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TblCountry(int id)
        {
            this.Id = id;
        }

		public TblCountry(int id, string name)
        {
            this.Id = id;
            Name = name;
        }
        public TblCountry(string name)
        {
            Name = name;
        }

        public TblCountry()
        {
            
        }
    }
}