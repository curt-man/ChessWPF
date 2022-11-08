using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Tile
    {
        public BlackWhite TileColor { get; private set; }
        public bool IsPossibleToMove { get; set; }
        public ChessPiece? ChessPiece { get; set; } = null;
        public Tile(BlackWhite tileColor, ChessPiece? chessPiece)
        {
            ChessPiece = chessPiece;
            TileColor = tileColor;
        }


        public bool isOccupied()
        {
            return ChessPiece != null;
        }


    }
}
