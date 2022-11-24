using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessWPF.MVVM.Model
{
    internal class Knight : ChessPiece
    {
        public override int Power => 3;
        public Knight(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }
        public override string PieceIcon => "M32 359.6V384H352V192C352 86 266 0 160 0H12.9C5.8 0 0 5.8 0 12.9C0 14.9 0.5 16.9 1.4 18.7L16 48L9.4 54.6C3.4 60.6 0 68.7 0 77.2V210.3C0 223.4 8 235.2 20.1 240L66.6 258.6C75.1 262 84.6 261.6 92.8 257.5L99.4 254.2C107.4 250.2 113.4 243 115.9 234.4L124.2 205.5C126.7 196.9 132.6 189.7 140.7 185.7L160 176V216.4C160 240.6 146.3 262.8 124.6 273.6L67.4 302.3C45.7 313.2 32 335.3 32 359.6ZM72 116C72 127 63 136 52 136C41 136 32 127 32 116C32 105 41 96 52 96C63 96 72 105 72 116ZM352 416H32C14.3 416 0 430.3 0 448C0 465.7 14.3 480 32 480H352C369.7 480 384 465.7 384 448C384 430.3 369.7 416 352 416Z";

        public override int[] CalculatePossibleMoves(int position)
        {
            return new int[1];
        }
    }
}