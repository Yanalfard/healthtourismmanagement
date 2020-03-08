namespace DashBoard.Models.Regular
{
    public class TblSickness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }

        public TblSickness(int id)
        {
            this.Id = id;
        }

		public TblSickness(int id, string name, int sectionId)
        {
            this.Id = id;
            Name = name;
            SectionId = sectionId;
        }
        public TblSickness(string name, int sectionId)
        {
            Name = name;
            SectionId = sectionId;
        }

        public TblSickness()
        {
            
        }
    }
}