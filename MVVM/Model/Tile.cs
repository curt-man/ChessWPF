using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessWPF.MVVM.Model
{
    internal class Tile
    {
        public SolidColorBrush Color { get; private set; }
        public ChessPiece? ChessPiece { get; set; } = null;
        public Tile(SolidColorBrush tileColor, ChessPiece? chessPiece)
        {
            ChessPiece = chessPiece;
            Color = tileColor;
        }

        public void IsPossibleToMove(bool option)
        {
            
        }
        public bool isOccupied()
        {
            return ChessPiece != null;
        }



    }
}
