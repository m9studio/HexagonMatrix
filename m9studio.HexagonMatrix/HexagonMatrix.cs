using System;

namespace m9studio.HexagonMatrix
{
    public class HexagonMatrix<T>
    {
        private T[,] matrixYZ;
        private T[,] matrixXY;
        private T[,] matrixXZ;
        private T[] matrixX;
        private T[] matrixY;
        private T[] matrixZ;
        private T matrixCenter;
        private int radius;
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Radius/*'/>
        public int Radius => radius;
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Length/*'/>
        public int Length => 3 * (int)Math.Pow(Radius, 2) + 3 * Radius + 1;

        #region Конструктор
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Constructor/*'/>
        public HexagonMatrix(int radius)
        {
            if (radius < 0)
                radius = 0;
            matrixYZ = new T[radius, radius];
            matrixXY = new T[radius, radius];
            matrixXZ = new T[radius, radius];
            matrixX = new T[radius];
            matrixY = new T[radius];
            matrixZ = new T[radius];
            this.radius = radius;
        }
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/ConstructorObj/*'/>
        public HexagonMatrix(int radius, T obj) : this(radius) => Fill(obj);
        #endregion

        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Fill/*'/>
        public void Fill(T obj)
        {
            for (int i = 0; i < Radius; i++)
            {
                for (int j = 0; j < Radius; j++)
                {
                    matrixYZ[i, j] = obj;
                    matrixXY[i, j] = obj;
                    matrixXZ[i, j] = obj;
                }
                matrixX[i] = obj;
                matrixY[i] = obj;
                matrixZ[i] = obj;
            }
            matrixCenter = obj;
        }

        #region Get
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/XYZ/*'/>
        public T Get(HexagonPosition Position) => Get(Position.X, Position.Y, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/XYZ/*'/>
        public T Get(int x, int y, int z)
        {
            if (!isLocated(x, y, z))
                return default(T);
            else if (x > 0 && y > 0)
                return getAB(x, y, matrixXY);
            else if (y > 0 && z > 0)
                return getAB(y, z, matrixYZ);
            else if (x > 0 && z > 0)
                return getAB(x, z, matrixXZ);
            else if (x > 0)
                return getA(x, matrixX);
            else if (y > 0)
                return getA(y, matrixY);
            else if (z > 0)
                return getA(z, matrixZ);
            else
                return matrixCenter;
        }
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/XY/*'/>
        public T GetXY(HexagonPosition Position) => GetXY(Position.X, Position.Y);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/XY/*'/>
        public T GetXY(int x, int y) => Get(x, y, 0);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/XZ/*'/>
        public T GetXZ(HexagonPosition Position) => GetXZ(Position.X, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/XZ/*'/>
        public T GetXZ(int x, int z) => Get(x, 0, z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/YZ/*'/>
        public T GetYZ(HexagonPosition Position) => GetYZ(Position.Y, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/YZ/*'/>
        public T GetYZ(int y, int z) => Get(0, y, z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/X/*'/>
        public T GetX(HexagonPosition Position) => GetX(Position.X);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/X/*'/>
        public T GetX(int x) => Get(x, 0, 0);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/Y/*'/>
        public T GetY(HexagonPosition Position) => GetY(Position.Y);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/Y/*'/>
        public T GetY(int y) => Get(0, y, 0);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/Z/*'/>
        public T GetZ(HexagonPosition Position) => GetZ(Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/Z/*'/>
        public T GetZ(int z) => Get(0, 0, z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Get/Center/*'/>
        public T GetCenter() => matrixCenter;
        #endregion
        #region Set
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/XYZ/*'/>
        public bool Set(T obj, HexagonPosition Position) => Set(obj, Position.X, Position.Y, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/XYZ/*'/>
        public bool Set(T obj, int x, int y, int z)
        {
            if (!isLocated(x, y, z))
                return false;
            else if (x > 0 && y > 0)
                return setAB(x, y, obj, matrixXY);
            else if (y > 0 && z > 0)
                return setAB(y, z, obj, matrixYZ);
            else if (x > 0 && z > 0)
                return setAB(x, z, obj, matrixXZ);
            else if (x > 0)
                return setA(x, obj, matrixX);
            else if (y > 0)
                return setA(y, obj, matrixY);
            else if (z > 0)
                return setA(z, obj, matrixZ);
            else
                matrixCenter = obj;
            return true;
        }
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/XY/*'/>
        public bool SetXY(T obj, HexagonPosition Position) => SetXY(obj, Position.X, Position.Y);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/XY/*'/>
        public bool SetXY(T obj, int x, int y) => Set(obj, x, y, 0);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/XZ/*'/>
        public bool SetXZ(T obj, HexagonPosition Position) => SetXZ(obj, Position.X, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/XZ/*'/>
        public bool SetXZ(T obj, int x, int z) => Set(obj, x, 0, z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/YZ/*'/>
        public bool SetYZ(T obj, HexagonPosition Position) => SetYZ(obj, Position.Y, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/YZ/*'/>
        public bool SetYZ(T obj, int y, int z) => Set(obj, 0, y, z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/X/*'/>
        public bool SetX(T obj, HexagonPosition Position) => SetX(obj, Position.X);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/X/*'/>
        public bool SetX(T obj, int x) => Set(obj, x, 0, 0);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/Y/*'/>
        public bool SetY(T obj, HexagonPosition Position) => SetY(obj, Position.Y);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/Y/*'/>
        public bool SetY(T obj, int y) => Set(obj, 0, y, 0);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/Z/*'/>
        public bool SetZ(T obj, HexagonPosition Position) => SetZ(obj, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/Z/*'/>
        public bool SetZ(T obj, int z) => Set(obj, 0, 0, z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/Set/Center/*'/>
        public bool SetCenter(T obj) => Set(obj, 0, 0, 0);
        #endregion
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/isLocated/*'/>
        public bool isLocated(HexagonPosition Position) => isLocated(Position.X, Position.Y, Position.Z);
        /// <include file='./lang-default.xml' path='Root/HexagonMatrix/isLocated/*'/>
        public bool isLocated(int x, int y, int z)
        {
            if (x > 0 && y > 0 && z > 0)
                return false;
            else if (x > Radius || y > Radius || z > Radius)
                return false;
            return true;
        }


        private T getAB(int A, int B, T[,] matrix)
        {
            if (!(A <= Radius && A > 0 && B <= Radius && B > 0))
                return default(T);
            return matrix[A - 1, B - 1];
        }
        private T getA(int A, T[] matrix)
        {
            if (!(A <= Radius && A > 0))
                return default(T);
            return matrix[A - 1];

        }
        private bool setAB(int A, int B, T obj, T[,] matrix)
        {
            if (!(A <= Radius && A > 0 && B <= Radius && B > 0))
                return false;
            matrix[A - 1, B - 1] = obj;
            return true;
        }
        private bool setA(int A, T obj, T[] matrix)
        {
            if (!(A <= Radius && A > 0))
                return false;
            matrix[A - 1] = obj;
            return true;
        }
        
        
        
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"HexagonMatrix: {{Radius: {Radius};}};";
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
            HexagonMatrix<T> Obj;
            try
            {
                Obj = (HexagonMatrix<T>)obj;
            }
            catch (Exception)
            {
                return false;
            }
            return Equals(Obj);
        }
        /// <inheritdoc cref="Equals(object)"/>
        public bool Equals(HexagonMatrix<T> obj)
        {
            if (obj.Radius != this.Radius)
                return false;
            if (!this.matrixCenter.Equals(obj.matrixCenter))
                return false;
            for (int i = 0; i < this.Radius; i++)
            {
                for (int j = 0; j < this.Radius; j++)
                {
                    if (!this.matrixYZ[i, j].Equals(obj.matrixYZ[i, j]))
                        return false;
                    if (!this.matrixXY[i, j].Equals(obj.matrixXY[i, j]))
                        return false;
                    if (!this.matrixXZ[i, j].Equals(obj.matrixXZ[i, j]))
                        return false;
                }
                if (!this.matrixX[i].Equals(obj.matrixX[i]))
                    return false;
                if (!this.matrixY[i].Equals(obj.matrixY[i]))
                    return false;
                if (!this.matrixZ[i].Equals(obj.matrixZ[i]))
                    return false;
            }
            return true;
        }
    }
}
