using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal abstract class ChessPiece
    {
        public MainColors PieceColor { get; protected set; }
        public virtual int Power { get; }

        public ChessPiece(MainColors pieceColor)
        {
            PieceColor = pieceColor;
        }

        public abstract int[,] Move(int column, int row);
    }
}
