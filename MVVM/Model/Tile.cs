using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BoardGamesWPF.MVVM.Model
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

        private Piece piece;

        public Piece Piece
        {
            get { return piece; }
            set
            {
                piece = value;
                OnPropertyChanged("Piece");
            }
        }

        public Tile(SolidColorBrush tileColor, Piece piece)
        {
            Piece = piece;
            TileColor = tileColor;
        }

        public bool IsOccupied()
        {
            return Piece != null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
