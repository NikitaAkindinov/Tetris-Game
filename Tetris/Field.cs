using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Field
    {
        int height;// Высота
        int width ;// Ширина
        public string box;// Рамка поля, обрамление.
        public string[,] playingFieled;

        public Field(int height, int width, string box)
        {
            this.height = height;
            this.width = width;
            this.box = box;
            playingFieled = new string[height, width];
        }
        /*
         * Метод отчистки массива игрового поля.
         * */
        public void ViewClearField()
        {
            for (int i = 0; i < playingFieled.GetLength(0); i++)// Перебор строк
                for (int j = 0; j < playingFieled.GetLength(1); j++)//Перебор столбцов
                    playingFieled[i, j] = Elements.EmptySpace;
        }

        /*
         * Метод опускания фигуры вниз.
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

        

        /* Метод проверки. Возможно ли построить на этом месте фигуру
         * если нет, то метод возвращает false и метод построения фигуры 
         * не выполняет построение. Если true, то фигура строится.
         * */
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

        /*
         * Основной метод поворота фигуры.
         * Фигура разворачивается, проверяется возможность построения фигуры
         * Если построение возможно, то фигура развёрнутая заносится в массив поля. 
         * Если поворот не возможен, то фигура не меняет своего положения.
         * 
         * */

        public int Turn(Elements elem)// Метод поворота фигуры
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

        /*
         * Метод отчистки заданного элемента с игрового поля
         * */
        public void DeliteElemtnts(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] = Elements.EmptySpace;
        }
        /* 
         * Построение данного элемента в массиве игрового поля
         * */

        public void BildElement(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] = elem.ElementsArray[i, j];
        }

        /*
         * Метод удаления заполненных строк в массиве игрвого поля.
         * */

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
        public  int Height
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

        public  int Width
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
