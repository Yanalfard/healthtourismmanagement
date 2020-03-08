namespace DashBoard.Models.Regular
{
    public class TblConfig
    {
        public int Id { get; set; }
        public string JwtKey { get; set; }

        public TblConfig(int id)
        {
            this.Id = id;
        }

		public TblConfig(int id, string jwtKey)
        {
            this.Id = id;
            JwtKey = jwtKey;
        }
        public TblConfig(string jwtKey)
        {
            JwtKey = jwtKey;
        }

        public TblConfig()
        {
            
        }
    }
}