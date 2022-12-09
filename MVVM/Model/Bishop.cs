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
    internal class Bishop : Piece
    {
        public override int Power => 3;
        public Bishop(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M128 0C110.3 0 96 14.3 96 32C96 48.1 107.9 61.4 123.4 63.7C78.4 106.8 8 190 8 288C8 335.4 38.8 360.3 64 372.7V416H256V372.7C281.2 360.2 312 335.3 312 288C312 250.7 301.8 215.6 286.7 183.9L187.3 283.3C181.1 289.5 170.9 289.5 164.7 283.3C158.5 277.1 158.5 266.9 164.7 260.7L270.8 154.6C247.6 116.5 219 85.1 196.6 63.7C212.1 61.4 224 48.1 224 32C224 14.3 209.7 0 192 0H128ZM32 448C14.3 448 0 462.3 0 480C0 497.7 14.3 512 32 512H288C305.7 512 320 497.7 320 480C320 462.3 305.7 448 288 448H32Z";
        public override int[] CalculatePossibleMoves(int position, ObservableCollection<Tile> Board)
        {
            int row = position / 8;
            int column = position % 8;
            List<int> possibleMoves = new List<int>();
            int move;



            for(int i = 1; i<8; i++)
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


            return possibleMoves.ToArray();

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
        }
    }
}