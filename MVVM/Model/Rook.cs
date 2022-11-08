using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Rook : ChessPiece
    {
        public override int Power => 5;
        public Rook(BlackWhite pieceColor) : base(pieceColor)
        {

        }
    }
}
