using System;

namespace m9studio.HexagonMatrix
{
    /// <include file='./lang-default.xml' path='Root/Position/HexagonPosition/*'/>
    public struct HexagonPosition : ICloneable
    {
        private int _x, _y, _z;
        /// <include file='./lang-default.xml' path='Root/Position/X/*'/>
        public int X 
        {
            get => _x;
            set
            {
                if (value < 0)
                    _x = 0;
                else if(Y == 0 || Z == 0)
                    _x = value;
            }
        }
        /// <include file='./lang-default.xml' path='Root/Position/Y/*'/>
        public int Y 
        {
            get => _y;
            set
            {
                if (value < 0)
                    _y = 0;
                else if (X == 0 || Z == 0)
                    _y = value;
            }
        }
        /// <include file='./lang-default.xml' path='Root/Position/Z/*'/>
        public int Z 
        { 
            get => _z;
            set
            {
                if (value < 0)
                    _z = 0;
                else if (Y == 0 || X == 0)
                    _z = value;
            }
        }

        /// <include file='./lang-default.xml' path='Root/Position/ConstructorObj/*'/>
        public HexagonPosition(HexagonPosition obj) : this(obj.X, obj.Y, obj.Z) { }
        /// <include file='./lang-default.xml' path='Root/Position/ConstructorXYZ/*'/>
        public HexagonPosition(int x, int y, int z)
        {
            _x = 0;
            _y = 0;
            _z = 0;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #region Move
        #region MoveX
        /// <include file='./lang-default.xml' path='Root/Position/Move/X/Up/*'/>
        public void MoveXUp() => MoveX(1);
        /// <include file='./lang-default.xml' path='Root/Position/Move/X/Down/*'/>
        public void MoveXDown() => MoveX(-1);
        /// <include file='./lang-default.xml' path='Root/Position/Move/X/Step/*'/>
        public void MoveX(int step) => Move(ref _x, ref _y, ref _z, step);
        /// <include file='./lang-default.xml' path='Root/Position/Move/X/UpStep/*'/>
        public void MoveXUp(int step) => MoveX(step);
        /// <include file='./lang-default.xml' path='Root/Position/Move/X/DownStep/*'/>
        public void MoveXDown(int step) => MoveX(-step);
        #endregion
        #region MoveY
        /// <include file='./lang-default.xml' path='Root/Position/Move/Y/Up/*'/>
        public void MoveYUp() => MoveY(1);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Y/Down/*'/>
        public void MoveYDown() => MoveY(-1);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Y/Step/*'/>
        public void MoveY(int step) => Move(ref _y, ref _x, ref _z, step);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Y/UpStep/*'/>
        public void MoveYUp(int step) => MoveY(step);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Y/DownStep/*'/>
        public void MoveYDown(int step) => MoveY(-step);
        #endregion
        #region MoveZ
        /// <include file='./lang-default.xml' path='Root/Position/Move/Z/Up/*'/>
        public void MoveZUp() => MoveZ(1);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Z/Down/*'/>
        public void MoveZDown() => MoveZ(-1);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Z/Step/*'/>
        public void MoveZ(int step) => Move(ref _z, ref _x, ref _y, step);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Z/UpStep/*'/>
        public void MoveZUp(int step) => MoveZ(step);
        /// <include file='./lang-default.xml' path='Root/Position/Move/Z/DownStep/*'/>
        public void MoveZDown(int step) => MoveZ(-step);
        #endregion
        #region private Move
        private void Move(ref int a, ref int b, ref int c, int step)
        {
            if(step > 0)
            {
                if(a == 0)
                {
                    int A = 0;
                    if (b > c)
                    {
                        A = c;
                    }
                    else
                    {
                        A = b;
                    }
                    step -= A;
                    b -= A;
                    c -= A;
                }
                a += step;
            }
            else if(step < 0)
            {
                step *= -1;
                if(a > 0)
                {
                    if(step > a)
                    {
                        step -= a;
                        a = 0;
                    }
                    else
                    {
                        a -= step;
                        step = 0;
                    }
                }
                b += step;
                c += step;
            }
        }
        #endregion
        #endregion
        #region Operator
        /// <inheritdoc/>
        public static HexagonPosition operator +(HexagonPosition A, HexagonPosition B)
        {
            HexagonPosition C = (HexagonPosition)A.Clone();
            C.MoveXUp(B.X);
            C.MoveYUp(B.Y);
            C.MoveZUp(B.Z);
            return C;
        }
        /// <inheritdoc/>
        public static HexagonPosition operator -(HexagonPosition A, HexagonPosition B)
        {
            HexagonPosition C = (HexagonPosition)A.Clone();
            C.MoveXDown(B.X);
            C.MoveYDown(B.Y);
            C.MoveZDown(B.Z);
            return C;
        }
        /// <inheritdoc/>
        public static HexagonPosition operator *(HexagonPosition A, int B)
        {
            HexagonPosition C = (HexagonPosition)A.Clone();
            B -= 1;
            C.MoveXUp(A.X * B);
            C.MoveYUp(A.Y * B);
            C.MoveZUp(A.Z * B);
            return C;
        }
        /// <inheritdoc/>
        public static bool operator !=(HexagonPosition A, HexagonPosition B)
        {
            return !A.Equals(B);
        }
        /// <inheritdoc/>
        public static bool operator ==(HexagonPosition A, HexagonPosition B)
        {
            return A.Equals(B);
        }
        #endregion


        /// <inheritdoc/>
        public override string ToString()
        {
            return $"HexagonPosition: {{x: {X}; y: {Y}; z: {Z};}};";
        }
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || this == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            HexagonPosition Obj = (HexagonPosition)obj;
            return Equals(Obj);
        }
        /// <inheritdoc cref="Equals(object)"/>
        public bool Equals(HexagonPosition obj)
        {
            if (X == obj.X && Y == obj.Y && Z == obj.Z)
                return true;
            return false;
        }
        /// <inheritdoc/>
        public object Clone() => new HexagonPosition(this);




        /// <include file='./lang-default.xml' path='Root/Position/ToPositionCircumscribedCircle/*'/>
        public Position ToPosition(bool CircumscribedCircle)
        {
            Position A = new Position();

            if(Z == 0)
            {
                A.X = X;
                A.Y = Y - (X / 2.0);
            }
            else if(X == 0)
            {
                A.X = -Z;
                A.Y = Y - (Z / 2.0);
            }
            else
            {
                A.X = X - Z;
                A.Y = -(X + Z) / 2.0;
            }
            if(CircumscribedCircle)
            {
                const double x = 0.5;
                const double y = 0.8660254037844386;
                A.X *= x * 1.5;
                A.Y *= y;
            }
            else
            {
                const double x = 0.5773502691896258;
                const double y = 1;
                A.X *= x * 1.5;
                A.Y *= y;
            }
            return A;
        }
        /// <include file='./lang-default.xml' path='Root/Position/ToPosition/*'/>
        public Position ToPosition() => ToPosition(true);

        /// <include file='./lang-default.xml' path='Root/Position/DistanceCircumscribedCircle/*'/>
        public double Distance(HexagonPosition Position, bool CircumscribedCircle)
        {
            Position A = (this - Position).ToPosition(CircumscribedCircle);
            return Math.Sqrt(Math.Pow(A.X, 2) + Math.Pow(A.Y, 2));
        }
        /// <include file='./lang-default.xml' path='Root/Position/Distance/*'/>
        public double Distance(HexagonPosition Position) => Distance(Position, true);
    }
}
