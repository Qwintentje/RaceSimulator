using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;


        public SectionData GetSectionData(Section section)
        {
            SectionData value = _positions.GetValueOrDefault(section, null);

            if (value == null)
            {
                value = new SectionData(null, 0, null, 0);
                _positions.Add(section, value);
            }

            return value;
        }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant participent in Participants)
            {
                participent.Equipment.Performance = Convert.ToInt32(new Random(DateTime.Now.Millisecond));
                participent.Equipment.Quality = Convert.ToInt32(new Random(DateTime.Now.Millisecond));
            }
        }
        public void placeParticipants(Track track, List<IParticipant> participants)
        {

        }   



    }
}
