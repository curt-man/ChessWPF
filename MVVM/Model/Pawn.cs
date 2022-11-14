using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWPF.MVVM.Model
{
    internal class Pawn : ChessPiece
    {
        public override int Power => 1;
        public Pawn(MainColors pieceColor) : base(pieceColor)
        {

        }
    }
}
