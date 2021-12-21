namespace Day_15
{
    public class Node
    {
        public int Value { get; set; }
        public bool Visited { get; set; } = false;
        public int TentativeDistance { get; set; } = int.MaxValue;
        public (int, int) Coordinates { get; set; }

        public Node(int value, (int, int) coordinates)
        {
            Value = value;
            Coordinates = coordinates;
        }
    }
}
