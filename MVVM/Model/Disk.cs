using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace BoardGamesWPF.MVVM.Model
{
    internal class Disk : Piece
    {
        public override int Power => 1;
        public override string PieceIcon => "M50 100C77.6143 100 100 77.6143 100 50C100 22.3857 77.6143 0 50 0C22.3857 0 0 22.3857 0 50C0 77.6143 22.3857 100 50 100ZM80 50C80 66.5686 66.5684 80 50 80C33.4316 80 20 66.5686 20 50C20 33.4314 33.4316 20 50 20C66.5684 20 80 33.4314 80 50ZM85 50C85 69.3301 69.3301 85 50 85C30.6699 85 15 69.3301 15 50C15 30.6699 30.6699 15 50 15C69.3301 15 85 30.6699 85 50Z";
        public Disk(SolidColorBrush pieceColor, PlayerColor playerColor) : base(pieceColor, playerColor)
        {

        }

        public override int[] CalculatePossibleMoves(int position, ObservableCollection<Tile> Board)
        {
            int row = position / 8;
            int column = position % 8;

            List<int> possibleMoves = new List<int>();
            List<int> maybePossibleMoves = new List<int>();
            int move;

            // Bishop
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + (column - i);
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + (column + i);
                if (move % 8 < column || !CanMoveFurther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + (column + i);
                if (move % 8 < column || !CanMoveFurther())
                    break;

            }
            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + (column - i);
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }

            // Rook
            for (int i = 1; i < 8; i++)
            {
                move = (row - i) * 8 + column;
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }

            for (int i = 1; i < 8; i++)
            {
                move = (row + i) * 8 + column;
                if (move % 8 < column || !CanMoveFurther())
                    break;
            }

            for (int i = 1; i < 8; i++)
            {
                move = row * 8 + (column + i);
                if (move % 8 < column || !CanMoveFurther())
                    break;
            }

            for (int i = 1; i < 8; i++)
            {
                move = row * 8 + (column - i);
                if (move % 8 > column || !CanMoveFurther())
                    break;
            }

            return possibleMoves.ToArray();


            bool CanMoveFurther()
            {
                if (move <= 63 && move >= 0)
                {
                    if (Board[move].Piece != null)
                    {
                        if (Board[move].Piece.PlayerColor != Board[position].Piece.PlayerColor)
                        {
                            maybePossibleMoves.Add(move);
                            return true;
                        }
                        else if (maybePossibleMoves.Count > 0)
                        {
                            possibleMoves.AddRange(maybePossibleMoves);
                        }
                    }
                }
                maybePossibleMoves.Clear();
                return false;
            }
        }
    }
}
