using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m9studio.HexagonMatrix
{
    public class HexagonPosition : ICloneable
    {
        private int _x = 0, _y = 0, _z = 0;
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

        public HexagonPosition() : this(0, 0, 0) { }
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
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region Move
        #region MoveX
        /// <summary>
        /// переместиться по x, на одну позицию вверх.
        /// </summary>
        public void MoveXUp() => MoveUp(ref _x, ref _y, ref _z);
        /// <summary>
        /// переместиться по x, на одну позицию вниз.
        /// </summary>
        public void MoveXDown() => MoveDown(ref _x, ref _y, ref _z);
        /// <summary>
        /// переместиться по x, step раз вверх.
        /// </summary>
        public void MoveX(int step) => Move(step, MoveXUp, MoveXDown);
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
        public void MoveYUp() => MoveUp(ref _y, ref _x, ref _z);
        /// <summary>
        /// переместиться по y, на одну позицию вниз.
        /// </summary>
        public void MoveYDown() => MoveDown(ref _y, ref _x, ref _z);
        /// <summary>
        /// переместиться по y, step раз вверх.
        /// </summary>
        public void MoveY(int step) => Move(step, MoveYUp, MoveYDown);
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
        public void MoveZUp() => MoveUp(ref _z, ref _x, ref _y);
        /// <summary>
        /// переместиться по z, на одну позицию вниз.
        /// </summary>
        public void MoveZDown() => MoveDown(ref _z, ref _x, ref _y);
        /// <summary>
        /// переместиться по z, step раз вверх.
        /// </summary>
        public void MoveZ(int step) => Move(step, MoveZUp, MoveZDown);
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
        private delegate void funcMove();
        private void Move(int step, funcMove fUp, funcMove fDown)
        {
            if (step > 0)
                for (int i = 0; i < step; i++)
                    fUp();
            else if (step < 0)
                for (int i = 0; i > step; i--)
                    fDown();
        }
        private void MoveUp(ref int moving, ref int a, ref int b)
        {
            if (moving > 0)
                moving++;
            else
            {
                if (a > 0 && b > 0)
                {

                    if (a > 0)
                        a--;
                    if (b > 0)
                        b--;
                }
                else
                    moving++;
            }
        }
        private void MoveDown(ref int moving, ref int a, ref int b)
        {
            if (moving > 0)
                moving--;
            else
            {
                a++;
                b++;
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
    }
}
