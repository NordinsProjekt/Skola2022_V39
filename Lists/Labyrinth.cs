using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    public sealed class Labyrinth
    {
        private int[,] spot;
        private int[] startPoint = new int[2];
        private int[] lastspot = new int[2];
        private int start = -5;
        private MyQueue<Point> qList = new MyQueue<Point>();
        private Random rnd = new Random();
        public Labyrinth (int x, int y)
        {
            spot = new int[x, y];
        }

        public void GenerateMaze()
        {
            for (int i = 0; i < spot.Length/8; i++)
            {
                var point = RandomPoint();
                spot[point[0], point[1]] = -1;
            }
        }
        /// <summary>
        /// Will provide a random point if its out of bounds.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlaceStartPoint(int x, int y)
        {
            if (x >= spot.GetLength(0) || y >= spot.GetLength(0) || x <0 || y<0)
            {
                startPoint = RandomPoint();
            }
            else
            {
                startPoint[0] = x;
                startPoint[1] = y;
            }
            spot[startPoint[0], startPoint[1]] = start;
        }
        //x up,ner
        //y vänster,höger
        public void SolveMaze(bool printMaze)
        {
            if (printMaze)
                PrintMazeColor("Start");
            Move(startPoint[0], startPoint[1], 0);
            while (true)
            {
                Point tempLocation = qList.Next;
                if (tempLocation == null) break;
                Move(tempLocation.x, tempLocation.y, tempLocation.number);
            }
            if (printMaze)
                PrintMazeColor("Solved");
        }

        private void Move(int x, int y, int number)
        {
            lastspot = new int[2] { x, y };
            MoveLeft(number);
            MoveRight(number);
            MoveDown(number);
            MoveUp(number);
        }
        private void SaveLocation(Point cords)
        {
            qList.Add(cords);
        }
        private bool MoveLeft(int number)
        {
            int counter = number;
            if (lastspot[1] - 1 < spot.GetLength(1) && lastspot[1]-1 >= 0)
            {
                if (spot[lastspot[0], lastspot[1]-1] == 0)
                {
                    if (spot[lastspot[0], lastspot[1]] == start)
                        counter = 0;
                    else
                        counter = spot[lastspot[0], lastspot[1]];
                }
                else
                    return false;
                spot[lastspot[0], lastspot[1] -1] = ++counter;
                SaveLocation(new Point(lastspot[0], lastspot[1]-1,counter));
                return true;
            }
            return false;
        }

        private bool MoveRight(int number)
        {
            //x up,ner
            //y vänster,höger
            int counter = number;
            if (lastspot[1] + 1 < spot.GetLength(1) && lastspot[1] + 1 >= 0)
            {
                if (spot[lastspot[0], lastspot[1] + 1] == 0)
                {
                    if (spot[lastspot[0], lastspot[1]] == start)
                        counter = 0;
                    else
                        counter = spot[lastspot[0], lastspot[1]];
                }
                else
                    return false;
                spot[lastspot[0], lastspot[1] + 1] = ++counter;
                SaveLocation(new Point(lastspot[0], lastspot[1] + 1, counter));
                return true;
            }
            return false;
        }

        private bool MoveDown(int number)
        {
            //x up,ner
            //y vänster,höger
            int counter = number;
            if (lastspot[0] + 1 < spot.GetLength(0) && lastspot[0] + 1 >= 0)
            {
                if (spot[lastspot[0]+1, lastspot[1]] == 0)
                {
                    if (spot[lastspot[0], lastspot[1]] == start)
                        counter = 0;
                    else
                        counter = spot[lastspot[0], lastspot[1]];
                }
                else
                    return false;
                spot[lastspot[0]+1, lastspot[1]] = ++counter;
                SaveLocation(new Point(lastspot[0] +1, lastspot[1], counter));
                return true;
            }
            return false;
        }

        private bool MoveUp(int number)
        {
            //x up,ner
            //y vänster,höger
            int counter = number;
            if (lastspot[0] - 1 <= spot.GetLength(0) && lastspot[0] - 1 >= 0)
            {
                if (spot[lastspot[0] - 1, lastspot[1]] == 0)
                {
                    if (spot[lastspot[0], lastspot[1]] == start)
                        counter = 0;
                    else
                        counter = spot[lastspot[0], lastspot[1]];
                }
                else
                    return false;
                spot[lastspot[0] - 1, lastspot[1]] = ++counter;
                SaveLocation(new Point(lastspot[0] -1, lastspot[1], counter));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Generate everything and solves the maze.
        /// Prints before and after shots.
        /// </summary>
        public void DoEverythingAndPrint()
        {
            GenerateMaze();
            var r = RandomPoint();
            PlaceStartPoint(r[0], r[1]);
            SolveMaze(true);
        }

        public void DoEverythingNotPrint()
        {
            GenerateMaze();
            var r = RandomPoint();
            PlaceStartPoint(r[0], r[1]);
            SolveMaze(false);
        }

        private int[] RandomPoint()
        {
            int rndX = rnd.Next(spot.GetLength(0));
            int rndY = rnd.Next(spot.GetLength(1));
            return new int[2] { rndX, rndY };
        }

        private void PrintMazeColor(string header)
        {
            Console.WriteLine(header);
            for (int i = 0; i < spot.GetLength(0); i++)
            {
                for (int j = 0; j < spot.GetLength(1); j++)
                {
                    if (spot[i, j].ToString().Count() == 1)
                    {
                        if (spot[i, j] == -1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (spot[i, j] == -5)
                            Console.ForegroundColor = ConsoleColor.Green;
                        if (spot[i, j] == 0)
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(" " + spot[i, j] + ",");
                    }
                    else
                    {
                        if (spot[i, j] == -1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (spot[i, j] == -5)
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(spot[i, j] + ",");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.CursorLeft -=1;
                Console.Write(" \n");
            }
        }
    }
}
