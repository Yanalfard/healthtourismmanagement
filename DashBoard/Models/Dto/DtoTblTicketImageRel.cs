using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblTicketImageRel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ImageId { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblTicketImageRel(TblTicketImageRel ticketImageRel, HttpStatusCode statusEffect)
        {
            Id = ticketImageRel.Id;
            TicketId = ticketImageRel.TicketId;
            ImageId = ticketImageRel.ImageId;

            StatusEffect = statusEffect;
        }

        public DtoTblTicketImageRel()
        {
        }
    }
}