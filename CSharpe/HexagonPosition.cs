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

        public HexagonPosition() : this(0, 0, 0) { }
        public HexagonPosition(HexagonPosition obj) : this(obj.x, obj.y, obj.z) { }
        public HexagonPosition(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }



        public void MoveXUp() => MoveUp(ref _x, ref _y, ref _z);
        public void MoveXDown() => MoveDown(ref _x, ref _y, ref _z);
        public void MoveX(int step) => Move(step, MoveXUp, MoveXDown);
        public void MoveXUp(int step) => MoveX(step);
        public void MoveXDown(int step) => MoveX(-step);

        public void MoveYUp() => MoveUp(ref _y, ref _x, ref _z);
        public void MoveYDown() => MoveDown(ref _y, ref _x, ref _z);
        public void MoveY(int step) => Move(step, MoveYUp, MoveYDown);
        public void MoveYUp(int step) => MoveY(step);
        public void MoveYDown(int step) => MoveY(-step);

        public void MoveZUp() => MoveUp(ref _z, ref _x, ref _y);
        public void MoveZDown() => MoveDown(ref _z, ref _x, ref _y);
        public void MoveZ(int step) => Move(step, MoveZUp, MoveZDown);
        public void MoveZUp(int step) => MoveZ(step);
        public void MoveZDown(int step) => MoveZ(-step);



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



        public override string ToString()
        {
            return $"HexagonPosition: {{x: {x}; y: {y}; z: {z};}};";
        }
        public override int GetHashCode()
        {
            return x ^ y ^ z;
        }
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            if(obj.GetType() != this.GetType())
                return false;
            HexagonPosition Obj = (HexagonPosition)obj;
            if (x == Obj.x && y == Obj.y && z == Obj.z)
                return true;
            return false;
        }

        public object Clone() => new HexagonPosition(this);
    }
}
