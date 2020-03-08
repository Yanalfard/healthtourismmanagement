namespace DashBoard.Models.Regular
{
    public class TblOperation
    {
        public int Id { get; set; }
        public string OperationName { get; set; }
        public long OperationPrice { get; set; }

        public TblOperation(int id)
        {
            this.Id = id;
        }

		public TblOperation(int id, string operationName, long operationPrice)
        {
            this.Id = id;
            OperationName = operationName;
            OperationPrice = operationPrice;
        }
        public TblOperation(string operationName, long operationPrice)
        {
            OperationName = operationName;
            OperationPrice = operationPrice;
        }

        public TblOperation()
        {
            
        }
    }
}