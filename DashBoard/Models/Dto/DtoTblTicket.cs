using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblTicket
    {
        public int Id { get; set; }
        public bool IsRegistered { get; set; }
        public int UserPassId { get; set; }
        public string Email { get; set; }
        public string TellNo { get; set; }
        public string Data { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblTicket(TblTicket ticket, HttpStatusCode statusEffect)
        {
            Id = ticket.Id;
            IsRegistered = ticket.IsRegistered;
            UserPassId = ticket.UserPassId;
            Email = ticket.Email;
            TellNo = ticket.TellNo;
            Data = ticket.Data;

            StatusEffect = statusEffect;
        }

        public DtoTblTicket()
        {
        }
    }
}