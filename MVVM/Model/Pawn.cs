using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessWPF.MVVM.Model
{
    internal class Pawn : ChessPiece
    {
        public override int Power => 1;
        
        public Pawn(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M264 104C264 141.1 244.6 173.6 215.4 192H224C241.7 192 256 206.3 256 224C256 241.7 241.7 256 224 256C224 352 248 384 248 384H72C72 384 96 352 96 256C78.3 256 64 241.7 64 224C64 206.3 78.3 192 96 192H104.5C75.4 173.6 56 141.1 56 104C56 46.6 102.6 0 160 0C217.4 0 264 46.6 264 104ZM32 416H288C305.7 416 320 430.3 320 448C320 465.7 305.7 480 288 480H32C14.3 480 0 465.7 0 448C0 430.3 14.3 416 32 416Z";
        public void Promote()
        {
            
        }
        public override int[] CalculatePossibleMoves(int position, ObservableCollection<Tile> Board)
        {
            int row = position / 8;
            int column = position % 8;
            List<int> possibleMoves = new List<int>();

            if(PlayerColor == PlayerColor.White)
            {
                if (column != 0)
                    CheckSidePossbileMove((row - 1) * 8 + column - 1);
                CheckCenterPossbileMove((row - 1) * 8 + column);
                if (column != 7)
                    CheckSidePossbileMove((row - 1) * 8 + column + 1);
                if (!hasMoved)
                    CheckCenterPossbileMove((row - 2) * 8 + column);
            }
            if (PlayerColor == PlayerColor.Black)
            {
                if (column != 0)
                    CheckSidePossbileMove((row + 1) * 8 + column - 1);
                CheckCenterPossbileMove((row + 1) * 8 + column);
                if (column != 7)
                    CheckSidePossbileMove((row + 1) * 8 + column + 1);
                if (!hasMoved)
                    CheckCenterPossbileMove((row + 2) * 8 + column);
            }

            return possibleMoves.ToArray();



            void CheckSidePossbileMove(int move)
            {
                if (Board[move].isOccupied())
                {
                    if (Board[move].ChessPiece.PlayerColor != Board[position].ChessPiece.PlayerColor)
                    {
                        possibleMoves.Add(move);
                    }
                }
                
            }
            void CheckCenterPossbileMove(int move)
            {
                if (!Board[move].isOccupied())
                {
                    possibleMoves.Add(move);
                }
            }
        }
    }
}
