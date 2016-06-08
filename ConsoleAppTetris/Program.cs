using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris;

namespace ConsoleAppTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Игра Тетрис";
            Field pole = new Field();
            Random r = new Random();
            int kol = 5;
            //Elements element = new Elements(r.Next(0, kol));
            Elements element = new Elements(0);

            pole.ViewClearField();
            
            //while (true)
            //{
            //    Console.Clear();
            //    BildField(pole);
            //    System.Threading.Thread.Sleep(1000);
            //    if(pole.Down(element) == 1)
            //    {
            //        element = new Elements(r.Next(0,kol));
            //    }

            //}


            

            Console.TreatControlCAsInput = true;
            bool gemeOv = true;
            while (gemeOv)
            {
                Console.Clear();
                BildField(pole);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            switch (pole.Down(element))
                            {
                                case 1:
                                    {
                                        element = new Elements(r.Next(0, kol));
                                    }
                                    break;
                                case 2:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Игра закончена!");
                                        if (ConsoleKey.Enter == Console.ReadKey().Key) ;
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