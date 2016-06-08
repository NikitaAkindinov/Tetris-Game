using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTetris
{
    class Elements
    {
        private static string emptySpace = "   ";// Символ наполнения пустого пространства.
        private static string item = " * ";    // Символ отображения фигур.
        int startDotWidth; // Начальная точка от которой будет вестись построение фигуры на поле. К ней же прибавляется смещение фигуры
        int startDotHeight = 0;// Начальная точка от которой будет вестись построение фигуры на поле. К ней же прибавляется смещение фигуры
        string[,] elementsArray;



        #region//Свойства
        public string[,] ElementsArray
{
            get
            {
                return elementsArray;
            }
        }

        public static string EmptySpace
        {
            get
            {
                return emptySpace;
            }
        }

        public static string Item
        {
            get
            {
                return item;
            }
        }

        public int StartDotHeight
        {
            get
            {
                return startDotHeight;
            }

            set
            {
                this.startDotHeight = value;
            }
        }

        public int StartDotWidth
        {
            get
            {
                return startDotWidth;
            }

            set
            {
                this.startDotWidth = value;
            }
        }
        #endregion

        // Конструктор класса
        public Elements(int numberElement)
        {
            /* Расшифровка номеров фигур
             * 0 - Квадрат   * *
             *              * *
             * 1 - Зигзаг       * *
             *                * * 
             * 2 - Прямая   * * * * * *
             * 3 - Уголок   * * * *
             *              * 
             * 4 - Уголок   * * * *
             *                    * 
             */

            switch (numberElement)
            {
                case 0:
                    {
                        elementsArray = new string[,] { { Item, Item }, { Item, Item } };
                        StartDotWidth = Field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 1:
                    {
                        elementsArray = new string[,] { { EmptySpace, Item, Item }, { Item, Item, EmptySpace } };
                        StartDotWidth = Field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 2:
                    {
                        elementsArray = new string[,] { { Item, Item, Item, Item, Item}, {  EmptySpace, EmptySpace, EmptySpace, EmptySpace, EmptySpace } };
                        StartDotWidth = Field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 3:
                    {
                        elementsArray = new string[,] { { Item, Item, Item, Item }, { Item, EmptySpace, EmptySpace, EmptySpace } };
                        StartDotWidth = Field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 4:
                    {
                        elementsArray = new string[,] { { Item, Item, Item, Item }, { EmptySpace, EmptySpace, EmptySpace, Item } };
                        StartDotWidth = Field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }break;
                
                default: break;
            }
        }

        // Метод поворота фигуры
        public void Left()
        {
            string[,] temp = new string[elementsArray.GetLength(1), elementsArray.GetLength(0)];

            for (int i = 0, I = elementsArray.GetLength(0) - 1; i < elementsArray.GetLength(0); i++, I--)
                for (int j = 0, J = elementsArray.GetLength(1) - 1; j < elementsArray.GetLength(1); j++, J--)
                    temp[J, i] = elementsArray[i, j];

            elementsArray = temp;
        }
        public void Right()
        {
            string[,] temp = new string[elementsArray.GetLength(1), elementsArray.GetLength(0)];

            for (int i = 0, I = elementsArray.GetLength(0) - 1; i < elementsArray.GetLength(0); i++, I--)
                for (int j = 0, J = elementsArray.GetLength(1) - 1; j < elementsArray.GetLength(1); j++, J--)
                    temp[j, I] = elementsArray[i, j];

            elementsArray = temp;
        }

    }

    class Field
    {
        static int height = 25;// Высота
        static int width = 10;// Ширина
        public string box = "###";// Рамка поля, обрамление.
        public string[,] playingFieled = new string[ Height,Width ];

        public void ViewClearField()
        {            
            for (int i = 0; i < playingFieled.GetLength(0); i++)// Перебор строк
                for (int j = 0; j < playingFieled.GetLength(1); j++)//Перебор столбцов
                    playingFieled[i, j] = Elements.EmptySpace;
        }

        /*
         * 0 - Всё хорошо
         * 1 - Ошибка перемещения. Нужно создать новую фигуру
         * 2 - Игра окончена
         * */
        public int Down(Elements elem)
        {
            
            if (elem.StartDotHeight == 0)
            {
                elem.StartDotHeight++;
                if (!CheckOpenPlace(elem))
                    return 2;
            }
            else
            {
                DeliteElemtnts(elem);
                elem.StartDotHeight++;
                if (!CheckOpenPlace(elem))
                {
                    elem.StartDotHeight--;
                    BildElement(elem);
                    return 1;
                }
            }
            BildElement(elem);

            return 0;
        }
        public int Turn(Elements elem)// Метод поворота фигуры
        {
            DeliteElemtnts(elem);
            elem.Right();
            if(!CheckOpenPlace(elem))
            {
                elem.Left();
                BildElement(elem);
                return 1;
            }
            BildElement(elem);
            return 0;
        }
        bool CheckOpenPlace(Elements elem) // Готово. Работает
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                    {
                        if (elem.StartDotHeight + i  == playingFieled.GetLength(0))
                            return false;

                        if (elem.StartDotWidth + j == playingFieled.GetLength(1))
                            return false;

                        if (elem.StartDotWidth + j == -1)
                            return false;

                        if (playingFieled[elem.StartDotHeight  + i, elem.StartDotWidth + j] == Elements.Item)
                            return false;
                    }
            return true;
        }

        public int Left(Elements elem)
        {   
            DeliteElemtnts(elem);
            elem.StartDotWidth--;
            if (!CheckOpenPlace(elem))
            {
                elem.StartDotWidth++;
                BildElement(elem);
                return 1;
            }
            BildElement(elem);

            return 0;
        }
        public int Right(Elements elem)
        {
            
            DeliteElemtnts(elem);
            elem.StartDotWidth++;
            if (!CheckOpenPlace(elem))
            {
                elem.StartDotWidth--;
                BildElement(elem);
                return 1;
            }
            BildElement(elem);

            return 0;
        }

        public void DeliteElemtnts(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight  + i, elem.StartDotWidth + j] = Elements.EmptySpace;
        }

        public void BildElement(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight  + i, elem.StartDotWidth + j] = elem.ElementsArray[i, j];
        }

        public static int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public static int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
    } 


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