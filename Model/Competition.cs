using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; } 
        public Queue<Track> Tracks { get; set; }
        public Competition()
        {
            this.Participants = new List<IParticipant>();
            this.Tracks = new Queue<Track>();
        }
        public Track NextTrack()
        {
            if (!Tracks.Any())
            {
                return null;
            }
            return Tracks.Dequeue();
        }

    }
}

