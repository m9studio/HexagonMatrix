using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m9studio.HexagonalMatrix
{
    public class HexagonalMatrix<T>
    {
        private T[,] matrixYZ;
        private T[,] matrixXY;
        private T[,] matrixXZ;
        private T[] matrixX;
        private T[] matrixY;
        private T[] matrixZ;
        private T matrixCenter;

        private int radius;
        public int Radius { get => radius; }
        public int Length { get => 3 * (int)Math.Pow(Radius, 2) + 3 * Radius + 1; }

        public HexagonalMatrix(int radius)
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
        public HexagonalMatrix(int radius, T obj) => Fill(obj);
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


        public T Get(int x, int y, int z)
        {
            if (x > 0 && y > 0 && z > 0)
                return default(T);
            else if (x > Radius || y > Radius || z > Radius)
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
        public T GetXY(int x, int y) => Get(x, y, 0);
        public T GetXZ(int x, int z) => Get(x, 0, z);
        public T GetYZ(int y, int z) => Get(0, y, z);
        public T GetX(int x) => Get(x, 0, 0);
        public T GetY(int y) => Get(0, y, 0);
        public T GetZ(int z) => Get(0, 0, z);
        public T GetCenter() => matrixCenter;


        public bool Set(T obj, int x, int y, int z)
        {
            if (x > 0 && y > 0 && z > 0)
                return false;
            else if (x > Radius || y > Radius || z > Radius)
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
        public bool SetXY(T obj, int x, int y) => Set(obj, x, y, 0);
        public bool SetXZ(T obj, int x, int z) => Set(obj, x, 0, z);
        public bool SetYZ(T obj, int y, int z) => Set(obj, 0, y, z);
        public bool SetX(T obj, int x) => Set(obj, x, 0, 0);
        public bool SetY(T obj, int y) => Set(obj, 0, y, 0);
        public bool SetZ(T obj, int z) => Set(obj, 0, 0, z);
        public bool SetCenter(T obj) => Set(obj, 0, 0, 0);



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




        public override string ToString()
        {
            return $"HexagonalMatrix(Radius : {Radius})";
        }
        public override int GetHashCode()
        {
            int r = Radius;
            for (int i = 0; i < Radius; i++)
            {
                for (int j = 0; j < Radius; j++)
                {
                    r += matrixYZ[i, j] != null ? matrixYZ[i, j].GetHashCode() : 0;
                    r += matrixXY[i, j] != null ? matrixXY[i, j].GetHashCode() : 0;
                    r += matrixXZ[i, j] != null ? matrixXZ[i, j].GetHashCode() : 0;
                }
                r += matrixX[i] != null ? matrixX[i].GetHashCode() : 0;
                r += matrixY[i] != null ? matrixY[i].GetHashCode() : 0;
                r += matrixZ[i] != null ? matrixZ[i].GetHashCode() : 0;
            }
            r += matrixCenter != null ? matrixCenter.GetHashCode() : 0;
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            HexagonalMatrix<T> Obj;
            try
            {
                Obj = (HexagonalMatrix<T>)obj;
            }
            catch (Exception)
            {
                return false;
            }
            if (Obj.Radius != this.Radius)
                return false;
            if (!this.matrixCenter.Equals(Obj.matrixCenter))
                return false;
            for (int i = 0; i < this.Radius; i++)
            {
                for (int j = 0; j < this.Radius; j++)
                {
                    if (!this.matrixYZ[i, j].Equals(Obj.matrixYZ[i, j]))
                        return false;
                    if (!this.matrixXY[i, j].Equals(Obj.matrixXY[i, j]))
                        return false;
                    if (!this.matrixXZ[i, j].Equals(Obj.matrixXZ[i, j]))
                        return false;
                }
                if (!this.matrixX[i].Equals(Obj.matrixX[i]))
                    return false;
                if (!this.matrixY[i].Equals(Obj.matrixY[i]))
                    return false;
                if (!this.matrixZ[i].Equals(Obj.matrixZ[i]))
                    return false;
            }
            return true;
        }
    }
}
