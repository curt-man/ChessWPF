﻿using System.Collections.Generic;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace BoardGamesWPF.MVVM.Model
{
    internal class Queen : Piece
    {
        public override int Power => 9;
        public Queen(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M312 56C312 25.1 286.9 0 256 0C225.1 0 200 25.1 200 56C200 86.9 225.1 112 256 112C286.9 112 312 86.9 312 56ZM64 480C64 497.7 78.3 512 96 512H416C433.7 512 448 497.7 448 480C448 462.3 433.7 448 416 448H96C78.3 448 64 462.3 64 480ZM34 169.4L9.2 185.8C3.5 189.7 0 196.2 0 203.1C0 206.3 0.7 209.5 2.2 212.4L104 416H408L509.8 212.4C511.2 209.5 512 206.3 512 203.1C512 196.2 508.5 189.7 502.8 185.8L478 169.4C469.8 164 459 165 452 172C439.1 184.9 421.1 193.9 404 187.5C386.1 180.8 375.9 167.4 371.2 151.7C367.5 139 357.3 128 344 128H328C314.7 128 304.1 139.2 298.4 151.2C292.6 163.4 280.6 176 256 176C231.4 176 219.4 163.4 213.6 151.2C207.9 139.2 197.3 128 184 128H168C154.7 128 144.5 139 140.7 151.7C136 167.3 125.8 180.8 107.9 187.5C90.9 193.9 72.8 184.8 59.9 172C53 165.1 42.1 164 34 169.4Z";

        public override int[] CalculatePossibleMoves(int position, ObservableCollection<Tile> Board)
        {
            int row = position / 8;
            int column = position % 8;

            List<int> possibleMoves = new List<int>();
            int move;

            // Bishop
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + (column - i);
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + (column + i);
                if (move % 8 < column || !CanMoveFurther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + (column + i);
                if (move % 8 < column || !CanMoveFurther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + (column - i);
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }

            // Rook
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + column;
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }

            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + column;
                if (move % 8 < column || !CanMoveFurther())
                    break;
            }

            for (int i = 1; i < 8; i++)
            {
                move = row * 8 + (column + i);
                if (move % 8 < column || !CanMoveFurther())
                    break;
            }

            for (int i = 1; i < 8; i++)
            {
                move = row * 8 + (column - i);
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }


            return possibleMoves.ToArray();

            bool CanMoveFurther()
            {
                if (move <= 63 && move >= 0)
                {
                    if (!Board[move].IsOccupied())
                    {
                        possibleMoves.Add(move);
                        return true;
                    }
                    else if (Board[move].Piece.PlayerColor != Board[position].Piece.PlayerColor)
                    {
                        possibleMoves.Add(move);
                    }
                }
                return false;
            }
        }
    }
}
