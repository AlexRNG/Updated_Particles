# Updated_Particles
Updated version of my console based particle system

Looking back on my particles project I had some gripes with how it had been coded and wanted to try and produce a more streamlined version to challenge myself.
When planning this project I had some different goals I wanted to reach, which are:
1. I wanted to have the ability to add multiple materials in which act differently.
2. I wanted the addShape function to be easier to use.
3. And I wanted it to be easier to look at.

### How it Works 
similarly to my Conway's Game of Life project, the map is split into cells and is iterated through to update. Each cell can be alive or dead and each has a material, on startUp each cell has the default material "empty".
Each cell is an object, and has the attributes to hold its co-ordinates, its material, and wheter its alive
### Cell Class
```c#
class Cell
    {
        public int _x;
        public int _y;
        public bool _alive;
        public string _material;

        public Cell(int column, int row)
        {
            _x = column;
            _y = row;
            _alive = false;
            _neighbors = new List<Cell>();
            _material = "empty";
        }
```
the Cell class only has one method and that is fall(). This function is what determines how each cell will change in the next iteration.
##### fall()

This most definitely could have been done in a more efficient way, the fall function gets called on for every cell in the map when it needs to update. When called, it will try and see if the space below the cell is empty, if so it is moved there and the map is updated accordingly. eg,
```c#
              this._alive = false;
              map[this._x][this._y + 1]._alive = true;
              this._material = "empty";
              map[this._x][this._y + 1]._material = "powder";
```
This effectively moves the cell to a new position, this can be done in all directions, 

If that space is occupied (ie isnt empty), it will "fall" to either available side of the cell below, if both ar abailable then a random number is used to decide which one it falls into.
If neither space is free the cell will attempt to shift to the side 
### Map Class
```c#
class Map
    {
        public int _width;
        public int _height;
        public List<List<Cell>> _map;

        public Map(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new List<List<Cell>>();
        }
```

The map class acts as a way to create and store map data, as the grid is a list of cell lists, it also gives me a means of accessing individual cells
Map functions include:
- GenerateMap():
this function takes an input of width and height and generates a list of nodes with those dimensions.
- DrawMap():
this function iterates over the map and prints it, different materials being printed in different characters

the update functions work by calling the fall function for each cell of a certain type, I did this so that different materials could move at different rates.
- UpdatePowder():
updates the position of any powder in the map 
- UpdateLiquid():
updates the position of any liquid in the map, when called, it updates every liquid cell 10 times 
- UpdateGas():
updates the position of any gas in the map, when called, it updates every gas cell 20 times 
- AddShape(): takes parameters of width, height, material, and a co-ordinate and creates a square of those dimensions and material from the given co-ordinate 

