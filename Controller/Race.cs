﻿using Model;
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
            if (_positions[section] == null)
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
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

/*        public static implicit operator Race(Track v)
        {
            throw new NotImplementedException();
        }*/
    }
}
