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
            if (_positions[section] != null)
            {
                return _positions[section];
            }
            else
            {
                _positions.Add(section, new SectionData());
                return _positions[section];
            }
        }
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
        }

        public void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                var performance_ = new Random();
                int performance = Convert.ToInt32(performance_);
                var quality_ = new Random();
                int quality = Convert.ToInt32(quality_);
                participant.Equipment.Performance = performance;
                participant.Equipment.Quality = quality;
            }
        }
    }
}
