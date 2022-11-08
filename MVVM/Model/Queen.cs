using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Queen : ChessPiece
    {
        public override int Power => 9;
        public Queen(BlackWhite pieceColor) : base(pieceColor)
        {

        }
    }
}
