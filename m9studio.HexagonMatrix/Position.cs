namespace m9studio.HexagonMatrix
{
    /// <include file='./lang-default.xml' path='Root/Position/Position/*'/>
    public struct Position
    {
        /// <include file='./lang-default.xml' path='Root/Position/X/*'/>
        public double X;
        /// <include file='./lang-default.xml' path='Root/Position/Y/*'/>
        public double Y;
        /// <include file='./lang-default.xml' path='Root/Position/ConstructorXY/*'/>
        public Position(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
