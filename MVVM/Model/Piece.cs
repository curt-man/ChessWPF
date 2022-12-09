using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoardGamesWPF.MVVM.Model
{
    internal abstract class Piece : ICloneable
    {
        public SolidColorBrush PieceColor { get; set; }
        public PlayerColor PlayerColor { get; set; }
        public virtual string PieceIcon { get; set; }
        public virtual int Power { get; }
        public bool hasMoved { get; set; } = false;

        public Piece(SolidColorBrush pieceColor, PlayerColor playerColor)
        {
            PieceColor = pieceColor;
            PlayerColor = playerColor;
        }

        public bool isSameColor(PlayerColor playerTurn)
        {
            return PlayerColor == playerTurn;
        }

        public abstract int[] CalculatePossibleMoves(int position, ObservableCollection<Tile> board);

        public virtual object Clone() => MemberwiseClone();
    }
}
