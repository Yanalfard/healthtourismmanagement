namespace DashBoard.Models.Regular
{
    public class TblSection
    {
        public int Id { get; set; }
        public string SectionName { get; set; }

        public TblSection(int id)
        {
            this.Id = id;
        }

		public TblSection(int id, string sectionName)
        {
            this.Id = id;
            SectionName = sectionName;
        }
        public TblSection(string sectionName)
        {
            SectionName = sectionName;
        }

        public TblSection()
        {
            
        }
    }
}