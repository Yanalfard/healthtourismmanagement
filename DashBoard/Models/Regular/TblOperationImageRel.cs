namespace DashBoard.Models.Regular
{
    public class TblOperationImageRel
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int ImageId { get; set; }

        public TblOperationImageRel(int id)
        {
            this.Id = id;
        }

		public TblOperationImageRel(int id, int operationId, int imageId)
        {
            this.Id = id;
            OperationId = operationId;
            ImageId = imageId;
        }
        public TblOperationImageRel(int operationId, int imageId)
        {
            OperationId = operationId;
            ImageId = imageId;
        }

        public TblOperationImageRel()
        {
            
        }
    }
}