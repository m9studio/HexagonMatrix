package ru.m9studio.HexagonMatrix;

public class HexagonMatrix<T> {
    private T[][] matrixYZ;
    private T[][] matrixXY;
    private T[][] matrixXZ;
    private T[] matrixX;
    private T[] matrixY;
    private T[] matrixZ;
    private T matrixCenter;
    private int radius;
    
    
    
    public int Radius() { return radius; }
    public int Length() { return 3 * (int)Math.pow(radius, 2) + 3 * radius + 1; }
    
    
    
    public HexagonMatrix(int radius)
    {
        if (radius < 0)
            radius = 0;
        matrixYZ = (T[][])new Object[radius][radius];
        matrixXY = (T[][])new Object[radius][radius];
        matrixXZ = (T[][])new Object[radius][radius];
        matrixX = (T[])new Object[radius];
        matrixY = (T[])new Object[radius];
        matrixZ = (T[])new Object[radius];
        this.radius = radius;
    }
    public HexagonMatrix(int radius, T obj)
    {
    	this(radius);
    	Fill(obj);
    }
    public void Fill(T obj)
    {
    	for (int i = 0; i < radius; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                matrixYZ[i][j] = obj;
                matrixXY[i][j] = obj;
                matrixXZ[i][j] = obj;
            }
            matrixX[i] = obj;
            matrixY[i] = obj;
            matrixZ[i] = obj;
        }
        matrixCenter = obj;
    }
    
    
    
    public T Get(HexagonPosition HexPos) { return Get(HexPos.getX(), HexPos.getY(), HexPos.getZ()); }
    public T Get(int x, int y, int z)
    {
        if (!isLocated(x, y, z))
            return null;
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
    public T GetXY(HexagonPosition HexPos) { return GetXY(HexPos.getX(), HexPos.getY()); }
    public T GetXY(int x, int y) { return Get(x, y, 0); }
    public T GetXZ(HexagonPosition HexPos) { return GetXZ(HexPos.getX(), HexPos.getZ()); }
    public T GetXZ(int x, int z) { return Get(x, 0, z); }
    public T GetYZ(HexagonPosition HexPos) { return GetYZ(HexPos.getY(), HexPos.getZ()); }
    public T GetYZ(int y, int z) { return Get(0, y, z); }
    public T GetX(HexagonPosition HexPos) { return GetX(HexPos.getX()); }
    public T GetX(int x) { return Get(x, 0, 0); }
    public T GetY(HexagonPosition HexPos) { return GetY(HexPos.getY()); }
    public T GetY(int y) { return Get(0, y, 0); }
    public T GetZ(HexagonPosition HexPos) { return GetZ(HexPos.getZ()); }
    public T GetZ(int z) { return Get(0, 0, z); }
    public T GetCenter() { return matrixCenter; }
    
    
    
    public boolean Set(T obj, HexagonPosition HexPos) { return Set(obj, HexPos.getX(), HexPos.getY(), HexPos.getZ()); }
    public boolean Set(T obj, int x, int y, int z)
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
    public boolean SetXY(T obj, HexagonPosition HexPos) { return SetXY(obj, HexPos.getX(), HexPos.getY()); }
    public boolean SetXY(T obj, int x, int y) { return Set(obj, x, y, 0); }
    public boolean SetXZ(T obj, HexagonPosition HexPos) { return SetXZ(obj, HexPos.getX(), HexPos.getZ()); }
    public boolean SetXZ(T obj, int x, int z) { return Set(obj, x, 0, z); }
    public boolean SetYZ(T obj, HexagonPosition HexPos) { return SetYZ(obj, HexPos.getY(), HexPos.getZ()); }
    public boolean SetYZ(T obj, int y, int z) { return Set(obj, 0, y, z); }
    public boolean SetX(T obj, HexagonPosition HexPos) { return SetX(obj, HexPos.getX()); }
    public boolean SetX(T obj, int x) { return Set(obj, x, 0, 0); }
    public boolean SetY(T obj, HexagonPosition HexPos) { return SetY(obj, HexPos.getY()); }
    public boolean SetY(T obj, int y) { return Set(obj, 0, y, 0); }
    public boolean SetZ(T obj, HexagonPosition HexPos) { return SetZ(obj, HexPos.getZ()); }
    public boolean SetZ(T obj, int z) { return Set(obj, 0, 0, z); }
    public boolean SetCenter(T obj) { return Set(obj, 0, 0, 0); }
    
    
    
    public boolean isLocated(HexagonPosition HexPos) 
    { 
    	return isLocated(HexPos.getX(), HexPos.getY(), HexPos.getZ()); 
    }
    public boolean isLocated(int x, int y, int z)
    {
        if (x > 0 && y > 0 && z > 0)
            return false;
        else if (x > radius || y > radius || z > radius)
            return false;
        return true;
    }
    
    
    
    private T getAB(int A, int B, T[][] matrix)
    {
        if (!(A <= radius && A > 0 && B <= radius && B > 0))
            return null;
        return matrix[A - 1][B - 1];
    }
    private T getA(int A, T[] matrix)
    {
        if (!(A <= radius && A > 0))
            return null;
        return matrix[A - 1];

    }
    private boolean setAB(int A, int B, T obj, T[][] matrix)
    {
        if (!(A <= radius && A > 0 && B <= radius && B > 0))
            return false;
        matrix[A - 1][B - 1] = obj;
        return true;
    }
    private boolean setA(int A, T obj, T[] matrix)
    {
        if (!(A <= radius && A > 0))
            return false;
        matrix[A - 1] = obj;
        return true;
    }
    
    
    @Override
    public int hashCode() {
    	int r = radius;
        for (int i = 0; i < radius; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                r += matrixYZ[i][j] != null ? matrixYZ[i][j].hashCode() : 0;
                r += matrixXY[i][j] != null ? matrixXY[i][j].hashCode() : 0;
                r += matrixXZ[i][j] != null ? matrixXZ[i][j].hashCode() : 0;
            }
            r += matrixX[i] != null ? matrixX[i].hashCode() : 0;
            r += matrixY[i] != null ? matrixY[i].hashCode() : 0;
            r += matrixZ[i] != null ? matrixZ[i].hashCode() : 0;
        }
        r += matrixCenter != null ? matrixCenter.hashCode() : 0;
        return r;
    }
    @Override
    public String toString() {
    	return "HexagonMatrix: {Radius: " + radius + ";};";
    }
    @Override
    public boolean equals(Object obj) {
    	if (this == obj)
            return true;
        if (obj.getClass() != this.getClass())
            return false;
    	if(obj == null || this == null)
    		return false;
        HexagonMatrix<T> Obj;
        try
        {
            Obj = (HexagonMatrix<T>)obj;
        }
		catch (Exception e) {
			return false;
		}
        if (Obj.radius != this.radius)
            return false;
        if (!this.matrixCenter.equals(Obj.matrixCenter))
            return false;
        for (int i = 0; i < this.radius; i++)
        {
            for (int j = 0; j < this.radius; j++)
            {
                if (!this.matrixYZ[i][j].equals(Obj.matrixYZ[i][j]))
                    return false;
                if (!this.matrixXY[i][j].equals(Obj.matrixXY[i][j]))
                    return false;
                if (!this.matrixXZ[i][j].equals(Obj.matrixXZ[i][j]))
                    return false;
            }
            if (!this.matrixX[i].equals(Obj.matrixX[i]))
                return false;
            if (!this.matrixY[i].equals(Obj.matrixY[i]))
                return false;
            if (!this.matrixZ[i].equals(Obj.matrixZ[i]))
                return false;
        }
        return true;
    }
}
