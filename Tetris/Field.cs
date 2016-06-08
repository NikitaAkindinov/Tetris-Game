using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Field
    {
        static int height = 25;// Высота
        static int width = 10;// Ширина
        public string box = "###";// Рамка поля, обрамление.
        public string[,] playingFieled = new string[Height, Width];

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
            if (!CheckOpenPlace(elem))
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
                        playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] = Elements.EmptySpace;
        }

        public void BildElement(Elements elem)
        {
            for (int i = 0; i < elem.ElementsArray.GetLength(0); i++)
                for (int j = 0; j < elem.ElementsArray.GetLength(1); j++)
                    if (elem.ElementsArray[i, j] == Elements.Item)
                        playingFieled[elem.StartDotHeight + i, elem.StartDotWidth + j] = elem.ElementsArray[i, j];
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
}
