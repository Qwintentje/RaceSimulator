﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Track(string name, Section[] sections)
        {
            Name = name;
            Sections = ArrayToLinkedList(sections);
        }

        public LinkedList<Section> ArrayToLinkedList(Section[] sections)
        {
            LinkedList<Section> linkedList = new LinkedList<Section>();
            foreach (var section in sections)
            {
                linkedList.AddLast(section);
            }

            return linkedList;

        }


    }
}
