using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tetris;

namespace ConsoleAppTetris
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Title = "Игра Тетрис";
            Field pole = new Field(25,10,"###");
            Random r = new Random();
            
            Elements element = new Elements(pole,r);
            pole.ViewClearField();

            Console.TreatControlCAsInput = true;
            bool gemeOv = true;
            while (gemeOv)
            {
                Console.Clear();
                //System.Threading.Thread.Sleep(1000);
                BildField(pole);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            switch (pole.Down(element))
                            {
                                case 1:
                                    {
                                        element = new Elements(pole,r);
                                        pole.DelLine();
                                    }
                                    break;
                                case 2:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Игра закончена!");
                                        Console.ReadKey();
                                        gemeOv = false;
                                    }break;
                                default: break;
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        {
                            pole.Left(element);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            pole.Right(element);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            pole.Turn(element);
                            BildField(pole);
                        }
                        break;
                    default:
                        break;
                }

            }
    }// End Main

        
        static void BildField(Field temp)// Построение рамки поля и вывод playingFieled в консоль.
        {
            
            for (int j = 0; j < temp.playingFieled.GetLength(1) + 2; j++) Console.Write(temp.box);
            Console.WriteLine();
            for (int i = 0; i < temp.playingFieled.GetLength(0); i++)
            {
                Console.Write(temp.box); 
                for (int j = 0; j < temp.playingFieled.GetLength(1); j++)
                    Console.Write(temp.playingFieled[i, j]);
                Console.WriteLine(temp.box);
            }
            for (int j = 0; j < temp.playingFieled.GetLength(1) +2; j++) Console.Write(temp.box);
            Console.WriteLine();
        }
        

    }// End Prog
    
}