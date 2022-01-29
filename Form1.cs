using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tag_Test
{
    public partial class Form1 : Form
    {
        int Size = 0;
        int miniSize = 0;
        Bitmap bp;
        Bitmap[,] map = new Bitmap[4, 4];
        int[,] map2 = new int[4, 4];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Bitmap loadedPicture = new Bitmap(dlg.FileName);

                if (loadedPicture.Width > loadedPicture.Height)
                    Size = loadedPicture.Height;
                else
                    Size = loadedPicture.Width;

                Point locate = new Point((loadedPicture.Width - Size) / 2, (loadedPicture.Height - Size) / 2);
                Bitmap finalPicture = loadedPicture.Clone(new Rectangle(locate, new Size(Size, Size)), loadedPicture.PixelFormat);


                finalPicture = new Bitmap(finalPicture, pictureBox1.Width, pictureBox1.Height);
                Size = pictureBox1.Width;
                miniSize = Size / 4;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        map[i, j] = finalPicture.Clone(new Rectangle(new Point(miniSize * i, miniSize * j), new Size(miniSize - 2, miniSize - 2)), finalPicture.PixelFormat);
                    }
                }

                map[3, 3] = new Bitmap(miniSize, miniSize);

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i == 3 && j == 3)
                            map2[i, j] = 0;
                        else
                            map2[i, j] = i + j + 1;
                    }
                }

                int X1, X2, Y1, Y2;
                Random rnd = new Random();
                for (int i = 0; i < 16; i++)
                {
                    X1 = rnd.Next(0, 4);
                    Y1 = rnd.Next(0, 4);
                    X2 = rnd.Next(0, 4);
                    Y2 = rnd.Next(0, 4);

                    int b = map2[X1, Y1];
                    map2[X1, Y1] = map2[X2, Y2];
                    map2[X2, Y2] = b;

                    bp = map[X1, Y1];
                    map[X1, Y1] = map[X2, Y2];
                    map[X2, Y2] = bp;
                }

                bool stop = false;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (map2[i, j] == 0)
                        {
                            int b = map2[3, 3];
                            map2[3, 3] = map2[i, j];
                            map2[i, j] = b;

                            bp = map[3, 3];
                            map[3, 3] = map[i, j];
                            map[i, j] = bp;
                            stop = true;
                            break;
                        }
                    }
                    if (stop) break;
                }

                Bitmap bitmap = new Bitmap(finalPicture.Width, finalPicture.Height);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var g = Graphics.FromImage(bitmap);
                        g.DrawImage(map[i, j], new Point(miniSize * i, miniSize * j));
                    }
                }

                pictureBox1.Image = bitmap;
                pictureBox2.Image = finalPicture;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs;
            int x = mouseEventArgs.X;
            int y = mouseEventArgs.Y;
            x = x / (pictureBox1.Width  / 4);
            y = y / (pictureBox1.Height / 4);

            if (map2[x, y] != 0)
            {
                if (x - 1 >= 0)
                {
                    if (map2[x - 1, y] == 0)
                    {
                        int b = map2[x - 1, y];
                        map2[x - 1, y] = map2[x, y];
                        map2[x, y] = b;

                        bp = map[x - 1, y];
                        map[x - 1, y] = map[x, y];
                        map[x, y] = bp;
                    }
                    else
                    {
                        if (x + 1 <= 3)
                        {
                            if (map2[x + 1, y] == 0)
                            {
                                int b = map2[x + 1, y];
                                map2[x + 1, y] = map2[x, y];
                                map2[x, y] = b;

                                bp = map[x + 1, y];
                                map[x + 1, y] = map[x, y];
                                map[x, y] = bp;
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    if (x + 1 <= 3)
                    {
                        if (map2[x + 1, y] == 0)
                        {
                            int b = map2[x + 1, y];
                            map2[x + 1, y] = map2[x, y];
                            map2[x, y] = b;

                            bp = map[x + 1, y];
                            map[x + 1, y] = map[x, y];
                            map[x, y] = bp;
                        }
                    }
                    else
                    {

                    }
                }


                if (y - 1 >= 0)
                {
                    if (map2[x , y - 1] == 0)
                    {
                        int b = map2[x, y - 1];
                        map2[x, y - 1] = map2[x, y];
                        map2[x, y] = b;

                        bp = map[x, y - 1];
                        map[x, y - 1] = map[x, y];
                        map[x, y] = bp;
                    }
                    else
                    {
                        if (y + 1 <= 3)
                        {
                            if (map2[x, y + 1] == 0)
                            {
                                int b = map2[x, y + 1];
                                map2[x, y + 1] = map2[x, y];
                                map2[x, y] = b;

                                bp = map[x, y + 1];
                                map[x, y + 1] = map[x, y];
                                map[x, y] = bp;
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    if (y + 1 <= 3)
                    {
                        if (map2[x, y + 1] == 0)
                        {
                            int b = map2[x, y + 1];
                            map2[x, y + 1] = map2[x, y];
                            map2[x, y] = b;

                            bp = map[x, y + 1];
                            map[x, y + 1] = map[x, y];
                            map[x, y] = bp;
                        }
                    }
                    else
                    {

                    }
                }
            }

            bool final = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 3 && j == 3)
                        if (map2[i, j] != 0)
                            final = false;
                        else { }
                    else
                    {
                        if (map2[i, j] != i + j + 1)
                            final = false;
                    }
                }
            }

            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            if (final)
            {
                bitmap = new Bitmap(pictureBox2.Image);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var g = Graphics.FromImage(bitmap);
                        g.DrawImage(map[i, j], new Point(miniSize * i, miniSize * j));
                    }
                }
            }
            pictureBox1.Image = bitmap;
        }

    }
}