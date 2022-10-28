package ru.m9studio.HexagonMatrix;

public class HexagonPosition implements Cloneable {
	private int _x = 0, _y = 0, _z = 0;

	public int getX() { return _x; }
	public int getY() { return _y; }
	public int getZ() { return _z; }
	
	public void setX(int value) 
	{
		if (value < 0)
            _x = 0;
        else if(_y == 0 || _z == 0)
            _x = value;
	}
	public void setY(int value) 
	{
		if (value < 0)
            _y = 0;
        else if (_x == 0 || _z == 0)
            _y = value;
	}
	public void setZ(int value) 
	{
		if (value < 0)
            _z = 0;
        else if (_y == 0 || _x == 0)
            _z = value;
	}
	
	
	
    public HexagonPosition()
    {
    	this(0, 0, 0);
    }
    public HexagonPosition(HexagonPosition obj) 
    {
    	this(obj.getX(), obj.getY(), obj.getZ());
    }
    public HexagonPosition(int x, int y, int z)
    {
        setX(x);
        setY(y);
        setZ(z);
    }
    
    public void MoveXUp() { MoveX(MoveUp(_x, _y, _z)); }
    public void MoveXDown() { MoveX(MoveDown(_x, _y, _z)); }
    private void MoveX(movingObj m) 
    {
    	_x = m.moving;
    	_y = m.a;
    	_z = m.b;
    }
    public void MoveX(int step) { Move(step, 0); }
    public void MoveXUp(int step) { MoveX(step); }
    public void MoveXDown(int step) { MoveX(-step); }
    
    
    
    public void MoveYUp() { MoveY(MoveUp(_y, _x, _z)); }
    public void MoveYDown() { MoveY(MoveDown(_y, _x, _z)); }
    private void MoveY(movingObj m) 
    {
    	_y = m.moving;
    	_x = m.a;
    	_z = m.b;
    }
    public void MoveY(int step) { Move(step, 1); }
    public void MoveYUp(int step) { MoveY(step); }
    public void MoveYDown(int step) { MoveY(-step); }
    
    
    
    public void MoveZUp() { MoveZ(MoveUp(_z, _x, _y)); }
    public void MoveZDown() { MoveZ(MoveDown(_z, _x, _y)); }
    private void MoveZ(movingObj m) 
    {
    	_z = m.moving;
    	_x = m.a;
    	_y = m.b;
    }
    public void MoveZ(int step) { Move(step, 3); }
    public void MoveZUp(int step) { MoveZ(step); }
    public void MoveZDown(int step) { MoveZ(-step); }

    private void Move(int step, int f)
    {
        if (step > 0)
            for (int i = 0; i < step; i++)
            	switch(f)
            	{
	            	case 0: MoveXUp();
	            	case 1: MoveYUp();
	            	case 2: MoveZUp();
            	}
        else if (step < 0)
            for (int i = 0; i > step; i--)
            	switch(f)
            	{
	            	case 0: MoveXDown();
	            	case 1: MoveYDown();
	            	case 2: MoveZDown();
            	}
    }
    private movingObj MoveUp(int moving, int a, int b)
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
        return new movingObj(moving, a, b);
    }
    private movingObj MoveDown(int moving, int a, int b)
    {
        if (moving > 0)
            moving--;
        else
        {
            a++;
            b++;
        }
        return new movingObj(moving, a, b);
    }
    private class movingObj{
    	public int moving;
        public int a;
        public int b;
        public movingObj(int M, int A, int B) 
        {
        	moving = M;
        	a = A;
        	b = B;
        }
    }
    
    @Override
    public String toString() {
    	return "HexagonPosition: {x: " + _x + "; y: " + _y + "; z: " + _z + ";};";
    }
    @Override
    public int hashCode() {
    	return _x ^ _y ^ _z;
    }
    @Override
    public boolean equals(Object obj) {
    	if(obj.getClass() != this.getClass())
    		return false;
    	if(obj == this)
    		return true;
    	if(obj == null || this == null)
    		return false;
    	HexagonPosition Obj = (HexagonPosition)obj;
        if (_x == Obj._x && _y == Obj._y && _z == Obj._z)
            return true;
    	return false;
    }
    @Override
    protected Object clone() {
    	return new HexagonPosition(this);
    }
}
