using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace BoardGamesWPF.MVVM.Model
{
    internal class King : Piece
    {
        public override int Power => 0;
        public King(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M224 0C241.7 0 256 14.3 256 32V48H272C289.701 48 304 62.3 304 80C304 97.7 289.701 112 272 112H256V160H416C426.3 160 435.9 164.9 442 173.3C448.1 181.7 449.7 192.4 446.4 202.1L375.1 416H72.9005L1.60048 202.1C-1.59952 192.4 0.00048399 181.6 6.00048 173.3C12.0005 165 21.7005 160 32.0005 160H192V112H176C158.3 112 144 97.7 144 80C144 62.3 158.3 48 176 48H192V32C192 14.3 206.3 0 224 0ZM32.0005 480C32.0005 462.3 46.3005 448 64.0005 448H83.6005H364.4H384C401.701 448 416 462.3 416 480C416 497.7 401.701 512 384 512H320H128H64.0005C46.3005 512 32.0005 497.7 32.0005 480Z";

        public override int[] CalculatePossibleMoves(int position, ObservableCollection<Tile> Board)
        {
            int row = position / 8;
            int column = position % 8;
            int move;
            List<int> possibleMoves = new List<int>();
            for (int i = -1; i<2; i++)
            {
                for(int j = -1; j<2; j++)
                {
                    move = (i + row) * 8 + j + column;
                    CheckPossibleMove();
                }
            }
            possibleMoves.RemoveAll(x => x == position);

            return possibleMoves.ToArray();



            void CheckPossibleMove()
            {
                if (move <= 63 && move >= 0)
                {
                    if (!((column == 0 && move % 8 == 7) || (column == 7 && move % 8 == 0)))
                        if (!Board[move].IsOccupied())
                        {
                            possibleMoves.Add(move);
                        }
                        else if (Board[move].Piece.PlayerColor != Board[position].Piece.PlayerColor)
                        {
                            possibleMoves.Add(move);
                        }
                }
            }
        }

        public bool IsInDanger(int position, ObservableCollection<Tile> Board)
        {
            int row = position / 8;
            int column = position % 8;

            List<int> possibleMoves = new List<int>();
            int move;

            // Bishop
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + (column - i);
                if (move % 8 > column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;
            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + (column + i);
                if (move % 8 < column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + (column + i);
                if (move % 8 < column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + (column - i);
                if (move % 8 > column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }

            foreach(int tempMove in possibleMoves)
            {
                if (Board[tempMove].Piece is not null and (Bishop or Queen))
                    return true;
            }
            possibleMoves.Clear();

            // Rook
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + column;
                if (move % 8 > column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + column;
                if (move % 8 < column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = row * 8 + (column + i);
                if (move % 8 < column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = row * 8 + (column - i);
                if (move % 8 > column || !(move <= 63 && move >= 0))
                    break;
                if (!CanMoveFarther())
                    break;

            }

            foreach (int tempMove in possibleMoves)
            {
                if (Board[tempMove].Piece is not null and (Rook or Queen))
                    return true;
            }
            possibleMoves.Clear();

            // Knight
            for (int i = -2; i < 5; i += 4)
            {
                move = (row + i) * 8 + column + 1;
                CheckPossbileMove();

                move = (row + i) * 8 + column + -1;
                CheckPossbileMove();
            }

            for (int i = -2; i < 5; i += 4)
            {
                move = (row + 1) * 8 + column + i;
                CheckPossbileMove();

                move = (row - 1) * 8 + column + i;
                CheckPossbileMove();

            }

            foreach (int tempMove in possibleMoves)
            {
                if (Board[tempMove].Piece is not null and Knight)
                    return true;
            }
            possibleMoves.Clear();

            // Pawn
            if (PlayerColor == PlayerColor.White)
            {
                if (column != 0)
                    CheckSidePossbileMove((row - 1) * 8 + column - 1);
                if (column != 7)
                    CheckSidePossbileMove((row - 1) * 8 + column + 1);
            }
            if (PlayerColor == PlayerColor.Black)
            {
                if (column != 0)
                    CheckSidePossbileMove((row + 1) * 8 + column - 1);
                if (column != 7)
                    CheckSidePossbileMove((row + 1) * 8 + column + 1);
            }

            foreach (int tempMove in possibleMoves)
            {
                if (Board[tempMove].Piece is not null and Pawn)
                    return true;
            }
            possibleMoves.Clear();

            return false;


            void CheckSidePossbileMove(int move)
            {
                if(move <= 63 && move >= 0)
                {
                    if (Board[move].IsOccupied())
                    {
                        if (Board[move].Piece.PlayerColor != Board[position].Piece.PlayerColor)
                        {
                            possibleMoves.Add(move);
                        }
                    }
                }

            }

            bool CanMoveFarther()
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
                return false;
            }

            void CheckPossbileMove()
            {
                if (move <= 63 && move >= 0)
                {
                    if (column < 4 && move % 8 < 6)
                    {
                        if (!Board[move].IsOccupied())
                        {
                            possibleMoves.Add(move);
                        }
                        else if (Board[move].Piece.PlayerColor != Board[position].Piece.PlayerColor)
                        {
                            possibleMoves.Add(move);
                        }
                    }

                    else if (column >= 4 && move % 8 > 1)
                    {
                        if (!Board[move].IsOccupied())
                        {
                            possibleMoves.Add(move);
                        }
                        else if (Board[move].Piece.PlayerColor != Board[position].Piece.PlayerColor)
                        {
                            possibleMoves.Add(move);
                        }
                    }
                }

            }
        }
    }
}