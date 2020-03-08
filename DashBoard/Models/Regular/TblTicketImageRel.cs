namespace DashBoard.Models.Regular
{
    public class TblTicketImageRel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ImageId { get; set; }

        public TblTicketImageRel(int id)
        {
            this.Id = id;
        }

		public TblTicketImageRel(int id, int ticketId, int imageId)
        {
            this.Id = id;
            TicketId = ticketId;
            ImageId = imageId;
        }
        public TblTicketImageRel(int ticketId, int imageId)
        {
            TicketId = ticketId;
            ImageId = imageId;
        }

        public TblTicketImageRel()
        {
            
        }
    }
}