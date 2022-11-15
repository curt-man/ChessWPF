using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChessWPF.MVVM.Model
{
    internal abstract class ChessPiece
    {
        public SolidColorBrush PieceColor { get; set; }
        public virtual string PieceIcon { get; set; }
        public virtual int Power { get; }

        public ChessPiece(SolidColorBrush pieceColor)
        {
            PieceColor = pieceColor;
        }

        public abstract int[,] Move(int column, int row);
    }
}
