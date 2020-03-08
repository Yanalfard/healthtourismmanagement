namespace DashBoard.Models.Regular
{
    public class TblNewsImageRel
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int ImageId { get; set; }

        public TblNewsImageRel(int id)
        {
            this.Id = id;
        }

		public TblNewsImageRel(int id, int newsId, int imageId)
        {
            this.Id = id;
            NewsId = newsId;
            ImageId = imageId;
        }
        public TblNewsImageRel(int newsId, int imageId)
        {
            NewsId = newsId;
            ImageId = imageId;
        }

        public TblNewsImageRel()
        {
            
        }
    }
}