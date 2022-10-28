namespace Model
{
    public class SectionData
    {
        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }
        public DateTime StartTimeLeft { get; set; }
        public DateTime StartTimeRight { get; set; }

        public bool AddParticipantToSection(IParticipant participant)
        {
            if (Left == null)
            {
                Left = participant;
                DistanceLeft = 0;
                return true;
            }

            if (Right == null)
            {
                Right = participant;
                DistanceRight = 0;
                return true;
            }

            return false;
        }
    }
}