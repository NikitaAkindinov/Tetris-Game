using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        Field pole;
        Random r;
        Elements element;
        Label[,] lable;
        
        public Form1()
        {
            InitializeComponent();
            InicLableArr();
            pole = new Field(20, 10, "###");
            r = new Random();
            
            element = new Elements(pole,r);

            pole.ViewClearField();
            timer1.Tick += Timer1;
            timer1.Start();
            timer1.Interval = 1000;
        }

        private void InicLableArr()
        {
            lable = new Label[tableLayoutPanel1.RowCount, tableLayoutPanel1.ColumnCount];
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    lable[i, j] = new Label() { BackColor = Color.Violet };
                    tableLayoutPanel1.Controls.Add(lable[i, j], j, i);
                }
            
        }

        private void BildField()// Построение  и вывод playingFieled.
        {
            Clear();
            for (int i = 0; i < pole.playingFieled.GetLength(0); i++)
                for (int j = 0; j < pole.playingFieled.GetLength(1); j++)
                    if(pole.playingFieled[i, j] == Elements.Item) lable[i,j].BackColor = Color.Black;
        }

        private void Clear()
        {
            for (int i = 0; i < pole.playingFieled.GetLength(0); i++)
                for (int j = 0; j < pole.playingFieled.GetLength(1); j++)
                    lable[i, j].BackColor = Color.White;
        }


        private void Timer1(object sender, EventArgs e)
        {
            switch (pole.Down(element))
            {
                case 1:
                    {
                        element = new Elements(pole, r);
                        pole.DelLine();
                        BildField();
                    }
                    break;
                case 2:
                    {
                        DialogResult result = MessageBox.Show("Игра окончена.", "", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    break;
                default: BildField(); break;
            }
        }
        private void FormKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Down:
                    {
                        bool ifels = true;
                        while (ifels)
                        {
                            switch (pole.Down(element))
                            {
                                case 1:
                                    {
                                        element = new Elements(pole, r);
                                        pole.DelLine();
                                        BildField();
                                        ifels = false;
                                    }
                                    break;
                                case 2:
                                    {
                                        DialogResult result = MessageBox.Show("Игра окончена.", "", MessageBoxButtons.OK);
                                        if (result == DialogResult.OK)
                                        {
                                            this.Close();
                                        }
                                    }
                                    break;
                                default: BildField(); break;
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    {
                        pole.Left(element);
                        BildField();
                    }
                    break;
                case Keys.Right:
                    {
                        pole.Right(element);
                        BildField();
                    }
                    break;
                case Keys.Up:
                    {
                        pole.Turn(element);
                        BildField();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
