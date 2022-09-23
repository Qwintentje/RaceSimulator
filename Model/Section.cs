﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }
        public enum SectionTypes
        {
            Straigth,
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
