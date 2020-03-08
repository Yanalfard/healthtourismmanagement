using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblOperationImageRel
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int ImageId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblOperationImageRel(TblOperationImageRel operationImageRel, HttpStatusCode statusEffect)
        {
            Id = operationImageRel.Id;
            OperationId = operationImageRel.OperationId;
            ImageId = operationImageRel.ImageId;

            StatusEffect = statusEffect;
        }

        public DtoTblOperationImageRel()
        {
        }
    }
}