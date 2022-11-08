using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Bishop : ChessPiece
    {
        public override int Power => 3;
        public Bishop(BlackWhite pieceColor) : base(pieceColor)
        {

        }
    }
}
