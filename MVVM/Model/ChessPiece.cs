using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal abstract class ChessPiece
    {
        public BlackWhite PieceColor { get; private set; }
        public virtual int Power { get; }

        public ChessPiece(BlackWhite pieceColor)
        {
            PieceColor = pieceColor;
        }
    }
}
