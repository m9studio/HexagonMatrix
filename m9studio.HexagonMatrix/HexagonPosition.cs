using System;

namespace m9studio.HexagonMatrix
{
    public struct HexagonPosition : ICloneable
    {
        private int _x, _y, _z;
        /// <summary>
        /// Позиция x.
        /// </summary>
        public int x 
        {
            get => _x;
            set
            {
                if (value < 0)
                    _x = 0;
                else if(y == 0 || z == 0)
                    _x = value;
            }
        }
        /// <summary>
        /// Позиция y.
        /// </summary>
        public int y 
        {
            get => _y;
            set
            {
                if (value < 0)
                    _y = 0;
                else if (x == 0 || z == 0)
                    _y = value;
            }
        }
        /// <summary>
        /// Позиция z.
        /// </summary>
        public int z 
        { 
            get => _z;
            set
            {
                if (value < 0)
                    _z = 0;
                else if (y == 0 || x == 0)
                    _z = value;
            }
        }
        /// <summary>
        /// Конструктор, при котором: x = 0; y = 0; z = 0;
        /// </summary>
        /// public HexagonPosition() { }
        /// <summary>
        /// Копирует позицию с другой позиции.
        /// </summary>
        /// <param name="obj">Позиция которую копируем.</param>
        public HexagonPosition(HexagonPosition obj) : this(obj.x, obj.y, obj.z) { }
        /// <summary>
        /// Конструктор, при котором хоть одно значение из x, y или z, должно быть 0;
        /// </summary>
        /// <remarks>
        /// Все точки должны быть не меньше 0.
        /// </remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public HexagonPosition(int x, int y, int z)
        {
            _x = 0;
            _y = 0;
            _z = 0;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region Move
        #region MoveX
        /// <summary>
        /// переместиться по x, на одну позицию вверх.
        /// </summary>
        public void MoveXUp() => MoveX(1);
        /// <summary>
        /// переместиться по x, на одну позицию вниз.
        /// </summary>
        public void MoveXDown() => MoveX(-1);
        /// <summary>
        /// переместиться по x, step раз вверх.
        /// </summary>
        public void MoveX(int step) => Move(ref _x, ref _y, ref _z, step);
        /// <summary>
        /// переместиться по x, step раз вверх.
        /// </summary>
        public void MoveXUp(int step) => MoveX(step);
        /// <summary>
        /// переместиться по x, step раз вниз.
        /// </summary>
        public void MoveXDown(int step) => MoveX(-step);
        #endregion
        #region MoveY
        /// <summary>
        /// переместиться по y, на одну позицию вверх.
        /// </summary>
        public void MoveYUp() => MoveY(1);
        /// <summary>
        /// переместиться по y, на одну позицию вниз.
        /// </summary>
        public void MoveYDown() => MoveY(-1);
        /// <summary>
        /// переместиться по y, step раз вверх.
        /// </summary>
        public void MoveY(int step) => Move(ref _y, ref _x, ref _z, step);
        /// <summary>
        /// переместиться по y, step раз вверх.
        /// </summary>
        public void MoveYUp(int step) => MoveY(step);
        /// <summary>
        /// переместиться по y, step раз вниз.
        /// </summary>
        public void MoveYDown(int step) => MoveY(-step);
        #endregion
        #region MoveZ
        /// <summary>
        /// переместиться по z, на одну позицию вверх.
        /// </summary>
        public void MoveZUp() => MoveZ(1);
        /// <summary>
        /// переместиться по z, на одну позицию вниз.
        /// </summary>
        public void MoveZDown() => MoveZ(-1);
        /// <summary>
        /// переместиться по z, step раз вверх.
        /// </summary>
        public void MoveZ(int step) => Move(ref _z, ref _x, ref _y, step);
        /// <summary>
        /// переместиться по z, step раз вверх.
        /// </summary>
        public void MoveZUp(int step) => MoveZ(step);
        /// <summary>
        /// переместиться по z, step раз вниз.
        /// </summary>
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





        public override string ToString()
        {
            return $"HexagonPosition: {{x: {x}; y: {y}; z: {z};}};";
        }
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
        public bool Equals(HexagonPosition Position)
        {
            if (x == Position.x && y == Position.y && z == Position.z)
                return true;
            return false;
        }
        public object Clone() => new HexagonPosition(this);



        public static HexagonPosition operator +(HexagonPosition A, HexagonPosition B)
        {
            HexagonPosition C = (HexagonPosition)A.Clone();
            C.MoveXUp(B.x);
            C.MoveYUp(B.y);
            C.MoveZUp(B.z);
            return C;
        }
        public static HexagonPosition operator -(HexagonPosition A, HexagonPosition B)
        {
            HexagonPosition C = (HexagonPosition)A.Clone();
            C.MoveXDown(B.x);
            C.MoveYDown(B.y);
            C.MoveZDown(B.z);
            return C;
        }
        public static HexagonPosition operator *(HexagonPosition A, int B)
        {
            HexagonPosition C = (HexagonPosition)A.Clone();
            B -= 1;
            C.MoveXUp(A.x * B);
            C.MoveYUp(A.y * B);
            C.MoveZUp(A.z * B);
            return C;
        }
        public static bool operator !=(HexagonPosition A, HexagonPosition B)
        {
            return !A.Equals(B);
        }
        public static bool operator ==(HexagonPosition A, HexagonPosition B)
        {
            return A.Equals(B);
        }



        /// <summary>
        /// R = 0.5;
        /// </summary>
        /// <param name="CircumscribedCircle">inscribed circle == !circumscribed circle</param>
        /// <returns></returns>
        public Position ToPosition(bool CircumscribedCircle)
        {
            Position A = new Position();

            if(z == 0)
            {
                A.x = x;
                A.y = y - (x / 2.0);
            }
            else if(x == 0)
            {
                A.x = -z;
                A.y = y - (z / 2.0);
            }
            else
            {
                A.x = x - z;
                A.y = -(x + z) / 2.0;
            }
            if(CircumscribedCircle)
            {
                const double x = 0.5;
                const double y = 0.8660254037844386;
                A.x *= x * 1.5;
                A.y *= y;
            }
            else
            {
                const double x = 0.5773502691896258;
                const double y = 1;
                A.x *= x * 1.5;
                A.y *= y;
            }
            return A;
        }
        public Position ToPosition() => ToPosition(true);



        public double Distance(HexagonPosition Position, bool CircumscribedCircle)
        {
            Position A = (this - Position).ToPosition(CircumscribedCircle);
            return Math.Sqrt(Math.Pow(A.x, 2) + Math.Pow(A.y, 2));
        }
        public double Distance(HexagonPosition Position) => Distance(Position, true);
    }
}
