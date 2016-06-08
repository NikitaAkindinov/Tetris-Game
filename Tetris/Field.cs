using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Field
    {
        int height = 25;// Высота
        int width = 10;// Ширина
        string box = "###";// Рамка поля, обрамление.
        string emptySpace = " ~ ";// Символ наполнения пустого пространства.
        string item = "*";    // Символ отображения фигур.
        string[,]  playingFieled;

        public Field(int i,int j)
        {
            this.height = i;
            this.width = j;
            playingFieled = new string[i, j];
            
        }
        void bild()
        {
            for (int i = 0; i < height + 2;i++ )
            {
                for (int j = 0; j < width + 2; j++)
                {

                }
            }
        }
    }
}
