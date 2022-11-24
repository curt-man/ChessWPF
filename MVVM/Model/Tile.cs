using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessWPF.MVVM.Model
{
    internal class Tile : INotifyPropertyChanged
    {

        private SolidColorBrush tileColor;
        public SolidColorBrush TileColor
        {
            get
            {
                return tileColor;
            }
            set
            {
                tileColor = value;
                OnPropertyChanged("TileColor");
            }
        }

        private ChessPiece chessPiece;

        public ChessPiece ChessPiece
        {
            get { return chessPiece; }
            set
            {
                chessPiece = value;
                OnPropertyChanged("ChessPiece");
            }
        }

        public Tile(SolidColorBrush tileColor, ChessPiece chessPiece)
        {
            ChessPiece = chessPiece;
            TileColor = tileColor;
        }

        public void IsPossibleToMove(bool option)
        {
            
        }

        public bool isOccupied()
        {
            return ChessPiece != null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
