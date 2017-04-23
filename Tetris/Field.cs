using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// Field. Класс игрового поля.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// The height(Высота игрового поля)
        /// </summary>
        int height;

        /// <summary>
        /// The width(Ширина игрового поля)
        /// </summary>
        int width ;

        /// <summary>
        /// The box(Рамка, обрамление игрового поля.)
        /// </summary>
        public string box;

        /// <summary>
        /// The playing fieled
        /// </summary>
        public string[,] playingFieled;

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="box">The box.</param>
        public Field(int height, int width, string box)
        {
            this.height = height;
            this.width = width;
            this.box = box;
            playingFieled = new string[height, width];
        }
        
        /// <summary>
        /// Views the clear field (Метод отчистки массива игрового поля).
        /// </summary>
        public void ViewClearField()
        {
            for (int i = 0; i < playingFieled.GetLength(0); i++)// Перебор строк
                for (int j = 0; j < playingFieled.GetLength(1); j++)//Перебор столбцов
                    playingFieled[i, j] = Elements.EmptySpace;
        }

        /// <summary>
        /// Метод опускания фигуры вниз.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns>
        /// 0 - Всё хорошо
        ///1 - Ошибка перемещения.Нужно создать новую фигуру
        ///2 - Игра окончена
        ///</returns>
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
        
        /// <summary>
        /// Checks the open place.
        /// Метод проверки. Возможно ли построить на этом месте фигуру
        /// если нет, то метод возвращает false и метод построения фигуры 
        /// не выполняет построение. Если true, то фигура строится.
        /// </summary>
        /// <remarks>
        /// Метод проверки. Возможно ли построить на этом месте фигуру
        /// если нет, то метод возвращает false и метод построения фигуры 
        /// не выполняет построение. Если true, то фигура строится.
        /// </remarks>
        /// <param name="elem">The elem.</param>
        /// <returns></returns>
        bool CheckOpenPlace(Elements elem) 
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                    {
                        if (elem.StartDotHeight + i == playingFieled.GetLength(0))
                            return false;

                        if (elem.StartDotWidth + j == playingFieled.GetLength(1))
                            return false;

                        if (elem.StartDotWidth + j == -1)
                            return false;

                        if (playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] == Elements.Item)
                            return false;
                    }
            return true;
        }
        
        /// <summary>
        /// Turns the specified elem.
        /// Основной метод поворота фигуры.
        /// Фигура разворачивается, проверяется возможность построения фигуры
        /// Если построение возможно, то фигура развёрнутая заносится в массив поля.
        /// Если поворот не возможен, то фигура не меняет своего положения.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>
        /// Возвращает 0 или 1
        /// 0 - 
        /// 1 -
        /// </returns>
        public int Turn(Elements elem)
        {
            DeliteElemtnts(elem);
            elem.Right();
            if (!CheckOpenPlace(elem))
            {
                elem.Left();
                BildElement(elem);
                return 1;
            }
            BildElement(elem);
            return 0;
        }

        /// <summary>
        /// Lefts the specified elem. Поворот в лево фигуры
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>
        /// Возвращает 0 или 1
        /// 0 - 
        /// 1 -
        /// </returns>
        public int Left(Elements elem)// Поворот в лево фигуры
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
        /// <summary>
        /// Rights the specified elem.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>
        /// Возвращает 0 или 1
        /// 0 - 
        /// 1 -
        /// </returns>
        public int Right(Elements elem)// Поворот в право фигуры
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

        /// <summary>
        /// Delites the elemtnts.  Метод отчистки заданного элемента с игрового поля
        /// </summary>
        /// <param name="elem">The elem.</param>
        public void DeliteElemtnts(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] = Elements.EmptySpace;
        }
        

        /// <summary>
        /// Bilds the element. Построение данного элемента в массиве игрового поля
        /// </summary>
        /// <param name="elem">The elem.</param>
        public void BildElement(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] = elem.ElementsArray[i, j];
        }

        

        /// <summary>
        /// Deletes the line. Метод удаления заполненных строк в массиве игрвого поля.
        /// </summary>
        public void DelLine()
        {
            
            for (int i = 0; i < playingFieled.GetLength(0); i++)
            {
                for (int j = 0; j < playingFieled.GetLength(1); j++)
                {
                    if (playingFieled[i,j] == Elements.EmptySpace) { break; }
                    if(j == playingFieled.GetLength(1) - 1)
                    {
                        TransformationFiled(i);
                    }
                }
            }
        }


        /// <summary>
        /// Transformations the filed.
        /// </summary>
        /// <param name="I">The i.</param>
        void TransformationFiled(int I)
        {
            bool bigI = false;
            string[,] temp = new string[playingFieled.GetLength(0), playingFieled.GetLength(1)];

            for (int i = 0; i < temp.GetLength(1); i++)
                temp[0, i] = Elements.EmptySpace;

            for (int i = 1; i < temp.GetLength(0); i++, bigI = (i - 1) >= I ? true : false )
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (bigI)
                    {
                        temp[i, j] = playingFieled[i, j];
                    }
                    else
                    {
                        temp[i, j] = playingFieled[i - 1, j];
                    }
                }
            }
            playingFieled = temp;
        }

        #region Свойства
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
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

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
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
        #endregion
    }
}
