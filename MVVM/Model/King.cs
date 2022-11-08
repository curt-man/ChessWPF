using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class King : ChessPiece
    {
        public override int Power => 0;
        public King(BlackWhite pieceColor) : base(pieceColor)
        {

        }
    }
}
