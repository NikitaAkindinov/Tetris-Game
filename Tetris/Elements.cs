using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// Класс выбора элемента
    /// </summary>
    public class Elements
    {
        /// <summary>
        /// The empty space. Символ наполнения пустого пространства.
        /// </summary>
        private static string emptySpace = "   ";

        /// <summary>
        /// The item. Символ отображения фигур.
        /// </summary>
        private static string item = " * ";

        /// <summary>
        /// The start dot width. Начальная точка от которой будет вестись построение фигуры на поле. К ней же прибавляется смещение фигуры
        /// </summary>
        int startDotWidth;

        /// <summary>
        /// The start dot height. Начальная точка от которой будет вестись построение фигуры на поле. К ней же прибавляется смещение фигуры
        /// </summary>
        int startDotHeight = 0;

        string[,] elementsArray;

        #region//Свойства
        /// <summary>
        /// Gets the elements array.
        /// </summary>
        /// <value>
        /// The elements array.
        /// </value>
        public string[,] ElementsArray
        {
            get
            {
                return elementsArray;
            }
        }

        /// <summary>
        /// Gets the empty space.
        /// </summary>
        /// <value>
        /// The empty space.
        /// </value>
        public static string EmptySpace
        {
            get
            {
                return emptySpace;
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public static string Item
        {
            get
            {
                return item;
            }
        }

        /// <summary>
        /// Gets or sets the start height of the dot.
        /// </summary>
        /// <value>
        /// The start height of the dot.
        /// </value>
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

        /// <summary>
        /// Gets or sets the start width of the dot.
        /// </summary>
        /// <value>
        /// The start width of the dot.
        /// </value>
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

        /// <summary>
        /// Происходит инициализация необходимого экземпляра класса.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="r"></param>
        /// <remarks>
        /// Расшифровка номеров фигур
        /// 0 - Квадрат 
        /// 1 - Зигзаг
        /// 2 - Прямая
        /// 3 - Уголок
        /// 4 - Уголок
        /// 5 - Зигзаг
        /// 6 - Тешка
        /// </remarks>
        public Elements(Field field, Random r)
        {
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

        /// <summary>
        /// Lefts this instance. Метод поворота фигуры
        /// </summary>
        public void Left()
        {
            string[,] temp = new string[elementsArray.GetLength(1), elementsArray.GetLength(0)];

            for (int i = 0, I = elementsArray.GetLength(0) - 1; i < elementsArray.GetLength(0); i++, I--)
                for (int j = 0, J = elementsArray.GetLength(1) - 1; j < elementsArray.GetLength(1); j++, J--)
                    temp[J, i] = elementsArray[i, j];

            elementsArray = temp;
        }

        /// <summary>
        /// Rights this instance. Метод поворота фигуры
        /// </summary>
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
