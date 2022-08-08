using System;
using System.Collections.Generic;
using System.Threading;

namespace New_Particles
{
    class Program
    {
        static void Main(string[] args)
        {
            Map game = new Map(117, 28);
            game.GenerateMap();
            var cell = game._map[0][1];
            //game._map[0][0]._alive = true;
            //game._map[0][0]._material = "powder";
            //game._map[1][1]._alive = true;
            //game._map[1][1]._material = "powder";
            //game._map[1][0]._alive = true;
            //game._map[1][0]._material = "powder";

            game.AddShape(12, 0, 0 ,"powder");
            //game.AddShape(10, 30, 9, "gas");
            game.AddShape(19, 80, 0, "gas");
            game.AddShape(25, 40, 1, "liquid");


            while (true)
            {
                game.UpdateLiquid();
                game.UpdatePowder();
                game.UpdateGas();
                game.DrawMap();
                Thread.Sleep(100);
                Console.Clear();


            }
        }
    }
    class Cell
    {
        public int _x;
        public int _y;
        public bool _alive;
        public string _material;
        public List<Cell> _neighbors;

        public Cell(int column, int row)
        {
            _x = column;
            _y = row;
            _alive = false;
            _neighbors = new List<Cell>();
            _material = "empty";
        }

        public void GetNeigbors(Map map)
        {
            if (this._y - 1 >= 0) // above
            {
                this._neighbors.Add(map._map[this._x][this._y - 1]);
            }
            if (this._x + 1 < map._width && this._y - 1 >= 0) // top right
            {
                this._neighbors.Add(map._map[this._x + 1][this._y - 1]);
            }
            if (this._x + 1 < map._width) // right
            {
                this._neighbors.Add(map._map[this._x + 1][this._y]);
            }
            if (this._x + 1 < map._width && this._y + 1 < map._height) // bottom right
            {
                this._neighbors.Add(map._map[this._x + 1][this._y + 1]);
            }
            if (this._y + 1 < map._height) // below
            {
                this._neighbors.Add(map._map[this._x][this._y + 1]);
            }
            if (this._x - 1 >= 0 && this._y + 1 < map._height) // bottom left
            {
                this._neighbors.Add(map._map[this._x - 1][this._y + 1]);
            }
            if (this._x - 1 >= 0) // left
            {
                this._neighbors.Add(map._map[this._x - 1][this._y]);
            }
            if (this._x - 1 >= 0 && this._y - 1 >= 0) // top left
            {
                this._neighbors.Add(map._map[this._x - 1][this._y - 1]);
            }
        }

        public void Fall(Map f)
        {
            var map = f._map;
            if (this._y + 1 < f._height)
            {
                if (this._material == "powder")
                {
                    if (map[this._x][this._y + 1]._material == "empty")
                    {
                        this._alive = false;
                        map[this._x][this._y + 1]._alive = true;
                        this._material = "empty";
                        map[this._x][this._y + 1]._material = "powder";
                    }
                    else if (map[this._x][this._y + 1]._material != "empty")
                    {
                        if (this._x + 1 < f._width && this._x - 1 > 0)
                        {
                            if (map[this._x + 1][this._y + 1]._material != "empty" && map[this._x - 1][this._y + 1]._material == "empty")
                            {
                                this._alive = false;
                                map[this._x - 1][this._y + 1]._alive = true;
                                this._material = "empty";
                                map[this._x - 1][this._y + 1]._material = "powder";
                            }
                            if (map[this._x + 1][this._y + 1]._material == "empty" && map[this._x - 1][this._y + 1]._material != "empty")
                            {
                                this._alive = false;
                                map[this._x + 1][this._y + 1]._alive = true;
                                this._material = "empty";
                                map[this._x + 1][this._y + 1]._material = "powder";
                            }
                            if (map[this._x + 1][this._y + 1]._material == "empty" && map[this._x - 1][this._y + 1]._material == "empty")
                            {
                                Random rnd = new Random();
                                int num = rnd.Next(1, 11);
                                if (num > 5)
                                {
                                    this._alive = false;
                                    map[this._x - 1][this._y + 1]._alive = true;
                                    this._material = "empty";
                                    map[this._x - 1][this._y + 1]._material = "powder";
                                }
                                else
                                {
                                    this._alive = false;
                                    map[this._x + 1][this._y + 1]._alive = true;
                                    this._material = "empty";
                                    map[this._x + 1][this._y + 1]._material = "powder";
                                }
                            }
                            if (map[this._x][this._y + 1]._material == "liquid")
                            {
                                this._material = "liquid";
                                map[this._x][this._y + 1]._material = "powder";
                            }
                            if (map[this._x + 1][this._y + 1]._material == "liquid")
                            {
                                this._material = "liquid";
                                map[this._x + 1][this._y + 1]._material = "powder";
                            }
                            if (map[this._x - 1][this._y + 1]._material == "liquid")
                            {
                                this._material = "liquid";
                                map[this._x - 1][this._y + 1]._material = "powder";
                            }
                        }
                    }
                }
                if (this._material == "liquid")
                {
                    if (this._y == 0)
                    {
                        this._alive = false;
                        this._material = "empty";
                    }
                    Random rnd = new Random();
                    int num = rnd.Next(1, 11);
                    if (this._x + 1 < f._width && this._x - 1 >= 0 && this._y + 1 < f._height && this._y - 1 >= 0)
                    {
                        if (map[this._x][this._y + 1]._material == "empty")
                        {
                            this._alive = false;
                            this._material = "empty";
                            map[this._x][this._y + 1]._material = "liquid";
                            map[this._x][this._y + 1]._alive = true;
                        }
                        else if (map[this._x][this._y + 1]._material != "empty")
                        {
                            if (map[this._x + 1][this._y + 1]._material == "empty" && map[this._x - 1][this._y + 1]._material != "empty")
                            {
                                this._material = "empty";
                                this._alive = false;
                                map[this._x + 1][this._y + 1]._material = "liquid";
                                map[this._x + 1][this._y + 1]._alive = true;
                            }
                            if (map[this._x + 1][this._y + 1]._material != "empty" && map[this._x - 1][this._y + 1]._material == "empty")
                            {
                                this._material = "empty";
                                this._alive = false;
                                map[this._x - 1][this._y + 1]._material = "liquid";
                                map[this._x - 1][this._y + 1]._alive = true;
                            }
                            
                            else if (map[this._x + 1][this._y]._material == "empty" && num > 5)
                            {
                                this._material = "empty";
                                this._alive = false;
                                map[this._x + 1][this._y]._material = "liquid";
                                map[this._x + 1][this._y]._alive = true;
                            }
                            else if (map[this._x - 1][this._y]._material == "empty"  && num <= 5)
                            {
                                this._material = "empty";
                                map[this._x - 1][this._y]._alive = true;
                                this._alive = false;
                                map[this._x - 1][this._y]._material = "liquid";
                                
                            }

                        }
                    }
                }
                if (this._material == "gas")
                {
                    Random rnd = new Random();
                    int num = rnd.Next(1, 11);
                    if (this._x + 1 < f._width && this._x - 1 >= 0 && this._y + 1 < f._height && this._y - 1 >= 0)
                    {
                        if (map[this._x][this._y - 1]._material == "empty")
                        {
                            this._alive = false;
                            this._material = "empty";
                            map[this._x][this._y - 1]._material = "gas";
                            map[this._x][this._y - 1]._alive = true;
                        }

                        else if (map[this._x][this._y - 1]._material != "empty")
                        {
                            if (map[this._x + 1][this._y - 1]._material == "empty" && map[this._x - 1][this._y - 1]._material != "empty")
                            {
                                this._material = "empty";
                                this._alive = false;
                                map[this._x + 1][this._y - 1]._material = "gas";
                                map[this._x + 1][this._y - 1]._alive = true;
                            }

                            if (map[this._x + 1][this._y - 1]._material != "empty" && map[this._x - 1][this._y - 1]._material == "empty")
                            {
                                this._material = "empty";
                                this._alive = false;
                                map[this._x - 1][this._y - 1]._material = "gas";
                                map[this._x - 1][this._y - 1]._alive = true;
                            }

                            else if (map[this._x + 1][this._y]._material == "empty" && num > 5)
                            {
                                this._material = "empty";
                                this._alive = false;
                                map[this._x + 1][this._y]._material = "gas";
                                map[this._x + 1][this._y]._alive = true;
                            }

                            else if (map[this._x - 1][this._y]._material == "empty" && num <= 5)
                            {
                                this._material = "empty";
                                map[this._x - 1][this._y]._alive = true;
                                this._alive = false;
                                map[this._x - 1][this._y]._material = "gas";
                            }
                        }
                    }
                }
            } 
        }
    }
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
        public void GenerateMap()
        {
            for (int x = 0; x < _width; ++x)
            {
                List<Cell> column = new List<Cell>();
                for (int y = 0; y < _height; ++y)
                {
                    column.Add(new Cell(x, y));
                }
                _map.Add(column);
            }
        }
        public void DrawMap()
        {
            var count = 0;
            string printLayer = "#";
            for (int y = 0; y < _height; ++y)
            {
                for (int x = 0; x < _width; ++x)
                {
                    if (_map[x][count]._material == "empty")
                    {
                        string temp = " ";
                        printLayer += temp;
                    }
                    if (_map[x][count]._material == "powder" )
                    {
                        string temp = "O";
                        printLayer += temp;
                    }
                    if (_map[x][count]._material == "liquid" && _map[x][count]._alive == true)
                    {
                        string temp = "^";
                        printLayer += temp;
                    }
                    if (_map[x][count]._material == "gas")
                    {
                        string temp = "%";
                        printLayer += temp;
                    }
                }
                Console.WriteLine(printLayer + "#");
                printLayer = "#";
                count += 1; 
            }
            printLayer = "";
            for (int i = 0; i < _width + 2; ++i)
            {
                printLayer += "#";
            }
            Console.WriteLine(printLayer);
        }
        public void UpdatePowder()
        {
            int count = 2;
            for (int i = 0; i < count; ++i)
            {
                for (int x = _width - 1; x >= 0; --x)
                {
                    for (int y = _height - 1; y >= 0; --y)
                    {
                        if (_map[x][y]._material == "powder")
                        {
                            _map[x][y].Fall(this);
                        }
                    }
                }
            }
        }
        public void UpdateLiquid()
        {
            int count = 10;
            for (int i = 0; i < count; ++i)
            {
                for (int x = _width - 1; x > 0; --x)
                {
                    for (int y = _height - 1; y > 0; --y)
                    {
                        if (_map[x][y]._material == "liquid")
                        {
                            _map[x][y].Fall(this);
                        }
                    }
                }
            }
        }
        public void UpdateGas()
        {
            int count = 20;
            for (int i = 0; i < count; ++i)
            {
                for (int x = 0; x < _width; ++x)
                {
                    for (int y = 0; y < _height; ++y)
                    {
                        if (_map[x][y]._material == "gas")
                        {
                            _map[x][y].Fall(this);
                        }
                    }
                }
            }
        }
        public void AddShape(int width, int _x, int _y, string material) 
        {
            if (_x + width < _width && _y + width < _height)
            {
                for (int x = _x; x < _x + width; ++x)
                {
                    for (int y = _y; y < _y + width; ++y)
                    {
                        _map[x][y]._alive = true;
                        _map[x][y]._material = material;
                    }
                }
            }
        }
    }
}
