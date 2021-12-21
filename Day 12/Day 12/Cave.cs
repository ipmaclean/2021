using System.Collections.Generic;

namespace Day_12
{
    public class Cave
    {
        public string Name { get; set; }
        public bool IsSmall => Name == Name.ToLower();
        public List<Cave> AdjacentCaves { get; set; } = new List<Cave>();

        public Cave(string name)
        {
            Name = name;
        }
    }
}
