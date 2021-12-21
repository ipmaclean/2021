using System.Collections.Generic;

namespace Day_12
{
    class RouteToCheck
    {
        public Cave Cave { get; set; }
        public List<Cave> Route { get; set; }
        public bool SmallCaveRevisted { get; set; }


        public RouteToCheck(Cave cave, List<Cave> route, bool smallCaveRevisted)
        {
            Cave = cave;
            Route = route.GetRange(0, route.Count);
            Route.Add(cave);
            SmallCaveRevisted = smallCaveRevisted;
        }
    }
}
