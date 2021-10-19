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
        private TrianglesTypes Type { set; get; } = TrianglesTypes.Common;
        static public string[] ColorsEquilateal { private set; get; } = { "red", "blue", "white", "green" };
        static public string[] ColorsRightAngled { private set; get; } = { "red", "blue" };

        public double Area { private set; get; } = 0;
        public Triangle((int, int) coord1, (int, int) coord2, (int, int) coord3, string color)
        {
            try
            {
                Vector2 vec1 = new Vector2(coord1.Item1 - coord2.Item1, coord1.Item2 - coord2.Item2);
                Vector2 vec2 = new Vector2(coord1.Item1 - coord3.Item1, coord1.Item2 - coord3.Item2);
                Vector2 vec3 = new Vector2(coord3.Item1 - coord2.Item1, coord3.Item2 - coord2.Item2);

                if (vec1.Length() == vec2.Length() && vec1.Length() == vec3.Length()) // Equilateral
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


                
                if (Vector2.Dot(vec1, vec2) == 0
                    ||
                    Vector2.Dot(vec1, vec3) == 0
                    ||
                    Vector2.Dot(vec3, vec2) == 0)
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

                Area = AreaCalculate(coord1, coord2, coord3);
            }
            catch (WrongTriangle ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

            
        }

        public double AreaCalculate ((int, int) coord1, (int, int) coord2, (int, int) coord3)
        {
           
            double area = 0.5 * (((coord1.Item1 - coord2.Item1) * (coord1.Item2 - coord3.Item2)) - ((coord1.Item2 - coord2.Item2) * (coord1.Item1 - coord3.Item1)));
            return area;
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
            Triangle tr = new Triangle((1, 3), (0, 4), (15, -3), "qwer");
            tr.Print();
            Triangle rightAngle = new Triangle((0, 0), (0, 3), (3, 0), "red");
            rightAngle.Print();


            Triangle equal = new Triangle((0, 0), (1, 3), (3, 1), "red");
            equal.Print();
        }
    }
}
