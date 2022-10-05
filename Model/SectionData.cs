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

        public SectionData()
        {
            DistanceLeft = 100;
            DistanceRight = 100;
        }

        public bool AddParticipantToSection(IParticipant participant)
        {
            if (Left == null)
            {
                Left = participant;
                return true;
            }

            if (Right == null)
            {
                Right = participant;
                return true;
            }

            return false;
        }
    }
}