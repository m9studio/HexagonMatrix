# HexagonalMatrix

The "HexagonalMatrix" object is a hexagonal matrix, where x, y and z are the coordinates of the cells. If we talk about the matrix in more detail, then at least 1 value of x, y or z must be equal to or less than the value 0;

How the matrix works:
1) To determine the coordinates, we use 3 parameters x, y and z. Also, we have a Radius that is equal to the maximum value of x, y and z;
2) In the usual representation, the hexagon matrix looks like this:
![1](https://user-images.githubusercontent.com/44808807/198515433-39817bd6-ef52-4cf9-a5c9-c185568ae5c1.png)
4) For convenient storage, we need to divide it into 7 sectors:
![2](https://user-images.githubusercontent.com/44808807/198515456-459be992-e1a4-4e17-8f0b-f62cfaadae49.png)
5) As a result, we get 3 square matrices, 3 arrays and an object in the center, the coordinates of which will be x <= 0; y <= 0; z <= 0;
![3](https://user-images.githubusercontent.com/44808807/198515466-c8305314-51fe-4175-b3b2-8fcc27df143f.png)

6) These examples show how to take data from a hexagonal matrix:
7) Getting data from a square matrix
```rb
obj.Get(2, 3, 0);
obj.GetXY(2, 3);

//this function call will not be an error
obj.Get(2, 3, -1);
``` 
Also works with "Set" methods. 
![4](https://user-images.githubusercontent.com/44808807/198515476-e23f569c-f8f7-4486-8f52-8e502b5b14ed.png)
8) Getting data from an array
```rb
obj.Get(0, 0, 3);
obj.GetZ(3);

//this function call will not be an error
obj.Get(-2, -1, 3);
``` 
Also works with "Set" methods. 
![5](https://user-images.githubusercontent.com/44808807/198515490-94a8c999-e827-4572-8353-499d313a48ab.png)
9) Getting the central data
```rb
obj.Get(0, 0, 0);
obj.GetCenter();

//this function call will not be an error
obj.Get(-2, 0, -1);
``` 
Also works with "Set" methods. 
![6](https://user-images.githubusercontent.com/44808807/198515498-5375e9bf-7365-41db-95aa-c9013ef7d5d3.png)
11) The example uses Radius 4.
12) If x, y and I all have values greater than 0 or greater than the "Radius" value, then the "Get" method will return "null", and the "Set" method will return "false".
