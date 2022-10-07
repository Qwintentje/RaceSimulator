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
        
        public SectionData(IParticipant left, int distanceLeft, IParticipant right, int distanceRight)
        {
            Left = left;
            DistanceLeft = distanceLeft;
            Right = right;
            DistanceRight = distanceRight;
        }

        public SectionData(){}

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