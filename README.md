# Hexagon Matrix

The "HexagonMatrix" object is a hexagonal matrix, where x, y and z are the coordinates of the cells. If we talk about the matrix in more detail, then at least 1 value of x, y or z must be equal to or less than the value 0;

How the matrix works:
1) To determine the coordinates, we use 3 parameters x, y and z. Also, we have a Radius that is equal to the maximum value of x, y and z;
2) In the usual representation, the hexagon matrix looks like this:
  <br>
  <img src="https://user-images.githubusercontent.com/44808807/198515433-39817bd6-ef52-4cf9-a5c9-c185568ae5c1.png" width="450">
  <br>
4) For convenient storage, we need to divide it into 7 sectors:
  <br>
  <img src="https://user-images.githubusercontent.com/44808807/198515456-459be992-e1a4-4e17-8f0b-f62cfaadae49.png" width="450">
  <br>
5) As a result, we get 3 square matrices, 3 arrays and an object in the center, the coordinates of which will be x <= 0; y <= 0; z <= 0;
  <br>
  <img src="https://user-images.githubusercontent.com/44808807/198515466-c8305314-51fe-4175-b3b2-8fcc27df143f.png" width="450">
  <br>

6) These examples show how to take data from a hexagonal matrix:
7) Getting data from a square matrix
  ```rb
  hexagon.Get(2, 3, 0);
  hexagon.GetXY(2, 3);

  //this function call will not be an error
  hexagon.Get(2, 3, -1);
  ``` 
  Also works with "Set" methods.
  <br>
  <img src="https://user-images.githubusercontent.com/44808807/198515476-e23f569c-f8f7-4486-8f52-8e502b5b14ed.png" width="450">
  <br>
8) Getting data from an array
  ```rb
  hexagon.Get(0, 0, 3);
  hexagon.GetZ(3);

  //this function call will not be an error
  hexagon.Get(-2, -1, 3);
  ``` 
  Also works with "Set" methods. 
  <br>
  <img src="https://user-images.githubusercontent.com/44808807/198515490-94a8c999-e827-4572-8353-499d313a48ab.png" width="450">
  <br>
9) Getting the central data
  ```rb
  hexagon.Get(0, 0, 0);
  hexagon.GetCenter();

  //this function call will not be an error
  hexagon.Get(-2, 0, -1);
  ``` 
  Also works with "Set" methods.
  <br>
  <img src="https://user-images.githubusercontent.com/44808807/198515498-5375e9bf-7365-41db-95aa-c9013ef7d5d3.png" width="450">
  <br>
11) The example uses Radius 4.
  <br>
12) If x, y and z all have values greater than 0 or greater than the "Radius" value, then the "Get" method will return "null", and the "Set" method will return "false".
