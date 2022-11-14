using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Tile
    {
        public MainColors TileColor { get; private set; }
        
        public ChessPiece? ChessPiece { get; set; } = null;
        public Tile(MainColors tileColor, ChessPiece? chessPiece)
        {
            ChessPiece = chessPiece;
            TileColor = tileColor;
        }

        public void IsPossibleToMove(bool option)
        {
            if (option)
                TileColor = MainColors.Pink;
        }
        public bool isOccupied()
        {
            return ChessPiece != null;
        }



    }
}
