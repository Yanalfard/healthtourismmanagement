namespace DashBoard.Models.Regular
{
    public class TblDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public bool NowActive { get; set; }

        public TblDoctor(int id)
        {
            this.Id = id;
        }

		public TblDoctor(int id, string name, int sectionId, bool nowActive)
        {
            this.Id = id;
            Name = name;
            SectionId = sectionId;
            NowActive = nowActive;
        }
        public TblDoctor(string name, int sectionId, bool nowActive)
        {
            Name = name;
            SectionId = sectionId;
            NowActive = nowActive;
        }

        public TblDoctor()
        {
            
        }
    }
}