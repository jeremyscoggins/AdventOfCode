namespace Day06
{
    internal class Point
    {  
        public int x { get; set; }
        public int y { get; set; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null &&
                   x == point.x &&
                   y == point.y;
        }
    }
}