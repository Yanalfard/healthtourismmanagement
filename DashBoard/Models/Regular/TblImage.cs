namespace DashBoard.Models.Regular
{
    public class TblImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }

        public TblImage(int id)
        {
            this.Id = id;
        }

		public TblImage(int id, string image, int status)
        {
            this.Id = id;
            Image = image;
            Status = status;
        }
        public TblImage(string image, int status)
        {
            Image = image;
            Status = status;
        }

        public TblImage()
        {
            
        }
    }
}