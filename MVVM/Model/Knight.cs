using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Knight : ChessPiece
    {
        public override int Power => 3;
        public Knight(BlackWhite pieceColor):base(pieceColor)
        {

        }
    }
}