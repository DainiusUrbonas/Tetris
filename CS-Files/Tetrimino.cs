using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TetrisWpf
{
    class Tetrimino  // The blocks class
    {
        private Point currPosition; // The current point position of the block
        private Point[] currShape; // The shape of the block as array of points
        private Brush currColor; // The color of the block
        private bool rotate;

        // Constuctor
        public Tetrimino() 
        {
            currPosition = new Point(0, -1);
            currColor = Brushes.Transparent;
            currShape = setRandomShape();
        }
        
        // Getters
        public Brush getCurrColor()
        {
            return currColor;
        }
        public Point getCurrPosition()
        {
            return currPosition;
        }
        public Point[] getCurrShape()
        {
            return currShape;
        }

        // Methods responsible for moving the shape
        public void moveLeft()
        {
            currPosition.X -= 1;
        }
        public void moveRight()
        {
            currPosition.X += 1;
        }
        public void moveDown()
        {
            currPosition.Y += 1;
        }
        public void moveRotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }
            }
        }

        // Method that generates one of 7 random shapes
        private Point[] setRandomShape()
        {
            Random rand = new Random();
            switch (rand.Next() % 7)
            {
                case 0:
                    rotate = true;
                    currColor = Brushes.Cyan;
                    return new Point[]
                    {
                        new Point(0,-1),
                        new Point(-1,-1),
                        new Point(1,-1),
                        new Point(2,-1)
                    };
                case 1:
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[]
                    {
                        new Point(1,-1),
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0)
                    };
                case 2:
                    rotate = true;
                    currColor = Brushes.Orange;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 3:
                    rotate = false;
                    currColor = Brushes.Yellow;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(0,-1),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 4:
                    rotate = true;
                    currColor = Brushes.Green;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,0)
                    };
                case 5:
                    rotate = true;
                    currColor = Brushes.Purple;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,-1)
                    };
                case 6:
                    rotate = true;
                    currColor = Brushes.Red;
                    return new Point[]
                    {
                        new Point(0,-1),
                        new Point(-1,-1),
                        new Point(0,0),
                        new Point(1,0)
                    };
                default:
                    return null;
            }
        }
    }
}
