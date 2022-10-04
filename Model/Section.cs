namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }
        public enum SectionTypes
        {
            Straight,
            LeftCorner,
            RightCorner,
            StartGrid,
            Finish
        }
        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }
    }
}
