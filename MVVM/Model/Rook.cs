using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace ChessWPF.MVVM.Model
{
    internal class Rook : ChessPiece
    {
        public override int Power => 5;
        public Rook(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M0 172.2V16C0 7.2 7.2 0 16 0H72C80.8 0 88 7.2 88 16V56C88 60.4 91.6 64 96 64H128C132.4 64 136 60.4 136 56V16C136 7.2 143.2 0 152 0H232C240.8 0 248 7.2 248 16V56C248 60.4 251.6 64 256 64H288C292.4 64 296 60.4 296 56V16C296 7.2 303.2 0 312 0H368C376.8 0 384 7.2 384 16V172.2C384 184.3 377.2 195.4 366.3 200.8L337.7 215.1C326.9 220.5 320 231.6 320.2 243.7C322.2 328.7 336 384 336 384H48C48 384 61.8 328.7 63.8 243.8C64.1 231.7 57.2 220.6 46.3 215.2L17.7 200.8C6.8 195.4 0 184.3 0 172.2ZM176 288H208C216.8 288 224 280.8 224 272V224C224 206.3 209.7 192 192 192C174.3 192 160 206.3 160 224V272C160 280.8 167.2 288 176 288ZM32 416H352C369.7 416 384 430.3 384 448C384 465.7 369.7 480 352 480H32C14.3 480 0 465.7 0 448C0 430.3 14.3 416 32 416Z";


        public override int[] CalculatePossibleMoves(int position)
        {
            int row = position / 8;
            int column = position % 8;
            int[] possibleMoves = new int[16];
            int moveNumber = 0;
            for(int i = 0; i<8; i++)
            {
                possibleMoves[moveNumber++] = row * 8 + column;
                possibleMoves[moveNumber++] = column * 8 - *(8-row);
            }

            return new int[1];
        }
    }
}
