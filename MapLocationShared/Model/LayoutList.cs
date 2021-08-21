using System;
using System.Collections.Generic;
using System.Text;

namespace MapLocationShared.Model
{
    public class LayoutList
    {
        public LayoutList(string Link, string Name)
        {
            this.Link = Link;
            this.Name = Name;
        }
        public string Link { get; set; }
        public string Name { get; set; }
    }
}
