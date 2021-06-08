using System;
using System.Collections.Generic;
using System.IO;

namespace bellman_ford
{
    class Program
    {
        static void Main(string[] args) 
        {
            (int, int) sizeGraph = (0, 0);
            List<(int, int, double)> edgesList = Read(ref sizeGraph);
            bool checkMark = true;
            Console.WriteLine("Стартовая вершина");
            int start = Convert.ToInt32(Console.ReadLine());
            List<(int, double)> bfA = BellmanFordAlgorithm(edgesList, sizeGraph, ref checkMark, start);
            Console.WriteLine("1. До всех\n2. До одного");
            switch (Console.ReadLine())
            {
                case "1":
                {
                    Console.WriteLine($"из вершини {start}");
                    foreach (var item in bfA)
                        Console.WriteLine($"в {item.Item1} = {item.Item2}");
                    break;
                }
                case "2":
                {
                    Console.WriteLine("Финишная вершина");
                    int finish = Convert.ToInt32(Console.ReadLine());
                    foreach (var item in bfA)
                    {
                        if (item.Item1 == finish)
                        {
                            Console.WriteLine($"из вершини {start} в {item.Item1} = {item.Item2}");
                        }
                    }
                    break;
                }
            }
        }
        static List<(int, int, double)> Read(ref (int, int) sizeGraph)
        { 
            List<(int, int, double)> list = new List<(int, int, double)>();
            StreamReader read = new StreamReader("test.txt");
            string[] size = read.ReadLine()?.Split(' ');
            if (size != null)
            {
                sizeGraph = (Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
                for (int i = 0; i < sizeGraph.Item2; ++i)
                {
                    size = read.ReadLine()?.Split(' ');
                    if (size != null)
                        list.Add((Convert.ToInt32(size[0]), Convert.ToInt32(size[1]), Convert.ToDouble(size[2])));
                }
            }
            return list;
        }
        static List<(int, double)> BellmanFordAlgorithm(List<(int, int, double)> edgesList, (int, int) sizeGraph, ref bool checkMark, int start)
        {
            List<(int, double)> list = new List<(int, double)>();
            for (int i = 0; i < sizeGraph.Item1; ++i)
                list.Add((i+1, Double.PositiveInfinity));
            list[start - 1] = (start, 0);
            for (int i = 0; i <= list.Count; ++i)
                foreach (var item in edgesList)
                {
                    double a = list[item.Item1 - 1].Item2 + item.Item3;
                    if (list[item.Item2 - 1].Item2 > a)
                    {
                        if (i == list.Count)
                        {
                            checkMark = false;
                            return list;   
                        }
                        list[item.Item2 - 1] = (list[item.Item2 - 1].Item1, list[item.Item1 - 1].Item2 + item.Item3);
                    }
                }
            return list;
        }
    }
}