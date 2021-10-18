using System;
using System.Linq;
using System.Numerics;

namespace LegoTriangle
{
    class WrongTriangle : Exception
    {
        public WrongTriangle(string message) : base(message) { }
    }
    class Triangle
    {
        private enum TrianglesTypes : byte 
        {
            RightAngled,
            Equilateral,
            Common
        }
        public string Color { private get; set; } 
        private TrianglesTypes Type { set; get; }
        static public string[] ColorsEquilateal { private set; get; } = { "red", "blue", "white", "green" };
        static public string[] ColorsRightAngled { private set; get; } = { "red", "blue" };

        public double Area { private set; get; } 
        public Triangle((int, int) coord1, (int, int) coord2, (int, int) coord3, string color)
        {
            try
            {

                var side1 = Math.Sqrt(Math.Pow(coord1.Item1 - coord2.Item1, 2) + Math.Pow(coord1.Item2 - coord2.Item2, 2));
                var side2 = Math.Sqrt(Math.Pow(coord1.Item1 - coord3.Item1, 2) + Math.Pow(coord1.Item2 - coord3.Item2, 2));
                var side3 = Math.Sqrt(Math.Pow(coord3.Item1 - coord2.Item1, 2) + Math.Pow(coord3.Item2 - coord2.Item2, 2));

                if (side1 == side2 && side1 == side3) // Equilateral
                {
                    if (ColorsEquilateal.Contains(color))
                    {
                        Type = TrianglesTypes.Equilateral;
                    }
                    else
                    {
                        throw new WrongTriangle("Wrong type");
                    }
                }

                if (side1 * side1 + side2 * side2 == side3 * side3
                    ||
                    side1 * side1 == side2 * side2 + side3 * side3
                    ||
                    side2 * side2 == side1 * side1 + side3 * side3)
                {
                    if (ColorsRightAngled.Contains(color))
                    {
                        Type = TrianglesTypes.RightAngled;
                    }
                    else
                    {
                        throw new WrongTriangle("Wrong triangle's type or color");
                    }
                }
            }
            catch (WrongTriangle ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

            Area = AreaCalculate(coord1, coord2, coord3);
        }

        public double AreaCalculate ((int, int) coord1, (int, int) coord2, (int, int) coord3)
        {
           
            Area = 0.5 * ((coord1.Item1 - coord2.Item1) * (coord1.Item2 - coord3.Item2)) - ((coord1.Item2 - coord2.Item2) * coord1.Item1 - coord3.Item1);
            return Area;
        }

        public void Print()
        {
            Console.WriteLine($"Type:{Type};  Area: {Area}");
        }

    }
    class Program
    {


        static void Main(string[] args)
        {
            Triangle tr = new Triangle((1, 3), (0, 4), (15, -3), "red");
            tr.Print();
        }
    }
}
