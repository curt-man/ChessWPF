using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override int[] CalculatePossibleMoves(int position)
        {
            int row = position / 8;
            int column = position % 8;
            int[] possibleMoves = new int[4];
            if(PlayerColor == PlayerColor.White)
            {
                possibleMoves[0] = position -= 8;
                possibleMoves[1] = position -= 7;
                possibleMoves[2] = position -= 6;
                if(isMoved)
                    possibleMoves[3] = position;
                else
                    possibleMoves[3] = position -= 16;
            }
            if (PlayerColor == PlayerColor.Black)
            {
                possibleMoves[0] = position += 8;
                possibleMoves[1] = position += 7;
                possibleMoves[2] = position += 6;
                if (isMoved)
                    possibleMoves[3] = position;
                else
                    possibleMoves[3] = position += 16;
            }


            return possibleMoves;
        }
    }
}
