using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Elements
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
        public Elements(Field field, Random r)
        {
            /* Расшифровка номеров фигур
             * 0 - Квадрат   * *
             *               * *
             * 1 - Зигзаг       * *
             *                * * 
             * 2 - Прямая   * * * * 
             * 3 - Уголок   * * * 
             *              * 
             * 4 - Уголок   * * *
             *                  * 
             * 5 - Зигзаг   * *
             *                * *
             * 6 - Тешка   *  
             *           * * *
             *                  
             *                  
             */


            switch (r.Next(7))
            {
                case 0:
                    {
                        elementsArray = new string[,] { { Item, Item }, { Item, Item } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 1:
                    {
                        elementsArray = new string[,] { { EmptySpace, Item, Item }, { Item, Item, EmptySpace } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 2:
                    {
                        elementsArray = new string[,] { { Item, Item, Item, Item }, { EmptySpace, EmptySpace, EmptySpace, EmptySpace } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 3:
                    {
                        elementsArray = new string[,] { { Item, Item, Item }, { Item, EmptySpace, EmptySpace } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 4:
                    {
                        elementsArray = new string[,] { { Item, Item, Item }, { EmptySpace, EmptySpace, Item } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 5:
                    {
                        elementsArray = new string[,] { { Item, Item, EmptySpace }, {  EmptySpace, Item, Item } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;
                case 6:
                    {
                        elementsArray = new string[,] { { EmptySpace, Item, EmptySpace }, { Item, Item, Item } };
                        StartDotWidth = field.Width / 2 - elementsArray.GetLength(1) / 2;
                    }
                    break;

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
}
