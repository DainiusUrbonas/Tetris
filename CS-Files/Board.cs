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
    class Board
    {
        private int Rows;
        private int Columns;
        private int Score;
        private int Level;
        private bool gameEnd;
        private Tetrimino currTetrimino; // Class for blocks
        private Label[,] BlockControls; // The labels for game Board
        static private Brush NoBrush = Brushes.Transparent; // For empty labels
        static private Brush SilverBrush = Brushes.Gray; // For borders

        // Constructor. Creates the game board grid and adds new tetrimino (the figure) to the board
        public Board(Grid TetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Columns = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            Level = 1;
            gameEnd = false;

            BlockControls = new Label[Columns, Rows];
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }   
            }
            currTetrimino = new Tetrimino();
            currTetriminoDraw();
        }

        // Getters
        public int getScore()
        {
            return Score;
        }
        public int getLevel()
        {
            return Level;
        }
        public bool getGameEnd()
        {
            return gameEnd;
        }

        // The draw method for Tetriminos
        private void currTetriminoDraw()
        {
            Point Position = currTetrimino.getCurrPosition();
            Point[] Shape = currTetrimino.getCurrShape();
            Brush Color = currTetrimino.getCurrColor();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Columns / 2) - 1), (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }

        // The method to destroy tetrimino
        private void currTetriminoErase()
        {
            Point Position = currTetrimino.getCurrPosition();
            Point[] Shape = currTetrimino.getCurrShape();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Columns / 2) - 1), (int)(S.Y + Position.Y) + 2].Background = NoBrush;
            }
        }

        // Checks if the row is full, then deletes the row, adds 100 points and +1 level and calls itself again
        // so that if there are more than one full row, they would all be deleted
        private void CheckRows()
        {
            bool full;
            for (int i = Rows -1; i > 0; i--)
            {
                full = true;
                for (int j = 0; j < Columns; j++)
                {
                    if(BlockControls[j,i].Background == NoBrush)
                    {
                        full = false;
                    }
                }
                if(full)
                {
                    RemoveRow(i);
                    Score += 100;
                    if(Score % 100 == 0)
                    {
                        Level++;
                    }
                    CheckRows();
                }
            }
        }

        // Removes a row
        private void RemoveRow(int row)
        {
            for (int i = row; i > 0; i--)
            {
                for (int j = 0; j < Columns; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }
        
        // Checks if there is nothing on the left of tetrimino then calls the tetrimino method for moving left
        // and draws it in new place
        public void CurrTetriminoMoveLeft()
        {
            Point Position = currTetrimino.getCurrPosition();
            Point[] Shape = currTetrimino.getCurrShape();
            bool move = true;
            currTetriminoErase();
            foreach (Point S in Shape)
            {
                if(((int)(S.X + Position.X) + ((Columns/2)-1)-1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Columns/2)-1)-1), (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetrimino.moveLeft();
                currTetriminoDraw();
            }
            else
            {
                currTetriminoDraw();
            }
        }

        // Does the same as the CurrTetriminoMoveRight method just with the right side
        public void CurrTetriminoMoveRight()
        {
            Point Position = currTetrimino.getCurrPosition();
            Point[] Shape = currTetrimino.getCurrShape();
            bool move = true;
            currTetriminoErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Columns / 2) - 1) + 1) >= Columns)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Columns / 2) - 1) + 1), (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetrimino.moveRight();
                currTetriminoDraw();
            }
            else
            {
                currTetriminoDraw();
            }
        }

        // Checks if there is nothing below tetrimino and calls the move down method,
        // if there is something below it, stops and creates new tetrimino
        public void CurrTetriminoMoveDown()
        {
            Point Position = currTetrimino.getCurrPosition();
            Point[] Shape = currTetrimino.getCurrShape();
            bool move = true;
            currTetriminoErase();
            foreach (Point S in Shape)
            {
                Point x = new Point(0,0);
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Columns / 2) - 1)), (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush && currTetrimino.getCurrPosition() == x)
                {
                    gameEnd = true;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Columns / 2) - 1)), (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetrimino.moveDown();
                currTetriminoDraw();
            }
            else
            {
                currTetriminoDraw();
                CheckRows();
                currTetrimino = new Tetrimino();
            }
        }

        // Creates fake rotated tetrimino and checks if its not over something, if so it calls the rotate method
        public void CurrTetriminoMoveRotate()
        {
            Point Position = currTetrimino.getCurrPosition();
            Point[] S = new Point[4];
            Point[] Shape = currTetrimino.getCurrShape();
            bool move = true;
            Shape.CopyTo(S, 0);
            currTetriminoErase();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)((S[i].Y + Position.Y) +2)) >= Rows)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Columns / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Columns / 2) - 1)) >= Columns)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Columns / 2) - 1)), (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetrimino.moveRotate();
                currTetriminoDraw();
            }
            else
            {
                currTetriminoDraw();
            }
        }
    }
}
