using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblOperation
    {
        public int Id { get; set; }
        public string OperationName { get; set; }
        public long OperationPrice { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblOperation(TblOperation operation, HttpStatusCode statusEffect)
        {
            Id = operation.Id;
            OperationName = operation.OperationName;
            OperationPrice = operation.OperationPrice;

            StatusEffect = statusEffect;
        }

        public DtoTblOperation()
        {
        }
    }
}