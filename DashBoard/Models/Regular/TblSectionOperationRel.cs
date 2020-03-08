namespace DashBoard.Models.Regular
{
    public class TblSectionOperationRel
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int OperationId { get; set; }

        public TblSectionOperationRel(int id)
        {
            this.Id = id;
        }

		public TblSectionOperationRel(int id, int sectionId, int operationId)
        {
            this.Id = id;
            SectionId = sectionId;
            OperationId = operationId;
        }

        {
            SectionId = sectionId;
            OperationId = operationId;
        }

        public TblSectionOperationRel()
        {
            
        }
    }
}