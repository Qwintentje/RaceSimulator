using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public IParticipant.TeamColors TeamColor { get; set; }
        
        public float SectionSpeed { get; set; }

        public Driver(String name, int points, IEquipment equipment, IParticipant.TeamColors teamColor)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColor = teamColor;
            SectionSpeed = 100 / (Equipment.Speed * Equipment.Performance);
        }
    }
}
