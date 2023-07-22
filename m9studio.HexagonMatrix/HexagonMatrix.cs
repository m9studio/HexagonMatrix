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
        /// <summary>
        /// Радиус матрицы.
        /// </summary>
        public int Radius { get => radius; }
        /// <summary>
        /// Кол-во ячеек в матрице.
        /// </summary>
        /// <remarks>
        /// Length = 3 * Radius^2 + 3 * Radius + 1;
        /// </remarks>
        public int Length { get => 3 * (int)Math.Pow(Radius, 2) + 3 * Radius + 1; }

        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="radius">Радиус создаваемой матрицы.</param>
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
        /// <summary>
        /// Конструктор, с предварительным заполенением.
        /// </summary>
        /// <param name="radius">Радиус создаваемой матрицы.</param>
        /// <param name="obj">Объект, которой будет заполнена матрица.</param>
        public HexagonMatrix(int radius, T obj) : this(radius) => Fill(obj);
        #endregion

        /// <summary>
        /// Заполнение матрицы.
        /// </summary>
        /// <param name="obj">Объект, которой будет заполнена матрица.</param>
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
        /// <summary>
        /// Получение элемента матрицы.
        /// </summary>
        /// <param name="Position">Позиция.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T Get(HexagonPosition Position) => Get(Position.x, Position.y, Position.z);
        /// <summary>
        /// Получение элемента матрицы.
        /// </summary>
        /// <param name="x">Позиция x.</param>
        /// <param name="y">Позиция y.</param>
        /// <param name="z">Позиция z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
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
        /// <summary>
        /// Получение элемента матрицы по x и y.
        /// </summary>
        /// <param name="Position">Позиция, из которой будут браться x и y.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetXY(HexagonPosition Position) => GetXY(Position.x, Position.y);
        /// <summary>
        /// Получение элемента матрицы по x и y.
        /// </summary>
        /// <param name="x">Позиция x.</param>
        /// <param name="y">Позиция y.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetXY(int x, int y) => Get(x, y, 0);
        /// <summary>
        /// Получение элемента матрицы по x и z.
        /// </summary>
        /// <param name="Position">Позиция, из которой будут браться x и z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetXZ(HexagonPosition Position) => GetXZ(Position.x, Position.z);
        /// <summary>
        /// Получение элемента матрицы по x и z.
        /// </summary>
        /// <param name="x">Позиция x.</param>
        /// <param name="z">Позиция z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetXZ(int x, int z) => Get(x, 0, z);
        /// <summary>
        /// Получение элемента матрицы по y и z.
        /// </summary>
        /// <param name="Position">Позиция, из которой будут браться y и z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetYZ(HexagonPosition Position) => GetYZ(Position.y, Position.z);
        /// <summary>
        /// Получение элемента матрицы по y и z.
        /// </summary>
        /// <param name="y">Позиция y.</param>
        /// <param name="z">Позиция z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetYZ(int y, int z) => Get(0, y, z);
        /// <summary>
        /// Получение элемента матрицы на линии x.
        /// </summary>
        /// <param name="Position">Позиция, из которой будет браться x.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetX(HexagonPosition Position) => GetX(Position.x);
        /// <summary>
        /// Получение элемента матрицы на линии x.
        /// </summary>
        /// <param name="x">Позиция x.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetX(int x) => Get(x, 0, 0);
        /// <summary>
        /// Получение элемента матрицы на линии y.
        /// </summary>
        /// <param name="Position">Позиция, из которой будет браться y.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetY(HexagonPosition Position) => GetY(Position.y);
        /// <summary>
        /// Получение элемента матрицы на линии y.
        /// </summary>
        /// <param name="y">Позиция y.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetY(int y) => Get(0, y, 0);
        /// <summary>
        /// Получение элемента матрицы на линии z.
        /// </summary>
        /// <param name="Position">Позиция, из которой будет браться z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetZ(HexagonPosition Position) => GetZ(Position.z);
        /// <summary>
        /// Получение элемента матрицы на линии z.
        /// </summary>
        /// <param name="z">Позиция z.</param>
        /// <returns>Элемент в матрице, на данной позиции.</returns>
        public T GetZ(int z) => Get(0, 0, z);
        /// <summary>
        /// Получение центрального элемента матрицы.
        /// </summary>
        /// <returns>Центральный элемент матрицы.</returns>
        public T GetCenter() => matrixCenter;
        #endregion
        #region Set
        public bool Set(T obj, HexagonPosition Position) => Set(obj, Position.x, Position.y, Position.z);
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
        public bool SetXY(T obj, HexagonPosition Position) => SetXY(obj, Position.x, Position.y);
        public bool SetXY(T obj, int x, int y) => Set(obj, x, y, 0);
        public bool SetXZ(T obj, HexagonPosition Position) => SetXZ(obj, Position.x, Position.z);
        public bool SetXZ(T obj, int x, int z) => Set(obj, x, 0, z);
        public bool SetYZ(T obj, HexagonPosition Position) => SetYZ(obj, Position.y, Position.z);
        public bool SetYZ(T obj, int y, int z) => Set(obj, 0, y, z);
        public bool SetX(T obj, HexagonPosition Position) => SetX(obj, Position.x);
        public bool SetX(T obj, int x) => Set(obj, x, 0, 0);
        public bool SetY(T obj, HexagonPosition Position) => SetY(obj, Position.y);
        public bool SetY(T obj, int y) => Set(obj, 0, y, 0);
        public bool SetZ(T obj, HexagonPosition Position) => SetZ(obj, Position.z);
        public bool SetZ(T obj, int z) => Set(obj, 0, 0, z);
        public bool SetCenter(T obj) => Set(obj, 0, 0, 0);
        #endregion

        public bool isLocated(HexagonPosition Position) => isLocated(Position.x, Position.y, Position.z);
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


        public override string ToString()
        {
            return $"HexagonMatrix: {{Radius: {Radius};}};";
        }
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
        public bool Equals(HexagonMatrix<T> Obj)
        {
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
