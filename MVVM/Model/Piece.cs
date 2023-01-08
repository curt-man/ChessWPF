using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace BoardGamesWPF.MVVM.Model
{
    internal abstract class Piece : ICloneable, INotifyPropertyChanged
    {
        private SolidColorBrush pieceColor;

        public SolidColorBrush PieceColor
        {
            get
            {
                return pieceColor;
            }
            set
            {
                pieceColor = value;
                OnPropertyChanged(nameof(PieceColor));
            }
        }

        private PlayerColor playerColor;
        public PlayerColor PlayerColor
        {
            get
            {
                return playerColor;
            }
            set
            {
                playerColor = value;
                OnPropertyChanged(nameof(PlayerColor));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
