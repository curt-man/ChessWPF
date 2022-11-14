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
        public King(MainColors pieceColor) : base(pieceColor)
        {

        }

        public override int[,] Move(int column, int row)
        {
            int[,] possibleMoves = new int[3,3];
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    possibleMoves[row+i,column+j] = 
                }
            }

        }
    }
}
